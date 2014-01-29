function MinutesToHours(totalminutes) {
    var hours = Math.floor(totalminutes / 60);
    var minutes = totalminutes % 60;
    return hours + ":" + minutes;
}