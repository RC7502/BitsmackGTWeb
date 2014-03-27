$(function () {
    getCardioSummary();
    getWeatherForecast();
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

function getWeatherForecast() {
    var table = $("#weatherfcTable");    
    $.ajax({
        url: "http://bitsmackgtapi.apphb.com/cardio/weatherforecast",
        success: function(returndata) {            
            for (var i = 0; i < returndata.length; i++) {
                var d = returndata[i];               
                var r = Row();
                r.append(Cell(d.ForecastDate));
                r.append(Cell(d.Summary));
                r.append(Cell(d.Temperature));
                r.append(Cell(d.ChanceOfPrecip));
                r.append(Cell(d.WindSpeed));
                if (isGoodWeather(d)) {
                    r.addClass("goodValue");
                } else {
                    r.css("color", "gray");
                }

                table.append(r);
            }
        }
    });   
}

function isGoodWeather(d) {
    switch(d.Summary) {
        case "Light Rain":
            return false;
        case "Drizzle":
            return false;
    }
    var temp = d.Temperature.substring(0, d.Temperature.length - 1);
    if (temp < 40 || temp > 90) {
        return false;
    }
    var pct = d.ChanceOfPrecip.substring(0, d.ChanceOfPrecip.length - 1);
    if (pct > 10) {
        return false;
    }
    var wind = d.WindSpeed.substring(0, d.WindSpeed.length - 4);
    if (wind > 15) {
        return false;
    }

    return true;
}

