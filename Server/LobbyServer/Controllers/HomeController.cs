﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LobbyServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
