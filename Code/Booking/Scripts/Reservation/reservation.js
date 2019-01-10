
function init()
{
    reservationHub();
    disableDate();
}
init();


function reservationHub()
{
    var hub = $.connection.signalRHub;

    // funkcije nakon odgovora server
    hub.client.addFlashMessageForReservation = function (disableDates) {

        $("#flashDiv").attr("class", "flashDiv");
        $("#dates").val(disableDates);
        $("#dateStart").datepicker("destroy");
        $("dateEnd").datepicker("detroy");
        disableDate();
    };
    
    //pocetak konekcije, funkcije koje pozivaju serverske fje
    $.connection.hub.start().done(function () {

        var buttonReservation = document.getElementById("addReservation");
        buttonReservation.addEventListener("click", function () {
            var disabledDates = $("#dates").val();
            var startDate = $("#dateStart").val();
            var endDate = $("#dateEnd").val();
            hub.server.addReservation(disabledDates, startDate, endDate);
        });

    });
};

function disableDate()
{
    var datesString = $("#dates").val();
    var dates = datesString.split(",");
    dates = dates.filter(d => d !== "");

    $(".datePickerReservation").each(function () {
        $(this).datepicker({
            dateFormat: 'mm-dd-yy',
            minDate: new Date(),
            beforeShowDay: function (date) {
                var dateString = (date.getMonth() + 1);
                if (date.getMonth() < 9) {
                    dateString = "0" + dateString;
                }
                dateString += "-";

                if (date.getDate() < 10) {
                    dateString += "0";
                }

                dateString += date.getDate() + "-" + date.getFullYear();

                if (!($.inArray(dateString, dates) !== -1)) {
                    return [true, "", "Available"];
                } else {
                    return [false, "", "Unvailable"];
                }
            }
        });
        $(this).datepicker().datepicker("setDate", new Date());
    });
}

