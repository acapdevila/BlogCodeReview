using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Domain.Common;
using Domain.Dtos;

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

        public static Result<Post> Create(PostEditorDto editor)
        {
            Result result = Validate(editor);

            if (result.IsFailure)
            {
                return Result.Fail<Post>(result.Error);
            }

            return Result.Ok(new Post(editor));

        }

        private Post(PostEditorDto postEditor) : this()
        {
            MapEditorValues(postEditor);
        }

        public Post() 
        {
             Tags = new List<Tag>();
        }
        
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Slug { get; set; } // Could be a value object
        public DateTime DatePost { get; set; }
        public string HtmlContent { get; set; }

        public string Autor { get; set; } // Could be a value object

        public ICollection<Tag> Tags { get; set; }

        public void MapEditorValues(PostEditorDto postEditor)
        {
            var validationResult = Validate(postEditor);

            if (validationResult.IsFailure)
            {
                throw new Exception(validationResult.Error);
            }

            Title = postEditor.Title;
            Subtitle = postEditor.Subtitle;
            Slug = postEditor.Slug.Replace(" ", "-"); // Todo: Move to set propery? It would be compatible with EF?
            DatePost = postEditor.DatePost;
            HtmlContent = postEditor.HtmlContent;
            Autor = postEditor.Autor;
        }


        #region Validations

        public static Result Validate(PostEditorDto editor)
        {
            var autorValidationResult = ValidateAutor(editor.Autor);

            var titelValidationResult = ValidateTitle(editor.Title);

            var slugValidationResult = ValidateSlug(editor.Slug);

            // More validations if needed
            // ...

            return Result.Combine(autorValidationResult, titelValidationResult, slugValidationResult);
        }

        private static Result ValidateAutor(string autor)
        {
            if (string.IsNullOrEmpty(autor))
            {
                return Result.Fail("Autor is required");
            }

            int maxAutorLength = 64;
            if (autor.Length > maxAutorLength)
            {
                return Result.Fail("Max autor length is " + maxAutorLength);
            }

            return Result.Ok();
        }

        private static Result ValidateTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return Result.Fail("Title is required");
            }

            int maxTitleLength = 256;
            if (title.Length > maxTitleLength)
            {
                return Result.Fail("Max title length is " + maxTitleLength);
            }

            return Result.Ok();
        }


        private static Result ValidateSlug(string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return Result.Fail("Url is required");
            }

            int maxLength = 128;
            if (slug.Length > maxLength)
            {
                return Result.Fail("Max url length is " + maxLength);
            }

            return Result.Ok();
        }




        #endregion

      
    }
}
