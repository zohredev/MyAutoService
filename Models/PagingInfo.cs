namespace MyAutoService.Models
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemPerPage { get; set; }
        public int CurrentPage { get; set; }
        //Math.Ceiling round mikone
        public int TotalPage => (int)Math.Ceiling((decimal)TotalItems / ItemPerPage);
        public string UrlParams { get; set; }
    }
}
