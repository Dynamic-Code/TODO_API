namespace API.Model
{
    public class AddTodo
    {
        public string userId { get; set; }
        public string TodoHeader { get; set; }
        public string? TodoContent { get; set; }
        public bool Status { get; set; }
    }
}
