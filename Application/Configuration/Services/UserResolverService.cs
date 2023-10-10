using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Configuration.Services
{
    public class UserResolverService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserResolverService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetUser()
        {
            return _contextAccessor.HttpContext.User?.Identity?.Name;
        }
    }
}
    