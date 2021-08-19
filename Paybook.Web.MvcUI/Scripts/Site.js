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


var sSiteUrlMain = location.protocol + "//" + location.hostname + (location.port && ":" + location.port) + "/";
var siteUrl = (sSiteUrlMain.indexOf("localhost") >= 0) ? sSiteUrlMain + "" : sSiteUrlMain;
function CallAjaxMethod(ajaxMethod, jsonVar, callback) {
    try {
        var sUrl = siteUrl + "" + ajaxMethod + jsonVar;
        console.log(sUrl);
        //        alert(sJsonVar);
        $.ajax({
            type: "GET",
            url: sUrl,
            //data: JSON.stringify(sJsonVar),
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("There is an issue processing your request in function " + ajaxMethod + ". Details: Request Status:" + XMLHttpRequest.status + "," + "Status: " + textStatus + "Error: " + errorThrown);
                //window[sMethodToExecute]("");
            },
            success: function (data) {
                try {
                    //window[callback](data);
                    callback(data);
                }
                catch (err) {
                    alert("There is an issue executing the function " + ajaxMethod + ", Reason: " + err);
                    //window[sMethodToExecute]("");
                }
            }
        });

    }
    catch (err) {
        alert("There is an issue calling the function " + ajaxMethod + ", Reason: " + err);
        //window[sMethodToExecute]("");
    }
}

function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    } else {
        var expires = "";
    }
    document.cookie = name + "=" + value + expires + "; path=/";
}

function readCookie(name) {
    var nameEQ = name + "=";
    //console.log("document.cookie: " + document.cookie.);
    //console.log("CookieName: " + nameEQ);
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1, c.length);
        }
        //console.log("CookieC: " + c);
        if (c.indexOf(nameEQ) == 0) {
            //console.log("CookieValue: " + c.substring(nameEQ.length, c.length));
            return c.substring(nameEQ.length, c.length);
        }
    }
    return null;
}

function eraseCookie(name) {
    createCookie(name, "", -1);
}