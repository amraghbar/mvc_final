namespace Project.PL.Areas.Admin.ViewModels
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
        public bool OrderStatus { get; set; }  

    }

}
