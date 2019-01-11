
function initMap() {
    var latitude = parseFloat($('#coordinates').attr("data-latitude"));
    var longitude = parseFloat($('#coordinates').attr("data-longitude"));

    var uluru = { lat: latitude, lng: longitude };
    var map = new google.maps.Map(
        document.getElementById('map'), { zoom: 10, center: uluru });
    var marker = new google.maps.Marker({ position: uluru, map: map });
}