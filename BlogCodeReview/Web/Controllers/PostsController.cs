using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Data;
using Data.Repository;
using Domain.Model;
using Services;
using Domain.Services;
using PagedList;
using Web.ViewModels.Posts;

namespace Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly PostsService _postsServicio;

        public PostsController() : this(new DatabaseContext())
        {

        }

        public PostsController(DatabaseContext context)
            : this(new PostsService(context, new TagMapper(new TagRepository(context))))
        {

        }


        public PostsController(PostsService postsServicio)
        {
            _postsServicio = postsServicio;
        }

        public ActionResult Index(int page = 1)
        {

            var viewModel = new ListPostsViewModel
            {
                ListaPosts = _postsServicio
                    .Posts()
                    .Select(m => new PostItemManagmentViewModel
                    {
                        Slug = m.Slug,
                        Title = m.Title,
                        Id = m.Id,
                        TagList = m.Tags
                    })
                    .ToPagedList(page, pageSize: 100)
            };


            return View(viewModel);
        }


        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = await _postsServicio.GetPostById(id.Value);
            if (post == null)

            {
                return HttpNotFound();
            }

            var viewModel = new PostDetailsViewModel(post);

            return View(viewModel);
        }

        public ActionResult Create()
        {
            var viewModel = new PostEditorViewModel(Post.CreateNewDefaultPost("blog autor name"));

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string boton, PostEditorViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }


            await _postsServicio.CreatePost(viewModel.ToPostEditorDto());

            if (boton.ToLower().Contains(@"exit"))
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Edit", new {viewModel.Id});
        }


        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var post = await _postsServicio.GetPostById(id.Value);
            if (post == null)
            {
                return HttpNotFound();
            }

            var viewModel = new PostEditorViewModel(post);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PostEditorViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _postsServicio.UpdatePost(viewModel.ToPostEditorDto());

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Save(PostEditorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _postsServicio.UpdatePost(viewModel.ToPostEditorDto());
            }

            return Content(string.Empty);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = await _postsServicio.GetPostById(id.Value);
            if (post == null)
            {
                return HttpNotFound();
            }

            var viewModel = new PostEditorViewModel(post);

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _postsServicio.DeletePost(id);
            return RedirectToAction("Index");
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _postsServicio.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}