
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Representa los datos de un usuario autenticado mediante Google.
/// </summary>
public class GoogleUserData
{
    /// <summary>
    /// Identificador único del usuario (sub). Clave primaria.
    /// </summary>
    [Key]
    [Required]
    public string Sub { get; set; } = string.Empty;

    /// <summary>
    /// Emisor del token (iss).
    /// </summary>
    public string? Iss { get; set; }

    /// <summary>
    /// ID de la aplicación autorizada (azp).
    /// </summary>
    public string? Azp { get; set; }

    /// <summary>
    /// Audiencia del token (aud).
    /// </summary>
    public string? Aud { get; set; }

    /// <summary>
    /// Correo electrónico del usuario.
    /// </summary>
    /// 
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    /// <summary>
    /// Indica si el correo fue verificado.
    /// </summary>
    public bool EmailVerified { get; set; }

    /// <summary>
    /// Nombre completo del usuario.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// URL de la foto de perfil.
    /// </summary>
    public string? Picture { get; set; }

    /// <summary>
    /// Nombre de pila del usuario.
    /// </summary>
    public string? GivenName { get; set; }

    /// <summary>
    /// Apellido del usuario.
    /// </summary>
    public string? FamilyName { get; set; }

    /// <summary>
    /// Expiración del token en formato Unix.
    /// </summary>
    public long Exp { get; set; }

    /// <summary>
    /// Token de Google asociado al usuario.
    /// </summary>
    [Required]
    public string GoogleToken { get; set; } = string.Empty;
}