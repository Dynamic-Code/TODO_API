using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class TodoData
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Todo { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
