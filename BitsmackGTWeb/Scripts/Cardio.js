$(function () {
    getCardioSummary();

});

function getCardioSummary() {

    $.ajax(
        {
            url: "http://bitsmackgtapi.apphb.com/cardio/summary",
            success: function (returndata) {
                $("#totalruns").html(returndata.TotalRuns);
                $("#avgmiles").html(returndata.AvgMilesPerRun);
                $("#avgadj5ktime").html(returndata.AvgAdj5KPace);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $("#cardiosummary").html(textStatus + "" + errorThrown);
            }
        });
}

