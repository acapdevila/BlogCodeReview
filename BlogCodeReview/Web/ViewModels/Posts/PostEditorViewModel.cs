using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Domain.Dtos;
using Domain.Model;
using Omu.ValueInjecter;

namespace Web.ViewModels.Posts
{
    public class PostEditorViewModel
    {
        private string _urlSlug;

        public PostEditorViewModel()
        {
            
        }

        public PostEditorViewModel(Post post)
        {
            this.InjectFrom(post);
            ContentHtml = post.HtmlContent;
            Tags = string.Join(" ", post.Tags.Select(m => m.Name));
        }

        public int Id { get; set; }

        [AllowHtml]
        [Display(Name = "Subtítulo")]
        [Required(ErrorMessage = "Escribe un subtítulo")]
        public string Subtitle { get; set; }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "Escribe un título")]
        [StringLength(128, ErrorMessage = "La longitud máxima es de {1} dígitos")]
        public string Title { get; set; }

        [Display(Name = "Url del post")]
        [Required(ErrorMessage = "Escribe una url")]
        [StringLength(50, ErrorMessage = "La longitud máxima es de {1} dígitos")]
        public string Slug
        {
            get { return _urlSlug; }
            set { _urlSlug = string.IsNullOrEmpty(value) ? value : value.Replace(" ", "-"); }
        }

        [Display(Name = @"Fecha")]
        [Required(ErrorMessage = @"Escribe una fecha")]
        public DateTime DatePost { get; set; }

        [AllowHtml]
        [Display(Name = "Contenido")]
        [Required(ErrorMessage = "Escribe un contenido")]
        public string ContentHtml { get; set; }

       
        [Required]
        public string Autor { get; set; }

        public string Tags { get; set; }


       
    }

    public static class PostViewModelExtensions
    {
        public static PostEditorDto ToPostEditorDto(this PostEditorViewModel postEditorViewModel)
        {
            var postEditorDto = new PostEditorDto();
            postEditorDto.InjectFrom(postEditorViewModel);
            postEditorDto.HtmlContent = postEditorViewModel.ContentHtml;

            foreach (var tagName in postEditorViewModel.Tags.Split(Tag.TagSeparator).ToList())
            {
                postEditorDto.Tags.Add(tagName);
            }

            return postEditorDto;
        }
    }


}