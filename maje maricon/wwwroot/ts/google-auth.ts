// Interfaz para tipar la respuesta de Google
interface GoogleCredentialResponse {
    credential: string; // JWT token
    select_by?: string;
}

// Interfaz para los datos del usuario decodificados
interface GoogleUserData {
    iss: string;
    azp: string;
    aud: string;
    sub: string;
    email: string;
    email_verified: boolean;
    name: string;
    picture: string;
    given_name: string;
    family_name: string;
    exp: number;
}


// Función para decodificar el JWT token (sin verificación - solo para desarrollo)
function parseJwt(token: string): GoogleUserData | null {
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
        const jsonPayload = decodeURIComponent(
            window.atob(base64)
                .split('')
                .map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
                .join('')
        );

        return JSON.parse(jsonPayload);

    } catch (error) {

        return null;
    }
}

// Función callback principal que maneja la respuesta de Google
async function handleCredentialResponse(response: GoogleCredentialResponse): Promise<void> {
    // Decodificar el token JWT para obtener los datos del usuario
    const userData = parseJwt(response.credential);

    if (userData) {
        displayUserInfo(userData);
        await fetch("Api/Auth/google", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                token: response.credential,
                user: userData
            }),
        })
    }
}


// Función para mostrar la información del usuario en la página
function displayUserInfo(userData: GoogleUserData): void {
    // Buscar un elemento donde mostrar la info del usuario
    const userInfoElement = document.getElementById('user-info') as HTMLDivElement;

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
function signOut(): void {
    const userInfoElement = document.getElementById('user-info') as HTMLDivElement;
    userInfoElement.remove();

}



// Hacer las funciones globales para que HTML pueda accederlas
declare global {
    interface Window {
        handleCredentialResponse: typeof handleCredentialResponse;
        signOut: typeof signOut;
    }
}

window.handleCredentialResponse = handleCredentialResponse;
window.signOut = signOut;


