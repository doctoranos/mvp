using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Context.Entities;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/completed-forms")]
    public class CompletedFormController : Controller
    {
        private readonly ICompletedFormService _service;

        public CompletedFormController(ICompletedFormService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<List<CompletedForm>> GetAllAsync()
        {
            return await _service.GetAllAsync();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<CompletedForm> InsertAsync([FromBody]CompletedForm form)
        {
            return await _service.InsertAsync(form);
        }
    }
}