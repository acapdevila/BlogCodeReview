using System.Collections.Generic;

namespace Domain.Model
{
    public interface IEntityWithTags
    {
        ICollection<Tag> Tags { get; set; }
    }
}
