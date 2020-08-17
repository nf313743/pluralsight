using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;

namespace Owin.Demo.Middleware
{
    public class DebugMiddlewareOptions
    {
        public Action<IOwinContext> OnIncomingRequest { get; set; } = delegate { };

        public Action<IOwinContext> OnOutgoingRequest { get; set; } = delegate { };
    }
}