﻿namespace WEB.Helpers
{
    public class RouteResponse
    {
        public string Controller { get; set; }
        public string Action { get; set; }

        public static List<RouteResponse> Routes = new List<RouteResponse>();

    }
}
