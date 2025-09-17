
using System.Threading.Tasks;

/// <summary>
/// Interfaz para el servicio de autenticación de usuarios de Google.
/// </summary>
public interface IAuthService
{
   /// <summary>
   /// Registra un usuario usando los datos proporcionados por Google.
   /// </summary>
   /// <param name="request">Datos del usuario y token de Google.</param>
   /// <returns>El usuario registrado.</returns>
   Task<GoogleUserData> RegisterGoogleUserAsync(GoogleUserRequest request);
}




