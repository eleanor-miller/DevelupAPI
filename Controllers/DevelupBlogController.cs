using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevelupAPI.Models;

namespace DevelupAPI.Controllers
{
    [ApiController]
    [Route("/blog")]
    public class DevelupBlogController : ControllerBase
    {
        //DAO : Data Access Object
        private readonly DatabaseContext _context;

        public DevelupBlogController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("all", Name = "GetDevelupBlog")]
        public async Task<ActionResult<List<BlogPost>>> GetAllBlogPosts()
        {
            return await _context.BlogPosts.ToListAsync();
        }

        [HttpPost("create")]
        public async Task<ActionResult<BlogPost>> CreateBlogPost(BlogPost p)
        {
            if (p.Title.Length < 1) return BadRequest("Your post needs a title!");
            if (p.Content.Length < 1) return BadRequest("Your post needs content!");

            p.Title = p.Title.Trim();
            p.Content = p.Content.Trim();
            p.Author = p.Author.Trim();

            _context.BlogPosts.Add(p);
            await _context.SaveChangesAsync();

            return Created("create", p);
        }

    }
}