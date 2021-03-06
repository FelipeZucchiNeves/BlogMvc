﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using BlogApplication.Data;
using BlogApplication.Data.Repositories;
using BlogApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var posts = _repository.GetAllPost();
            return View(posts);
        }

        public IActionResult Post(int id)
        {
            var post = _repository.GetPost(id);
            return View(post);
        }

    }
}
