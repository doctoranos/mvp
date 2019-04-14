using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Services;
using Api.Context.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("Api")]
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