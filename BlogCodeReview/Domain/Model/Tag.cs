using System.Collections.Generic;

namespace Domain.Model
{
    public class Tag
    {
        public Tag()
        {
            Posts = new List<Post>();
        }


        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
