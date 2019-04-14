using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Context;
using Api.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public interface ICompletedFormService
    {
        Task<List<CompletedForm>> GetAllAsync();
        Task<CompletedForm> InsertAsync(CompletedForm form);
    }
    
    public class CompletedFormService : ICompletedFormService
    {
        private readonly DoctoranosDbContext _context;

        public CompletedFormService(DoctoranosDbContext context)
        {
            _context = context;
        }

        public async Task<List<CompletedForm>> GetAllAsync()
        {
            return await _context.CompletedForms
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<CompletedForm> InsertAsync(CompletedForm form)
        {
            form.CreatedAt = DateTime.UtcNow;
            
            _context.Add(form);
            await _context.SaveChangesAsync();

            return form;
        }
    }
}