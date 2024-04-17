

$(document).ready(function () {

    GetLeaveReport();

});

$("#txtFromDate").datepicker({
    dateFormat: 'mm/dd/yy',
    changeMonth: true,
    changeYear: true,
    //minDate: dateText
    timeFormat: 'hh:mm TT'
});

$("#txtTodate").datepicker({
    dateFormat: 'mm/dd/yy',
    changeMonth: true,
    changeYear: true,
    //minDate: dateText
    timeFormat: 'hh:mm TT'
});



$('#CountryId').on('change', function () {
    debugger
    var CountryId = $("#CountryId option:selected").val();
    if (CountryId != '') {
        $('#CompanyID').empty();
        $('#CompanyID').append($("<option></option>").val('').html('Select Company'));
        $.ajax({
            type: "GET",
            url: "../Leave/GetCompanyList?CountryId=" + CountryId,
            success: function (data) {
                debugger
                if (data != '') {
                    $.each(data, function (item, value) {
                        $('#CompanyID').append($("<option></option>").val(value.companyId).html(value.company));
                    })
                    $('#CompanyID').prop("disabled", false);
                }
            },
            error: function (xhr) {
                $('#CompanyID').empty();
            }
        });
    }
    else {
        $('#CompanyID').empty();
        $('#CompanyID').prop("disabled", true);
    }
});

$('#CompanyID').on('change', function () {
    var companyId = $("#CompanyID option:selected").val();
    if (companyId != '') {
        $('#DepartmentID').empty();
        $('#DepartmentID').append($("<option></option>").val('').html('Select Department'));
        $.ajax({
            type: "POST",
            url: "../Leave/GetDepartmentList",
            data: JSON.stringify({ "company": companyId }),
            contentType: "application/json; charset=utf-8",
            context: document.body,
            success: function (data) {
                if (data != '') {
                    $.each(data, function (item, value) {
                        $('#DepartmentID').append($("<option></option>").val(value.departmentId).html(value.department));
                    })
                    $('#DepartmentID').prop("disabled", false);
                }
            },
            error: function (xhr) {
                $('#DepartmentID').empty();
            }
        });
    }
    else {
        $('#DepartmentID').empty();
        $('#DepartmentID').prop("disabled", true);
    }
});

$('#DepartmentID').on('change', function () {
    debugger
    var DepartmentID = $("#DepartmentID option:selected").val();
    var CompanyID = 0;
    if (DepartmentID != '') {
        $('#EmployeeID').empty();
        $('#EmployeeID').append($("<option></option>").val('').html('Select Employee'));
        $.ajax({
            type: "POST",
            url: "../Leave/GetEmpList?DepartmentID=" + DepartmentID,
            //data: JSON.stringify({ "CompanyID": CompanyID, "DepartmentID": DepartmentID }),
            //contentType: "application/json; charset=utf-8",
            //context: document.body,
            success: function (data) {
                debugger
                if (data != '') {
                    $.each(data, function (item, value) {
                        $('#EmployeeID').append($("<option></option>").val(value.empId).html(value.empname));
                    })
                    $('#EmployeeID').prop("disabled", false);
                }
            },
            error: function (xhr) {
                $('#EmployeeID').empty();
            }
        });
    }
    else {
        $('#EmployeeID').empty();
        $('#EmployeeID').prop("disabled", true);
    }
});


function GetLeaveReport() {
    debugger
    var countryid = $("#CountryId option:selected").val();
    var companyid = $("#CompanyID option:selected").val();
    var depid = $("#DepartmentID option:selected").val();
    var emp = $("#EmployeeID option:selected").html();
    var empname = emp.split('-');
    var empName1 = empname[1];
    var fromdate = $('#txtFromDate').val();
    var todate = $('#txtTodate').val();
    //var empid= $('#txtfromdate').val();
    var empid = $('#hiddenemp').val();
    var data = {

        Countryid: countryid,
        Depid: depid,
        EMP_ID: empName1,
        FROMDATE: fromdate,
        TODATE: todate,
        CompanyId: companyid,
    };

    var appendrowth = "";
    $("#tblleaveReportBody").empty();

    $.ajax({
        url: '../Leave/GetLeaveReport',
        type: "POST",
        data: data,
        dataType: "json",
        success: function (ResponseDt) {
            var ResponseDtlength = ResponseDt.data.length;

            var scount = 0;
            for (var i = 0; i < ResponseDtlength; i++) {
                scount = scount + 1;


                var appendrow = `<tr class="odd"><td>` + scount + `</td>
                                <td class="sorting_1">` + ResponseDt.data[i].emP_ID + ` </td>
                                <td>` + ResponseDt.data[i].emp_Full_Name + `</td>
                                <td>` + ResponseDt.data[i].fromdate + `</td>
                                <td>` + ResponseDt.data[i].todate + `</td>
                                <td>` + ResponseDt.data[i].leavE_COUNT + `</td>
                                <td>` + ResponseDt.data[i].leaveType + `</td>
                                <td>` + ResponseDt.data[i].reasons + `</td></tr>`;

                appendrowth = appendrowth + appendrow;
            }

            $("#tblleaveReportBody").append(appendrowth);
            SetDataTable();

        }
    });
}

$('#btnReportSearch').click(function () {
    debugger
    GetLeaveReport();
});

function SetDataTable() {
    $('#tblLeaveReport').DataTable({
        //order: [[2, 'asc']],
        //rowGroup: {
        //    dataSrc: 2
        //}
        "bDestroy": true,
        "searching": false,
        "paging": true,
        // "bFilter": false,
        "bInfo": false,
        "aLengthMenu": [[15, 25, 50, -1], [15, 25, 50, "All"]],
        "iDisplayLength": 15,
        //"pageLength": 25,
        // "pagingType": "simple" //"full_numbers"
        language: {
            paginate: {
                previous: 'Prev',
                next: 'Next'
            },
            aria: {
                paginate: {
                    previous: 'Previous',
                    next: 'Next'
                }
            }
        }


    });
}