using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Api.Context.Entities
{
    public class CompletedForm
    {
        [Key, Column("id")] 
        public int Id { get; set; }
        
        [Column("form_id")] 
        public int FormId { get; set; }
        
        [Column("body")] 
        public string Body { get; set; }
        
        [Column("created_at")] 
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public Form Form { get; set; }
    }
}