var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
// Inicializar mapa
const map = L.map('map').setView([4.60971, -74.08175], 14); // Bogotá fallback
// Cargar tiles de OSM
L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '&copy; OpenStreetMap contributors'
}).addTo(map);
// Función para buscar lugares con Overpass API
function searchPlaces(lat, lng) {
    return __awaiter(this, void 0, void 0, function* () {
        const query = `
    [out:json];
    (
      node["amenity"~"cafe|restaurant|park"](around:2000, ${lat}, ${lng});
    );
    out;
  `;
        const url = "https://overpass-api.de/api/interpreter?data=" + encodeURIComponent(query);
        try {
            const res = yield fetch(url);
            const data = yield res.json();
            renderPlaces(data.elements);
        }
        catch (err) {
            console.error("Error con Overpass API:", err);
        }
    });
}
// Renderizar markers y cards
function renderPlaces(places) {
    const placesContainer = document.getElementById("places");
    if (!placesContainer) {
        console.error("No se encontró el contenedor de lugares");
        return;
    }
    placesContainer.innerHTML = "";
    places.forEach((place) => {
        if (!place.lat || !place.lon)
            return;
        // Marker
        const marker = L.marker([place.lat, place.lon]).addTo(map);
        marker.bindPopup(place.tags.name || "Lugar sin nombre");
        // Card
        const card = document.createElement("div");
        card.classList.add("card");
        card.innerHTML = `
      <h2>${place.tags.name || "Lugar sin nombre"}</h2>
      <p>${place.tags.amenity || "Sin categoría"}</p>
    `;
        placesContainer.appendChild(card);
    });
}
// Geolocalización del navegador
if (navigator.geolocation) {
    navigator.geolocation.getCurrentPosition((pos) => {
        const { latitude, longitude } = pos.coords;
        map.setView([latitude, longitude], 15);
        // Marcar ubicación del usuario
        L.marker([latitude, longitude], {
            icon: L.divIcon({
                className: "user-marker",
                html: "💖",
                iconSize: [24, 24]
            })
        }).addTo(map).bindPopup("Estás aquí");
        searchPlaces(latitude, longitude);
    }, (err) => {
        console.warn("Error geolocalización:", err.message);
        searchPlaces(4.60971, -74.08175); // fallback Bogotá
    });
}
else {
    console.warn("Tu navegador no soporta geolocalización.");
    searchPlaces(4.60971, -74.08175);
}
export {};
//# sourceMappingURL=app.js.map