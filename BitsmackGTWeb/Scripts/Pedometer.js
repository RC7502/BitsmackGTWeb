$(function () {
    getPedometerSummary();

});

function getPedometerSummary() {

    $.ajax(
        { 
            url: "http://bitsmackgtapi.apphb.com/pedometer/summary",
            success:    function (returndata) {
                $("#numofdays").html(returndata.NumOfDays);
                $("#avgsteps").html(returndata.AverageSteps);
                $("#trendsteps").html(returndata.TrendSteps);
                $("#newstepgoal").html(returndata.NewStepGoal);
                if (returndata.AverageSteps > returndata.TrendSteps) {
                    $("#trendsteps").addClass("badValue");
                } else {
                    $("#trendsteps").addClass("goodValue");
                }
                $("#avgsleep").html(MinutesToHours(returndata.AvgSleep));
                $("#sleepgoal").html(returndata.SleepStartTime + " - " + returndata.SleepEndTime);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $("#pedometersummary").html(textStatus + "" + errorThrown);
            }
        });
}
