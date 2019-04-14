using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Services;
using Api.Context.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/forms")]
    public class FormController : Controller
    {
        private readonly IFormService _service;

        public FormController(IFormService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<List<Form>> GetAllAsync()
        {
            return await _service.GetAllAsync();
        }
        
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<Form> GetByIdAsync(int id)
        {
            return await _service.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<Form> InsertAsync([FromBody] Form form)
        {
            return await _service.InsertAsync(form);
        }

        [HttpPut("{id}")]
        public async Task<Form> UpdateAsync(int id, [FromBody] Form form)
        {
            return await _service.UpdateAsync(id, form);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await _service.DeleteAsync(id);
        }
    }
}