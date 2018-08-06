using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspNetCore.Fundamentals.WebApp
{
    public class LogoutViewModel
    {
        [BindNever]
        public string RequestId { get; set; }
    }
}
