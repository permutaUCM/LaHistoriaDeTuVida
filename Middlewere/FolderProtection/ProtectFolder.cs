using Microsoft.AspNetCore.Http;
using LHDTV.Middleware;
using Microsoft.AspNetCore.Authorization;
using LHDTV.Helpers;
using System;
using System.Threading.Tasks;
namespace LHDTV.Middleware
{
    public class ProtectFolder
    {
        private readonly RequestDelegate _next;
        private readonly PathString _path;
        private readonly string _policyName;

        public ProtectFolder(RequestDelegate next, ProtectFolderOptions options)
        {
            _next = next;
            _path = options.Path;
            _policyName = options.PolicyName;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.StartsWithSegments(_path))
            {
               Console.WriteLine("Interceptado");
            }

            await _next(httpContext);
        }
    }
}