function startTime() {
    var mydate = new Date();
    var year = mydate.getYear();

    if (year < 1000)
        year += 1900

    var day = mydate.getDay(); // Current Day of week - 2
    var month = mydate.getMonth(); // Current Month 2
    var daym = mydate.getDate(); // Current Date -24
    var h = mydate.getHours(); //Hours
    var apm = "AM";
    if (h >= 12) {
        h = h - 12;
        apm = "PM";
    }
    else {
        apm = "AM";
    }
    var m = mydate.getMinutes();//Minutes
    var s = mydate.getSeconds();//Seconds

    h = checkTime(h);
    m = checkTime(m);
    s = checkTime(s);

    function checkTime(i) {
        if (i < 10) { i = "0" + i };  // add zero in front of numbers < 10
        return i;
    }

    var dayarray = new Array("Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat")
    var montharray = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec")

    $("#idDateTimeShow div.time").html(h + ":" + m);
    $("#idDateTimeShow div.apm").html(apm);
    $("#idDateTimeShow div.seconds").html(s);
    $("#idDateTimeShow div.day").html(montharray[month] + " " + daym + ", " + year + " (" + dayarray[day] + ")");

    var t = setTimeout(function () { startTime() }, 1000);
}