$(function () {
    getPedometerSummary();

});

function getPedometerSummary() {

    $.ajax(
        { 
            url: "http://bitsmackgtapi.apphb.com/pedometer/summary",
            success:    function (returndata) {
                $("#pedometersummary").html(returndata.NewStepGoal);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $("#pedometersummary").html(textStatus + "" + errorThrown);
            }
        });
}
