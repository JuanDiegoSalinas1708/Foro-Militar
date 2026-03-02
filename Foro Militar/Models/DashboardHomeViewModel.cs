using Foro.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foro_Militar.Models
{
    public class DashboardHomeViewModel
    {
        public List<Post> TrendingPosts { get; set; }
        public List<Post> BestOfWeek { get; set; }

        public List<Community> MostFollowedCommunities { get; set; }
        public List<Community> MostActiveCommunities { get; set; }
        public List<Community> NewHighlightedCommunities { get; set; }

        public Dictionary<string, List<Community>> TopCommunitiesByCategory { get; set; }
    }

}