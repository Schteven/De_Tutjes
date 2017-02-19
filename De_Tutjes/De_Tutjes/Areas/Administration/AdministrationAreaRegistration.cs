using System.Web.Mvc;

namespace De_Tutjes.Areas.Administration
{
    public class AdministrationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Administration";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {   
            context.MapRoute(
                "Administration_Child_CreateStep",
                "Administration/Children/Create/{step}/{id}",
                new { step = UrlParameter.Optional, id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Administration_default",
                "Administration/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
            
        }
    }
}