$(document).ready(function () {
    getyear()
    getmonth()

});
$('#btnFetchData').click(function () {
    debugger;
    var errors = '';
    var Month = $("#month option:selected").val();
    var year = $("#year option:selected").html();
    var EmpId = $('#txtid').val();
    $('#divMessage').html('');
    $('#divMessage').empty();
    //var EmpName = $('#txtempname').val();
    //var PrevOutstadAmt = $('#txtOutstanding').val();
    //var onOutstanding = $('#txtasOutstanding').val();
    //$("#tblReimbursement tr").detach();
    /* $("#tblReimbursement > tr").remove();*/
    /*  $("#tblReimbursement tr").remove();*/
    if (typeof Month === "undefined" || Month == '' || Month == '0') {
        errors += '<div class="col-sm-3">Please select month.</div>';
        $("#month").addClass(' error');
    }
    else {
        $("#month").removeClass(' error');
    }
    if (typeof year === "undefined" || year == '' || year == '0') {
        errors += '<div class="col-sm-3">Please select year.</div>';
        $("#year").addClass(' error');
    }
    else {
        $("#year").removeClass(' error');
    }
    if (EmpId == '')
        errors += '<div class="col-sm-3">please enter EmpId.</div>';
    else {
        $("#txtid").removeClass(' error');
    }


    if (errors == '') {
        var _jsonFetchData = {};
        var formData = new FormData();
        _jsonFetchData.Month = Month;
        _jsonFetchData.year = year;
        _jsonFetchData.EmpId = EmpId;
        var rowCount = $('#tblReimbursement tbody tr').length;
        var ClaimTypeOption = '';
        var ClaimSubTypeOption = '';
        var ClaimStatusOption = '';
        formData.append('EmpId', EmpId);
        formData.append('year', year);
        formData.append('Month', Month);
        $.ajax({
            url: "../Reimbursement/GetReimbursement",
            type: 'POST',
            /*contentType: "application/json; charset=utf-8",*/
            /*data: JSON.stringify(_jsonFetchData),*/
            data: formData,
            contentType: false,
            processData: false,
            context: document.body,
            success: function (data) {
                debugger
                var tbodyCliamdata = "";
                var count = 0;
                var tsum = 0;
                //var obj = (JSON.parse(data.d));
                var dt1 = data.dt;
                var dt2 = data.dtt;
                var _SubType = data.subType;
                var _ClaimSatus = data.claimSatus;
                let ms = Date.parse("March 21, 2012");
                if (dt1.length > 0) {
                    debugger;
                    for (var i = 0; i <= dt1.length - 1; i++) {
                        var selectedId = data.dt[i].payoutTypeID;
                        var selectedSubId = data.dt[i].payoutSubTypeID;
                        var selectedClaimSatusId = data.dt[i].claimStatus;
                        var dt = data.dt[i].claimDate;
                        /*var dt = calEvent.start;*/
                        var dateString = (dt).substr(6);
                        var currentTime = new Date(parseInt(dateString));
                        var month = currentTime.getMonth() + 1;
                        var day = currentTime.getDate();
                        var year = currentTime.getFullYear();
                        var date = day + "/" + month + "/" + year;

                        var fullDate = new Date(parseInt(dt.substr(6)));
                        var rowCountS = i + 1;
                        var txtClaimAmt = "txtClaimAmt" + rowCountS;
                        var txtKMIN = "txtKMIN" + rowCountS;
                        var txtKMOut = "txtKMOut" + rowCountS;
                        var ddlClaimTypeId = "ClaimType" + rowCountS;
                        var ddlClaimSubTypeId = "ClaimSubType" + rowCountS;
                        var txtRemarks = "txtRemarks" + rowCountS;
                        /* var txtClaimStatus = "txtClaimStatus" + rowCountS;*/
                        var fileClaim = "fileClaim" + rowCountS;
                        if (dt2 != '') {

                            ClaimTypeOption = '<option value =""> Select Claim Type</option>';
                            $(dt2).each(function (i, value) {

                                if (selectedId == this.id)
                                    ClaimTypeOption = ClaimTypeOption + '<option selected="selected" value=' + this.id + '>' + this.payoutType + '</option>';
                                else
                                    ClaimTypeOption = ClaimTypeOption + '<option  value=' + this.id + '>' + this.payoutType + '</option>';
                            });
                        }
                        if (_SubType != '') {

                            ClaimSubTypeOption = '<option value =""> Select Claim Sub Type</option>';
                            $(_SubType).each(function (i, value) {

                                if (selectedSubId == this.id)
                                    ClaimSubTypeOption = ClaimSubTypeOption + '<option selected="selected" value=' + this.id + '>' + this.payoutSubType + '</option>';
                                else
                                    ClaimSubTypeOption = ClaimSubTypeOption + '<option  value=' + this.id + '>' + this.payoutSubType + '</option>';
                            });
                        }

                        //if (_ClaimSatus != '') {

                        //    ClaimStatusOption = '<option value =""> Select Claim Status</option>';
                        //    $(_ClaimSatus).each(function (i, value) {
                        //        debugger
                        //        if (selectedClaimSatusId == this.id)
                        //            ClaimStatusOption = ClaimStatusOption + '<option selected="selected" value=' + this.id + '>' + this.claimStatuss + '</option>';
                        //        else
                        //            ClaimStatusOption = ClaimStatusOption + '<option  value=' + this.id + '>' + this.claimStatuss + '</option>';

                        //    });
                        //}
                        debugger
                        count = count + 1;
                        tsum += dt1[i].claimAmount
                        tbodyCliamdata += "<tr>";
                        tbodyCliamdata += "<td>" + count + "</td>";
                        tbodyCliamdata += "<td><select onchange='bindSutbtypeClaim(this)' class='form-control' id='" + ddlClaimTypeId + "'>'" + ClaimTypeOption + "' </select></td>";
                        tbodyCliamdata += "<td><select onchange='bindSutbtypedisable(this)' class='form-control' id='" + ddlClaimSubTypeId + "'>'" + ClaimSubTypeOption + "'</select></td>";
                        //tbodyCliamdata += "<td><input type='text' placeholder='click here' class='form-control'data-field='datetime'><div id='dtBox' class='dtBox'></div></td>";
                        tbodyCliamdata += "<td><input type='text' required value='" + dt + "' placeholder='click here' class='form-control'data-field='datetime'><div id='dtBox' class='dtBox'></div></td>";

                        tbodyCliamdata += "<td><input  type='text' onkeyup='TotalAmt(this)' ID='" + txtClaimAmt + "'   value='" + dt1[i].claimAmount + "' placeholder='Amount' class='form-control amt'></td>";

                        tbodyCliamdata += "<td><input type='text' ID='" + txtKMIN + "' disabled value='" + dt1[i].kmin + "' placeholder='KM IN' class='form-control'></td>";
                        tbodyCliamdata += "<td><input type='text' ID='" + txtKMOut + "' disabled value='" + dt1[i].kmout + "' placeholder='KM OUT' class='form-control'></td>";
                        tbodyCliamdata += "<td><input type='text' ID='" + txtRemarks + "' value='" + dt1[i].remarks + "' placeholder='Remarks' class='form-control'></td>";
                        //tbodyCliamdata += "<td><input type='text' ID='" + txtClaimStatus + "' value='" + dt1[i].ClaimStatus + "' placeholder='Claim Status' class='form-control'></td>";
                        //tbodyCliamdata += "<td><select  class='form-control colorchg' id='" + txtClaimStatus + "'>'" + ClaimStatusOption + "' </select></td>";
                        //tbodyCliamdata += "<td><input type='text' ID='txtMRemarks' value='" + dt1[i].MRemarks + "' placeholder='Remarks' class='form-control'></td>";
                        tbodyCliamdata += "<td><input type='file' style='width:150px;' onchange='saveRouteFileOnServer(this)'   name='" + fileClaim + "' id='" + fileClaim + "' value='' class='local' /></td>";

                        tbodyCliamdata += "</tr>";
                        //$('#ClaimType1 option[value=' + dt1[i].PayoutTypeID + ']').attr("selected", 'true');//PayoutSubTypeID


                    };
                    $('#tbodyReimbursement').html(tbodyCliamdata);
                    $("#txtTotal").val(tsum);
                    $("#tfooter").css("display", "table-row");

                    $("#btndiv").css("display", "block");
                    //datebind();
                }
                else {
                    debugger
                    var formData = new FormData();
                    formData.append('Id', 1);
                    var _jsonddlbind = {};
                    _jsonddlbind.Id = 1;
                    var ClaimTypeOpt = '';
                    var ClaimSubTypeOpt = '';
                    $("#tbodyReimbursement tr").remove();

                    $.ajax({
                        //url: "../webpage/reimbursement.aspx/Bindddl",
                        url: "../Reimbursement/GetReimbursement",
                        type: 'post',
                        //contentType: 'application/json;charset=utf-8',
                        //dataType: 'json',
                        data: formData,
                        contentType: false,
                        processData: false,
                        /*data: JSON.stringify(_jsonddlbind),*/
                        success: function (data) {
                            debugger;
                            var obj = (JSON.parse(data.d));
                            var dt2 = obj.dtt;
                            var _SubType = obj.SubType;
                            var _ClaimSatus = data.ClaimSatus;
                            for (var j = 0; j <= dt2.length - 1; j++) {
                                ClaimTypeOpt = ClaimTypeOpt + '<option  value=' + dt2[j].id + '>' + dt2[j].PayoutType + '</option>';
                            }

                            if (obj.SubType != '') {
                                for (var k = 0; k <= _SubType.length - 1; k++) {
                                    ClaimSubTypeOpt = ClaimSubTypeOpt + '<option value=' + _SubType[k].ID + '>' + _SubType[k].PayoutSubType + '</option>';
                                }
                            }
                            //if (_ClaimSatus != '') {

                            //    ClaimStatusOption = '<option value =""> Select Claim Status</option>';
                            //    $(_ClaimSatus).each(function (i, value) {
                            //        debugger
                            //        if (selectedClaimSatusId == this.Id)
                            //            ClaimStatusOption = ClaimStatusOption + '<option selected="selected" value=' + this.Id + '>' + this.ClaimStatuss + '</option>';
                            //        else
                            //            ClaimStatusOption = ClaimStatusOption + '<option  value=' + this.Id + '>' + this.ClaimStatuss + '</option>';

                            //    });
                            //}
                            debugger;
                            var rowCount = $('#tblReimbursement tbody tr').length;
                            var txtDatetime = "datetime" + (rowCount + 1);
                            var ClaimType = "ClaimType" + (rowCount + 1);
                            var ddlClaimSubTypeId = "ClaimSubType" + (rowCount + 1);
                            var txtClaimAmt = "txtClaimAmt" + (rowCount + 1)
                            var txtKMIN = "txtKMIN" + (rowCount + 1);
                            var txtKMOut = "txtKMOut" + (rowCount + 1);
                            var txtRemarks = "txtRemarks" + (rowCount + 1);
                            //var txtClaimStatus = "txtClaimStatus" + (rowCount + 1);
                            var fileClaim = "fileClaim" + (rowCount + 1);
                            var Count = rowCount + 1;
                            var tsum = 0;
                            var newRow = "<tr>" +
                                "<th>" + Count + "</th>" +
                                "<td><select class='form-control' onchange='bindSutbtypeClaim(this)'     id='" + ClaimType + "'>" + ClaimTypeOpt + "</select></td>" +
                                "<td><select class='form-control' onchange='bindSutbtypedisable(this)'     id='" + ddlClaimSubTypeId + "'>" + ClaimSubTypeOpt + "</select></td>" +
                                "<td><input type='text' placeholder='click here' class='form-control'  data-field='datetime' readonly><div id='dtBox' class='dtBox'></td>" +
                                "<td><input class='form-control' type='text' id='" + txtClaimAmt + "' placeholder='Claim Amt'></td>" +
                                "<td><input class='form-control' type='text' id='" + txtKMIN + "' placeholder='KMIN'></td>" +
                                "<td><input class='form-control' type='text' id='" + txtKMOut + "' placeholder='KMOUT'></td>" +
                                "<td><input class='form-control' type='text' id='" + txtRemarks + "' placeholder='Remarks'></td>" +
                                //"<td><select  class='form-control colorchg' id='" + txtClaimStatus + "'>'" + ClaimStatusOption + "' </select></td>" +
                                // "<td><input class='form-control' type='text' id='" + txtMRemarks + "' placeholder='KMOUT'></td>" +
                                "<td><input class='form-control' type='file' id='" + fileClaim + "' ></td>" +
                                //"<td onclick='removerClaimRow(event,this)'><i class='fa fa-close btn btn-sm btn-danger'></i></td>" +
                                "</tr>";
                            $('#tblReimbursement tbody').append(newRow);
                            $("#txtTotal").val(tsum);
                            $("#tfooter").css("display", "table-row");
                            //datebind();
                            bgstatus();
                        }
                    });
                }

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


//Function for submit reimbursement //
function checkValidations() {
    debugger;
    var ClaimDetail = [];
    var formData = new FormData();
    var today = new Date();
    var errors = '';
    $('#divMessage').html('');
    $('#divMessage').empty();
    var Month = $("#month option:selected").val();
    var year = $("#year option:selected").html();
    var EmpId = $('#txtid').val();
    var EmpName = $('#txtempname').val();
    var PrevOutstadAmt = $('#txtOutstanding').val();
    var onOutstanding = $('#txtasOutstanding').val();

    if (typeof Month === "undefined" || Month == '' || Month == '0') {
        errors += '<div class="col-sm-3">Please select month.</div>';
        $("#ddlmounth").addClass(' error');
    } else {
        $("#ddlmounth").removeClass(' error');
    }

    if (typeof year === "undefined" || year == '' || year == '0') {
        errors += '<div class="col-sm-3">Please select year.</div>';
        $("#ddlyear").addClass(' error');
    } else {
        $("#ddlyear").removeClass(' error');
    }

    if (EmpId == '') {
        errors += '<div class="col-sm-3">please enter EmpId.</div>';
    } else {
        $("#txtid").removeClass(' error');
    }

    if (EmpName == '') {
        errors += '<div class="col-sm-3">please enter Emp Name.</div>';
    } else {
        $("#txtempname").removeClass(' error');
    }

    if (errors == '') {
        $('#tbodyReimbursement').find('tr').each(function () {
            debugger;

            var base64Data = null;
            var row = $(this);
            var _ClaimType = row.find("td").eq(1).find('option:selected').val();
            var _ClaimTypeName = row.find("td").eq(1).find('option:selected').html();
            var _ClaimTypeSub = row.find("td").eq(2).find('option:selected').val();
            var _ClaimSubTypeName = row.find("td").eq(2).find('option:selected').html();
            var invoiceDate = row.find("td").eq(3).find('input[type="datetime"]').val();
            var _ClaimAmt = row.find("td").eq(4).find('input[type="text"]').val();
            var _KMIN = row.find("td").eq(5).find('input[type="text"]').val();
            var _KMOUT = row.find("td").eq(6).find('input[type="text"]').val();
            var _Remarks = row.find("td").eq(7).find('input[type="text"]').val();
            //var fileUpload = row.find("td").eq(8).find('input[type="file"]').get(0);
            // var _fileUpload = row.find("td").eq(8).find('input[type="file"]').val();
            var ddddd = imagebase64;
            debugger
            if (_ClaimType > 0) {
                var d = new Date();
                debugger
                var claim = {
                    PayoutTypeID: _ClaimType,
                    PayoutSubTypeID: _ClaimTypeSub,
                    ClaimDate: String(d.getDate() + "-" + (d.getMonth() + 1) + "-" + d.getFullYear()),
                    InvoiceDate: invoiceDate,
                    ClaimAmt: _ClaimAmt,
                    PayoutType: _ClaimTypeName,
                    PayoutSubType: _ClaimSubTypeName,
                    KMIN: _KMIN,
                    KMOUT: _KMOUT,
                    Remarks: _Remarks,
                    FileData: ddddd
                };
                ClaimDetail.push(claim);

            }
        });

        // Add the ClaimDetail array to the formData
        formData.append('ClaimDetail', JSON.stringify(ClaimDetail));
        formData.append('Month', Month);
        formData.append('year', year);
        formData.append('EmpId', EmpId);
        formData.append('EmpName', EmpName);

        $.ajax({

            type: "POST",
            url: "../Reimbursement/SaveReimbursement",
            //data: JSON.stringify({ _jsonClaimData }),
            /*data: { 'ClaimDetail': ClaimDetail, 'Month': Month, 'year': year, 'EmpId': EmpId, 'EmpName': EmpName },*/
            data: formData,
            contentType: false,
            processData: false,
            success: function (data) {
                debugger;
                if (data == "success") {
                    debugger;

                    setTimeout(function () {
                        GetInOutTime();
                    }, 2000);
                    $("#spanSuccess").addClass('text-success').html();
                    $('#txt').html("Reimbursement Data successfully saved..");
                    $('#successModal').modal('show');
                    //location.reload();
                }
                else {
                    $("#msgerror").addClass('text-danger').html('');
                    $('#txtwar').html(result);
                    $('#warningModal').modal('show');
                }
            }
        });
    } else {
        $('#divMessage').append(errors);
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

function removerClaimRow(e, control) {
    $(control).closest("tr").remove();
    e.preventDefault();
}

function bindSutbtypedisable(control) {
    debugger
    var row = $(control).closest('tr');
    var ind = row[0].rowIndex;
    var _ClaimType = row.find('td:eq(1) select ');
    var _ClaimSubType = $(row).find("td").eq(2).find('option:selected').html();
    // var _ClaimSubType = $(rows).find("td").eq(2).find('option:selected').html();
    //var ind = control.closest('tr').rowIndex;
    //var ddlClaimSubTypeId = "ClaimSubType" + ind;
    //$('#' + ddlClaimSubTypeId).empty();
    //$('#' + ddlClaimSubTypeId + 'option:selected').remove();
    //var ddlid = control.id;
    debugger;
    // var row_index = $(this).parent().index('tr');
    var ddlClaimTypeId = "ClaimType" + ind;
    var txtClaimAmt = "txtClaimAmt" + ind;
    var txtKMIN = "txtKMIN" + ind;
    var txtKMOut = "txtKMOut" + ind;
    $("#tblReimbursement tbody tr").each(function () {
        debugger;
        var currentRow = $(this);
        var currentIndex = currentRow[0].rowIndex;
        //var rows = $(this);
        //var index = this.rowIndex;
        // var _ClaimType = $(rows).find("td").eq(1).find('option:selected').val();
        // var _ClaimSubType = $(rows).find("td").eq(2).find('option:selected').html();

        if (ind == currentIndex) {
            // var selectedClaimType = _ClaimSubType.val();
            if (_ClaimSubType != '' && _ClaimSubType != "undefined") {
                if (_ClaimSubType == 'Fuel') {
                    debugger
                    $("#" + txtKMIN).prop("enable", true);
                    $("#" + txtKMOut).prop("enable", true);
                }
                else {
                    $("#" + txtKMIN).prop("disabled", true);
                    $("#" + txtKMOut).prop("disabled", true);
                }
            }
            else {
                $("#" + ddlClaimSubTypeId).empty();
            }

        }

    });


};

var imagebase64 = "";

function encodeImageFileAsURL(element) {
    debugger
    var file = element.files[0];
    var reader = new FileReader();
    reader.onloadend = function () {
        debugger
        imagebase64 = reader.result;
    }
    reader.readAsDataURL(file);
}

function saveRouteFileOnServer(element) {
    debugger;//input#fileClaim1 {accept: "", alt: "", autocomplete: "", …}
    var file = element.files[0];
    var reader = new FileReader();
    reader.onloadend = function () {
        debugger;
        imagebase64 = reader.result;
        ////var a = imagebase64.Replace("data:image/png;base64,", "");
        //$("#hfBase64").val(imagebase64);
    }
    //return imagebase64;
    reader.readAsDataURL(file);
}

function Base64ToBytes(base64) {

    debugger;
    var s = window.atob(base64);
    var bytes = new Uint8Array(s.length);
    for (var i = 0; i < s.length; i++) {
        bytes[i] = s.charCodeAt(i);
    }
    return bytes;
};

function bgstatus() {

    $(".colorchg option").each(function (i, value) {

        var ss = this.value; //+ ',' + this.text
        //if (ss == "1") {
        //    $(".colorchg option:selected").css('background-color', 'green');
        //    //$('#mOptions').css('background-color', '#0f0');
        //}
        if ($(this).val() == 1) {
            $(this).css({ 'background': '#28a745' });
        }
        else if ($(this).val() == 2) {
            $(this).css({ 'background': '#ffc107' });
        }
        else if ($(this).val() == 3) {
            $(this).css({ 'background': '#dc3545' });
        }

    });

}

function TotalAmt(control) {

    $("#txtTotal").val('');
    var sum = 0;
    $('.totalprice').each(function () {

        sum += parseFloat($(this).val());  // Or this.innerHTML, this.innerText

    })

    $("#txtTotal").val(sum);
}
function getyear() {
    debugger
    //$('#year').empty();
    //$('#year').append($("<option></option>").val('').html('Select year'));
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
                });
                //select the current year by default //
                var currentyear = new Date().getFullYear().toString();
                /* $("#year").val(currentyear);*/
                $("#year option[value='" + currentyear + "']").prop("selected", true);

                $("#year").prop("disabled", false);

            }
        },
        error: function (data, error) {
            debugger
            $("#spanMessage").addClass('text-danger').html(data);
        }

    });
}
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
                });
                //select the current year by default //
                //const moonLanding = new Date('December 20, 69 00:20:18');
                var currentDate = new Date();
                var currentYear = currentDate.getFullYear();
                var currentMonth = (currentDate.getMonth() + 1).toString(); // Months are zero-based, so add 1

                // Pad the month with a leading zero if necessary
                currentMonth = currentMonth.length === 1 ? '' + currentMonth : currentMonth;

                var currentMonthYear = currentMonth;

                $("#month option[value='" + currentMonthYear + "']").prop("selected", true);

                $("#month").prop("disabled", false);
            }
        },
        error: function (data, error) {
            debugger
            $("#spanMessage").addClass('text-danger').html(data);
        }

    });
}

//function validatePreviousRow() {
//    debugger
//    var isValid = true;
//    var prevrow = $('#tblReimbursement tbody tr:last');
//    prevrow.find('input,select').each(function () {
//        var value = $(this).val();

//        if (!value.trim()) {
//            isValid = false;
//            return false;
//        }

//    });
//    return isValid;
//}

function validatePreviousRow() {
    debugger;
    var isValid = true;

    // Get all rows except the last one
    var rows = $('#tblReimbursement tbody tr:not(:last)');

    // Iterate over each row
    rows.each(function () {
        var row = $(this);

        // Check each input/select element in the row
        row.find('input, select').each(function () {
            var value = $(this).val();

            // If any value is missing, set isValid to false and exit the loop
            if (!value.trim()) {
                isValid = false;
                return false;
            }
        });

        // If isValid is already false, exit the loop
        if (!isValid) {
            return false;
        }
    });

    return isValid;
}


$('#btnAddMore').on('click', function (e) {
    debugger
    var isPreviousrow = 'true';
    var rowCount = $('#tblReimbursement tbody tr').length;
    if (rowCount > 0) {
        if (isPreviousrow) {
            debugger;
            var newRow = $("<tr></tr>");

            var txtDatetime = "datetime" + (rowCount + 1);
            var ClaimType = "ClaimType" + (rowCount + 1);
            var ddlClaimSubTypeId = "ClaimSubType" + (rowCount + 1);
            var txtClaimAmt = "txtClaimAmt" + (rowCount + 1);
            var txtKMIN = "txtKMIN" + (rowCount + 1);
            var txtKMOut = "txtKMOut" + (rowCount + 1);
            var txtRemarks = "txtRemarks" + (rowCount + 1);
            var fileClaim = "fileClaim" + (rowCount + 1);

            var Count = rowCount + 1;
            var ClaimTypeOptionAdd = '';
            var ClaimSubTypeOptionAdd = '';

            $('#ClaimType1 option').each(function (i, value) {
                debugger;
                var ss = this.value + ',' + this.text;
                ClaimTypeOptionAdd = ClaimTypeOptionAdd + '<option value=' + this.value + '>' + this.text + '</option>';
            });

            $("#ClaimSubType1 option").each(function (i, value) {
                debugger;
                var ss = this.value + ',' + this.text;
                ClaimSubTypeOptionAdd = ClaimSubTypeOptionAdd + '<option value=' + this.value + '>' + this.text + '</option>';
            });

            var newRowHTML = "<td>" + Count + "</td>" +
                "<td><select class='form-control ClaimType' onchange='bindSutbtypeClaim(this)' id='" + ClaimType + "'>" + ClaimTypeOptionAdd + "</select></td>" +
                "<td><select class='form-control ClaimSubType' onchange='bindSutbtypedisable(this)' id='" + ddlClaimSubTypeId + "'>" + ClaimSubTypeOptionAdd + "</select></td>" +
                "<td><input type='datetime'  placeholder='click here' class='form-control datepicker' data-field='datetime'  id='" + txtDatetime + "' ></td>" +
                // "<td><div id='date-picker - example' class='md - form md - outline input -with-post - icon datepicker' inline='true'><input placeholder='Select date' type='text' id='' class='form-control' ></div> <i class='fas fa-calendar input - prefix'></i></td>"
                "<td><input class='form-control totalprice' onkeyup='TotalAmt(this)' type='text' id='" + txtClaimAmt + "' placeholder='Claim Amt'></td>" +
                "<td><input class='form-control' type='text' id='" + txtKMIN + "' placeholder='KMIN'></td>" +
                "<td><input class='form-control' type='text' id='" + txtKMOut + "' placeholder='KMOUT'></td>" +
                "<td><input class='form-control' type='text' id='" + txtRemarks + "' placeholder='Remarks'></td>" +
                "<td><input class='form-control' type='file' onchange='encodeImageFileAsURL(this)' id='" + fileClaim + "' ></td>" +
                "<td onclick='removerClaimRow(event,this)'><i class='fa fa-close btn btn-sm btn-danger'></i></td>";

            newRow.html(newRowHTML);
            $('#tblReimbursement tbody').append(newRow);
            $("#" + txtDatetime).datepicker({
                dateFormat: "yy-mm-dd",
                changeMonth: true,
                changeYear: true,
                //minDate: dateText
                timeFormat: "hh:mm TT"
            });
            e.preventDefault();
        }
        else {
            e.preventDefault();
            alert("Please fill all the fields in the previous row before adding a new row.");
        }
    }
    else {

        var _jsonddlbind = {};
        _jsonddlbind.Id = 1;
        var ClaimTypeOpt = '';
        var ClaimSubTypeOpt = '';


        $("#tbodyReimbursement tr").remove();

        $.ajax({
            url: "../Reimbursement/Bindddl",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(_jsonddlbind),
            context: document.body,
            success: function (data) {
                debugger;

                var dt2 = data.dtt;
                var _SubType = data.subType;
                var _ClaimSatus = data.claimSatus;

                ClaimTypeOpt = '<option value="">Select Claim Type</option>';
                ClaimSubTypeOpt = '<option value="">Select Claim SubType</option>';

                for (var j = 0; j <= dt2.length - 1; j++) {
                    debugger;
                    ClaimTypeOpt = ClaimTypeOpt + '<option  value=' + dt2[j].id + '>' + dt2[j].payoutType + '</option>';
                }

                if (data.SubType != '') {
                    debugger;
                    for (var k = 0; k <= _SubType.length - 1; k++) {
                        ClaimSubTypeOpt = ClaimSubTypeOpt + '<option value=' + _SubType[k].id + '>' + _SubType[k].payoutSubType + '</option>';
                    }
                }
                debugger
                var rowCount = $('#tblReimbursement tbody tr').length;
                var txtDatetime = "datetime" + (rowCount + 1);
                var ClaimType = "ClaimType" + (rowCount + 1);
                var ddlClaimSubTypeId = "ClaimSubType" + (rowCount + 1);
                var txtClaimAmt = "txtClaimAmt" + (rowCount + 1)
                var txtKMIN = "txtKMIN" + (rowCount + 1);
                var txtKMOut = "txtKMOut" + (rowCount + 1);
                var txtRemarks = "txtRemarks" + (rowCount + 1);
                /*var txtClaimStatus = "txtClaimStatus" + (rowCount + 1);*/
                var fileClaim = "fileClaim" + (rowCount + 1);
                var Count = rowCount + 1;
                var tsum = 0;
                var newRowHTML = "<td>" + Count + "</td>" +
                    "<td><select class='form-control ClaimType' onchange='bindSutbtypeClaim(this)' id='" + ClaimType + "'>" + ClaimTypeOpt + "</select></td>" +
                    "<td><select class='form-control ClaimSubType' onchange='bindSutbtypedisable(this)' id='" + ddlClaimSubTypeId + "'>" + ClaimSubTypeOpt + "</select></td>" +
                    "<td><input type='datetime' placeholder='click here' class='form-control' data-field='datetime'  id='" + txtDatetime + "'></td>" +
                    "<td><input class='form-control totalprice' type='text' onkeyup='TotalAmt(this)' id='" + txtClaimAmt + "' placeholder='Claim Amt'></td>" +
                    "<td><input class='form-control' type='text' id='" + txtKMIN + "' placeholder='KMIN'></td>" +
                    "<td><input class='form-control' type='text' id='" + txtKMOut + "' placeholder='KMOUT'></td>" +
                    "<td><input class='form-control' type='text' id='" + txtRemarks + "' placeholder='Remarks'></td>" +
                    "<td><input class='form-control' type='file' onchange='encodeImageFileAsURL(this)' id='" + fileClaim + "' ></td>";

                var newRow = $("<tr></tr>").append(newRowHTML);

                $('#tblReimbursement tbody').append(newRow);
                $("#" + txtDatetime).datepicker({
                    dateFormat: "yy-mm-dd",
                    changeMonth: true,
                    changeYear: true,
                    //minDate: dateText
                    timeFormat: "hh:mm TT"
                });
                $("#txtTotal").val(tsum);
                $("#tfooter").css("display", "table-row");
                $("#btndiv").css("display", "block");
            }
        });
    }
});
    
function bindSutbtypeClaim(control) {
    debugger;
    var row = $(control).closest('tr');
    var ind = row[0].rowIndex;
    var claimTypeSelect = row.find('td:eq(1) select');
    var ddlClaimSubType = row.find('td:eq(2) select');

    $("#tblReimbursement tbody tr").each(function () {
        debugger
        var currentRow = $(this);
        var currentIndex = currentRow[0].rowIndex;

        if (ind === currentIndex) {
            var selectedClaimType = claimTypeSelect.val();
            var ddlClaimSubTypeId = "ClaimSubType" + currentIndex;
            var claimSubTypeDropdown = currentRow.find('td:eq(2) select');

            if (selectedClaimType !== '' && selectedClaimType !== undefined) {
                claimSubTypeDropdown.empty();
                debugger
                var formData = new FormData();
                formData.append('ClaimType', selectedClaimType);

                $.ajax({
                    url: "../Reimbursement/bindSutbtypeClaim",
                    type: "POST",
                    data: formData,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        claimSubTypeDropdown.append('<option value="">Select Sub Type</option>');
                        debugger
                        if (data !== '') {
                            $.each(data.lstSubClaimType, function (index, value) {
                                debugger
                                claimSubTypeDropdown.append("<option value=" + value.id + ">" + value.payoutSubType + "</option>");
                            });
                        }
                    },
                    error: function (xhr) {
                        claimSubTypeDropdown.empty();
                        claimSubTypeDropdown.prop("disabled", true);
                    }
                });
            } else {
                claimSubTypeDropdown.empty();
            }
        }
    });
}





