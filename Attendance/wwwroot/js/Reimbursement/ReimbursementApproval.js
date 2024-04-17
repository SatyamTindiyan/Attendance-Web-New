
var today = new Date();
function datebind() {
    debugger
    $(".dtBox").DateTimePicker({
        //minDate: today
        dateFormat: "dd/MM/yyyy hh:mm"
    });
}

$("#btnFetchData").click(function () {
    debugger;
    var errors = '';
    var Month = $("#month option:selected").val();
    var year = $("#year option:selected").html();
    var EmpId = $('#txtid').val();
    //var EmpName = $('#txtempname').val();
    //var PrevOutstadAmt = $('#txtOutstanding').val();
    //var onOutstanding = $('#txtasOutstanding').val();
    if (typeof Month === "undefined" || Month == '' || Month == '0') {
        errors += '<div class="col-sm-3">Please select month.</div>';
        $("#ddlmounth").addClass(' error');
    }
    else {
        $("#ddlmounth").removeClass(' error');
    }
    if (typeof year === "undefined" || year == '' || year == '0') {
        errors += '<div class="col-sm-3">Please select year.</div>';
        $("#ddlyear").addClass(' error');
    }
    else {
        $("#ddlyear").removeClass(' error');
    }
    if (EmpId == '')
        errors += '<div class="col-sm-3">please enter EmpId.</div>';
    else {
        $("#txtid").removeClass(' error');
    }
    //if (EmpName == '')
    //    errors += '<div class="col-sm-3">please enter Emp Name.</div>';
    //else {
    //    $("#txtempname").removeClass(' error');
    //}

    if (errors == '') {
        var _jsonFetchData = {};
        var formData = new FormData();
        //_jsonFetchData.ClaimDetail = ClaimDetail;
        _jsonFetchData.Month = Month;
        _jsonFetchData.year = year;
        _jsonFetchData.EmpId = EmpId;
        //_jsonClaimData.EmpName = EmpName;
        //_jsonClaimData.PrevOutstadAmt = PrevOutstadAmt;
        //_jsonClaimData.onOutstanding = onOutstanding
        var rowCount = $('#tblReimbursement tbody tr').length;
        var ddlClaimTypeId = "ClaimType" + (rowCount + 1);
        var ClaimTypeOption = '<option value =""> Select Claim Type</option>';
        var ddlClaimSubTypeId = "ClaimSubType" + (rowCount + 1);
        var ClaimSubTypeOption = '<option value =""> Select Claim Sub Type</option>';
        formData.append('Month', Month);
        formData.append('year', year);
        formData.append('EmpId', EmpId);

        $.ajax({
            url: "../webpage/ReimbursementApproval.aspx/FetchData",
            type: 'post',
            contentType: 'application/json;charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(_jsonFetchData),
            success: function (data) {
                debugger
                var tbodyCliamdata = "";
                var count = 0;
                var tsum = 0;
                var obj = (JSON.parse(data.d));
                var dt1 = obj.dt;
                var dt2 = obj.dtt;
                var _SubType = obj.SubType;
                //if (data != '') {
                if (obj.dt != '') {
                    for (var j = 0; j <= dt2.length - 1; j++) {
                        ClaimTypeOption = ClaimTypeOption + '<option value=' + dt2[j].id + '>' + dt2[j].PayoutType + '</option>';
                    }
                }
                if (obj.SubType != '') {
                    for (var k = 0; k <= _SubType.length - 1; k++) {
                        ClaimSubTypeOption = ClaimSubTypeOption + '<option value=' + _SubType[k].PayoutTypeID + '>' + _SubType[k].PayoutSubType + '</option>';
                    }
                }

                for (var i = 0; i <= dt1.length - 1; i++) {
                    count = count + 1;
                    var rowCountS = i + 1;
                    var txtClaimAmt = "txtClaimAmt" + rowCountS;
                    tsum += dt1[i].claimAmount
                    tbodyCliamdata += "<tr ID=ABCD>";
                    tbodyCliamdata += "<td>" + count + "</td>";
                    tbodyCliamdata += "<td><input type='checkbox' id='' name=' value=''></td>";
                    tbodyCliamdata += "<td><select onchange='bindSutbtypeClaim(this)' class='form-control' id='" + ddlClaimTypeId + "'>'" + ClaimTypeOption + "'</select></td>";
                    tbodyCliamdata += "<td><select class='form-control' id='" + ddlClaimSubTypeId + "'>'" + ClaimSubTypeOption + "'</select></td>";
                    tbodyCliamdata += "<td><input type='text' placeholder='Date' class='form-control'  data-field='datetime'><div id='dtBox' class='dtBox'></div></td>";
                    tbodyCliamdata += "<td><input type='text' ID='" + txtClaimAmt + "' disabled value='" + dt1[i].claimAmount + "' placeholder='Amount' class='form-control amt'></td>";
                    tbodyCliamdata += "<td><input type='text' ID='txtKMIN' value='" + dt1[i].kmin + "' placeholder='KM IN' class='form-control'></td>";
                    tbodyCliamdata += "<td><input type='text' ID='txtKMOut' value='" + dt1[i].kmout + "' placeholder='KM OUT' class='form-control'></td>";
                    tbodyCliamdata += "<td><input type='text' ID='txtRemarks' value='" + dt1[i].remarks + "' placeholder='Remarks' class='form-control'></td>";
                    tbodyCliamdata += "<td><input type='text' ID='txtClaimStatus' value='" + dt1[i].claimStatus + "' placeholder='Claim Status' class='form-control'></td>";
                    tbodyCliamdata += "<td><input type='text' ID='txtMRemarks' value='" + dt1[i].mRemarks + "' placeholder='Remarks' class='form-control'></td>";
                    //tbodyCliamdata += "<td><input type='file' style='width:150px;'  name='fileClaim' id='fileClaim' /></td>";
                    //tbodyCliamdata += "<td><a href='button' class='btn btn-sm btn-xs btn-info'><i class='fa fa-upload' aria-hidden='true'></i></a>" +
                    //    "<button type='button' class='btn btn-sm btn-xs btn-success'><i class='fa fa-download' aria-hidden='true'></i></button></td>";
                    tbodyCliamdata += "<td><input type='text' ID='txtProjectHeadStatus' value='HeadStatustest' placeholder='Remarks' class='form-control'></td>";
                    tbodyCliamdata += "<td><input type='text' ID='txtHeadRemarks' value='HeadRemarks' placeholder='Remarks' class='form-control'></td>";
                    tbodyCliamdata += "<td><input type='text' ID='txtPayableAmount' value='PayableAmount' placeholder='Remarks' class='form-control'></td>";
                    tbodyCliamdata += "<td><input type='text' ID='txtPaymentRemarks' value='PayableAmount' placeholder='Remarks' class='form-control'></td>";
                    tbodyCliamdata += "<td><button type='button' class='btn btn-sm btn-xs btn-success'><i class='fa fa-download' aria-hidden='true'></i></button></td>";
                    tbodyCliamdata += "</tr>";
                };
                $('#tbodyReimbursement').html(tbodyCliamdata);
                //if (data != '') {
                //    $('#divMessage').html(data);
                //    $('#divMessage').addClass(' shadow-border');

                //}
                debugger
                $("#txtTotal").val(tsum);
                $("#tfooter").css("display", "table-row");
                datebind();
            },
            error: function (xhr) {

                var ss = xhr.responseText;
                $('#divMessage').html('Your Reimbursement not  Fetch Data.');
                $('#divMessage').addClass(' shadow-border');
            }
        });
    }
    else {
        $('#divMessage').html(errors);
        $('#divMessage').addClass(' shadow-border');
        return false;
    }
});


function bindSutbtypeClaim(control) {
    debugger
    var ind = control.closest('tr').rowIndex;
    var ddlClaimSubTypeId = "ClaimSubType" + ind;
    $('#' + ddlClaimSubTypeId).empty();
    $('#' + ddlClaimSubTypeId + 'option:selected').remove();
    var ddlid = control.id;

    // var row_index = $(this).parent().index('tr');
    var ddlClaimTypeId = "ClaimType" + ind;
    var txtClaimAmt = "txtClaimAmt" + ind;
    $("#tblReimbursement tbody tr").each(function () {
        debugger;
        var rows = $(this);
        var index = this.rowIndex;
        var _ClaimType = $(rows).find("td").eq(2).find('option:selected').val();

        if (ind == index) {
            if (_ClaimType != '' && _ClaimType != "-1") {
                $('#' + ddlClaimSubTypeId).empty();
                $('#' + ddlClaimSubTypeId + 'option:selected').remove();
                var _jsonSubTyepData = {};
                _jsonSubTyepData.ClaimType = _ClaimType;
                $.ajax({
                    url: "../webpage/ReimbursementApproval.aspx/BindSubTypeClaim",
                    type: 'post',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'json',
                    data: JSON.stringify(_jsonSubTyepData),
                    success: function (data) {
                        debugger;
                        var obj = (JSON.parse(data.d));
                        var opSubTypeId = "";
                        opSubTypeId = '<option value="-1">Select Sub Type</option>';
                        if (obj != '') {

                            $.each(obj, function (item, value) {

                                opSubTypeId += ("<option value=" + value.PayoutTypeID + ">" + value.PayoutSubType + "</option>");

                            })

                            $("#" + ddlClaimSubTypeId).append(opSubTypeId);
                            //$("#" + ddlClaimSubTypeId).prop("disabled", false);
                            if (_ClaimType == 3) {

                                $("#" + txtClaimAmt).prop("disabled", false);
                                //$("#txtClaimAmt").prop("disabled", false)
                            }
                        }


                    },
                    error: function (xhr) {

                        $("#" + ddlClaimSubTypeId).empty();
                        $("#" + ddlClaimSubTypeId).prop("disabled", true);

                    }
                });
            }
            else {
                $("#" + ddlClaimSubTypeId).empty();
            }

        }

    });


};
function checkValidations() {
    debugger;
    var ClaimDetail = new Array();
    var formData = new FormData();
    var errors = '';
    var Month = $("#ddlmounth option:selected").val();
    var year = $("#ddlyear option:selected").val();
    var EmpId = $('#txtid').val();
    var EmpName = $('#txtempname').val();
    var PrevOutstadAmt = $('#txtOutstanding').val();
    var onOutstanding = $('#txtasOutstanding').val();
    if (typeof Month === "undefined" || Month == '' || Month == '0') {
        errors += '<div class="col-sm-3">Please select month.</div>';
        $("#ddlmounth").addClass(' error');
    }
    else {
        $("#ddlmounth").removeClass(' error');
    }
    if (typeof year === "undefined" || year == '' || year == '0') {
        errors += '<div class="col-sm-3">Please select year.</div>';
        $("#ddlyear").addClass(' error');
    }
    else {
        $("#ddlyear").removeClass(' error');
    }
    if (EmpId == '')
        errors += '<div class="col-sm-3">please enter EmpId.</div>';
    else {
        $("#txtid").removeClass(' error');
    }
    if (EmpName == '')
        errors += '<div class="col-sm-3">please enter Emp Name.</div>';
    else {
        $("#txtempname").removeClass(' error');
    }
    if (errors == '') {
        $('#tbodyReimbursement').find('tr').each(function () {
            debugger;
            var row = $(this);

            var ischecked = row.find("td").eq(1).find('input[type="checkbox"]').is(':checked');
            if (ischecked) {
                var _ClaimType = row.find("td").eq(2).find('option:selected').val();
                var _ClaimTypeSub = row.find("td").eq(3).find('option:selected').val();
                var _ClaimDate = row.find("td").eq(4).find('input[type = "text"]').val();
                var _ClaimAmt = row.find("td").eq(5).find('input[type = "text"]').val();
                var _KMIN = row.find("td").eq(6).find('input[type = "text"]').val();
                var _KMOUT = row.find("td").eq(7).find('input[type = "text"]').val();
                var _Remarks = row.find("td").eq(8).find('input[type = "text"]').val();
                var _ClimStatus = row.find("td").eq(9).find('input[type = "text"]').val();
                var _MRemarks = row.find("td").eq(10).find('input[type = "text"]').val();

            }
            var ClaimData = {};
            if (_ClaimType > 0) {
                ClaimData.ClaimType = _ClaimType;
                ClaimData.ClaimTypeSub = _ClaimTypeSub;
                // ClaimData.ClaimDate = _ClaimDate;
                ClaimData.ClaimAmt = _ClaimAmt;
                ClaimData.KMIN = _KMIN;
                ClaimData.KMOUT = _KMOUT;
                ClaimData.Remarks = _Remarks;
                ClaimData.ClaimStatus = _ClimStatus;
                ClaimData.MRemarks = _MRemarks;
                debugger;
                ClaimDetail.push(ClaimData);
            }

        });
        //formData.append('ClaimDetail', JSON.stringify(ClaimDetail));
        debugger;
        var _jsonClaimData = {};
        _jsonClaimData.ClaimDetail = ClaimDetail;
        _jsonClaimData.Month = Month;
        _jsonClaimData.year = year;
        _jsonClaimData.EmpId = EmpId;
        _jsonClaimData.EmpName = EmpName;
        _jsonClaimData.PrevOutstadAmt = PrevOutstadAmt;
        _jsonClaimData.onOutstanding = onOutstanding;
        $.ajax({

            url: "../webpage/ReimbursementApproval.aspx/Reimbursementt",
            type: 'post',
            contentType: 'application/json;charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(_jsonClaimData),
            success: function (data) {
                debugger
                if (data != '') {
                    $('#divMessage').html(data);
                    $('#divMessage').addClass(' shadow-border');

                }
            },
            error: function (xhr) {
                var ss = xhr.responseText;
                $('#divMessage').html('Your allocation not saved successfully.');
                $('#divMessage').addClass(' shadow-border');
            }
        });
    }
    else {
        $('#divMessage').html(errors);
        $('#divMessage').addClass(' shadow-border');
        return false;
    }
}

$('#searchData').keyup(function () {
    debugger;
    var searchText = $(this).val();
    $('#tblReimbursement tr').each(function () {
        debugger;
        var found = 'false';

        $(this).each(function () {
            debugger;
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

function getyear() {
    debugger
    $('#year').empty();
    $('#year').append($("<option></option>").val('').html('Select year'));
    $.ajax({
        type: "POST",
        url: "/Reimbursement/Getyear",
        context: document.body,
        success: function (data) {
            debugger

            if (data != '') {
                $.each(data.yearList, function (item, value) {
                    debugger
                    $("#year").append($("<option></option>").val(value.encryptcountryID).html(value.text));
                })
                $("#year").prop("disabled", false);

            }
        },
        error: function (data, error) {
            debugger
            $("#spanMessage").addClass('text-danger').html(data);
        }

    });
}
$('#year').change(function () {
    debugger
    getmonth();
});
function getmonth() {
    debugger
    var iselectyear = $("#year option:selected").val();
    var iselectyearS = $("#year option:selected").html();
    //$('#month').empty();
    //$('#month').append($("<option></option>").val('').html('Select Month'));

    $.ajax({
        type: "GET",
        url: "/Reimbursement/Getmonth",
        context: document.body,
        data: { 'year': iselectyearS },
        success: function (data) {
            debugger

            if (data != '') {
                $.each(data.ddlMonths, function (item, value) {
                    debugger

                    $("#month").append($("<option></option>").val(value.value).html(value.text));
                })
                $("#month").prop("disabled", false);
            }
        },
        error: function (data, error) {
            debugger
            $("#spanMessage").addClass('text-danger').html(data);
        }

    });
}