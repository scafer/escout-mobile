using escout.Models.Database;

namespace escout.Models
{
    public class Option
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public Option(string name, string imageUrl)
        {
            Name = name;
            ImageUrl = imageUrl;
        }

        public Option(DbEvent dbEvent)
        {
            Name = dbEvent.Name;
        }
    }
}
