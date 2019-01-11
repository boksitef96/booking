
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
    hub.client.addFlashMessageForReservation = function (disableDates, roomId) {

        var roomIdCurrent = $("#flashDiv").attr("data-id");
        if (roomId == roomIdCurrent) {
            $("#flashDiv").attr("class", "flashDiv");
            $("#dates").val(disableDates);
            $("#dateStart").datepicker("destroy");
            $("#dateEnd").datepicker("destroy");
            disableDate();
        }
    };

    hub.client.addFlashMessageForAccomodationDetails = function (text, roomId) {
        var roomIdCurrent = $("#flashDiv").attr("data-id");
        if (roomId == roomIdCurrent) {
            $("#flashDiv").attr("class", "flashDiv");
            $("#flashDiv").html(text);
        }
    }
    
    //pocetak konekcije, funkcije koje pozivaju serverske fje
    $.connection.hub.start().done(function () {
        
        var buttonReservation = document.getElementById("addReservation");
        if (buttonReservation) {
            buttonReservation.addEventListener("click", function () {
                console.log("aaa");
                var disabledDates = $("#dates").val();
                var startDate = $("#dateStart").val();
                var endDate = $("#dateEnd").val();
                var roomId = $("#flashDiv").attr("data-id");
                hub.server.addReservation(disabledDates, startDate, endDate, roomId);
            });
        }
    });
};

function disableDate()
{
    var datesString = $("#dates").val();
    if (datesString) {
        var dates = datesString.split(",");
        dates = dates.filter(d => d !== "");
    }

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

