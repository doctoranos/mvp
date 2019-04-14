using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Context;
using Api.Context.Entities;
using Api.Controllers;
using Api.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Test
{
    //TODO: Add fixture for Api test host.
    
    public class FormTest : IDisposable
    {
        private readonly DoctoranosDbContext _context;
        private readonly FormController _controller;
        private readonly Form _testForm;

        public FormTest()
        {
            var options = new DbContextOptionsBuilder<DoctoranosDbContext>()
                .UseNpgsql("User ID=sa;Password=sa;Host=localhost;Port=5431;Database=dev;Pooling=true;")
                .Options;

            _context = new DoctoranosDbContext(options);
            var service = new FormService(_context);
            _controller = new FormController(service);
            
            _testForm = new Form
            {
                Title = "Test Title",
                Questions = new List<Question>
                {
                    new Question { Body = "Test body 1" },
                    new Question { Body = "Test body 2" },
                    new Question { Body = "Test body 3" }
                }
            };

            Dispose();
        }

        [Fact]
        public async Task InsertForm()
        {
            await _controller.InsertAsync(_testForm);

            _context.Forms.Should().HaveCount(1);
            _context.Forms.First().Should().BeEquivalentTo(_testForm, opt => 
                opt.Excluding(x => x.Id)
                    .Excluding(x => x.Questions));

            _context.Questions.Should().HaveCount(3);
            _context.Questions.Should().BeEquivalentTo(_testForm.Questions, opt =>
                opt.Excluding(x => x.Id)
                    .Excluding(x => x.FormId)
                    .Excluding(x => x.Form));
        }
        
        [Fact]
        public async Task UpdateForm()
        {
            var result = await _controller.InsertAsync(_testForm);

            _testForm.Title = Guid.NewGuid().ToString();
            _testForm.Questions.ForEach(x => x.Body = Guid.NewGuid().ToString());
            
            await _controller.UpdateAsync(result.Id, _testForm);

            _context.Forms.Should().HaveCount(1);
            _context.Forms.First().Should().BeEquivalentTo(_testForm, opt => 
                opt.Excluding(x => x.Id)
                    .Excluding(x => x.Questions));

            _context.Questions.Should().HaveCount(3);
            _context.Questions.Should().BeEquivalentTo(_testForm.Questions, opt =>
                opt.Excluding(x => x.Form)
                    .Excluding(x => x.Id)
                    .Excluding(x => x.FormId));
        }
        
        [Fact]
        public async Task DeleteForm()
        {
            var result = await _controller.InsertAsync(_testForm);
            
            _context.Forms.Should().HaveCount(1);
            _context.Questions.Should().HaveCount(3);

            // For some reason can not delete without previous detach. In WebHost - all is fine.
            _context.Entry(result).State = EntityState.Detached;
            
            await _controller.DeleteAsync(result.Id);
            
            _context.Forms.Should().HaveCount(0);
            _context.Questions.Should().HaveCount(0);
        }

        public void Dispose()
        {
            _context.Forms.RemoveRange(_context.Forms.ToList());
            _context.SaveChanges();
        }
    }
}