namespace DZ3_04._02._2024_3.Models
{
    public class Note
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public List<string> Tags { get; set; }

        public Note()
        {
            Tags = new List<string>();
        }
    }
}
