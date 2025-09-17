
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Contexto de base de datos para la autenticación de usuarios de Google.
/// </summary>
public class AuthDbContext : DbContext
{
    /// <summary>
    /// Inicializa una nueva instancia del contexto de autenticación.
    /// </summary>
    /// <param name="options">Opciones de configuración del contexto.</param>
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Conjunto de usuarios autenticados mediante Google.
    /// </summary>
    public DbSet<GoogleUserData> GoogleAuthUsers { get; set; }
}