using System;

namespace RandomUserCore.Models
{
    public partial class ImageDetailEntity : EntityBase
    {

        public Guid Id { get; set; }
        public string Original { get; set; }
        public string Thumbnail { get; set; }

        public Guid UserId { get; set; }
        public virtual UserEntity User { get; set; }
    }
}