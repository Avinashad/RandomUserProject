using System;
using Newtonsoft.Json;

namespace RandomUserCore.Models.IntegrationModels
{
    public class User : CommonModel
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ImageDetail ImageDetail { get; set; }
    }
}