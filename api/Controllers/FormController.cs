using System.Collections.Generic;
using System.Threading.Tasks;
using api.Context.Entities;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api")]
    public class FormController : Controller
    {
        private readonly IFormService _service;

        public FormController(IFormService service)
        {
            _service = service;
        }

        [HttpGet("forms")]
        public async Task<List<Form>> GetAllAsync()
        {
            return await _service.GetAllAsync();
        }

    }
}