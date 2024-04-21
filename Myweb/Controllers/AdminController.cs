using Microsoft.AspNetCore.Mvc;
using Myweb.Models;
using Myweb.Services;
using Myweb.ViewModels;

namespace Myweb.Controllers
{
    public class AdminController : Controller
    {
        private readonly PostService _postService;

        public AdminController(PostService postService)
        {
            _postService = postService;
        }

        // GET: /Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Admin/Create
        [HttpPost]
        public async Task<IActionResult> Create(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var post = new Post
                {
                    Title = model.Title,
                    Content = model.Content,
                    UserId = 1, // Example: In a real application, retrieve the logged-in user's ID
                    CreatedAt = DateTime.Now
                };

                await _postService.CreatePostAsync(post);

                return RedirectToAction("Index", "Home"); // Redirect to home page after post creation
            }

            return View(model); // If model is not valid, return to create view with validation errors
        }

        // GET: /Admin/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);

            if (post == null)
            {
                return NotFound(); // If post not found, return 404
            }

            var model = new PostViewModel
            {
                Id = post.PostId,
                Title = post.Title,
                Content = post.Content
            };

            return View(model);
        }

        // POST: /Admin/Edit/{id}
        [HttpPost]
        public async Task<IActionResult> Edit(int id, PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingPost = await _postService.GetPostByIdAsync(id);

                if (existingPost == null)
                {
                    return NotFound(); // If post not found, return 404
                }

                existingPost.Title = model.Title;
                existingPost.Content = model.Content;
                existingPost.UpdatedAt = DateTime.Now;

                await _postService.UpdatePostAsync(existingPost);

                return RedirectToAction("Index", "Home"); // Redirect to home page after post update
            }

            return View(model); // If model is not valid, return to edit view with validation errors
        }

        // GET: /Admin/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);

            if (post == null)
            {
                return NotFound(); // If post not found, return 404
            }

            return View(post);
        }

        // POST: /Admin/Delete/{id}
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);

            if (post == null)
            {
                return NotFound(); // If post not found, return 404
            }

            await _postService.DeletePostAsync(post);

            return RedirectToAction("Index", "Home"); // Redirect to home page after post deletion
        }
    }
}
