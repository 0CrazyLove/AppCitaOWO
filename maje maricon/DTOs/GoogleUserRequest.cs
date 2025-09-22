
/// <summary>
/// Representa una solicitud de autenticación de usuario mediante Google.
/// </summary>
public class GoogleUserRequest
{
    /// <summary>
    /// Token de autenticación de Google.
    /// </summary>
    public string GoogleToken { get; set; } = string.Empty;

    /// <summary>
    /// Datos del usuario autenticado por Google.
    /// </summary>
    public GoogleUserData User { get; set; } = new GoogleUserData();
}