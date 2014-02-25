$(function () {
    getGoals();
});

function getGoals() {
    var oTable = $("#goalSummaryTable").dataTable({
        "oLanguage": {
            "sSearch": "Search all columns:"
        },
        //"sAjaxSource": "http://bitsmackgtapi.apphb.com/goal/summary",
        "sAjaxSource": "http://localhost:53690/goal/summary",
        "bServerSide": false,
        "bProcessing": true,
        "bSortClasses": false,
        "bDeferRender": true,
        "aaSorting": [[0, 'desc']],
        "aoColumns": [
            { "sName": "Name" },
            {
                "sName": "Average",
                "mRender": function (data, type, full) {
                    if (full[0] == "Standing Desk") {
                        return SecondsToTime(data);
                    }
                    return data;
                }
            },
            {
                "sName": "Trend",
                "mRender": function (data, type, full) {
                    if (full[0] == "Standing Desk") {
                        return SecondsToTime(data);
                    }
                    return data;
                }
            },
            {
                "sName": "New Goal",
                "mRender": function (data, type, full) {
                    if (full[0] == "Standing Desk") {
                        return SecondsToTime(data);
                    }
                    return data;
                }
            }
        ]
    });
}