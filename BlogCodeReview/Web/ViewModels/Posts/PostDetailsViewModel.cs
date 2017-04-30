using System;
using System.Collections.Generic;
using Domain.Model;
using Omu.ValueInjecter;
using Web.ViewModels.Tags;

namespace Web.ViewModels.Posts
{
    public class PostDetailsViewModel
    {

        public PostDetailsViewModel(Post post)
        {
            Tags = new List<TagItemViewModel>();
            this.InjectFrom(post);

            foreach (var tag in post.Tags)
            {
                Tags.Add(new TagItemViewModel(tag));
            }

        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Slug { get; set; }
        public DateTime DatePost { get; set; }
        public string HtmlContent { get; set; }

        public string Autor { get; set; }

        public ICollection<TagItemViewModel> Tags { get; set; }

    }
}