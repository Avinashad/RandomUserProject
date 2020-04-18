using System;
using System.Collections.Generic;

namespace RandomUserCore.Models
{
    public partial class UserEntity : EntityBase
    {
        public UserEntity()
        {
            CreatedAt = DateTime.Now;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual ImageDetailEntity ImageDetail { get; set; }  
    }


}