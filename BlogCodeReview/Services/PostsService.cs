using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Domain.Dtos;
using Domain.Model;
using Domain.Services;

namespace Services
{
    public class PostsService
    {
        private readonly DatabaseContext _db;
        private readonly TagMapper _asignadorTags;

        public PostsService(DatabaseContext db, TagMapper asignadorTags)
        {
            _db = db;
            _asignadorTags = asignadorTags;
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

        public async Task CreatePost(PostEditorDto editorPost)
        {
            var post = Post.CreateNewDefaultPost(editorPost.Autor);
            post.CopyValues(editorPost, _asignadorTags);
            _db.Posts.Add(post);
            await _db.SaveChangesAsync();
            editorPost.Id = post.Id;
        }

        public async Task UpdatePost(PostEditorDto editorPost)
        {
            var post = await GetPostById(editorPost.Id);
            post.CopyValues(editorPost, _asignadorTags);
            await _db.SaveChangesAsync();
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
