using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApplication.Data.Repositories;
using BlogApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.Controllers
{
    [Authorize(Roles = "admin")]
    public class PanelController : Controller
    {
        private IRepository _repository;

        public PanelController(IRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var posts = _repository.GetAllPost();
            return View(posts);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null) return View(new Post());

            var post = _repository.GetPost((int)id);
            return View(post);


        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            if (post.Id > 0) _repository.UpdatePost(post);
            else _repository.AddPost(post);


            if (await _repository.SaveChangesAsync()) return RedirectToAction("Index");

            return View();

        }


        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _repository.RemovePost(id);
            await _repository.SaveChangesAsync();
            return RedirectToAction("Index");


        }
    }
}
