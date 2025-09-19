var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
// Función para decodificar el JWT token (sin verificación - solo para desarrollo)
function parseJwt(token) {
    try {
        // Validar que el token tenga el formato correcto (3 partes separadas por puntos)
        const parts = token.split('.');
        if (parts.length !== 3) {
            console.error('Invalid JWT format');
            return null;
        }
        const base64Url = parts[1];
        if (!base64Url) {
            console.error('Invalid JWT payload');
            return null;
        }
        const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        const jsonPayload = decodeURIComponent(window.atob(base64)
            .split('')
            .map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
            .join(''));
        return JSON.parse(jsonPayload);
    }
    catch (error) {
        return null;
    }
}
// Función callback principal que maneja la respuesta de Google
function handleCredentialResponse(response) {
    return __awaiter(this, void 0, void 0, function* () {
        // Decodificar el token JWT para obtener los datos del usuario
        const userData = parseJwt(response.credential);
        if (userData) {
            displayUserInfo(userData);
            yield fetch("http://localhost:5163/api/auth/google", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({
                    token: response.credential,
                    user: userData
                }),
            });
        }
    });
}
// Función para mostrar la información del usuario en la página
function displayUserInfo(userData) {
    // Buscar un elemento donde mostrar la info del usuario
    const userInfoElement = document.getElementById('user-info');
    userInfoElement.innerHTML = `
      <div style="border: 1px solid #ddd; padding: 15px; border-radius: 8px; margin: 10px 0;">
        <h3>¡Bienvenido!</h3>
        <img src="${userData.picture}" alt="Profile" style="width: 50px; height: 50px; border-radius: 50%;">
        <p><strong>Nombre:</strong> ${userData.name}</p>
        <p><strong>Email:</strong> ${userData.email}</p>
        <button onclick="signOut()">Cerrar Sesión</button>
      </div>
    `;
}
// Función para cerrar sesión (opcional)
function signOut() {
    const userInfoElement = document.getElementById('user-info');
    userInfoElement.remove();
}
window.handleCredentialResponse = handleCredentialResponse;
window.signOut = signOut;
export {};
//# sourceMappingURL=google-auth.js.map