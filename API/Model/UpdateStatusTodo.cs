namespace API.Model
{
    public class UpdateStatusTodo
    {
        public string UserId { get; set; }
        public int Id { get; set; }
        public bool UpdateStatus { get; set; }
    }
}
