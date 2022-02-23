using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WipStitchAPI.Models;

namespace WipStitchAPI.Controllers
{
    [ApiController]
    [Route("/projects")]
    public class WipStitchController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public WipStitchController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Project>>> GetAllProjects()
        {
            return await _context.Projects.ToListAsync();
        }

        [HttpGet("{ProjectName}")]
        public async Task<ActionResult<Project>> GetProjectByName(string ProjectName)
        {
            var project = await _context.Projects.FindAsync(ProjectName);

            if (project == null) return NotFound($"{ProjectName} not found.");

            return project;
        }

        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject(Project p)
        {
            if (p.ProjectName.Length < 1) return BadRequest("Your project needs a title!");

            Project newProject = new Project();
            newProject.ProjectName = p.ProjectName;
            newProject.PhotoURL = p.PhotoURL;
            newProject.StartDate = p.StartDate;
            newProject.EndDate = p.EndDate;
            newProject.YarnBrand = p.YarnBrand;
            newProject.YarnWeight = p.YarnWeight;
            newProject.YarnColorway = p.YarnColorway;
            newProject.YarnDyeLot = p.YarnDyeLot;
            newProject.YarnFavorite = p.YarnFavorite;
            newProject.ProjectNotes = p.ProjectNotes;

            _context.Projects.Add(p);
            await _context.SaveChangesAsync();

            return Created("Projects", p);
        }

        [HttpPut("{ProjectName}")]
        public async Task<ActionResult<Project>> UpdateProject(Project p)
        {
            _context.Projects.Add(p);
            await _context.SaveChangesAsync();

            return Created("update", p);
        }

        [HttpDelete("{ProjectName}")]
        public async Task<ActionResult<Project>> DeleteProject(string ProjectName)
        {
            var project = await _context.Projects.FindAsync(ProjectName);

            if (project == null) return NotFound($"{ProjectName} not found.");

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return Ok(project);
        }

    }
}