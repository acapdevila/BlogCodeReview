using System;
using System.Collections.Generic;
using Domain.Common;
using Domain.Dtos;
using Domain.Services;

namespace Domain.Model
{
    public class Post : Entity, IEntityWithTags
    {

        public static Post CreateNewDefaultPost(string autor)
        {
            return new Post
            {
                Autor = autor
            };
        }

        public Post() 
        {
             Tags = new List<Tag>();
        }
        
        

        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Slug { get; set; }
        public DateTime DatePost { get; set; }
        public string HtmlContent { get; set; }

        public string Autor { get; set; }

        public ICollection<Tag> Tags { get; set; }


        public void CopyValues(PostEditorDto editorPost, TagMapper tagMapper)
        {
            Title = editorPost.Title;
            Subtitle = editorPost.Subtitle;
            Slug = editorPost.Slug;
            DatePost = editorPost.DatePost;
            HtmlContent = editorPost.HtmlContent;

            tagMapper.MapTagsToEntity(this, editorPost.Tags);
        }
    }
}
