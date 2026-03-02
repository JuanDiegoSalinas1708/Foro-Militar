using Foro_Militar.Models;
using System.Data.Entity;
using System.Linq;

namespace Foro.Web.Services
{
    public class DashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService()
        {
            _context = new AppDbContext();
        }

        public DashboardHomeViewModel BuildHome()
        {
            var model = new DashboardHomeViewModel
            {
                TrendingPosts = _context.Posts
                    .Where(p => !p.IsDeleted)
                    .Include(p => p.Comments)
                    .Include(p => p.Votes)
                    .Include(p => p.Community)
                    .OrderByDescending(p => p.Comments.Count + p.Votes.Count)
                    .Take(5)
                    .ToList(),

                MostFollowedCommunities = _context.Communities
                    .Include(c => c.UserCommunities)
                    .Include(c => c.Posts)
                    .OrderByDescending(c => c.UserCommunities.Count)
                    .Take(5)
                    .ToList(),

                MostActiveCommunities = _context.Communities
                    .OrderByDescending(c => c.Posts.Count)
                    .Take(5)
                    .ToList(),

                NewHighlightedCommunities = _context.Communities
                    .OrderByDescending(c => c.CreatedAt)
                    .Take(5)
                    .ToList(),

                BestOfWeek = _context.Posts
                    .Where(p => !p.IsDeleted &&
                                p.CreatedAt >= System.DateTime.Now.AddDays(-7))
                    .Include(p => p.Votes)
                    .Include(p => p.Community)
                    .OrderByDescending(p => p.Votes.Count)
                    .Take(5)
                    .ToList()
            };

            return model;
        }
    }
}