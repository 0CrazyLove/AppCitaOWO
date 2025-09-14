// Tipos para la API de Overpass
interface OverpassElement {
  type: string;
  id: number;
  lat?: number;
  lon?: number;
  tags: {
    name?: string;
    amenity?: string;
    [key: string]: string | undefined;
  };
}

interface OverpassResponse {
  elements: OverpassElement[];
}

// Declaraciones globales para Leaflet (si no tienes @types/leaflet instalado)
declare const L: any;

// Inicializar mapa
const map = L.map('map').setView([4.60971, -74.08175], 14); // Bogotá fallback

// Cargar tiles de OSM
L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
  attribution: '&copy; OpenStreetMap contributors'
}).addTo(map);

// Función para buscar lugares con Overpass API
async function searchPlaces(lat: number, lng: number): Promise<void> {
  const query: string = `
    [out:json];
    (
      node["amenity"~"cafe|restaurant|park"](around:2000, ${lat}, ${lng});
    );
    out;
  `;
  
  const url: string = "https://overpass-api.de/api/interpreter?data=" + encodeURIComponent(query);

  try {
    const res: Response = await fetch(url);
    const data: OverpassResponse = await res.json();
    renderPlaces(data.elements);
  } catch (err) {
    console.error("Error con Overpass API:", err);
  }
}

// Renderizar markers y cards
function renderPlaces(places: OverpassElement[]): void {
  const placesContainer: HTMLElement | null = document.getElementById("places");
  
  if (!placesContainer) {
    console.error("No se encontró el contenedor de lugares");
    return;
  }
  
  placesContainer.innerHTML = "";

  places.forEach((place: OverpassElement) => {
    if (!place.lat || !place.lon) return;

    // Marker
    const marker = L.marker([place.lat, place.lon]).addTo(map);
    marker.bindPopup(place.tags.name || "Lugar sin nombre");

    // Card
    const card: HTMLDivElement = document.createElement("div");
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
  navigator.geolocation.getCurrentPosition(
    (pos: GeolocationPosition) => {
      const { latitude, longitude }: GeolocationCoordinates = pos.coords;
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
    },
    (err: GeolocationPositionError) => {
      console.warn("Error geolocalización:", err.message);
      searchPlaces(4.60971, -74.08175); // fallback Bogotá
    }
  );
} else {
  console.warn("Tu navegador no soporta geolocalización.");
  searchPlaces(4.60971, -74.08175);
}