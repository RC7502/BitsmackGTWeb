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
                getTransactions();
                pedometerMonthAvgChart();
                weightCalSummaryTable();
                weightCalDetailTable();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $("#pedometersummary").html(textStatus + "" + errorThrown);
            }
        });
}

function getTransactions() {
    var oTable = $("#recentPedometerEntries").dataTable({
        "oLanguage": {
            "sSearch": "Search all columns:"
        },
        "sAjaxSource": "http://bitsmackgtapi.apphb.com/pedometer/detail",
        "bServerSide": false,
        "bProcessing": true,
        "bSortClasses": false,
        "bDeferRender": true,
        "aaSorting": [[0,'desc']],
        "aoColumns": [
            { "sName": "Date" },
            { "sName": "Steps" },
            {
                "sName": "Sleep",
                "mRender": function(data) {
                    return MinutesToHours(data);
                }
            },
            { "sName": "Created" },
            { "sName": "Updated" }
        ]
    });
}

function pedometerMonthAvgChart() {
    $.ajax({
        url: "http://bitsmackgtapi.apphb.com/pedometer/monthaverages",
        success: function(chartData) {
            $('#pedometerMonthAvg').highcharts({
                chart: {
                    renderTo: 'container',
                    type: 'column'
                },
                title: {
                    text: 'Average Steps/Day By Month'
                },
                xAxis: {
                    categories: chartData.Categories
                },
                yAxis: {
                    plotLines: [{
                        value: chartData.PlotLine,
                        color: '#ff0000',
                        width: 2,
                        zIndex: 4
                    }]
                },
                series: [{
                    name: 'Steps Per Day',
                    data: chartData.SeriesData
                }]
            });
        }
    });

}

function weightCalSummaryTable() {
    $.ajax({
        url: "http://bitsmackgtapi.apphb.com/goal/weightsummary",
        success: function (returndata) {
            $("#weightDateRange").html(returndata.DateRange);
            $("#weightLoss").html(returndata.WeightTrendChange + " lbs.");
            $("#calConsumedPerDay").html(returndata.CalConsumedPerDay);
            $("#calBurnedPerDay").html(returndata.CalBurnedPerDay);
            $("#newCalorieGoal").html(returndata.NewDailyCalorieGoal);
            var lbsPerWeek = returndata.WeightTrendChange / (returndata.NumDays / 7);
            if (lbsPerWeek > 1) {
                $("#weightLoss").addClass("goodValue");
            }
            else if (lbsPerWeek < 0) {
                $("#weightLoss").addClass("badValue");
            }
        }
    });
}

function weightCalDetailTable() {
    var oTable = $("#weightCalTable").dataTable({
        "oLanguage": {
            "sSearch": "Search all columns:"
        },
        "sAjaxSource": "http://bitsmackgtapi.apphb.com/goal/weightdetail",
        //"sAjaxSource": "http://localhost:53690/goal/summary",
        "bServerSide": false,
        "bProcessing": true,
        "bSortClasses": false,
        "bDeferRender": true,
        "aaSorting": [[0, 'desc']],
        "aoColumns": [
            { "sName": "Date" },
            {"sName": "Weight"},
            {"sName": "Trend"},
            {"sName": "Calories Consumed"}
        ],
        "fnRowCallback": function (nRow, aData, iDisplayIndex) {
            /* Append the grade to the default row class name */
            if (aData[1] != 0) {
                if (aData[1] < aData[2]) {
                    $('td:eq(1)', nRow).addClass("goodValue");
                } else {
                    $('td:eq(1)', nRow).addClass("badValue");
                }
            }
            
            return nRow;
        }
    });
}
