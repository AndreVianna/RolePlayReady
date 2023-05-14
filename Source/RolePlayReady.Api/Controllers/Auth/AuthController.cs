namespace RolePlayReady.Api.Controllers.Auth;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth", Order = 0)]
[ApiExplorerSettings(GroupName = "Authentication & Authorization")]
[Produces("application/json")]
public class AuthController : ControllerBase {
    private readonly IAuthHandler _handler;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthHandler handler, ILogger<AuthController> logger) {
        _handler = handler;
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginRequest request) {
        var login = request.ToDomain();
        var result = await _handler.SignInAsync(login).ConfigureAwait(false);
        if (result.IsInvalid) {
            _logger.LogDebug("'{user}' fail to login (bad request).", request.Email);
            return BadRequest(result.Errors.UpdateModelState(ModelState));
        }

        if (!result.IsSuccess) {
            _logger.LogDebug("'{user}' failed to login (failed attempt).", request.Email);
            return Unauthorized();
        }

        _logger.LogDebug("'{user}' logged in successfully.", request.Email);
        return Ok(result.Token!.ToLoginResponse());
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterRequest request)
    {
        var registration = request.ToDomain();
        var result = await _handler.RegisterAsync(registration).ConfigureAwait(false);
        if (result.IsInvalid)
        {
            _logger.LogDebug("'{user}' failed to register (bad request).", request.Email);
            return BadRequest(result.Errors.UpdateModelState(ModelState));
        }

        if (result.IsConflict)
        {
            _logger.LogDebug("'{user}' is already in use.", request.Email);
            return Conflict($"'{request.Email}' is already in use.");
        }

        _logger.LogDebug("'{user}' registered successfully.", request.Email);
        return Ok();
    }
}