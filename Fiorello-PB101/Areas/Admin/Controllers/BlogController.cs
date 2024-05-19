using Fiorello_PB101.Models;
using Fiorello_PB101.Services;
using Fiorello_PB101.Services.Interfaces;
using Fiorello_PB101.ViewModels.Blog;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello_PB101.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var blogs = await _blogService.GetAllWithDetailsAsync();
            return View(blogs);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogVM blog)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool existBlog = await _blogService.ExistAsync(blog.Title,blog.Description);
            if (existBlog)
            {
                ModelState.AddModelError("Title", "A blog with the same title and description already exists.");
                return View();
            }
            await _blogService.CreateAsync(new Blog {Title=blog.Title,Description=blog.Description,Image=blog.Image });
                return RedirectToAction(nameof(Index));
            
            
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            var blog = await _blogService.GetBlogDetailsAsync((int)id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            var blog = await _blogService.GetByIdAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            await _blogService.DeleteAsync(blog);
            return RedirectToAction(nameof(Index));
        }
    }
}

