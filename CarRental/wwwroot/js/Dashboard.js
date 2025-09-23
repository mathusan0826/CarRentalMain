//<script src="https://maps.googleapis.com/maps/api/js?key=YOUR_GOOGLE_MAPS_API_KEY"></script>

//function initTrackingMap() {
//    const map = new google.maps.Map(document.getElementById("trackingMap"), { zoom: 12, center: { lat: 9.6615, lng: 80.0255 } });
//    const vehicles = [
//        { name: "Honda Civic", lat: 9.6615, lng: 80.0255, type: "CAR" },
//        { name: "Toyota Aqua", lat: 9.6620, lng: 80.0260, type: "CAR" },
//        { name: "Suzuki Swift", lat: 9.6610, lng: 80.0250, type: "CAR" },
//        { name: "Toyota Hiace", lat: 9.6625, lng: 80.0265, type: "VAN" },
//        { name: "Mitsubishi Delica", lat: 9.6618, lng: 80.0258, type: "VAN" },
//        { name: "Toyota Coaster", lat: 9.6630, lng: 80.0270, type: "BUS" },
//        { name: "Jeep Wrangler", lat: 9.6612, lng: 80.0252, type: "JEEP" }
//    ];
//    vehicles.forEach(v => {
//        const marker = new google.maps.Marker({
//            position: { lat: v.lat, lng: v.lng }, map, title: v.name,
//            icon: { url: getVehicleIcon(v.type), scaledSize: new google.maps.Size(32, 32) }
//        });
//        const infoWindow = new google.maps.InfoWindow({
//            content: `<div class="p-2" style="font-family:Arial;font-size:14px;">
//                            <strong>${v.name}</strong><br>
//                            <span class="badge bg-secondary">${v.type}</span><br>
//                            <small>Lat:${v.lat.toFixed(4)}, Lng:${v.lng.toFixed(4)}</small>
//                         </div>`
//        });
//        marker.addListener('click', () => { infoWindow.open(map, marker); });
//    });
//}
//function getVehicleIcon(type) {
//    const icons = { 'CAR': 'https://maps.google.com/mapfiles/ms/icons/red-dot.png', 'VAN': 'https://maps.google.com/mapfiles/ms/icons/blue-dot.png', 'BUS': 'https://maps.google.com/mapfiles/ms/icons/green-dot.png', 'JEEP': 'https://maps.google.com/mapfiles/ms/icons/yellow-dot.png' };
//    return icons[type] || icons['CAR'];
//}

//function countUp() {
//    document.querySelectorAll('.count').forEach(counter => {
//        const target = +counter.getAttribute('data-target'); let count = 0; const step = target / 100;
//        const updateCount = () => { count += step; if (count < target) { counter.innerText = Math.ceil(count); requestAnimationFrame(updateCount); } else { counter.innerText = target; } };
//        updateCount();
//    });
//}

//window.addEventListener('load', () => { initTrackingMap(); countUp(); });

