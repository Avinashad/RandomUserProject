using System;

namespace RandomUserCore.Models.IntegrationModels
{
    public class ImageDetail : CommonModel
    {
        public string Original { get; set; }
        public string Thumbnail { get; set; }
        public Guid UserId { get; set; }

    }
}