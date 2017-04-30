using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Model;
using Omu.ValueInjecter;

namespace Web.ViewModels.Tags
{
    public class TagItemViewModel
    {
        public TagItemViewModel(Tag tag)
        {
            this.InjectFrom(tag);
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }
}