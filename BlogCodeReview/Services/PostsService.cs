using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Data;
using Domain.Dtos;
using Domain.Model;
using Domain.Services;

namespace Services
{
    public class PostsService
    {
        private readonly DatabaseContext _db;
        private readonly TagMapper _tagMapper;

        public PostsService(DatabaseContext db, TagMapper tagMapper)
        {
            _db = db;
            _tagMapper = tagMapper;
        }

        public IQueryable<Post> Posts()
        {
            return _db.Posts.Include(m => m.Tags);
        }

        
        public async Task<Post> GetPostById(int id)
        {
            return await Posts()
                        .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Result> CreatePost(PostEditorDto editorPost)
        {
            var createPostResult = Post.Create(editorPost);

            if (createPostResult.IsFailure)
            {
                return createPostResult;
            }

            Post post = createPostResult.Value;

            _tagMapper.MapTagsToEntity(post, editorPost.Tags);

            _db.Posts.Add(post);
            await _db.SaveChangesAsync();

            editorPost.Id = post.Id;

            return Result.Ok();
        }

        public async Task<Result> UpdatePost(PostEditorDto editorPost)
        {
            var validationPostResult = Post.Validate(editorPost);

            if (validationPostResult.IsFailure)
            {
                return validationPostResult;
            }

            Post post = await GetPostById(editorPost.Id);

            post.MapEditorValues(editorPost);

            _tagMapper.MapTagsToEntity(post, editorPost.Tags);

            await _db.SaveChangesAsync();

            return Result.Ok();
        }

        public async Task DeletePost(int id)
        {
            var post = await GetPostById(id);
            _db.Posts.Remove(post);
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
         
        }

       
    }
}
