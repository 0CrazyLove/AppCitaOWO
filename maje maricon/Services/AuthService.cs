using System.Threading.Tasks;



namespace GoogLoginTest.Services
{
    /// <summary>
    /// Servicio para la gestión de autenticación y registro de usuarios de Google.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly AuthDbContext _context;

        /// <summary>
        /// Inicializa una nueva instancia del servicio de autenticación.
        /// </summary>
        /// <param name="context">Contexto de base de datos para autenticación.</param>
        public AuthService(AuthDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registra un usuario usando los datos proporcionados por Google.
        /// </summary>
        /// <param name="request">Datos del usuario y token de Google.</param>
        /// <returns>El usuario registrado.</returns>
        public async Task<GoogleUserData> RegisterGoogleUserAsync(GoogleUserRequest request)
        {
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
                GoogleToken = request.Token
            };

            _context.GoogleAuthUsers.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
