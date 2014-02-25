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
                $("#avgadj5ktime").html(SecondsToTime(returndata.AvgAdj5KPace));
                goalTrainingPaces(returndata.AvgAdj5KPace);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $("#cardiosummary").html(textStatus + "" + errorThrown);
            }
        });
}

function goalTrainingPaces(goaltime) {
    var distances = [1, 2, 3, 3.1, 4, 5, 6, 6.2, 7, 8, 9, 10, 13.1];
    $.each(distances, function(index, distance) {
        var newRow = $("<tr></tr>");
        newRow.append($("<td></td>").append(distance));
        var newGoalTime = GoalPaceAtDiffDistance(distance, goaltime);
        newRow.append($("<td></td>").append(SecondsToTime(newGoalTime)));
        var newGoalPace = newGoalTime / distance;
        newRow.append($("<td></td>").append(SecondsToTime(newGoalPace)));
        $('#trainingpaces').append(newRow);
    });

}