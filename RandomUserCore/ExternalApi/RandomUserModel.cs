using System;
using System.Collections.Generic;

namespace RandomUserCore.ExternalApi
{
    internal class RandomUserModel
    {
        public List<UserModel> Results { get; set; }

    }
    public class UserModel
    {
        public Name Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Picture Picture { get; set; }
        public Dob Dob { get; set; }
    }
    public class Dob
    {
        public DateTime Date { get; set; }
        public int Age { get; set; }
    }

    public class Picture
    {
        public string Large { get; set; }
        public string Medium { get; set; }
        public string thumbnail { get; set; }
    }
    public class Name
    {
        public string Title { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
    }
}