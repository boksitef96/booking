function initWeather() {
    var latitude = parseFloat($('#coordinates').attr("data-latitude"));
    var longitude = parseFloat($('#coordinates').attr("data-longitude"));
    $.ajax({
        url: "https://api.openweathermap.org/data/2.5/weather?lat=" + latitude + "&lon=" + longitude + "&mode=html&APPID=8aa8eb55555024179f696bdbe89a97b4&",
        dataType: "html",
        success: function (data) {
            console.log(data, "succes");
            var first = data.split("<body>");
            var second = first[1].split("</body>");
            var weather = second[0];
            console.log(second[0]);
            document.getElementById("accomodationWeather").innerHTML = weather;
        },
        error: function (data) {
            console.log(data, "error");
        }
    });
}
initWeather();