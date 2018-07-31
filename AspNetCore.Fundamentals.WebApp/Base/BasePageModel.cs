using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Fundamentals.WebApp.Base
{
    public class BasePageModel : PageModel
    {
        protected readonly ILogger _logger;
        public BasePageModel(ILogger logger)
        {
            _logger = logger;
        }
    }
}
