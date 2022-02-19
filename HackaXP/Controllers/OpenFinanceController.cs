using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class OpenFinanceController : ControllerBase
    {


        [HttpGet]
        [Route("demo")]
        public IActionResult Demo()
        {
            return Ok("Our api is working!");
        }
    }
}
