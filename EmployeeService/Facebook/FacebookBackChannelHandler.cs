using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace EmployeeService.Facebook
{
    // Implemented FacebookBackHandler to address the breaking change introduced
    // by Facebook to its API version 2.4
    // ver 2.3 -> https://graph.facebook.com/v2.3/me?access_token=ABC
    // ver 2.4 -> https://graph.facebook.com/v2.4/me?fields=id,email&access_token=ABC

    public class FacebookBackChannelHandler: HttpClientHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if(!request.RequestUri.AbsolutePath.Contains("/oauth"))
            {
                request.RequestUri = new Uri(request.RequestUri.AbsoluteUri.Replace("?access_token", "&access_token"));
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}