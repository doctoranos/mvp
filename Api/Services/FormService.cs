using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Context;
using Api.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public interface IFormService
    {
        Task<List<Form>> GetAllAsync();
        Task<Form> GetByIdAsync(int id);
        Task<Form> InsertAsync(Form form);
        Task<Form> UpdateAsync(int id, Form form);
        Task DeleteAsync(int id);
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
                .AsNoTracking()
                .Include(x => x.Questions)
                .ToListAsync();
        }

        public async Task<Form> GetByIdAsync(int id)
        {
            return await _context.Forms
                .AsNoTracking()
                .Include(x => x.Questions)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Form> InsertAsync(Form form)
        {
            _context.Attach(form);
            
            await _context.SaveChangesAsync();
            return form;
        }

        public async Task<Form> UpdateAsync(int id, Form form)
        {
            form.Id = id;
            
            _context.Attach(form);
            _context.Entry(form).State = EntityState.Modified;

            if (form.Questions != null)
            {
                foreach (var question in form.Questions)
                {
                    _context.Entry(question).State = EntityState.Modified;
                }
            }

            await _context.SaveChangesAsync();
            return form;
        }

        public async Task DeleteAsync(int id)
        {
            var form = new Form{ Id = id };
            
            _context.Attach(form);
            _context.Entry(form).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }
    }
}