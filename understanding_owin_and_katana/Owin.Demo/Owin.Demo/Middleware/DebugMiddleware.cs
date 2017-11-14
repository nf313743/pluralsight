using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin;
using AppFun = System.Func<
                    System.Collections.Generic.IDictionary<string, object>,
                    System.Threading.Tasks.Task>;

namespace Owin.Demo.Middleware
{
    public class DebugMiddleware
    {
        private AppFun _next;
        private DebugMiddlewareOptions _options;

        public DebugMiddleware(AppFun next, DebugMiddlewareOptions options)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            var ctx = new OwinContext(environment);
            _options.OnIncomingRequest(ctx);
            await _next(environment);
            _options.OnOutgoingRequest(ctx);
        }
    }
}