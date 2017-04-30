using System;
using System.Collections.Generic;

namespace Domain.Dtos
{
    public class PostEditorDto
    {
        public int Id { get; set; }
        public string Subtitle { get; set; }
        
        public string Title { get; set; }
        
        public string Slug { get; set; }
        
        public DateTime DatePost { get; set; }
        
        public string HtmlContent { get; set; }

        public string Autor { get; set; }
        
        public List<string> Tags { get; set; }
    }
}
