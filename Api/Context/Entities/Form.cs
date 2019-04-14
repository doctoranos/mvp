using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Context.Entities
{
    public class Form
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        public List<Question> Questions { get; set; }
        public List<CompletedForm> CompletedForms { get; set; }
    }
}