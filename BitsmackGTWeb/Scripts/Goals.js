$(function () {
    getGoals();
});

function getGoals() {
    var oTable = $("#goalSummaryTable").dataTable({
        "oLanguage": {
            "sSearch": "Search all columns:"
        },
        "sAjaxSource": "http://bitsmackgtapi.apphb.com/goal/summary",
        "bServerSide": false,
        "bProcessing": true,
        "bSortClasses": false,
        "bDeferRender": true,
        "aaSorting": [[0, 'desc']],
        "aoColumns": [
            { "sName": "Name" },
            {
                "sName": "Average",
                "mRender": function (data) {
                    return SecondsToTime(data);
                }
            },
            {
                "sName": "New Goal",
                "mRender": function (data) {
                    return SecondsToTime(data);
                }
            }
        ]
    });
}