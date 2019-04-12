using System.Collections.Generic;
using System.Threading.Tasks;
using api.Context;
using api.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public interface IFormService
    {
        Task<List<Form>> GetAllAsync();
    }

    public class FormService : IFormService
    {
        private readonly DoctoranosDbContext _context;

        public FormService(DoctoranosDbContext context)
        {
            _context = context;
        }

        public async Task<List<Form>> GetAllAsync()
        {
            return await _context.Forms
                .Include(x => x.Questions)
                .ToListAsync();
        }
    }
}