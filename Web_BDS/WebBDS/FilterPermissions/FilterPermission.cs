using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using WebBDS.Commons;
using WebBDS.Extensions;
using WebBDS.ResponseModels;

namespace WebBDS.FilterPermissions;

public class FilterPermission : ActionFilterAttribute
{
    public UserRoleEnum[]? UserRole { get; set; }
    public ActionFilterEnum Action { get; set; }

    public override void OnActionExecuting(ActionExecutingContext actionContext)
    {
        StringValues values;
        var checkAction = false;
        
        //test
        var newToken = WriteToken("john_doe", "123@gmail.com", "admin", "12345");
        actionContext.HttpContext.Request.Headers["ACCESS_TOKEN"] = newToken;
        //test
        
        // USER_DATA
        // ACCESS_TOKEN
        if (actionContext.HttpContext.Request.Headers.TryGetValue("ACCESS_TOKEN", out values))
        {
            var headerValue = values.FirstOrDefault();
            if (!String.IsNullOrEmpty(headerValue))
            { 
                var userToken = ReadToken(headerValue);
                if (UserRole is null)
                {
                    checkAction = true;
                }
                else
                {
                    var t = UserRole.FirstOrDefault(x => x.ToString().ToLower() == userToken.Role!.ToLower()).ToString();
                    if (t != "0")
                    {
                        checkAction = true;
                    }
                }
            }
        }
        
        
        // actionContext.HttpContext.Response.Redirect("/Home/Index");
        if (!checkAction)
        {
            actionContext.Result = new JsonResult(new
            {
                success = false,
                status = 401,
                error = "401 Unauthorized"
            });
        }
        base.OnActionExecuting(actionContext);
    }

    private void AccessDeniedReturn()
    {
        // HttpContext.Current.Response.Redirect("/AccessDenied/Index");
    }

    public override void OnResultExecuting(ResultExecutingContext context)
    {
        // var newToken = WriteToken("john_doe", "123@gmail.com", "admin", "12345");
        // context.HttpContext.Request.Headers["Authorization"] = newToken;
        
        // var t = context.HttpContext.Response.Headers.Add("test", _value);
        //
        //
        // base.OnActionExecuting(context);
    }

    public string WriteToken(string fullName, string email, string role, string userId)
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim("username", fullName),
            new Claim("email", email),
            new Claim("role", role)
        };

        var token = new JwtSecurityToken(
            claims: claims,
            issuer: config["jwt:Issuer"],
            audience: config["jwt:Issuer"],
            expires: DateTime.UtcNow.AddDays(30),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["jwt:key"])),
                SecurityAlgorithms.HmacSha256Signature)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public UserToken ReadToken(string tokenString)
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["jwt:key"])),
            ValidIssuer = config["jwt:Issuer"],
            ValidAudience = config["jwt:Issuer"],
            ValidateIssuer = true,
            ValidateAudience = true,
            ClockSkew = TimeSpan.Zero
        };
        var handler = new JwtSecurityTokenHandler();
        var claimsPrincipal = handler.ValidateToken(tokenString, tokenValidationParameters, out var validatedToken);

        var jwtToken = validatedToken as JwtSecurityToken;
        if (jwtToken == null )
        {
            throw new SecurityTokenException("Invalid token");
        }

        var userToken = new UserToken();
        
        userToken.UserId = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
        userToken.UserName = jwtToken.Claims.FirstOrDefault(c => c.Type == "username")?.Value;
        userToken.Email = jwtToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
        userToken.Role = jwtToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value;

        return userToken;
    }
}