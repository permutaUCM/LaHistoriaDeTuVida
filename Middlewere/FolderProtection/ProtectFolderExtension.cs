using Microsoft.AspNetCore.Builder;
using LHDTV.Helpers;

namespace LHDTV.Middleware
{
    public static class ProtectFolderExtensions
    {
        public static IApplicationBuilder UseProtectFolder(
            this IApplicationBuilder builder,
            ProtectFolderOptions options)
        {
            return builder.UseMiddleware<ProtectFolder>(options);
        }
    }
}