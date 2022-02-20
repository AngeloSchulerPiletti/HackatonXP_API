using HackaXP.Data.DTO.Febraban;
using HackaXP.Data.DTO.OpenFinance;
using HackaXP.Data.VO.Engine;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HackaXP.Business.Implementation
{
    public class OpenFinanceBusiness : IOpenFinanceBusiness
    {
        private string _baseUrl = "https://openapi.xpi.com.br";
        private string _febrabanBaseUrl = "https://api-indice.febraban.org.br/api/v1";
        private string _accessToken;
        private HttpClient _apiClient = new HttpClient();
        private HttpClient _accessClient = new HttpClient();
        private IEngineFinancialHealthyMeasure _engine;

        public OpenFinanceBusiness(IEngineFinancialHealthyMeasure engine)
        {
            _engine = engine;
        }

        public async Task<CostumerOpenFinanceData> GetCostumer(string costumerName)
        {
            await UpdateAccessToken();

            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this._accessToken);

            HttpResponseMessage response = await _apiClient.GetAsync($"{_baseUrl}/openbanking/users/{costumerName}");
            response.EnsureSuccessStatusCode();

            var responseBody = response.Content.ReadAsStringAsync();
            CostumerOpenFinanceData costumerOpenFinanceData = JsonConvert.DeserializeObject<CostumerOpenFinanceData>(responseBody.Result);

            return costumerOpenFinanceData;
        }



        public FebrabanFormVO CalculateFinancialHealthy(CostumerOpenFinanceData costumerData)
        {
            EngineOwnMeasureVO answers = _engine.Calculate(costumerData); // Retorna uma resposta numérica percetual estrturuada para cada pergunta
            if (answers == null) return null;

            FebrabanFormVO febrabanFormVO = _engine.TranslateToFebrabanJson(answers); // Engine Traduz a resposta numérica percentual para a estrutura de resposta esperada pelo servidor

            return febrabanFormVO;
        }

        public async Task<FebrabanResponseData> SendQuestionaryToFebraban(FebrabanFormVO febrabanFormVO)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{_febrabanBaseUrl}/integration/index_value");
            string febrabanFormJson = JsonConvert.SerializeObject(febrabanFormVO, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            request.Content = new StringContent(febrabanFormJson);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await _apiClient.SendAsync(request);

            var responseBody = response.Content.ReadAsStringAsync();
            FebrabanResponseData result = JsonConvert.DeserializeObject<FebrabanResponseData>(responseBody.Result);

            return result;

        }

        public async Task<FebrabanCompleteResultData> GetFormResultFromFebraban(FebrabanResponseData febrabanResponseData)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{_febrabanBaseUrl}/integration/get_calculated_index/{febrabanResponseData.Data}");
            request.Headers.Host = "api-indice.febraban.org.br";
            request.Headers.Add("USER-TOKEN", febrabanResponseData.UserToken);

            HttpResponseMessage response = await _apiClient.SendAsync(request);

            var responseBody = response.Content.ReadAsStringAsync();
            FebrabanCompleteResultData result = JsonConvert.DeserializeObject<FebrabanCompleteResultData>(responseBody.Result);

            return result;
        }

        private async Task<bool> UpdateAccessToken()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/oauth2/v1/access-token");
            request.Content = new FormUrlEncodedContent(OpenFinanceApiAuth.Credentials);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            HttpResponseMessage response = await _accessClient.SendAsync(request);

            var responseBody = response.Content.ReadAsStringAsync();

            dynamic result = JsonConvert.DeserializeObject(responseBody.Result);
            this._accessToken = result["access_token"];

            return true;
        }
    }
}
