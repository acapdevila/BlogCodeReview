using System.Collections.Generic;
using System.Linq;
using Domain.Model;
using Domain.Repository;

namespace Domain.Services
{
    public class TagMapper
    {
        private readonly ITagRepository _tagRepository;
        private IEntityWithTags _entity;

        public TagMapper(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public void MapTagsToEntity(IEntityWithTags entidad, List<string> listaTags)
        {
            _entity = entidad;

            var tagsToBeDeleted = GetTagsToBeDeleted(listaTags);

            var tagsToBeAdded = GetTagsToBeAdded(listaTags);

            DeleteTags(tagsToBeDeleted);

            AddTags(tagsToBeAdded);
        }

        private void AddTags(IEnumerable<string> tagsToBeAdded)
        {
            foreach (var tagToBeAdded in tagsToBeAdded)
            {
                var tag = GetOrCreateTag(tagToBeAdded);
                _entity.Tags.Add(tag);
            }
        }

        private Tag GetOrCreateTag(string tagNameToAdd)
        {
            tagNameToAdd = tagNameToAdd.Trim();
            var tag = _tagRepository.GetTagByName(tagNameToAdd);

            if (tag == null)
            {
                tag = new Tag
                {
                    Name = tagNameToAdd,
                    Slug = tagNameToAdd.GenerateSlug()
                };
            }

            return tag;
        }

        private void DeleteTags(List<Tag> tagsPorEliminar)
        {
            while (tagsPorEliminar.Any())
            {
                var tag = tagsPorEliminar.First();

                _entity.Tags.Remove(tag);
                tagsPorEliminar.Remove(tag);
            }
        }

        private IEnumerable<string> GetTagsToBeAdded(List<string> listaTags)
        {
            return listaTags.Except(_entity.Tags.Select(t => t.Name));
        }

        private List<Tag> GetTagsToBeDeleted(List<string> listaTags)
        {
            if (listaTags == null)
            {
                return _entity.Tags.ToList();
            }

            return _entity.Tags.Where(m => !listaTags.Contains(m.Name)).ToList();
        }
    }
}
