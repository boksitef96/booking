function accomodationHub()
{
    var hub = $.connection.signalRHub;

    // funkcije nakon odgovora server
    hub.client.addFlashMessageForAccomodation = function (name,country,id) {
        $("#flashDiv").attr("class", "flashDiv");
        var newAccomodation = 
            `<div class="card col-lg-3">
                <h4> ${name} </h4>
                <h5> ${country}</h5>
                <hr />
                <a class="btn btn-block btn-default" href='/show-rooms/${id}/True'>Detaljnije<i style="margin-left:5px" class="fas fa-info-circle"></i></a>
            </div>`
        $(".cardsWrapper").append(newAccomodation)
    };

    //pocetak konekcije, funkcije koje pozivaju serverske fje
    $.connection.hub.start().done(function () {
        var buttonAccomodation = document.getElementById("addAccomodation");
        if (buttonAccomodation)
        {
            buttonAccomodation.addEventListener("click", function () {
                var name = $("#Accomodation_Name").val();
                var country = $("#Accomodation_Country").val();
                setTimeout(hub.server.addAccomodation(name, country), 3000);
               
            });
        }
    });
};
accomodationHub();