
$(document).ready(function () {
    // var d = new Date();
    // var n = d.getTimezoneOffset();
    // var ans = new Date(d.getTime() + n *60 *1000);

    //alert(ans.getHours() + ':' + ans.getMinutes() + ':' + ans.getSeconds())
    latlong();
    getLocation();
    SearchVendor();
    SearchEmployee();
    GetEmployeeVendorList();
    checkRadioVendor();
    setTimeout(function () {
        $("#ddlemployee").attr('disabled', true);
        $("#tblemployeediv").hide();
    }, 1000);
    GetVendorTeamAttendanceList();
    GetInOutTime();
    distanceTo();


});

function checkRadioVendor() {

    var $radios = $("input:radio[name=vendor]");
    if ($radios.is(':checked') === false) {
        $radios.filter('[value=supplier]').prop('checked', true);

        $("#ddlemployee").attr('disabled', true);
    }
};

$(function () {
    debugger
    $('input[name="vendor"]').on('click', function () {
        debugger

        if ($(this).val() == 'supplier') {
            debugger
            $("#btnTeamOUTMARK").prop('disabled', false);
            $("#Vendorname").prop("disabled", false);
            $("#VendorID").prop("disabled", false);
            $("#ddlemployee").prop("disabled", true);
            $("#ddlemployee").find('option:first').prop('selected', 'selected');
            // $('#ddlemployee :selected').empty('');
            $("#ddlemployee").removeClass('border-danger');
            $("#lblTeamintime").html('');
            $("#lblTeamouttime").html('');
            // $('#tblvendorth').show();
            // $('#tblvendor').empty();
            $("#tblemployeediv").hide();
            $("#tblvendordiv").show();
            GetVendorTeamAttendanceList();
        }
        else if ($(this).val() == 'employee') {
            debugger
            $("#btnTeamOUTMARK").prop('disabled', false);
            $("#ddlemployee").prop("disabled", false);
            $("#Vendorname").prop("disabled", true);
            $("#VendorID").prop("disabled", true);
            $("#Vendorname").val('');
            $("#VendorID").val('');
            $("#lblTeamintime").html('');
            $("#lblTeamouttime").html('');
            $("#VendorID").removeClass('border-danger');
            $("#Vendorname").removeClass('border-danger');
            //$('#tblvendor').empty();
            // $('#tblvendorth').hide();
            $("#tblvendordiv").hide();
            $("#tblemployeediv").show();
            GetEmployeeTeamAttendanceList();

        }
    });
});

function distanceTo(lat2, lon2) {
    debugger
    latlong(function (lat1, lon1) {
        debugger
        var lat1 = lat1;
        var lon1 = lon1;
    });
    var unit;
    var lat1 = $("#txtlat").val();
    var lon1 = $("#txtlang").val();
    var rlat1 = Math.PI * lat1 / 180
    var rlat2 = Math.PI * lat2 / 180
    var rlon1 = Math.PI * lon1 / 180
    var rlon2 = Math.PI * lon2 / 180
    var theta = lon1 - lon2
    var rtheta = Math.PI * theta / 180
    var dist = Math.sin(rlat1) * Math.sin(rlat2) + Math.cos(rlat1) * Math.cos(rlat2) * Math.cos(rtheta);
    dist = Math.acos(dist)
    dist = dist * 180 / Math.PI
    dist = dist * 60 * 1.1515
    if (unit == "K") { dist = dist * 1.609344 }
    if (unit == "N") { dist = dist * 0.8684 }
    $("#hdlocation").val(dist);
    $("#hdlocation1").val(dist);
    return dist
}
function latlong(callback) {
    debugger
    var lat = '';
    var lng = '';


    $.ajax({
        type: "GET",
        url: "/Attendance/Getlatlong",
        data: "",
        success: function (data) {
            debugger;
            lat = data.result.lat;
            lng = data.result.long;
            corporateAddress = data.result.corporateAddress;
            $("#txtlat").val(lat);
            $("#txtlang").val(lng);
            $("#txtlat1").val(lat);
            $("#txtlang1").val(lng);
            $("#ulocation").val(corporateAddress);
            $("#ulocation1").val(corporateAddress);
            $("#lat").val(lat);
            $("#lng").val(lng);

            var latlng = new google.maps.LatLng(lat, lng);
            debugger
            var geocoder = geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'latLng': latlng }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    if (results[8]) {
                        document.getElementById("ulocation").innerHTML = results[8].formatted_address;
                        document.getElementById("ulocation1").innerHTML = results[8].formatted_address;

                    }
                }
            });
            if (typeof callback === "function") {
                callback(lat, lng);
            }

        },

    });
}
function getLocation() {
    debugger
    var options = {
        enableHighAccuracy: true,
        timeout: 200000,
        maximumAge: 2000
    };

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition, posiotionError, options);
    } else {

        setTimeout(function () {
            debugger
            posiotionError();
            document.getElementById("Label1").value = "location is not supported by this browser,please open in chrome!!";

        }, 3000);

    }
}

function showPosition(position) {
    debugger
    var lat2 = position.coords.latitude
    var long2 = position.coords.longitude;

    var lat = position.coords.latitude;
    var lng = position.coords.longitude;

    //$("#txtlat").val(lat);
    //$("#txtlang").val(lng);
    //$("#txtlat1").val(lat);
    //$("#txtlang1").val(lng);
    //$("#lat").val(lat);
    //$("#lng").val(lng);
    distanceTo(lat, lng);

    //var latlng = new google.maps.LatLng(lat, lng);

    //var geocoder = geocoder = new google.maps.Geocoder();
    //geocoder.geocode({ 'latLng': latlng }, function (results, status) {
    //    if (status == google.maps.GeocoderStatus.OK) {
    //        if (results[8]) {
    //            document.getElementById("ulocation").innerHTML = results[8].formatted_address;
    //            document.getElementById("ulocation1").innerHTML = results[8].formatted_address;

    //        }
    //    }
    //});
}

function backTologin() {
    debugger
    window.location.href = "../Account/Login"
}

function posiotionError(position) {

    switch (position.code) {
        case 0:
            backTologin();
            break;
        case 1:
            backTologin();
            break;
        case 2:
            backTologin();
            break;
        case 3:
            backTologin();
            break;
    }
}

$('#btnAttInMark').click(function () {
    debugger
    var onduty = 0;
    if ($("#onDuty").prop('checked') == true) {
        onduty = $('#onDuty').val();
    }
    debugger
    var temintime = $("#lblintime").html();

    debugger
    var currentdate = new Date();
    var datetime = "" + (currentdate.getMonth() + 1) + "/"
        + currentdate.getDate() + "/"
        + currentdate.getFullYear() + " "
        + currentdate.getHours() + ":"
        + currentdate.getMinutes() + ":"
        + currentdate.getSeconds();

    var Rindistance = $('#hdlocation').attr("value");
    var lat = $("#lat").val();
    var lng = $("#lng").val();

    var data = {
        INTIMETZ: datetime,
        INLAT: lat,
        INLOG: lng,
        RADIALINDISTANCE: Rindistance,
        ONDUTY: onduty,
    }
    $.ajax({
        type: 'POST',
        url: '../Attendance/InTimeMark',
        data: data,
        success: function (result) {
            debugger
            if (result == "Success") {
                // $("#lblintime").val('Marked');
                setTimeout(function () {
                    GetInOutTime();
                }, 2000);
                $('#txt').html("Attendance Marked successfully");
                $('#successModal').modal('show');
            }
            else if (result == "NotExist") {
                $('#txtwar').html('Please fill infra details before marking attendance ');
                $('#warningModal').modal('show');
                return false;
            }
            else if (result == "Punchintimeexist") {
                $('#txtwar').html('In-time Already Marked');
                $('#warningModal').modal('show');
                return false;
            }
            else if (result == "AlreadyExist") {
                $('#txtwar').html('Plaese Fill Previous Day Infra Before Making the Attendance');
                $('#warningModal').modal('show');
                return false;
            }
            else {

                //  $("#spanSuccess").addClass('text-success').html(result);
                $('#txterror').html("Some Error");
                $('#dangerModal').modal('show');
                //setTimeout(function () {
                //    $("#spanSuccess").html('');
                //}, 3000);
            }
        },
        error: function (result) {
            // $("#spanError").addClass('text-danger').html(result);
            $('#txtwar').html(result);
            $('#warningModal').modal('show');
        }

    });

});

function GetInOutTime() {
    debugger
    var data = {

        loginid: $("#empid").val(),
        ardate: $("#empid").val(),
    };
    $.ajax({
        type: "GET",
        url: "/Attendance/GetInOutTime",
        data: data,
        success: function (data) {
            debugger;
            var msg = "Marked";
            var msgg2 = "Not Marked";

            $("#hdintime").val(data.result.id);

            if (data.result.intimetz != null) {
                debugger
                $("#lblintime").val(msg);
                // $("#lblintime").html(data.result.intimetz);
            }
            else {
                $("#lblintime").val(msgg2);
            }
            if (data.result.outtimetz != null) {
                $("#lblouttime").val(msg);
            }
            else {
                $("#lblouttime").val(msgg2);
            }
        },
        error: function () {
            $("#spanError").addClass('text-danger').html('something went wrong..')
            setTimeout(function () {
                $("#spanError").html('');
            }, 3000);
        }
    });
}

var teamid = '';
function GetTeamInOutTime() {
    debugger
    var empid = $('#ddlemployee :selected').val();
    var data = {
        TeamEmpid: empid,
    };
    $.ajax({
        type: 'GET',
        url: "../Attendance/GetTeamInOutTime",
        data: data,
        success: function (result) {
            debugger
            if (result.length == 0) {
                $("#lblTeamintime").html("");
                $("#lblTeamouttime").html("");
            } else {
                teamid = result[0].id;

                if (result.length > 0) {

                    $("#lblTeamintime").html(result[0].INTIME);
                    $("#lblTeamouttime").html(result[0].OUTTIME);
                }
                else {
                    $("#lblTeamintime").html("");
                    $("#lblTeamouttime").html("");
                }
            }

        },
        error: function () {
            $("#spanError").addClass('text-danger').html(result);
        }
    });
}


//$("#ddlemployee").on('change', function () {


//});

$("#btnAttOutMark").click(function () {
    debugger
    var temintime = $("#lblintime").html();
    var temouttime = $("#lblouttime").html();
    var intimeid = $('#hdintime').val();

    /*if (temintime !== "") {*/
    /*if (temouttime == "" || temouttime == null) {*/
    debugger
    var dt = new Date();
    var time = dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();

    var currentdate = new Date();
    var datetime = "" + (currentdate.getMonth() + 1) + "/"
        + currentdate.getDate() + "/"
        + currentdate.getFullYear() + " "
        + currentdate.getHours() + ":"
        + currentdate.getMinutes() + ":"
        + currentdate.getSeconds();

    var Routdistnace = $('#hdlocation').attr("value");
    var lat = $("#lat").val();
    var lng = $("#lng").val();

    var data = {

        ID: intimeid,
        OUTTIMETZ: datetime.toString(),
        OUTLAT: lat,
        OUTLOG: lng,
        RADIALOUTDISTANCE: Routdistnace,
    }
    $.ajax({
        type: "POST",
        url: "../Attendance/OutTimeMark",
        data: data,

        success: function (result) {
            debugger
            //setTimeout(function () {
            //    GetInOutTime();
            //}, 2000);
            if (result == "Success") {
                setTimeout(function () {
                    GetInOutTime();
                }, 2000);
                $("#spanSuccess").addClass('text-success').html();
                $('#txt').html("Attendance Marked successfully");
                $('#successModal').modal('show');
            }
            else if (result == "NotExist") {
                // $("#spanSuccess").addClass('text-danger').html('Please fill infra details before marking attendance ');
                $('#txtwar').html('Please fill infra details before marking attendance ');
                $('#warningModal').modal('show');
                return false;
            }

            //else if (result == "In-time is not Marked") {
            //    debugger
            //    // $("#spanSuccess").addClass('text-danger').html('Please fill infra details before marking attendance ');
            //    $('#txtwar').html("In-time is not Marked");
            //    $('#warningModal').modal('show');
            //    return false;
            //}
            else if (result == "AlreadyExist") {
                // $("#spanSuccess").addClass('text-danger').html('Please fill infra details before marking attendance ');
                $('#txtwar').html("Out-time already Marked ");
                $('#warningModal').modal('show');
                return false;
            }

        },
        error: function (result) {
            debugger
            // $("#spanErrorOut").addClass('text-danger').html('something went wrong..');
            $("#txtwar").html('something went wrong..');
            $("#warningModal").modal('show');
        }
    });
});


var vendorid = '';
function SearchVendor() {
    $("#VendorID").autocomplete({
        my: "left top",
        at: "left bottom",

        source: function (request, response) {
            $.ajax({
                url: "/Attendance/GetVendorList",
                type: "POST",
                dataType: "json",
                data: { SearchValue: request.term },
                success: function (data) {
                    debugger
                    response($.map(data, function (item) {
                        debugger
                        return {
                            label: item[0].supplier,
                            value: item[0].supplier,
                            VendorID: item[0].supplierId
                        };
                        //setTimeout(function () {
                        //    GetVendorTeamAttendanceList();
                        //}, 2000);

                    }))
                }
            })
        },
        //messages: {
        //    noResults: '',
        //    results: ''
        //},
        select: function (event, ui) {
            debugger
            $("#hiddenID").val(ui.item.VendorID);
            $("#VendorID").val(ui.item.value);
            vendorid = ui.item.vendorid;
            setTimeout(function () {
                // GetVendorTeamAttendanceList();
                $('#lblTeamintime').html('');
                $('#lblTeamouttime').html('');
                $('#Vendorname').val('');
                $("#Vendorname").attr("disabled", false);
            }, 1000);

        },
        change: function (event, ui) {

            if ((ui.item) == null) {
                event.target.value = "";

                $('#txtwar').html('Option must be selected from the list only.');
                $('#warningModal').modal('show');
            }
        }
    });
}

var empid = '';
function SearchEmployee() {
    debugger
    $("#ddlemployee").autocomplete({
        //my: "left top",
        //at: "left bottom",

        source: function (request, response) {
            debugger
            $.ajax({
                url: "/Attendance/GetEmployeeVendorList",
                type: "POST",
                dataType: "json",
                data: { SearchValue: request.term },
                success: function (data) {
                    debugger
                    response($.map(data, function (item) {
                        debugger
                        return {
                            label: item[0].EmployeeName,
                            value: item[0].emp_Full_Name,
                            empid: item[0].emP_ID
                        };


                    }))
                }
            })
        },
        messages: {
            noResults: '',
            results: ''
        },
        select: function (event, ui) {
            debugger
            $("#hiddenemp").val(ui.item.empid);
            $("#ddlemployee").val(ui.item.value);
            empid = ui.item.empid;

            setTimeout(function () {
                // GetVendorTeamAttendanceList();
                $('#lblTeamintime').html('');
                $('#lblTeamouttime').html('');

            }, 1000);

        },
        change: function (event, ui) {

            if ((ui.item) == null) {
                event.target.value = "";

                $('#txtwar').html('Option must be selected from the list only.');
                $('#warningModal').modal('show');
            }
        }
    });
}

function GetEmployeeVendorList() {
    debugger
    $("#ddlemployee").empty();
    $("#ddlemployee").append($("<option></option>").val('').html('Select Employee'));

    $.ajax({
        type: "GET",
        url: "../Attendance/GetEmployeeVendorList",
        contentType: "application/json; charset=utf-8",
        context: document.body,
        //data: { lstEmployee },
        success: function (data) {
            debugger
            if (data != '') {
                $.each(data.lstEmployee, function (item, value) {
                    debugger
                    $("#ddlemployee").append($("<option></option>").val(value.emP_ID).html(value.employeeName));
                    // $("#Holydaydesc").append($("<option></option>").val(value.encryptid).html(value.holidaY_DESC));
                });
                $("#ddlemployee").prop("disabled", false);

            }
        },
        error: function (error) {
            alert(error);
        },

        //messages: {
        //    noResults: '',
        //    results: function (resultsCount) { }
        //}
    });
}


$("#btnTeamINMARK").click(function () {
    debugger
    var onduty = 0;
    var isValidteam = 0;
    if ($("#onduty").prop('checked') == true) {
        onduty = $("#onDuty").val();
    }
    if ($("input[type='radio'].radioType").is(':checked')) {
        debugger
        var emp_typeVal = $("input[type='radio'].radioType:checked").val();

        if (emp_typeVal == 'supplier') {

            var emptype = $("#vendor").val();
            var VendorID = $("#hiddenID").val();
            var vendor = $("#VendorID").val();
        }
        else if (emp_typeVal == 'employee') {

            //emptype = $('#ddlemployee').val();
            var empid = $("#hiddenemp").val();
            var empname = $("#ddlemployee").val();
            var emp2 = $("#ddlemployee").val();
            vendorid = "";
        }
    }
    var temintime = $("#lblTeamintime").html();
    if (temintime == null || temintime == "") {
        debugger
        var dt = new Date();
        var time = dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();
        var currentdate = new Date();
        var datetime = "" + (currentdate.getMonth() + 1) + "/"
            + currentdate.getDate() + "/"
            + currentdate.getFullYear() + " "
            + currentdate.getHours() + ":"
            + currentdate.getMinutes() + ":"
            + currentdate.getSeconds();

        /* var empid = $("#hiddenemp").val();*/

        var Rindistance1 = $('#hdlocation').attr("value");
        var Rdistance = $('#hdlocation1').val();
        var Rindistance = $('#hdlocation1').attr("value");
        var vendorname = $("#Vendorname").val();



        var lat = $("#lat").val();
        var lng = $("#lng").val();
        var Onduty = $('#onDuty').val();


        var isDisVenName = $('#Vendorname').prop('disabled');
        var isDisDDLemp = $('#ddlemployee').is(':disabled');
        var isDisVenId = $('#VendorID').is(':disabled');

        if (empname == "" && isDisDDLemp == (false)) {
            $('#ddlemployee').addClass('border-danger');
            $('#VendorID').removeClass('border-danger');
            $('#Vendorname').removeClass('border-danger');

            isValidteam = 1;
        } else if (empname == "" && isDisDDLemp == (true) && isDisVenId == (true)) {
            $('#ddlemployee').addClass('border-danger');
            $('#VendorID').removeClass('border-danger');
            $('#Vendorname').removeClass('border-danger');
            isValidteam = 1;
        }
        else if (empname == '' && isDisVenId == (true)) {
            $('#ddlemployee').addClass('border-danger');
            $('#VendorID').removeClass('border-danger');
            $('#Vendorname').removeClass('border-danger');
            isValidteam = 1;
        }
        else {
            $('#ddlemployee').removeClass('border-danger');
            $('#VendorID').removeClass('border-danger');
            $('#Vendorname').removeClass('border-danger');
        }
        if (VendorID == '' && emp_typeVal == 'employee') {
            $('#VendorID').removeClass('border-danger');

        } else if (VendorID == '' && emp_typeVal == 'supplier') {
            $('#VendorID').addClass('border-danger');
            isValidteam = 1;
        }
        else {
            $('#VendorID').removeClass('border-danger');
            $('#ddlemployee').removeClass('border-danger');

        }

        if (vendorname == '' && emp_typeVal == 'employee') {
            $('#Vendorname').removeClass('border-danger');

        } else if (vendorname == '' && emp_typeVal == 'supplier') {
            $('#ddlemployee').removeClass('border-danger');
            $('#Vendorname').addClass('border-danger');
            isValidteam = 1;
        }
        else {
            $('#Vendorname').removeClass('border-danger');
        }


        if (isValidteam == 1) {
            $("#spanErrorteamIN").html('Please Fill All fields...');
            setTimeout(function () {
                $("#spanErrorteamIN").html('');
            }, 3000);

        } else {
            /*vendorid = $("#VendorID").val();*/
            var data = {

                INTIME: datetime,
                INLAT: lat,
                INLOG: lng,
                ONDUTY: Onduty,
                RADIALINDISTANCE: Rdistance,
                EMPID: empid,
                STATUS: onduty,
                EMPTYPE: emp_typeVal,
                SUPPLIERID: VendorID,
                EMPNAME: vendorname,

            }

            $.ajax({
                type: 'POST',
                url: '../Attendance/TeamInTimeMark',
                data: data,
                /*contentType: 'application/json',*/
                success: function (result) {
                    debugger
                    if (result == "Success") {
                        setTimeout(function () {
                            GetInOutTime();
                        }, 2000);
                        $("#spanSuccess").addClass('text-success').html();
                        $('#txt').html("Attendance Marked successfully");
                        $('#successModal').modal('show');
                    }
                    else if (result == "NotExist") {
                        // $("#spanSuccess").addClass('text-danger').html('Please fill infra details before marking attendance ');
                        $('#txtwar').html('Please fill infra details before marking attendance ');
                        $('#warningModal').modal('show');
                        return false;
                    }
                    else if (result == "Punchintimeexist") {
                        //$("#spanSuccess").addClass('text-danger').html(result);
                        $('#txtwar').html('In-time Already Marked');
                        $('#warningModal').modal('show');
                        return false;
                    }
                    else {

                        //  $("#spanSuccess").addClass('text-success').html(result);
                        $('#txterror').html("Some Error");
                        $('#dangerModal').modal('show');
                        //setTimeout(function () {
                        //    $("#spanSuccess").html('');
                        //}, 3000);
                    }
                },
                error: function (result) {
                    // $("#spanError").addClass('text-danger').html(result);
                    $('#txtwar').html(result);
                    $('#warningModal').modal('show');
                }
            });

        }
    }

});

$('#succ').click(function () {
    debugger;
    GetTeamInOutTime();
    GetVendorTeamAttendanceList();
    GetEmployeeTeamAttendanceList();
    $("#spanSuccessteamIN").html('');
    $("#Vendorname").val('');

});

$("#btnTeamOUTMARK").click(function () {
    debugger
    if ($("input[type='radio'].radioType").is(':checked')) {

        var emp_typeVal = $("input[type='radio'].radioType:checked").val();
    }

    if (emp_typeVal == 'supplier') {
        var emptype = $("#vendor").val();
        var VendorID = $("#hiddenID").val();
        var id = $("#hiddenIDaaa").val();

        teamid = $("#hdteamOuttimeId").val();
        var temouttime = $("#lblTeamouttime").html();
        var temintime = $("#lblTeamintime").html();

        if (temintime != "") {

            if (temouttime == null || temouttime == "") {

                var currentdate = new Date();
                var datetime = "" + (currentdate.getMonth() + 1) + "/"
                    + currentdate.getDate() + "/"
                    + currentdate.getFullYear() + " "
                    + currentdate.getHours() + ":"
                    + currentdate.getMinutes() + ":"
                    + currentdate.getSeconds();
                /*alert(datetime);*/
                var empid = $("#ddlemployee :selected").val();
                var Routdistace = $('#hdlocation1').attr("value");
                var lat = $("#lat").val();
                var lng = $("#lng").val();
                var Onduty = $("#onDuty").val();

                var data = {
                    OUTTIME: datetime.toString(),
                    OUTLAT: lat,
                    OUTLOG: lng,
                    ONDUTY: Onduty,
                    EMPID: empid,
                    RADIALOUTDISTANCE: Routdistace,
                    ID: teamid,
                };
                $.ajax({
                    type: "POST",
                    url: "../Attendance/TeamOutTimeMark",
                    data: data,

                    success: function (result) {

                        if (result == "Success") {
                            setTimeout(function () {
                                GetInOutTime();
                            }, 2000);
                            // $("#spanSuccess").addClass('text-success').html('Attendance Marked successfully');
                            $('#txt').html("Attendance Marked successfully");
                            $('#successModal').modal('show');
                        }
                        else if (result == "NotExist") {
                            // $("#spanSuccess").addClass('text-danger').html('Please fill infra details before marking attendance ');
                            $('#txtwar').html('Please fill infra details before marking attendance ');
                            $('#warningModal').modal('show');
                            return false;
                        }

                        else if (result == "In-time is not Marked") {
                            debugger
                            // $("#spanSuccess").addClass('text-danger').html('Please fill infra details before marking attendance ');
                            $('#txtwar').html("In-time is not Marked");
                            $('#warningModal').modal('show');
                            return false;
                        }
                        else if (result == "Out-time already Marked") {
                            // $("#spanSuccess").addClass('text-danger').html('Please fill infra details before marking attendance ');
                            $('#txtwar').html("Out-time already Marked ");
                            $('#warningModal').modal('show');
                            return false;
                        }
                    },
                    error: function () {
                        // $("#spanErrorOUT").addClass('text-danger').html('Something Went wrong..');
                        $('#txtwar').html('Something Went wrong');
                        $('#warningModal').modal('show');
                    }
                });
            }

        }



    }

    else if (emp_typeVal == 'employee') {

        //emptype = $('#ddlemployee').val();
        var empid = $("#hiddenemp").val();
        var empname = $('#ddlemployee :selected').val();
        var emp2 = $("#ddlemployee").val();
        teamid = $("#hdteamOuttimeId").val();
        vendorid = "";

        var temouttime = $("#lblTeamouttime").html();
        var temintime = $("#lblTeamintime").html();

        if (temintime != "") {

            if (temouttime == null || temouttime == "") {

                var currentdate = new Date();
                var datetime = "" + (currentdate.getMonth() + 1) + "/"
                    + currentdate.getDate() + "/"
                    + currentdate.getFullYear() + " "
                    + currentdate.getHours() + ":"
                    + currentdate.getMinutes() + ":"
                    + currentdate.getSeconds();

                var empid = $('#ddlemployee :selected').val();
                var Routdistace = $('#hdlocation').attr("value");
                var Rindistance = $('#hdlocation1').attr("value");
                var lat = $("#lat").val();
                var lng = $("#lng").val();
                var Onduty = $('#onDuty').val();

                var data = {
                    OUTTIME: datetime.toString(),
                    OUTLAT: lat,
                    OUTLOG: lng,
                    ONDUTY: Onduty,
                    EMPID: empid,
                    RADIALOUTDISTANCE: Routdistace,
                    ID: teamid,
                };
                $.ajax({
                    type: 'POST',
                    url: '../Attendance/TeamOutTimeMark',
                    data: data,

                    success: function (result) {

                        if (result == "success") {
                            setTimeout(function () {
                                GetTeamInOutTime();
                                GetVendorTeamAttendanceList();
                                GetEmployeeTeamAttendanceList();
                                $('#Vendorname').val('');
                            }, 3000);
                            //   $("#spanSuccessOUT").addClass('text-success').html('Attendance Marked successfully');
                            $('#txt').html("Attendance Marked successfully");
                            $('#successModal').modal('show');
                        }
                        else {
                            $('#txtwar').html(result);
                            $('#warningModal').modal('show');
                        }
                    },
                    error: function () {
                        $('#txtwar').html('Something Went wrong');
                        $('#warningModal').modal('show');
                    }
                });
            } else {
                //  $("#spanErrorOUT").addClass('text-danger').html('Attendance already Marked ');


                $('#txtwar').html('Attendance already Marked ');
                $('#warningModal').modal('show');
            }
        } else {

            //  $("#spanErrorOUT").addClass('text-danger').html('first mark intime ');
            $('#txtwar').html('first mark intime..');
            $('#warningModal').modal('show');
        }
        /*}*/
    }
});


function GetVendorTeamAttendanceList() {

    debugger;
    var data = {

        "VendorID": vendorid,
    };
    $.ajax({
        url: "../Attendance/GetVendorTeamAttendance",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        data: "{}",
        datatype: "json",
        success: function (response) {
            // $('#tblemployeediv').hide();
            // $('#tblvendordiv').show();
            var tr = '';
            $("#tblvendor").empty();
            if (response != null) {
                debugger
                if (response.length > 0) {
                    var scount = 0;
                    for (var i = 0; i < response.length; i++) {
                        var editattached = "'";
                        scount = scount + 1;
                        //var dateappend =  response[i].INTIME ;
                        tr = $('<tr id="++"/>');
                        tr += "<tr>";
                        tr += "<td>" + scount + "</td>";
                        tr += "<td hidden id=id" + scount + ">" + response[i].id + "</td>";
                        /* tr.append("<td id=VendorID" + scount +">" + response[i].VendorID + "</td>");*/
                        tr += "<td id=Vendor" + scount + ">" + response[i].supplier + "</td>";
                        tr += "<td id=EMPNAME" + scount + ">" + response[i].empname + "</td>";
                        if (response[i].intime) {
                            tr += "<td><input type='hidden' id='hiddenINTIME" + scount + "' value='" + response[i].intime + "'><span id='INTIME" + scount + "'>Marked</span></td>";
                        }
                        else {
                            tr += "<td><input type='hidden' id='hiddenINTIME" + scount + "' value=''>Not Marked<span id='INTIME" + scount + "'></span></td>";
                        }
                        if (response[i].outtime) {
                            tr += "<td><input type='hidden' id='hiddenOUTTIME" + scount + "' value='" + response[i].outtime + "'><span id='OUTTIME" + scount + "'>Marked</span></td>";
                        }
                        else {
                            tr += "<td><input type='hidden' id='hiddenOUTTIME" + scount + "' value=''> Not Marked<span id='OUTTIME" + scount + "'></span></td>";
                        }
                        //tr += "<td id=INTIME" + scount + ">" + response[i].intime + "</td>";
                        //tr += "<td id=OUTTIME" + scount + ">" + response[i].outtime + "</td>";
                        tr += "<td><button type = 'button' class= 'use-address' id='btndetail' onclick=VendorTeamAttendance(" + scount + ") ><i class='fa fa-clock-o' aria-hidden='true'></button></td>";
                        tr += "</tr>";
                        //tr.append("<td><button type = 'button' class= 'use-address' id='btndetail' onclick=VendorTeamAttendance(" + response[i].ID + "," + editattached + response[i].Vendor + editattached + "," + editattached + response[i].EMPNAME + editattached + ",'04/20/2022 14:28:30') ><i class='fa fa-clock-o' aria-hidden='true'></button></td>");
                        //tr.append(`<td><button type = 'button' class= 'use-address' id='btndetail' onclick=VendorTeamAttendance(` + response[i].ID + `,` + response[i].Vendor + `,` + response[i].EMPNAME + `,` + dateappend +  `) ><i class='fa fa-clock-o' aria-hidden='true'></button></td>`);
                        //var deletebtn = '<a class="btn btn-smm btn-success text-white" onclick="VendorTeamAttendance(' + response[i].ID + ')"> <i class="fa fa-clock-o" aria-hidden="true"></i></a >';
                        //tr.append("<td>" + deletebtn + "</td>");
                        //tr += '</tr>';
                        $('#tblvendor').append(tr);
                    }

                }
            }
        },
        error: function () {
            $("#spanError").addClass('text-danger').html('something went wrong..')
            setTimeout(function () {
                $("#spanError").html('');
            }, 3000);
        }

    });
}
function GetEmployeeTeamAttendanceList() {
    debugger
    //var empid = $('#ddlemployee :selected').val();
    var emp_typeVal = $("input[type='radio'].radioType:checked").val();

    var data = {

        EMPTYPE: emp_typeVal,
    };
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "../Attendance/GetEmployeeTeamAttendance",
        data: data,
        dataType: "json",
        success: function (response) {
            // $('#tblemployeediv').show();
            var tr = '';
            debugger
            $("#tblemployee").empty();
            if (response != null) {
                if (response.length > 0) {

                    var scount = 0;
                    for (var i = 0; i < response.length; i++) {
                        debugger
                        var editattached = "'";
                        scount = scount + 1;

                        //var dateappend =  response[i].INTIME ;
                        tr = $('<tr id="++"/>');
                        tr += "<tr>";
                        tr += "<td>" + scount + "</td>";
                        tr += "<td hidden id=eid" + scount + ">" + response[i].id + "</td>";
                        /* tr.append("<td id=VendorID" + scount +">" + response[i].VendorID + "</td>");*/
                        tr += "<td id=Vendor " + scount + ">" + response[i].empid + "</td>";
                        tr += "<td id=EEMPNAME" + scount + ">" + response[i].empname + "</td>";
                        if (response[i].intime) {
                            tr += "<td><input type='hidden' id='hiddenEINTIME" + scount + "' value='" + response[i].intime + "'><span id='EINTIME" + scount + "'>Marked</span></td>";
                        }
                        else {
                            tr += "<td><input type='hidden' id='hiddenEINTIME" + scount + "' value=''>Not Marked<span id='EINTIME" + scount + "'></span></td>";
                        }
                        if (response[i].outtime) {
                            tr += "<td><input type='hidden' id='hiddenEOUTTIME" + scount + "' value='" + response[i].outtime + "'><span id='EOUTTIME" + scount + "'>Marked</span></td>";
                        }
                        else {
                            tr += "<td><input type='hidden' id='hiddenEOUTTIME" + scount + "' value=''> Not Marked<span id='EOUTTIME" + scount + "'></span></td>";
                        }

                        //tr += "<td id=EINTIME" + scount + ">" + response[i].intime + "</td>";
                        //tr += "<td id=EOUTTIME" + scount + ">" + response[i].outtime + "</td>";
                        tr += "<td><button type = 'button' class= 'use-address' id='btndetails' onclick=EmployeeTeamAttendance(" + scount + ") ><i class='fa fa-clock-o' aria-hidden='true'></button></td>";
                        tr += "</tr>";
                        //tr.append("<td><button type = 'button' class= 'use-address' id='btndetail' onclick=EmployeeTeamAttendance(" + scount + ") ><i class='fa fa-clock-o' aria-hidden='true'></button></td>");
                        //tr += "</tr>";
                        //tr.append("<td><button type = 'button' class= 'use-address' id='btndetail' onclick=VendorTeamAttendance(" + response[i].ID + "," + editattached + response[i].Vendor + editattached + "," + editattached + response[i].EMPNAME + editattached + ",'04/20/2022 14:28:30') ><i class='fa fa-clock-o' aria-hidden='true'></button></td>");
                        //tr.append(`<td><button type = 'button' class= 'use-address' id='btndetail' onclick=VendorTeamAttendance(` + response[i].ID + `,` + response[i].Vendor + `,` + response[i].EMPNAME + `,` + dateappend +  `) ><i class='fa fa-clock-o' aria-hidden='true'></button></td>`);
                        //var deletebtn = '<a class="btn btn-smm btn-success text-white" onclick="VendorTeamAttendance(' + response[i].ID + ')"> <i class="fa fa-clock-o" aria-hidden="true"></i></a >';
                        //tr.append("<td>" + deletebtn + "</td>");
                        $('#tblemployee').append(tr);
                    }

                }
            }

        },
        error: function () {
            $("#spanError").addClass('text-danger').html('something went wrong..')
            setTimeout(function () {
                $("#spanError").html('');
            }, 3000);
        }

    });
}

function VendorTeamAttendance(ii) {
    debugger
    $("#btnTeamOUTMARK").prop('disabled', false);
    var id = $("#id" + ii).html();
    $('#hdteamOuttimeId').val(id);
    $('#VendorID').val($("#Vendor" + ii).html());
    $('#Vendorname').val($("#EMPNAME" + ii).html());
    $("#Vendorname").attr("disabled", "disabled");
    $('#lblTeamintime').html($("#INTIME" + ii).html());
    $('#lblTeamouttime').html($("#OUTTIME" + ii).html());
    //console.log($("#OUTTIME" + ii).html());
    //console.log($("#id" + ii).html());

}

function EmployeeTeamAttendance(ii) {
    debugger
    $("#btnTeamOUTMARK").prop('disabled', false);
    var id = $("#eid" + ii).html();
    $('#hdteamOuttimeId').val(id);
    // $('#VendorID').val($("#Vendor" + ii).html());
    $('#ddlemployee').val($("#EEMPNAME" + ii).html());
    $("#ddlemployee").attr("disabled", "disabled");
    $('#lblTeamintime').html($("#EINTIME" + ii).html());
    $('#lblTeamouttime').html($("#EOUTTIME" + ii).html());
    //console.log($("#OUTTIME" + ii).html());
    //console.log($("#id" + ii).html());

}


$('#Vendorname').keyup(function () {
    debugger
    var searchText = $(this).val();
    $('#tblvendor tr').each(function () {
        var found = 'false';
        //$(this).find('td:nth(0)').each(function () {     /////// when you want to searching on particuler column.
        $(this).each(function () {
            if ($(this).text().toLowerCase().indexOf(searchText.toLowerCase()) >= 0) {
                found = 'true';
            }
        });
        if (found == 'true') {
            $(this).show();
        }
        else {
            $(this).hide();
        }
    });
});