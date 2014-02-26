function MinutesToHours(totalminutes) {
    var hours = Math.floor(totalminutes / 60);
    var minutes = Math.round(totalminutes % 60);
    if (minutes < 10) {
        minutes = "0" + "" + minutes;
    }
    return hours + ":" + minutes;
}


function SecondsToTime(totalseconds) {
    var hours = Math.floor(totalseconds / 3600);
    totalseconds %= 3600;
    var minutes = Math.floor(totalseconds / 60);
    if (minutes < 10) {
        minutes = "0" + "" + minutes;
    }
    var seconds = Math.round(totalseconds % 60);
    if (seconds < 10) {
        seconds = "0" + "" + seconds;
    }
    if (hours > 0) {
        return hours + ":" + minutes + ":" + seconds;
    }
    return minutes + ":" + seconds;
}

function GoalPaceAtDiffDistance(distance, fivekgoalseconds) {
    var newtime = fivekgoalseconds / (Math.pow(3.1 / distance, 1.06));
    return newtime;
}

