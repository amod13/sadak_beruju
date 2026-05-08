using System.Web.Mvc;

namespace _4pix_Beruju.Areas.LocalLevel
{
    public class LocalLevelAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LocalLevel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "LocalLevel_default",
                "LocalLevel/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}