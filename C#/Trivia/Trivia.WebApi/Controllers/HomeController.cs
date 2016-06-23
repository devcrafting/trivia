using System.Web.Http;

namespace Trivia.WebApi.Controllers
{
    public class HomeController : ApiController
    {
        public string Get()
        {
            return "It works !!";
        }
    }
}
