using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers.Datos
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcosController : ControllerBase
    {
        public IActionResult GetMarcoDetails(int id)
        {
            return Ok(QuerysToBD.GetMarcoDetails(id));
        }
    }
}
