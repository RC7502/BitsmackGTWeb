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
                var direction;
                if (returndata.AverageSteps > returndata.TrendSteps) {
                    direction = "Negative :-(";
                }
                else {
                    direction = "Positive!";
                }
                $("#directionsteps").html(direction);
                $("#newstepgoal").html(returndata.NewStepGoal);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $("#pedometersummary").html(textStatus + "" + errorThrown);
            }
        });
}
