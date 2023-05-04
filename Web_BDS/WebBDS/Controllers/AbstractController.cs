using Microsoft.AspNetCore.Mvc;
using WebBDS.Extensions;

namespace WebBDS.Controllers;

public class AbstractController : Controller
{
    // private readonly IJwtTokenHandler _jwtTokenHandler;
    //
    // public AbstractController(IJwtTokenHandler jwtTokenHandler)
    // {
    //     _jwtTokenHandler = jwtTokenHandler;
    // }
    // public bool CheckRoleUser(string[] listRole,bool andor)// andor true: and, false: or
    // {
    //     var accessToken = Request.Cookies["ACCESS_TOKEN"];
    //     if (accessToken is not null)
    //     {
    //         var user = _jwtTokenHandler.ReadToken(accessToken);
    //         var userRole = user.Claims.FirstOrDefault(x => x.Type == "role");
    //         if (userRole is not null)
    //         {
    //             if (andor)
    //             {
    //                 // true check role and listrole
    //                 foreach (string role in listRole)
    //                 {
    //                     if (!userRole.Value.Equals(role))
    //                     {
    //                         return false;
    //                     }
    //                 }
    //                 return true;
    //             }
    //             else
    //             {
    //                 // check role or listrole
    //                 foreach (var role in listRole)
    //                 {
    //                     if (userRole.Value.Equals(role))
    //                     {
    //                         return true;
    //                     }
    //                 }
    //                 return false;
    //             }
    //         }
    //     }
    //     return false;
    // }
    //
    // public string CreateToken(string fullName,string email,string role)
    // {
    //     return _jwtTokenHandler.WriteToken(fullName,email,role);
    // }
}