using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DesafioHyperativa.API.Infra;


[ApiController]
[Route("api/[controller]")]
[ApiConventionType(typeof(DefaultApiConventions))]
public abstract class CustomController : ControllerBase
{
    private int? _userId;
    public long UserId
    {
        get
        {
            if (!_userId.HasValue)
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;

                var userIdClaim = claimsIdentity.FindFirst(CustomClaims.UserId);
                _userId = Convert.ToInt32(userIdClaim.Value);
            }

            return _userId.Value;
        }
    }

    private readonly ILogger<CustomController> _logger;
    public CustomController(ILogger<CustomController> logger)
    {
        _logger = logger;
    }
}
