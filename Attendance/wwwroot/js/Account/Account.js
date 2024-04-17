$(document).ready(function () {



});

$('#btnLogin').click(function () {


    var data = $("#loginfrm").serialize();

    $.ajax({
        type: "POST",
        url: '../Account/Login',
        data: data,
        success: function (result) {
            if (result == "Fail") {
                window.location.href = '../Account/Login';
            }
            else
                window.location.href = '../Home/Index';
        },
        error: function () {
            alert("something wrong");
        }

    });
});

//function GetUserLocation() {

//    if (navigator.geolocation) {
//        1
//        alert('your browser support geolocation')
//         toIsoStringInLocalTime();
//        navigator.geolocation.getCurrentPosition(mycoords)
//    } else {
//        alert('your browser does not support geolocation')
//    }

//}

//function mycoords(position) {
//    var lat = position.coords.latitude;
//    var longt = position.coords.longitude;
//    console.log(lat, longt);
//    //2
//    alert("Latitude:" + lat + "" + "Longitude:" + longt);
//    const timezone = Intl.DateTimeFormat().resolvedOptions().timeZone;

//    //3
//    alert(timezone);


//    let s = new Date().toLocaleString();

//    //4
//    alert(s);
//    debugger;

//    var d = new Date();
//    var tz = d.toString().split("GMT")[1].split(" (")[0]; // timezone, i.e. -0700

//    //5
//    alert(tz);
//    debugger;

//    var d = new Date();
//    var tz = d.toString().split("GMT")[1]; // timezone, i.e. -0700 (Pacific Daylight Time)

//    //6
//    alert(tz);
//    debugger;
//    var d = new Date();
//    var tz = d.getTimezoneOffset(); // timezone, i.e. -0700 (Pacific Daylight Time)

//    var theAdd = new Date(d);
//    var finalDate = theAdd.setMinutes(theAdd.getMinutes() + tz);

//    //var d1 = new Date();
//    //d1.toUTCString() + MimeType(tz);


//    //7
//    alert(finalDate);
//    debugger;

//    var gmtRe = /GMT([\-\+]?\d{4})/; // Look for GMT, + or - (optionally), and 4 characters of digits (\d)
//    var d = new Date().toString();
//    var tz = gmtRe.exec(d)[1]; // timezone, i.e. -0700

//    //8
//    alert(tz);
//    debugger;
//    var tzRe = /\(([\w\s]+)\)/; // Look for "(", any words (\w) or spaces (\s), and ")"
//    var d = new Date().toString();
//    var tz = tzRe.exec(d)[1]; // timezone, i.e. "Pacific Daylight Time"

//    //9
//    alert(tz);
//    debugger;

//    var offset = new Date().getTimezoneOffset();

//    //10
//    alert(offset);
//    debugger;
//    console.log(new Date().toTimeString().slice(9));
//    console.log(Intl.DateTimeFormat().resolvedOptions().timeZone);
//    console.log(new Date().getTimezoneOffset() / -60);

//    currentTime = new Date();
//    time = currentTime.getTime();
//    hours = currentTime.getHours();
//    //11
//    alert(currentTime, time, hours);
//    debugger;
//    // console.log(offset);
//    // if offset equals -60 then the time zone offset is UTC+01

//    //console.log(s);
//}


//function toIsoStringInLocalTime(date) {
//    return new Date((date.getTime() + (-date.getTimezoneOffset() * 60000))).toISOString()
//}