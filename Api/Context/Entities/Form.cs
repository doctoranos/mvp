using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Api.Context.Entities
{
    public class Form
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        public List<Question> Questions { get; set; }
        
        [JsonIgnore]
        public List<CompletedForm> CompletedForms { get; set; }
    }
}