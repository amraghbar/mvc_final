﻿namespace Project.PL.ViewModel
{
	public class UsersViewModel
    {
        public string id { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string City { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
