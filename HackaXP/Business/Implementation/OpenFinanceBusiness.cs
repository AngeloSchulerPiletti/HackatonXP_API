using HackaXP.Data.DTO.OpenFinance;
using HackaXP.Data.VO.Engine;
using Newtonsoft.Json;
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

        public object CalculateFinancialHealthy(CostumerOpenFinanceData costumerData)
        {
            EngineOwnMeasureVO answers = _engine.Calculate(costumerData); // Retorna uma resposta numérica percetual estrturuada para cada pergunta
            FebrabanFormVO febrabanFormVO = _engine.TranslateToFebrabanJson(answers); // Engine Traduz a resposta numérica percentual para a estrutura de resposta esperada pelo servidor
            // Salva a resposta numérica percetual estrturuada para o usuário em questão
            // Aqui mesmo envia a resposta para a Febraban
            // Salva a resposta da Febraban
            // Retorna a resposta da Febraban para o Front
            return answers;
        }

        private object SendQuestionaryToFebraban()
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
