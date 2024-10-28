using Microsoft.EntityFrameworkCore.Metadata;

namespace Stan_Iustina_Lab2.Models
{
    public class Author
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AuthorName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}