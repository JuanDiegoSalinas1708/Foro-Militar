using System.Collections.Generic;

namespace Foro_Militar.Models
{
    public class CommunityViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string Description { get; set; }

        public string Country { get; set; }

        public string ImageUrl { get; set; }

        public string BannerUrl { get; set; }

        public int TotalPosts { get; set; }

        public int TotalFollowers { get; set; }

        public string ColorBaseCalculated { get; set; }

        public string CreatedAtFormatted { get; set; }

        public string CreatedByName { get; set; }

        public List<CategoryInfo> Categories { get; set; }

        public class CategoryInfo
        {
            public string Name { get; set; }
            public string ColorHex { get; set; }

            public string ColorHexSoft => ColorHex + "22"; // Agrega transparencia al color
        }
    }
}
