using System;

namespace Ads.Application.Dto
{
    public class AdListDto
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public DateTime AddedDateTime { get; set; }

        public string UserName { get; set; }

        public string CategoryName { get; set; }
        public bool IsFollowed { get; set; }
    }
}