namespace MyAutoService.Models.ViewModels
{
    public class UsersListViewModel
    {
        public List<ApplicationUser> Users { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
