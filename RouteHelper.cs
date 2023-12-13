using WEB.Helpers;

namespace WEB
{
    public class RouteHelper
    {
        public void GetCurrntRoute(RouteData routeData)
        {
            var route = RouteResponse.Routes.FirstOrDefault( x => x.Action == routeData.Values["Action"] && x.Controller == routeData.Values["Controller"]);
            if (route == null)
            {
                var routeResponse = new RouteResponse()
                {
                    Controller = routeData.Values["controller"]?.ToString(),
                    Action = routeData.Values["action"]?.ToString()
                };
            }
            RouteResponse.Routes.Add(route);
        }
    }
}
