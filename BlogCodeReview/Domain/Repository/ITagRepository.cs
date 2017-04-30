using Domain.Model;

namespace Domain.Repository
{
     public interface ITagRepository
    {
        Tag GetTagByName(string tagName);
    }
}
