using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace WiredBrainCoffee
{
    public class PromoConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            int letterCount = 0;
            int numCount = 0;
            int sum = 0;

            foreach (var unit in values["token"].ToString())
            {
                letterCount += char.IsLetter(unit) ? 1 : 0;
                numCount += char.IsDigit(unit) ? 0 : 1;
                sum += char.IsDigit(unit) ? int.Parse(unit.ToString()) : 0;
            }

            return letterCount == 3 && numCount == 3 && sum % 3 == 0;
        }
    }
}