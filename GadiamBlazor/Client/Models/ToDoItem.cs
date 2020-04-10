namespace GadiamBlazor.Client.Models
{
    public class TodoItem
    {
        public string Title { get; set; }

        public bool IsDone { get; set; }

        public TodoItem(string title)
        {
            Title = title;
        }
    }
}
