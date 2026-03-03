using Foro.Entities.Models;
using Foro_Militar.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Foro_Militar.Controllers
{
    public class CommunitiesController : Controller
    {
        private readonly AppDbContext _context = new AppDbContext();

        // GET: /Communities
        public ActionResult Index()
        {
           var communities = _context.Communities
            .Include(c => c.Posts)
            .Include(c => c.UserCommunities)
            .Include(c => c.Categories)
            .Include(c => c.MainCategory)
            .Include(c => c.CreatedByUser)
            .ToList()
            .Select(c => new CommunityViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Country = c.Country,
                Slug = c.Slug,
                ImageUrl = c.ImageUrl,
                BannerUrl = c.BannerUrl,

                CreatedAtFormatted = c.CreatedAt.ToString("MMM yyyy"),
                CreatedByName = c.CreatedByUser.Username,

                TotalPosts = c.Posts.Count,
                TotalFollowers = c.UserCommunities.Count,
                Categories = c.Categories.Select(cat => new CommunityViewModel.CategoryInfo
                {
                    Name = cat.Name,
                    ColorHex = cat.ColorHex
                }).ToList(),
                ColorBaseCalculated = c.MainCategory != null
                    ? c.MainCategory.ColorHex
                    : "#7c3aed"
            })
            .ToList();

            return View(communities);
        }

        // GET: /Communities/Details/5
        public ActionResult Details(int id)
        {
            var community = _context.Communities
                .Include(c => c.Posts)
                .Include(c => c.UserCommunities)
                .Include(c => c.Categories)
                .FirstOrDefault(c => c.Id == id);

            if (community == null)
                return HttpNotFound();

            var model = new CommunityViewModel
            {
                Id = community.Id,
                Name = community.Name,
                Description = community.Description,
                Country = community.Country,
                TotalPosts = community.Posts.Count,
                TotalFollowers = community.UserCommunities.Count,
                Categories = community.Categories.Select(cat => new CommunityViewModel.CategoryInfo
                {
                    Name = cat.Name,
                    ColorHex = cat.ColorHex
                }).ToList(),
                ColorBaseCalculated = community.Categories.Any()
                    ? community.Categories.FirstOrDefault().ColorHex
                    : "#7c3aed"
            };

            return View(model);
        }
    }
}