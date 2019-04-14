using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Services;
using Api.Context.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
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
        
        [HttpPost("forms")]
        public async Task<Form> InsertAsync([FromBody] Form form)
        {
            return await _service.InsertAsync(form);
        }
        
        [HttpPut("forms/{id}")]
        public async Task<Form> UpdateAsync(int id, [FromBody] Form form)
        {
            return await _service.UpdateAsync(id, form);
        }
        
        [HttpDelete("forms/{id}")]
        public async Task DeleteAsync(int id)
        {
            await _service.DeleteAsync(id);
        }
    }
}