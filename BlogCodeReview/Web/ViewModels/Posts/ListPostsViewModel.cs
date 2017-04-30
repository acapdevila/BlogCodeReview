using System.Collections.Generic;
using System.Linq;
using Domain.Model;
using PagedList;

namespace Web.ViewModels.Posts
{
    public class ListPostsViewModel
    {
        public IPagedList<PostItemManagmentViewModel> ListaPosts { get; set; }
    }

    public class PostItemManagmentViewModel
    {
        public PostItemManagmentViewModel()
        {
            TagList = new List<Tag>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public ICollection<Tag> TagList { get; set; }

        public string Tags
        {
            get { return string.Join(" ", TagList.Select(m => m.Name)); }
        }
    }
}
