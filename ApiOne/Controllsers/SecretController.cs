using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiOne.Controllsers
{
    public class SecretController:Controller
    {
        [Route("/secret")]
        [Authorize]
        public string Index()
        {
            return "Secret message from ApiOne";
        }
    }
}
