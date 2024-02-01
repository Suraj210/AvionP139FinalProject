namespace Avion.Areas.Admin.ViewModels.Account
{
    public class UserVM
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public IList<string> RoleName { get; set; }
        public bool EmailConfirm { get; set; }
    }
}
