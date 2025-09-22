using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GoogLoginTest.Services
{
    /// <summary>
    /// Servicio para la gestión de autenticación y registro de usuarios de Google.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly LoginDbContext _context;

        /// <summary>
        /// Inicializa una nueva instancia del servicio de autenticación.
        /// </summary>
        /// <param name="context">Contexto de base de datos para autenticación.</param>
        public AuthService(LoginDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registra un usuario usando los datos proporcionados por Google.
        /// Valida si el usuario ya existe antes de registrarlo.
        /// </summary>
        /// <param name="request">Datos del usuario y token de Google.</param>
        /// <returns>El usuario registrado o existente.</returns>
        /// <exception cref="InvalidOperationException">Se lanza cuando el usuario ya existe.</exception>
        public async Task<GoogleUserData> RegisterGoogleUserAsync(GoogleUserRequest request)
        {
            // Validar si el usuario ya existe por Email o Sub (ID de Google)
            var existingUser = await _context.GoogleAuthUsers.FirstOrDefaultAsync(u => u.Email == request.User.Email || u.Sub == request.User.Sub);

            if (existingUser != null)
            {
                
                existingUser.GoogleToken = request.GoogleToken;
                existingUser.Exp = request.User.Exp;
                await _context.SaveChangesAsync();
                return existingUser;
                
            }
            // Crear nuevo usuario si no existe
            var user = new GoogleUserData
            {
                Sub = request.User.Sub,
                Iss = request.User.Iss,
                Azp = request.User.Azp,
                Aud = request.User.Aud,
                Email = request.User.Email,
                EmailVerified = request.User.EmailVerified,
                Name = request.User.Name,
                Picture = request.User.Picture,
                GivenName = request.User.GivenName,
                FamilyName = request.User.FamilyName,
                Exp = request.User.Exp,
                GoogleToken = request.GoogleToken
            };

            _context.GoogleAuthUsers.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

      
    }
}