using System;
using System.Linq;
using Domain.Model;
using Domain.Repository;

namespace Data.Repository
{
    public class TagRepository : ITagRepository
    {
        private readonly DatabaseContext _db;

        public TagRepository(DatabaseContext context)
        {
            _db = context;
        }

        public Tag GetTagByName(string tagName)
        {
            return _db.Tags.FirstOrDefault(m => m.Name.ToLower() == tagName.ToLower());
        }

     
    }
}
