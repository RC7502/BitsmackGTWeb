$(function () {
    getPedometerSummary();
    getTransactions();
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
