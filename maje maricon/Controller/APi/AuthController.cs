using GoogLoginTest.Services;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
/// <summary>
/// Controlador para la autenticación de usuarios mediante Google.
/// </summary>
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    /// <summary>
    /// Inicializa una nueva instancia del controlador de autenticación.
    /// </summary>
    /// <param name="authService">Servicio de autenticación.</param>
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Registra un usuario usando los datos de Google.
    /// </summary>
    /// <param name="request">Datos del usuario y token de Google.</param>
    /// <returns>Respuesta HTTP con el resultado de la operación.</returns>
    [HttpPost("google")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleUserRequest request)
    {
        if (request == null || request.User == null) return BadRequest("Invalid data");

        var user = await _authService.RegisterGoogleUserAsync(request);
        return Ok(user);
    }
}
