using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Context.Entities
{
    public class Question
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("form_id")]
        public int FormId { get; set; }

        [Column("body")]
        public string Body { get; set; }

        public Form Form { get; set; }
    }
}