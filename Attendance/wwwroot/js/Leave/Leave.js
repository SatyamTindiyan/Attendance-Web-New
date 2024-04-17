

$(document).ready(function () {

    GetLeaveApprovalList();
});

var approvallbl = $('#myHiddenVar').val();
var Lcnt = 0;


$('#ckbCheckAll').click(function () {

    // $(".CordinatorA").attr("checked", this.checked);
    $('.CordinatorA').prop('checked', false);
    $('.CordinatorR').prop('checked', false);
    if ($('#ckbCheckAll').is(":checked") == true) {
        $(".CordinatorA").prop("checked", true);
    }

});

$('#ckbCheckAllOne').click(function () {
    $('.ManagerA').prop('checked', false);
    $('.ManagerR').prop('checked', false);
    if ($('#ckbCheckAllOne').is(":checked") == true) {

        $(".ManagerA").prop("checked", true);
    }
});

$('#ckbCheckAllTwo').click(function () {

    // $(".HeadA").attr("checked", this.checked);
    $('.HeadA').prop('checked', false);
    $('.HeadR').prop('checked', false);
    if ($('#ckbCheckAllTwo').is(":checked") == true) {
        $(".HeadA").prop("checked", true);
    }
});


//$("#txtfromdate").datepicker({
//    dateFormat: 'yy-mm-dd',
//    start: '-1m',
//    end: '+1m',
//    autoclose:true,
//    changeMonth: true,
//    changeYear: true,
//    //minDate: dateText
//    timeFormat: 'hh:mm TT'
//});

//$("#txttodate").datepicker({
//    dateFormat: 'yy-mm-dd',
//    changeMonth: true,
//    changeYear: true,
//    //minDate: dateText
//    timeFormat: 'hh:mm TT'
//});
//$("#txtfromdate").datepicker({
//    dateFormat: 'yy-mm-dd',
//    autoclose: true,
//    changeMonth: true,
//    changeYear: true,
//    timeFormat: 'hh:mm TT',
//    beforeShowDay: function (date) {
//        // Get the current date
//        var currentDate = new Date();
//        currentDate.setHours(0, 0, 0, 0); // Set time to midnight for comparison

//        // Get the first day of the current month
//        var firstDayCurrentMonth = new Date(currentDate);
//        firstDayCurrentMonth.setDate(1);

//        // Get the last day of the last month
//        var lastDayLastMonth = new Date(currentDate);
//        lastDayLastMonth.setDate(0);

//        // Get the last day of the current month
//        var lastDayCurrentMonth = new Date(firstDayCurrentMonth);
//        lastDayCurrentMonth.setMonth(lastDayCurrentMonth.getMonth() + 1);
//        lastDayCurrentMonth.setDate(0);

//        // Enable dates only for the current month and the last month and next 10 days of the current month
//        return [date >= lastDayLastMonth && date <= currentDate || date >= firstDayCurrentMonth && date <= lastDayCurrentMonth || (date >= currentDate && date <= new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate() + 10))];
//    },
//    onSelect: function (dateText) {
//        //  selected date
//        var fromDate = new Date(dateText);

//        // Calculate the todate as the next 15 days from the fromdate
//        var toDate = new Date(fromDate);
//        toDate.setDate(toDate.getDate() + 15);

//        // Update the todate options
//        $("#txttodate").datepicker("option", "minDate", fromDate);
//        $("#txttodate").datepicker("option", "maxDate", toDate);

//        // Enable the todate after selecting "fromdate"
//        $("#txttodate").prop("disabled", false);
//    }
//});

//$("#txttodate").datepicker({
//    dateFormat: 'yy-mm-dd',
//    autoclose: true,
//    changeMonth: true,
//    changeYear: true,
//    timeFormat: 'hh:mm TT',
//    beforeShow: function (input, inst) {
//        var fromDate = $("#txtfromdate").datepicker("getDate");
//        if (!fromDate) {
//            return { minDate: 0, maxDate: null };
//        }
//    }
//});

function daysdifference(startDateStr, endDateStr) {
    debugger
    var parts = startDateStr.split('/');
    var parts2 = endDateStr.split('/');

    var formattedDateStr = parts[1] + '/' + parts[0] + '/' + parts[2];
    var formattedDateStr2 = parts2[1] + '/' + parts2[0] + '/' + parts2[2];

    var startDay = new Date(formattedDateStr);
    var endDay = new Date(formattedDateStr2);
        
      
    var millisBetween = startDay.getTime() - endDay.getTime();

    var days = millisBetween / (1000 * 3600 * 24);
    return Math.round(Math.abs(days));
}  


$("#txttodate").change(function () {
    debugger

    var startDateStr = $("#txtfromdate").val();
    var endDateStr = $("#txttodate").val();

    var dayCount = daysdifference(startDateStr, endDateStr);
    
    $("#leavecount").val(dayCount);
});


$("#txtfromdate").datepicker({
    dateFormat: 'dd/mm/yy',
    changeMonth: true,
    changeYear: true,
    //minDate: dateText
    timeFormat: 'hh:mm TT'
});

$("#txttodate").datepicker({
    dateFormat: 'dd/mm/yy',
    changeMonth: true,
    changeYear: true,
    //minDate: dateText
    timeFormat: 'hh:mm TT'
});

$('#ddlleaveName').change(function () {
    debugger;
    var leavecode = $('#ddlleaveName').find(":selected").text();

    //var myArray = leavecode.split("-");
    //var leavetypeCode = myArray[1];


   // GetEmployeeLeaveCount(leavetypeCode);

    const Array = leavecode.split("LEAVE");
    $("#leaveTypeCount").val(Array);
});

function GetEmployeeLeaveCount(leavetypeCode) {
    debugger;
    var data = {
        LEAVE_CODE: leavetypeCode,
    };

    $.ajax({
        type: "GET",
        url: "../Leave/GetEmployeeLeaveCount/",
        //data: { "data": data },
        data: data,
        context: document.body,
        success: function (dataTab) {
            debugger;
            Lcnt = dataTab.data[0].Leavecount;
            $('#leaveTypeCount').val(Lcnt);
        }
    })
}

$('#btnSaveLeave').click(function () {
    debugger
    var leavetypeName = $('#ddlleaveName').find(":selected").val()
    var leavecount = $('#leavecount').val();
    var leavecode = $('#ddlleaveName').find(":selected").text();
    var leaveRemaining = $('#leaveTypeCount').val();
    var myArray = leavecode.split("-");
    var leavetypeCode = myArray[1];

    var fromdate = $('#txtfromdate').val();
    var todate = $('#txttodate').val();
    var reason = $('#ddlreason').val();
    debugger;
    var txtaltmobile = $('#txtaltmobile').val();
    var remarks = $('#txtremarks').val();
    var isError = 0;
    var errortext = "";

    if (leavecode == '') {
        $('#ddlleaveName').addClass('border-danger');
        isError = 1;
    }
    else {
        $('#ddlleaveName').removeClass('border-danger');
    }

    if (reason == '' || reason == 'Select Reason') {
        $('#ddlreason').addClass('border-danger');
        isError = 1;
    }
    else {
        $('#ddlreason').removeClass('border-danger');
    }

    if (leavecount == '') {
        $('#leavecount').addClass('border-danger');
        isError = 1;
    }
    else {
        $('#leavecount').removeClass('border-danger');
    }
    if (fromdate == '') {
        $('#txtfromdate').addClass('border-danger');
        isError = 1;
    }
    else {
        $('#txtfromdate').removeClass('border-danger');
    }
    if (todate == '') {
        $('#txttodate').addClass('border-danger');
        isError = 1;
    }
    else {
        $('#txttodate').removeClass('border-danger');
    }
    if (txtaltmobile == '') {
        $('#txtaltmobile').addClass('border-danger');
        isError = 1;
    }
    else {
        $('#txtaltmobile').removeClass('border-danger');
    }
    //count validate
    if (leavetypeCode == 'SL') {
        if (parseInt(leavecount) > parseInt(leaveRemaining)) {
            errortext = "   max SL apply 10  ";
            $('#lcount').addClass('border-danger');
            isError = 1
        }
    }
    if (leavetypeCode == 'PL') {
        if (parseInt(leavecount) > parseInt(leaveRemaining)) {
            errortext = "max PL apply 15  ";
            $('#lcount').addClass('border-danger');
            isError = 1
        }
    }
    if (leavetypeCode == 'CL') {
        if (parseInt(leavecount) > parseInt(leaveRemaining)) {
            errortext = "max CL apply 2  ";
            $('#leavecount').addClass('border-danger');
            isError = 1
        }
    }
    if ((fromdate > todate) || todate < fromdate) {
        errortext = "date should be greater than fromdate";
        $('#txtfromdate').removeClass('border-danger');
        isError = 1;
    }

    if (isError == 1) {
        $("#validatemsg").addClass('text-danger').html('Please Fill All fields' + ' ' + errortext);
    }
    else {

        var data = {
            LEAVE_DESC: leavetypeName,
            LEAVE_COUNT: leavecount,
            LEAVE_FROM_DATE: fromdate,
            LEAVE_TO_DATE: todate,
            REASONS: reason,
            ALTMOBILE: txtaltmobile,
            REMARKS: remarks,
            LEAVE_CODE: leavetypeCode,
            //LEAVE_MONTH: 4,
            //LEAVE_YEAR:2022,

        };
        $.ajax({
            type: 'POST',
            url: "../Leave/SaveLeaveRequest",
            data: data,

            success: function (result) {
                debugger
                if (result == 'Success') {
                    $("#validatemsg").addClass('text-success').html('Leave successfully apply');
                    setTimeout(function () {

                        $("#validatemsg").html('');
                        closeModel();
                        location.reload();
                    }, 3000);

                }
                else {
                    $("#validatemsg").addClass('text-danger').html(result);
                    setTimeout(function () {
                        $("#validatemsg").html('');
                        closeModel();
                        location.reload();
                    }, 3000);
                }
            },
            error: function () {
                $("#validatemsg").addClass('text-danger').html('Something went wrong...');
                setTimeout(function () {
                    $("#validatemsg").html('');
                    closeModel();
                }, 1000);

            },

        });
    };
});



/*var dataTable = $('#tblManageleave').DataTable({});*/

$('#btnManageLeave').click(function () {
    debugger;
    ManageDataTable();
    //$.ajax({
    //    url: '../Leave/ManageLeaveRequest',
    //    type: "POST",
    //    /* data: data,*/
    //    contentType: 'application/json;charset=utf-8',
    //    dataType: "json",
    //    success: function (response) {
    //        $("#tblManageleave").empty();
    //        if (response.length > 0) {
    //            debugger;
    //            var tr;
    //            var scount = 0;
    //            for (var i = 0; i < response.length; i++) {

    //                scount = scount + 1;
    //                tr = $('<tr/>');
    //                tr.append("<td>" + scount + "</td>");
    //                tr.append("<td>" + response[i].EMP_ID + "</td>");
    //                tr.append("<td>" + response[i].Emp_Full_Name + "</td>");
    //                tr.append("<td>" + response[i].FROMDATE + "</td>");
    //                tr.append("<td>" + response[i].TODATE + "</td>");
    //                tr.append("<td>" + response[i].LeaveType + "</td>");
    //                tr.append("<td>" + response[i].LEAVE_COUNT + "</td>");
    //                tr.append("<td>" + response[i].APPstatusCORDINATOR + "</td>");
    //                tr.append("<td>" + response[i].APPstatusMANAGER + "</td>");
    //                tr.append("<td>" + response[i].APPstatusHEAD + "</td>");
    //                //tr.append("<td> <img data-toggle='modal' data-target='#infraFullImg' id='infatmgId''name='Infra' style='max-width:50px; max-height:50px;' src=" + response[i].INFRA_IMAGE + "></td>");
    //                //var deletebtn = '<a class="btn btn-smm btn-danger text-white" onclick="DeleteInfraRecordDetails(' + response[i].RecordId + ')"> <i class="fa fa-trash" aria-hidden="true"></i></a >';
    //                //tr.append("<td>" + deletebtn + "</td>");
    //                $('#tblManageleave').append(tr);
    //            }

    //        }
    //    },

    //});
});


//$('#tblManageleaveHead').dataTable({

//    "processing": true,
//    "serverSide": false,
//    "filter": true,
//    "orderMulti": true,
//    "ajax": {
//        "url": '/Leave/ManageLeaveRequestsNew',
//        "type": 'POST',
//        "dataType": 'json',
//    },

//    "columns": [
//        { "data": "EMP_ID", "autowidth": true },
//        { "data": "Emp_Full_Name", "autowidth": true },
//        { "data": "FROM_DATE", "autowidth": true },
//        { "data": "TO_DATE", "autowidth": true },
//        { "data": "LEAVE_CODE", "autowidth": true },
//        { "data": "LEAVE_COUNT", "autowidth": true },
//        { "data": "CORDINATOR", "autowidth": true },
//        { "data": "MANAGER", "autowidth": true },
//        { "data": "HEAD", "autowidth": true }
//    ],
//});

$("#tblManageleaveHead").dataTable({

    "processing": true,
    "serverSide": false,
    "filter": true,
    "orderMulti": true,
    "ajax": {
        "url": "/Leave/ManageLeaveRequestsNew",
        "type": "POST",
        "dataType": "json",
        "dataSrc": "data"
    },

    "columns": [
        { "data": "emP_ID", "autowidth": true },
        { "data": "emp_Full_Name", "autowidth": true },
        { "data": "froM_DATE", "autowidth": true },
        { "data": "tO_DATE", "autowidth": true },
        { "data": "leavE_CODE", "autowidth": true },
        { "data": "leavE_COUNT", "autowidth": true },
        { "data": "cordinator", "autowidth": true },
        { "data": "manager", "autowidth": true },
        { "data": "head", "autowidth": true }
    ],
});

function ManageDataTable() {
    debugger
    $('#tblManageleaveHead').dataTable({

        "ajax": {
            "url": '../Attendance/AttendanceDetail',
            "type": 'POST',
            "contentType": 'application/json;charset=utf-8',
            "dataType": 'json',
            "dataSrc": ""
        },

        "columns": [
            { 'data': 'EMPID' },
            { 'data': 'Emp_Full_Name' },
            { 'data': 'DATE' },
            { 'data': 'INTIME' },
            { 'data': 'OUTTIME' },
            { 'data': 'RADIALINDISTANCE' },
            { 'data': 'RADIALINDISTANCE' },
            { 'data': 'Country' },
            { 'data': 'CORDINATOR' },
            { 'data': 'MANAGER' },
            { 'data': 'HEAD' }
        ],
    });
}


function GetLeaveApprovalList() {
    debugger
    var data = {
        FROM: $('#txtfromdate').val(),
        TO_DATE: $('#txttodate').val(),
    };

    var appendrowth = "";
    $("#leaveApprovalbody").empty();
    $.ajax({
        url: "../Leave/GetLeaveApprovalList",
        type: "POST",
        data: data,
        dataType: "json",
        success: function (ResponseDt) {
            debugger
            var ResponseDtlength = ResponseDt.data.length;
            debugger
            var scount = 1;
            for (var i = 0; i < ResponseDtlength; i++) {

                var CordinatorA = 'CordinatorA';
                var CordinatorR = 'CordinatorR';
                var ManagerA = 'ManagerA';
                var ManagerR = 'ManagerR';
                var HeadA = 'HeadA';
                var HeadR = 'HeadR';
                CordinatorA = CordinatorA + i;
                CordinatorR = CordinatorR + i;
                ManagerA = ManagerA + i;
                ManagerR = ManagerR + i;
                HeadA = HeadA + i;
                HeadR = HeadR + i;
                var manager = ResponseDt.data[i].apPstatusMANAGER;
                var head = ResponseDt.data[i].apPstatusHEAD;
                var cordinator = ResponseDt.data[i].apPstatusCORDINATOR

                scount = scount + i;
                var appendrow = `<tr class="odd">
                    <td>` + scount + `</td>
                    <td class="sorting_1">` + ResponseDt.data[i].emP_ID + ` </td>
                    <td>` + ResponseDt.data[i].emp_Full_Name + `</td>
                    <td>` + ResponseDt.data[i].fromdate + `</td>
                    <td>` + ResponseDt.data[i].todate + `</td>
                    <td>` + ResponseDt.data[i].applydate + `</td>
                    <td>` + ResponseDt.data[i].leavE_COUNT + `</td>
                    <td>` + ResponseDt.data[i].leaveType + `</td>
                    <td>` + ResponseDt.data[i].reasons + `</td>

                    <td><input type="checkbox" id=`+ ManagerA + ` class="ManagerA" name="chk" value="2" onclick="SelectOnlyOne(this,` + i + `)"><input type="hidden" id ="manager" value = ` + manager +`>
                    <span class="text-success">Approved</span> <br><input type="checkbox" id=`+ ManagerR + ` class="ManagerR" name="3" value="3" onclick="SelectOnlyOne(this,` + i + `)"><input type="hidden" id ="manager" value = ` + manager +`>
                    <span class="text-danger">Reject</span></td><td><input type="checkbox" id=`+ HeadA + ` class="HeadA" name="chk" value="2" onclick="SelectOnlyOne(this,` + i + `)"><input type="hidden" id ="head" value = ` + head +`>
                    <span class="text-success">Approved</span> <br><input type="checkbox" id=`+ HeadR + ` name="chk" class="HeadR" value="3" onclick="SelectOnlyOne(this,` + i + `)"><input type="hidden" id ="head" value = ` + head +`>
                    <span class="text-danger">Reject</span></td></tr>`;

                appendrowth = appendrowth + appendrow;
            }

            $("#leaveApprovalbody").append(appendrowth);
            /*SetDataTable();*/
            setTimeout(function () {
                debugger
                for (var i = 0; i < ResponseDtlength; i++) {
                    debugger
                    var CordinatorAchk = 'CordinatorA' + i;
                    var CordinatorRchk = 'CordinatorR' + i;
                    var ManagerAchk = 'ManagerA' + i;
                    var ManagerRchk = 'ManagerR' + i;
                    var HeadAchk = 'HeadA' + i;
                    var HeadRchk = 'HeadR' + i;

                    if (approvallbl == 2) {

                        $('#' + ManagerAchk).prop('disabled', true);
                        $('#' + ManagerRchk).prop('disabled', true);
                        $('#' + HeadAchk).prop('disabled', true);
                        $('#' + HeadRchk).prop('disabled', true);
                    }

                    if (approvallbl == 3) {
                        $('#' + CordinatorAchk).prop('disabled', true);
                        $('#' + CordinatorRchk).prop('disabled', true);
                        $('#' + HeadAchk).prop('disabled', true);
                        $('#' + HeadRchk).prop('disabled', true);
                    }
                    if (approvallbl == 4) {
                        $('#' + CordinatorAchk).prop('disabled', true);
                        $('#' + CordinatorRchk).prop('disabled', true);
                        $('#' + ManagerAchk).prop('disabled', true);
                        $('#' + ManagerRchk).prop('disabled', true);
                    }
                    debugger
                    if (ResponseDt.data[i].disableOption == 'head') {
                        $('#' + HeadAchk).prop('disabled', true);
                        $('#' + HeadRchk).prop('disabled', true);
                    }
                    if (ResponseDt.data[i].disableOption == 'manager') {
                        $('#' + ManagerAchk).prop('disabled', true);
                        $('#' + ManagerRchk).prop('disabled', true);
                    }

                    if (ResponseDt.data[i].apPstatusMANAGER == 2) {
                        $('#' + ManagerAchk).prop('checked', true);

                    }
                    if (ResponseDt.data[i].apPstatusMANAGER == 3) {
                        $('#' + ManagerRchk).prop('checked', true);
                    }
                    if (ResponseDt.data[i].apPstatusHEAD == 2) {
                        $('#' + HeadAchk).prop('checked', true);
                    }
                    if (ResponseDt.data[i].apPstatusHEAD == 3) {
                        $('#' + HeadRchk).prop('checked', true);
                    }
                }

            }, 2000);
        }
    });
}


var appdata = [];

$("#btnLeaveapproval").click(function () {
    debugger;
    $('#tblLeaveapproval tbody tr').each(function () {
        var row = $(this);
        // get current row 2nd TD
        var appList = {};
        debugger
        var CordinatorA = row.find(".CordinatorA").is(':checked');
        var CordinatorR = row.find(".CordinatorR").is(':checked');
        var ManagerA = row.find(".ManagerA").is(':checked');
        var ManagerR = row.find(".ManagerR").is(':checked');
        var manager = row.find("#manager").val();
        var HeadA = row.find(".HeadA").is(':checked');
        var HeadR = row.find(".HeadR").is(':checked');
        var head = row.find("#head").val();

        if (CordinatorA || CordinatorR || ManagerA || ManagerR || HeadA || HeadR) {
            debugger;
            var currentDate = new Date();
            //var APPLYDATE = currentDate.getFullYear() + '-' + ('0' + (currentDate.getMonth() + 1)).slice(-2) + '-' + ('0' + currentDate.getDate()).slice(-2);

            var EMPID = row.find("td:eq(1)").text();
            var APPLYDATE = row.find("td:eq(5)").text();
            if (CordinatorA == true) {
                debugger
                var valCordnatorA = row.find(".CordinatorA").val();
                appList.APPstatusCORDINATOR = valCordnatorA;
            };
            if (CordinatorR == true) {
                var valCordnatorR = row.find(".CordinatorR").val();
                appList.APPstatusCORDINATOR = valCordnatorR;
                appList.APPstatusMANAGER = 1;
                appList.APPstatusHEAD = 1;
            }

            if (ManagerA == true) {
                debugger
                var valManagerA = row.find(".ManagerA").val();
                var valHeadA = row.find(".HeadA").val();
                var valCordinatorA = row.find(".CordinatorA").val();
                appList.APPstatusMANAGER = valManagerA;
                appList.APPstatusHEAD = head;
                appList.APPstatusCORDINATOR = valCordinatorA;
            }
            if (ManagerR == true) {
                var valManagerR = row.find(".ManagerR").val();
                appList.APPstatusMANAGER = ManagerR;
                appList.APPstatusHEAD = 1;
                appList.APPstatusCORDINATOR = 1;
            }

            if (HeadA == true) {
                var valHeadA = row.find(".HeadA").val();
                var valManagerA = row.find(".ManagerA").val();
                var valCordinatorA = row.find(".CordinatorA").val();
                appList.APPstatusHEAD = valHeadA;
                appList.APPstatusCORDINATOR = 1;
                appList.APPstatusMANAGER = manager;
            }
            if (HeadR == true) {
                var valHeadR = row.find(".HeadR").val();
                appList.APPstatusHEAD = valHeadR;
                appList.APPstatusCORDINATOR = 1;
                appList.APPstatusMANAGER = manager;
            }

            appList.EMPID = EMPID;
            appList.APPLYDATE = APPLYDATE;
            appdata.push(appList);
        }
    });
    if (appdata.length > 0) {

        $.ajax({
            type: 'POST',
            url: "../Leave/SaveLeaveApproved",
            data: { 'appdata': appdata },
            document: document.body,
            success: function (result) {
                $('#msgSuccess').addClass('label-success').html('Leave Approved Successfully');
                setTimeout(function () {
                    location.reload();
                }, 5000);

            },
            error: function () {
                $('#msgError').addClass('label-danger').html('Something went wrong..');
                setTimeout(function () {
                    location.reload();
                }, 5000);

            }

        });
    }
});


$('#btnSearch').click(function () {
    debugger
    GetLeaveApprovalList()
});

$("#btnRequestLeave").click(function () {

    $("#requestLeaveModal").modal("show");
    LoadDropDownContent();
});



function closeModel() {
    $("#requestLeaveModal").modal("hide");
}

function LoadDropDownContent() {
    debugger
    $('#ddlleaveName').empty();
    $('#ddlleaveName').append($("<option></option>").val('').html('Select Leave Name'));
    $.ajax({
        type: "POST",
        url: "../Leave/GetLeaveNameList",
        data: '{}',
        /* data: JSON.stringify({ "CountryId": CountryId }),*/
        contentType: "application/json; charset=utf-8",
        context: document.body,
        success: function (data) {
            debugger
            if (data != '') {
                $.each(data, function (item, value) {
                    debugger
                    $('#ddlleaveName').append($("<option></option>").val(value.iD).html(value.leavE_DESC /*+ "-" + value.leavE_TYPE + ""*/));
                })
                $('#ddlleaveName').prop("disabled", false);
            }
        },
        error: function (xhr) {
            $('#ddlleaveName').empty();
        }
    });
}


//function SetDataTable() {
//    $('#tblLeaveapproval').DataTable({
//        //order: [[2, 'asc']],
//        //rowGroup: {
//        //    dataSrc: 2
//        //}
//        "searching": false,
//        "paging": true,
//        // "bFilter": false,
//        "bInfo": false,
//        "aLengthMenu": [[15, 25, 50, -1], [15, 25, 50, "All"]],
//        "iDisplayLength": 15,
//        //"pageLength": 25,
//        // "pagingType": "simple" //"full_numbers"
//        language: {
//            paginate: {
//                previous: 'Prev',
//                next: 'Next'
//            },
//            aria: {
//                paginate: {
//                    previous: 'Previous',
//                    next: 'Next'
//                }
//            }
//        }


//    });
//}

function SelectOnlyOne(e, i) {
    $('#CordinatorA' + i).prop('checked', false);
    $('#CordinatorR' + i).prop('checked', false);
    $('#ManagerA' + i).prop('checked', false);
    $('#ManagerR' + i).prop('checked', false);
    $('#HeadA' + i).prop('checked', false);
    $('#HeadR' + i).prop('checked', false);
    $('#' + e.id).prop('checked', true);
    //debugger
    //alert(e.id)


}





