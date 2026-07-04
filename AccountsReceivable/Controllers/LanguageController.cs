using System;
using System.Web;
using System.Web.Mvc;

namespace AccountsReceivable.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language/SetLanguage?lang=es&returnUrl=/Receivables
        public ActionResult SetLanguage(string lang, string returnUrl)
        {
            if (lang != "en" && lang != "es")
            {
                lang = "en";
            }

            var cookie = new HttpCookie("lang", lang)
            {
                Expires = DateTime.Now.AddYears(1)
            };
            Response.Cookies.Add(cookie);

            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                returnUrl = Url.Action("Index", "Receivables");
            }

            return Redirect(returnUrl);
        }
    }
}
