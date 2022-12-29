using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class TodoData
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string TodoHeader { get; set; }
        public string? TodoContent { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
