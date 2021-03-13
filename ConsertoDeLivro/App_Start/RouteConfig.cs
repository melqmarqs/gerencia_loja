using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ConsertoDeLivro {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            Random random = new Random();

            //routes.MapRoute(
            //      name: "Informacoes usuario",
            //      url: "User/%" + random.Next().ToString() + "{id}" + random.Next().ToString() + "_",
            //      defaults: new { controller = "Usuario", action = "InfoUser", id = UrlParameter.Optional }
            //    );

            //routes.MapRoute(
            //    name: "Apagar usuario",
            //    url: "Delete/&_{id}" + random.Next() / 2,
            //    defaults: new { controller = "Usuario", action = "Excluir", id = UrlParameter.Optional }
            //    );

            //routes.MapRoute(
            //    name: "Pedidos usuario",
            //    url: "UserOrders",
            //    defaults: new { controller = "Pedido", action = "ListaPedidos", id = UrlParameter.Optional }
            //    );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index"}
            );
        }
    }
}
