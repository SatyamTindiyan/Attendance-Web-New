
//$(document).ready(function () {
//    getyear()
//    getmonth()
//});

//$('#claimSubType').on('change', function () {
//    debugger
//    var ClaimSubTypeId = $("#claimSubType option:selected").val();
//    var claimTypeId = $("#claimType option:selected").val();
//    if (claimTypeId == 3 && ClaimSubTypeId == 12) {
//        $('#kmIN').empty();
//        $('#kmIN').prop("disabled", false);
//        $('#kmOUT').prop("disabled", false);

//    }
//    else {
//        $('#kmIN').empty();
//        $('#kmOUT').empty();
//        $('#kmIN').prop("disabled", true);
//        $('#kmOUT').prop("disabled", true);
//    }
//});
//function GetCalaimRequestDetails() {
//    debugger
//    var _jsonddlbind = {};
//    _jsonddlbind.Id = 1;
//    var ClaimTypeOpt = '';
//    var ClaimSubTypeOpt = '';
//    /*$("#tbodyReimbursement tr").remove();*/

//    var errors = '';
//    var Month = $("#ddlmounth option:selected").val();
//    var year = $("#ddlYears option:selected").html();
//    var EmpId = $('#txtid').val();
//    $('#divMessage').html('');
//    $('#divMessage').empty();
//    $('#kmIN').addClass('disabled');
//    //var EmpName = $('#txtempname').val();
//    //var PrevOutstadAmt = $('#txtOutstanding').val();
//    //var onOutstanding = $('#txtasOutstanding').val();
//    //$("#tblReimbursement tr").detach();
//    /* $("#tblReimbursement > tr").remove();*/
//    /*  $("#tblReimbursement tr").remove();*/
//    if (typeof Month === "undefined" || Month == '' || Month == '0') {
//        errors += '<div class="col-sm-3">Please select month.</div>';
//        $("#ddlmounth").addClass(' error');
//    }
//    else {
//        $("#ddlmounth").removeClass(' error');
//    }
//    if (typeof year === "undefined" || year == '' || year == '0') {
//        errors += '<div class="col-sm-3">Please select year.</div>';
//        $("#ddlyear").addClass(' error');
//    }
//    else {
//        $("#ddlyear").removeClass(' error');
//    }
//    if (EmpId == '')
//        errors += '<div class="col-sm-3">please enter EmpId.</div>';
//    else {
//        $("#txtid").removeClass(' error');
//    }
//    var formData = new FormData();
//    var _jsonFetchData = {};
//    //_jsonFetchData.ClaimDetail = ClaimDetail;
//    _jsonFetchData.Month = Month;
//    _jsonFetchData.year = year;
//    _jsonFetchData.EmpId = EmpId;
//    formData.append('Month', Month);
//    formData.append('year', year);
//    formData.append('EmpId', EmpId);

//    $.ajax({
//        //url: "../webpage/reimbursement.aspx/Bindddl",
//        url: "../Reimbursement/GetReimbursement",
//        type: 'post',
//        //contentType: 'application/json;charset=utf-8',
//        //dataType: 'json',
//        //data: JSON.stringify(_jsonFetchData),
//        data: formData,
//        contentType: false,
//        processData: false,
//        success: function (data) {
//            debugger
//            var obj = data.dt;
//            var dt2 = data.dtt;
//            var _SubType = data.subType;
//            var _ClaimSatus = data.claimSatus;

//            $('#claimType').append("<option>Select ClaimType</option>");
//            for (var j = 0; j <= dt2.length - 1; j++) {

//                /* ClaimTypeOpt = ClaimTypeOpt + '<option  value=' + dt2[j].id + '>' + dt2[j].PayoutType + '</option>';*/
//                $('#claimType').append($("<option></option>").val(dt2[j].id).text(dt2[j].payoutType));
//                var cliamTypeId = dt2[j].id;
//            }

//            if (obj.SubType != '') {
//                $('#claimSubType').append($("<option>Select SubClaimType </option>"));
//                for (var k = 0; k <= _SubType.length - 1; k++) {
//                    /*ClaimSubTypeOpt = ClaimSubTypeOpt + '<option value=' + _SubType[k].ID + '>' + _SubType[k].PayoutSubType + '</option>';*/
//                    $('#claimSubType').append($("<option></option>").val(_SubType[k].id).text(_SubType[k].payoutSubType));

//                }
//            }
//            if (_ClaimSatus != '') {
//                $('#ddlclaimStatus').append($("<option>Select Claim Status</option>"));
//                for (var m = 0; m <= _ClaimSatus.length - 1; m++) {

//                    /*ClaimSubTypeOpt = ClaimSubTypeOpt + '<option value=' + _SubType[k].ID + '>' + _SubType[k].PayoutSubType + '</option>';*/
//                    $('#ddlclaimStatus').append($("<option</option>").val(_ClaimSatus[m].id).text(_ClaimSatus[m].claimStatuss));
//                }

//                //    ClaimStatusOption = '<option value =""> Select Claim Status</option>';
//                //    $(_ClaimSatus).each(function (i, value) {
//                //        if (selectedClaimSatusId == this.Id)
//                //            ClaimStatusOption = ClaimStatusOption + '<option selected="selected" value=' + this.Id + '>' + this.ClaimStatuss + '</option>';
//                //        else
//                //            ClaimStatusOption = ClaimStatusOption + '<option  value=' + this.Id + '>' + this.ClaimStatuss + '</option>';

//                //    });
//            }

//            $('#requestClaimModal').modal('show');
//            //var rowCount = $('#tblReimbursement tbody tr').length;
//            //var txtDatetime = "datetime" + (rowCount + 1);
//            //var ClaimType = "ClaimType" + (rowCount + 1);
//            //var ddlClaimSubTypeId = "ClaimSubType" + (rowCount + 1);
//            //var txtClaimAmt = "txtClaimAmt" + (rowCount + 1)
//            //var txtKMIN = "txtKMIN" + (rowCount + 1);
//            //var txtKMOut = "txtKMOut" + (rowCount + 1);
//            //var txtRemarks = "txtRemarks" + (rowCount + 1);
//            //var txtClaimStatus = "txtClaimStatus" + (rowCount + 1);
//            //var fileClaim = "fileClaim" + (rowCount + 1);
//            //var Count = rowCount + 1;
//            //var tsum = 0;
//            //var newRow = "<tr>" +
//            //    "<th>" + Count + "</th>" +
//            //    "<td colspan='9'><h6>No Records Found</h6></td>" +
//            //    "<td hidden><select class='form-control' onchange='bindSutbtypeClaim(this)'     id='" + ClaimType + "'>" + ClaimTypeOpt + "</select><input type='hidden' value='" + ClaimType + "' id='ddllaimTypeIdOptional'/></td>" +
//            //    "<td hidden><select class='form-control' onchange='bindSutbtypedisable(this)'     id='" + ddlClaimSubTypeId + "'>" + ClaimSubTypeOpt + "</select><input type='hidden' value='" + ddlClaimSubTypeId + "' id='ddllaimSubTypeIdOptional'/></td>" +
//            //    "<td hidden><input type='text' placeholder='click here' class='form-control'  data-field='datetime' readonly><div id='dtBox' class='dtBox'></td>" +
//            //    "<td hidden><input class='form-control' type='text' id='" + txtClaimAmt + "' placeholder='Claim Amt'></td>" +
//            //    "<td hidden><input class='form-control' type='text' id='" + txtKMIN + "' placeholder='KMIN'></td>" +
//            //    "<td hidden><input class='form-control' type='text' id='" + txtKMOut + "' placeholder='KMOUT'></td>" +
//            //    "<td hidden><input class='form-control' type='text' id='" + txtRemarks + "' placeholder='Remarks'></td>" +
//            //   /* "<td hidden><select  class='form-control colorchg' id='" + txtClaimStatus + "'>'" + ClaimStatusOption + "' </select><input type='hidden' value='" + txtClaimStatus + "' id='ddllaimStatusIdOptional'/></td>" +*/
//            //    // "<td><input class='form-control' type='text' id='" + txtMRemarks + "' placeholder='KMOUT'></td>" +
//            //    "<td hidden><input class='form-control' type='file' id='" + fileClaim + "' ></td>" +
//            //    /*"<td><button class='btn btn-light btn - sm' id='AddClaim' onclick='GetCalaimRequestDetails()'>Add Claim</button></td>" +*/
//            //    //"<td onclick='removerClaimRow(event,this)'><i class='fa fa-close btn btn-sm btn-danger'></i></td>" +
//            //    "</tr>";
//            //$('#tblReimbursement tbody').append(newRow);
//            //$("#txtTotal").val(tsum);
//            //$("#tfooter").css("display", "table-row");
//            //datebind();
//            /* bgstatus();*/
//        }
//    });

//    var ClaimAmount = $('#ClaimAmount').val();
//    var KMIN = $('#KMIN').val();
//    var KMOUT = $('#KMOUT').val();
//    var Remarks = $('#Remarks').val();
//    var ClaimStatus = $('#ClaimStatus').val();

//    var ddllaimTypeId = $('#ddllaimTypeId').val();
//    var ddlClaimTypeId = $('#ddllaimTypeIdOptional').val();
//    var ddlClaimSubTypeIdOptional = $('#ddllaimSubTypeIdOptional').val();
//    var ddllaimStatusIdOptional = $('#ddllaimStatusIdOptional').val();


//    /* = $('#ddllaimTypeIdOptional').val();*/


//    var ClaimTypeOption = $('#ClaimTypeOption').val();
//    var ddlClaimSubTypeId = $('#ddlClaimSubTypeId').val();
//    var ClaimStatusId = $('#ClaimStatusId').val();
//    var fileClaimId = $('#fileClaimId').val();

//    //$('#claimStatus').append($("<option></option>").val().html(value.ClaimSatus));
//    //$('#claimType').append($("<option></option>").val(value.ddllaimTypeId).html(value.ClaimTypeOption));
//    //  $('#claimType').append($("<option></option>").attr({ value: ddllaimTypeId, selected: "selected" }).text(ClaimTypeOption));
//    // $("#claimType").val(ddllaimTypeId);
//    // $("#claimType").text(ClaimTypeOption);
//    //$('#clamSubType').append($("<option></option>").val().html(value.ClaimSatus));

//    $('#' + ddlClaimTypeId + ' ' + 'option').each(function (i, value) {
//        debugger
//        var ss = this.value + ',' + this.text
//        //ClaimTypeOptionAdd = ClaimTypeOptionAdd + '<option value=' + this.value + '>' + this.text + '</option>';
//        $('#claimType').append($("<option></option>").val(this.value).text(this.text));

//    });

//    $('#' + ddllaimTypeId + ' ' + 'option').each(function (i, value) {
//        debugger
//        var ss = this.value + ',' + this.text
//        //ClaimTypeOptionAdd = ClaimTypeOptionAdd + '<option value=' + this.value + '>' + this.text + '</option>';
//        $('#claimType').append($("<option></option>").val(this.value).text(this.text));

//    });


//    $('#' + ddlClaimSubTypeIdOptional + ' ' + 'option').each(function (i, value) {
//        debugger
//        var ss = this.value + ',' + this.text
//        //ClaimTypeOptionAdd = ClaimTypeOptionAdd + '<option value=' + this.value + '>' + this.text + '</option>';
//        $('#clamSubType').append($("<option></option>").val(this.value).text(this.text));

//    });
//    $('#' + ddlClaimSubTypeId + ' ' + 'option').each(function (i, value) {
//        debugger
//        var ss = this.value + ',' + this.text
//        //ClaimTypeOptionAdd = ClaimTypeOptionAdd + '<option value=' + this.value + '>' + this.text + '</option>';
//        $('#clamSubType').append($("<option></option>").val(this.value).text(this.text));
//    });

//    $('#' + ddllaimStatusIdOptional + ' ' + 'option').each(function (i, value) {
//        debugger
//        var ss = this.value + ',' + this.text
//        //ClaimTypeOptionAdd = ClaimTypeOptionAdd + '<option value=' + this.value + '>' + this.text + '</option>';
//        $('#ddlclaimStatus').append($("<option></option>").val(this.value).text(this.text));
//    });

//    $('#' + ClaimStatusId + ' ' + 'option').each(function (i, value) {
//        debugger
//        var ss = this.value + ',' + this.text
//        //ClaimTypeOptionAdd = ClaimTypeOptionAdd + '<option value=' + this.value + '>' + this.text + '</option>';
//        $('#ddlclaimStatus').append($("<option></option>").val(this.value).text(this.text));
//    });

//    $('#remarks').val(Remarks);
//    $('#KMOUT').val(KMOUT);
//    $('#KMIN').val(KMIN);
//    $('#txtclaimAmount').val(ClaimAmount);
//    //$('#fileClaim').val(fileClaimId);
//    /* $('#requestClaimModal').modal('show');*/
//}
//function HideModelPopup() {
//    debugger
//    $('#requestClaimModal').modal('hide');
//}
//$('#btnFetchData').click(function () {
//    debugger
//    var errors = '';
//    var Month = $("#month option:selected").val();
//    /*Month = 4;*/
//    var year = $("#year option:selected").html();
//    /*year = 2023*/
//    var EmpId = $('#txtid').val();
//    $('#divMessage').html('');
//    $('#divMessage').empty();
//    //var EmpName = $('#txtempname').val();
//    //var PrevOutstadAmt = $('#txtOutstanding').val();
//    //var onOutstanding = $('#txtasOutstanding').val();
//    //$("#tblReimbursement tr").detach();
//    /* $("#tblReimbursement > tr").remove();*/
//    /*  $("#tblReimbursement tr").remove();*/
//    if (typeof Month === "undefined" || Month == '' || Month == '0') {
//        errors += '<div class="col-sm-3">Please select month.</div>';
//        $("#month").addClass(' error');
//    }
//    else {
//        $("#month").removeClass(' error');
//    }
//    if (typeof year === "undefined" || year == '' || year == '0') {
//        errors += '<div class="col-sm-3">Please select year.</div>';
//        $("#year").addClass(' error');
//    }
//    else {
//        $("#year").removeClass(' error');
//    }
//    if (EmpId == '')
//        errors += '<div class="col-sm-3">please enter EmpId.</div>';
//    else {
//        $("#txtid").removeClass(' error');
//    }


//    if (errors == '') {
//        var _jsonFetchData = {};
//        var formData = new FormData();
//        _jsonFetchData.Month = Month;
//        _jsonFetchData.year = year;
//        _jsonFetchData.EmpId = EmpId;

//        formData.append('Month', Month);
//        formData.append('year', year);
//        formData.append('EmpId', EmpId);
//        var rowCount = $('#tblReimbursement tbody tr').length
//        var ClaimTypeOption = '';
//        var ClaimSubTypeOption = '';
//        var ClaimStatusOption = '';
//        $.ajax({

//            url: "../Reimbursement/GetReimbursement",
//            type: 'POST',
//            //contentType: "application/json; charset=utf-8",
//            //data: JSON.stringify(_jsonFetchData),
//            //context: document.body,
//            data: formData,
//            contentType: false,
//            processData: false,
//            success: function (data) {
//                debugger
//                var tbodyCliamdata = "";
//                var count = 0;
//                var tsum = 0;
//                //var obj = (JSON.parse(data.d));
//                var dt1 = data.dt;
//                var dt2 = data.dtt;
//                var _SubType = data.subType;
//                var _ClaimSatus = data.claimSatus;
//                let ms = Date.parse("March 21, 2012");

//                if (dt1.length > 0) {

//                    for (var i = 0; i <= dt1.length - 1; i++) {

//                        var selectedId = data.dt[i].payoutTypeID;
//                        var selectedSubId = data.dt[i].payoutSubTypeID;
//                        var selectedClaimSatusId = data.dt[i].claimStatus;
//                        var dt = data.dt[i].claimDate;
//                        /*var dt = calEvent.start;*/
//                        var dateString = (dt).substr(6);
//                        var currentTime = new Date(parseInt(dateString));
//                        var month = currentTime.getMonth() + 1;
//                        var day = currentTime.getDate();
//                        var year = currentTime.getFullYear();
//                        var date = day + "/" + month + "/" + year;

//                        var fullDate = new Date(parseInt(dt.substr(6)));
//                        var rowCountS = i + 1;
//                        var txtClaimAmt = "txtClaimAmt" + rowCountS;
//                        var txtKMIN = "txtKMIN" + rowCountS;
//                        var txtKMOut = "txtKMOut" + rowCountS;
//                        var ddlClaimTypeId = "ClaimType" + rowCountS;
//                        var ddlClaimSubTypeId = "ClaimSubType" + rowCountS;
//                        var txtRemarks = "txtRemarks" + rowCountS;
//                        var txtClaimStatus = "txtClaimStatus" + rowCountS;
//                        var fileClaim = "fileClaim" + rowCountS;

//                        count = count + 1;
//                        tsum += dt1[i].claimAmount
//                        tbodyCliamdata += "<tr>";
//                        tbodyCliamdata += "<td>" + count + "</td>";
//                        tbodyCliamdata += "<td>" + dt1[i].payoutType + "</td>";
//                        tbodyCliamdata += "<td class='col-desktop'>" + dt1[i].payoutSubType + "</td>";

//                        tbodyCliamdata += "<td>" + dt1[i].claimDate + "</td>";


//                        tbodyCliamdata += "<td>" + dt1[i].claimAmount + "</td>";

//                        tbodyCliamdata += "<td class='col-desktop'>" + dt1[i].kmin + "</td>";
//                        tbodyCliamdata += "<td class='col-desktop'>" + dt1[i].kmout + "</td>";
//                        tbodyCliamdata += "<td class='col-desktop'>" + dt1[i].remarks + "</td>";

//                        tbodyCliamdata += "<td>" + dt1[i].claimStatuss + "</td>";

//                        tbodyCliamdata += "<td class='col-desktop'><img src='" + dt1[i].images + "' style='max-width:50px; max-height:50px;' name='" + fileClaim + "' id='" + fileClaim + "'></td>";
//                        tbodyCliamdata += "<td class='col-mobile'><button id='btnFetchData' class='btn btn-sm btn-info' onclick='GetCalaimRequestDetails(" + dt1[i].id + ")'><i class='fa fa-eye'></i></button></td >";

//                        tbodyCliamdata += "</tr>";


//                    };
//                    $('#tbodyReimbursement').html(tbodyCliamdata);
//                    $("#txtTotal").val(tsum);
//                    $("#tfooter").css("display", "table-row");

//                    $("#btndiv").css("display", "block");
//                    //datebind();
//                }
//                else {
//                    debugger
//                    var _jsonddlbind = {};
//                    _jsonddlbind.Id = 1;
//                    var formbind = 0;
//                    formbind.Id = 1;
//                    var ClaimTypeOpt = '';
//                    var ClaimSubTypeOpt = '';
//                    $("#tbodyReimbursement tr").remove();

//                    var errors = '';
//                    var Month = $("#ddlmounth option:selected").val();
//                    /*Month = 4*/
//                    var year = $("#ddlYears option:selected").val();
//                    /*year = 2023*/
//                    var EmpId = $('#txtid').val();
//                    $('#divMessage').html('');
//                    $('#divMessage').empty();
//                    //var EmpName = $('#txtempname').val();
//                    //var PrevOutstadAmt = $('#txtOutstanding').val();
//                    //var onOutstanding = $('#txtasOutstanding').val();
//                    //$("#tblReimbursement tr").detach();
//                    /* $("#tblReimbursement > tr").remove();*/
//                    /*  $("#tblReimbursement tr").remove();*/
//                    if (typeof Month === "undefined" || Month == '' || Month == '0') {
//                        errors += '<div class="col-sm-3">Please select month.</div>';
//                        $("#ddlmounth").addClass(' error');
//                    }
//                    else {
//                        $("#ddlmounth").removeClass(' error');
//                    }
//                    if (typeof year === "undefined" || year == '' || year == '0') {
//                        errors += '<div class="col-sm-3">Please select year.</div>';
//                        $("#ddlyear").addClass(' error');
//                    }
//                    else {
//                        $("#ddlyear").removeClass(' error');
//                    }
//                    if (EmpId == '')
//                        errors += '<div class="col-sm-3">please enter EmpId.</div>';
//                    else {
//                        $("#txtid").removeClass(' error');
//                    }

//                    var _jsonFetchData = {};
//                    var formData = new FormData();
//                    //_jsonFetchData.ClaimDetail = ClaimDetail;
//                    _jsonFetchData.Month = Month;
//                    _jsonFetchData.year = year;
//                    _jsonFetchData.EmpId = EmpId;
//                    formData.append("Month", Month);
//                    formData.append("year", year);
//                    formData.append("EmpId", EmpId);

//                    $.ajax({
//                        //url: "../webpage/reimbursement.aspx/Bindddl",
//                        url: "../Reimbursement/GetReimbursement",
//                        type: 'post',
//                        //contentType: 'application/json;charset=utf-8',
//                        //dataType: 'json',
//                        //data: JSON.stringify(_jsonFetchData),
//                        data: formData,
//                        contentType: false,
//                        processData: false,
//                        success: function (data) {
//                            debugger
//                            var obj = data.dt;
//                            var dt2 = data.dtt;
//                            var _SubType = data.subType;
//                            var _ClaimSatus = data.claimSatus;
//                            if (dt2 != '') {
//                                ClaimTypeOpt = '<option value =""> Select Claim Type</option>';
//                                for (var j = 0; j <= dt2.length - 1; j++) {
//                                    ClaimTypeOpt = ClaimTypeOpt + '<option  value=' + dt2[j].id + '>' + dt2[j].payoutType + '</option>';
//                                }
//                            }
//                            //if (obj.SubType != '') {
//                            //    ClaimSubTypeOpt = '<option value =""> Select Claim SubType</option>';
//                            //    for (var k = 0; k <= _SubType.length - 1; k++) {
//                            //        ClaimSubTypeOpt = ClaimSubTypeOpt + '<option value=' + _SubType[k].ID + '>' + _SubType[k].PayoutSubType + '</option>';
//                            //    }
//                            //}
//                            if (_ClaimSatus != '') {
//                                debugger
//                                ClaimStatusOption = '<option value =""> Select Claim Status</option>';
//                                $(_ClaimSatus).each(function (i, value) {
//                                    if (selectedClaimSatusId == this.Id)
//                                        ClaimStatusOption = ClaimStatusOption + '<option selected="selected" value=' + this.id + '>' + this.claimStatuss + '</option>';
//                                    else
//                                        ClaimStatusOption = ClaimStatusOption + '<option  value=' + this.id + '>' + this.claimStatuss + '</option>';

//                                });
//                            }

//                            var rowCount = $('#tblReimbursement tbody tr').length;
//                            var txtDatetime = "datetime" + (rowCount + 1);
//                            var ClaimType = "ClaimType" + (rowCount + 1);
//                            var ddlClaimSubTypeId = "ClaimSubType" + (rowCount + 1);
//                            var txtClaimAmt = "txtClaimAmt" + (rowCount + 1)
//                            var txtKMIN = "txtKMIN" + (rowCount + 1);
//                            var txtKMOut = "txtKMOut" + (rowCount + 1);
//                            var txtRemarks = "txtRemarks" + (rowCount + 1);
//                            var txtClaimStatus = "txtClaimStatus" + (rowCount + 1);
//                            var fileClaim = "fileClaim" + (rowCount + 1);
//                            var Count = rowCount + 1;
//                            var tsum = 0;
//                            var newRow = "<tr>" +
//                                "<th>" + Count + "</th>" +
//                                "<td colspan='9'><h6>No Records Found</h6></td>" +
//                                "<td hidden><select class='form-control' onchange='bindSutbtypeClaim(this)'     id='" + claimType + "'>" + claimTypeOpt + "</select><input type='hidden' value='" + claimType + "' id='ddllaimTypeIdOptional'/></td>" +
//                                "<td hidden><select class='form-control' onchange='bindSutbtypedisable(this)'     id='" + ddlClaimSubTypeId + "'>" + claimSubTypeOpt + "</select><input type='hidden' value='" + ddlClaimSubTypeId + "' id='ddllaimSubTypeIdOptional'/></td>" +
//                                "<td hidden><input type='text' placeholder='click here' class='form-control'  data-field='datetime' readonly><div id='dtBox' class='dtBox'></td>" +
//                                "<td hidden><input class='form-control' type='text' id='" + txtClaimAmt + "' placeholder='Claim Amt'></td>" +
//                                "<td hidden><input class='form-control' type='text' id='" + txtKMIN + "' placeholder='KMIN'></td>" +
//                                "<td hidden><input class='form-control' type='text' id='" + txtKMOut + "' placeholder='KMOUT'></td>" +
//                                "<td hidden><input class='form-control' type='text' id='" + txtRemarks + "' placeholder='Remarks'></td>" +
//                                "<td hidden><select  class='form-control colorchg' id='" + txtClaimStatus + "'>'" + claimStatusOption + "' </select><input type='hidden' value='" + txtClaimStatus + "' id='ddllaimStatusIdOptional'/></td>" +
//                                // "<td><input class='form-control' type='text' id='" + txtMRemarks + "' placeholder='KMOUT'></td>" +
//                                "<td hidden><input class='form-control' type='file' id='" + fileClaim + "' ></td>" +
//                                /*"<td><button class='btn btn-light btn - sm' id='AddClaim' onclick='GetCalaimRequestDetails()'>Add Claim</button></td>" +*/
//                                //"<td onclick='removerClaimRow(event,this)'><i class='fa fa-close btn btn-sm btn-danger'></i></td>" +
//                                "</tr>";
//                            $('#tblReimbursement tbody').append(newRow);
//                            $("#txtTotal").val(tsum);
//                            $("#tfooter").css("display", "table-row");
//                            //datebind();
//                            // bgstatus();
//                        }
//                    });
//                }

//            },
//            error: function (xhr) {
//                var ss = xhr.responseText;
//                $('#divMessage').html('Your Reimbursement not  Fetch Data.');
//                $('#divMessage').addClass(' shadow-border');
//            }
//        });
//    }
//    else {
//        $('#divMessage').html(errors);
//        $('#divMessage').addClass(' shadow-border');
//        return false;
//    }
//});
//$('#searchData').keyup(function () {

//    var searchText = $(this).val();
//    $('#tbodyReimbursement tr').each(function () {

//        var found = 'false';

//        $(this).each(function () {

//            if ($(this).text().toLowerCase().indexOf(searchText.toLowerCase()) >= 0) {
//                found = 'true';
//            }
//        });
//        if (found == 'true') {
//            $(this).show();
//        }
//        else {
//            $(this).hide();
//        }
//    });
//});
//function bindSutbtypeClaim() {
//    debugger
//    var _jsonSubTyepData = {};
//    var formData = new FormData();
//    var _ClaimType = $("#claimType option:selected").val();
//    $("#claimSubType").empty();
//    //$('#claimSubType').find('option') .remove() .end() .append('<option value="">Select Claim SubType</option>').val('whatever');
//    formData.append('_ClaimType', _ClaimType);
//    _jsonSubTyepData.ClaimType = _ClaimType;
//    $.ajax({
//        url: "../Reimbursement/bindSutbtypeClaim",
//        type: "POST",
//        //contentType: "application/json; charset=utf-8",
//        //data: JSON.stringify(_jsonSubTyepData),
//        data: formData,
//        contentType: false,
//        processData: false,
//        /*context: document.body,*/
//        success: function (data) {
//            debugger
//            /* var obj = (JSON.parse(data.d));*/
//            var obj = data.subType;
//            if (obj != '') {

//                $.each(obj, function (item, value) {
//                    debugger
//                    $('#claimSubType').append('<option value="' + value.id + '">' + value.payoutSubType + '</option>');
//                    /*opSubTypeId += ("<option value=" + value.ID + ">" + value.PayoutSubType + "</option>");*/

//                })

//                /* $("#" + ddlClaimSubTypeId).append(opSubTypeId);*/

//            }
//            else {
//                /* $("#" + ddlClaimSubTypeId).append(opSubTypeIdemp);*/

//            }


//        },
//        error: function (xhr) {

//            //$("#" + ddlClaimSubTypeId).empty();
//            //$("#" + ddlClaimSubTypeId).prop("disabled", true);

//        }
//    });
//}
//function checkValidations() {
//    debugger
//    var ClaimDetail = new Array();
//    var formData = new FormData();
//    var today = new Date();
//    var errors = '';
//    $('#divMessage').html('');
//    $('#divMessage').empty();
//    var Month = $("#month option:selected").val();
//    var year = $("#year option:selected").val();
//    var EmpId = $('#txtid').val();
//    var EmpName = $('#txtempname').val();
//    var claimType = $('#claimType option:selected').val();
//    var clamSubType = $('#claimSubType option:selected').val();
//    var claimDate = $('#txtclaimDate').val();
//    var claimAmount = $('#txtclaimAmount').val();
//    var kmIN = $('#kmIN').val();
//    var kmOUT = $('#kmOUT').val();
//    var remarks = $('#remarks').val();
//    var claimStatus = 2;
//    var PrevOutstadAmt = $('#txtOutstanding').val();
//    var onOutstanding = $('#txtasOutstanding').val();
//    var BaseFile = $('#hfBase64').val();
//    var image = $('#fileClaim').val();
//    //if (typeof Month === "undefined" || Month == '' || Month == '0') {
//    //    errors += '<div class="col-sm-3">Please select month.</div>';
//    //    $("#ddlmounth").addClass(' error');
//    //}
//    //else {
//    //    $("#ddlmounth").removeClass(' error');
//    //}
//    //if (typeof year === "undefined" || year == '' || year == '0') {
//    //    errors += '<div class="col-sm-3">Please select year.</div>';
//    //    $("#ddlyear").addClass(' error');
//    //}
//    //else {
//    //    $("#ddlyear").removeClass(' error');
//    //}
//    if (EmpId == '')
//        errors += '<div class="col-sm-3">please enter EmpId.</div>';
//    else {
//        $("#txtid").removeClass(' error');
//    }
//    if (EmpName == '')
//        errors += '<div class="col-sm-3">please enter Emp Name.</div>';
//    else {
//        $("#txtempname").removeClass(' error');
//    }
//    if (claimType == '' || claimType == 'Select ClaimType')
//        errors += $("#claimType").addClass('border-danger');
//    else {
//        $("#claimType").removeClass('border-danger');
//    }

//    if (clamSubType == '' || clamSubType == 'Select SubClaimType')
//        errors += $("#clamSubType").addClass('border-danger');
//    else {
//        $("#clamSubType").removeClass('border-danger');
//    }

//    if (claimDate == '' || claimDate == null)
//        errors += $("#txtclaimDate").addClass('border-danger');
//    else {
//        $("#txtclaimDate").removeClass(' border-danger');
//    }

//    if (claimAmount == '' || claimAmount == null)
//        errors += $("#txtclaimAmount").addClass('border-danger');
//    else {
//        $("#txtclaimAmount").removeClass('border-danger');
//    }

//    if (BaseFile == '' || BaseFile == null)
//        errors += $("#fileClaim").addClass('border-danger');
//    else {
//        $("#fileClaim").removeClass('border-danger');
//    }
//    if (errors == '') {
//        debugger
//        var reader = new FileReader();
//        var fileName;
//        var contentType;

//        $.ajax({

//            type: "POST",
//            url: "../Reimbursement/ReimbursementSaveMobile",
//            //data: JSON.stringify({ _jsonClaimData }),
//            data: { 'Month': Month, 'year': year, 'EmpId': EmpId, 'EmpName': EmpName, 'claimType': claimType, 'clamSubType': clamSubType, 'claimDate': claimDate, 'claimAmount': claimAmount, 'remarks': remarks, 'claimStatus': claimStatus, 'BaseFile': BaseFile, 'kmIN': kmIN, 'kmOUT': kmOUT },
//            context: document.body,
//            success: function (data) {

//                if (data == "success") {

//                    clearFormData();
//                    $('#divMessage').html(data.d);
//                    $('#divMessage').addClass(' shadow-border');
//                    $("#divMessage").addClass('text-success').html('Reimbursement Data successfully saved..');
//                }
//                else {
//                    $("#divMessage").addClass('text-danger').html(data);
//                }
//            },
//            error: function (xhr) {

//                var ss = xhr.responseText;
//                $('#divMessage').html('Your Reimbursement  not saved successfully.');
//                $('#divMessage').addClass('shadow-border');
//            }
//        });
//    }
//    else {
//        $('#divMessage').html('please fill all mandetory fields');
//        $('#divMessage').addClass(' shadow-border');
//        return false;
//    }
//}

//var imagebase64 = "";
//function saveRouteFileOnServer(element) {
//    debugger
//    //input#fileClaim1 {accept: "", alt: "", autocomplete: "", …}
//    var file = element.files[0];
//    var reader = new FileReader();
//    reader.onloadend = function () {

//        imagebase64 = reader.result;
//        //var a = imagebase64.Replace("data:image/png;base64,", "");
//        $("#hfBase64").val(imagebase64);
//    }
//    //return imagebase64;
//    reader.readAsDataURL(file);
//}
//function Base64ToBytes(base64) {

//    debugger;
//    var s = window.atob(base64);
//    var bytes = new Uint8Array(s.length);
//    for (var i = 0; i < s.length; i++) {
//        bytes[i] = s.charCodeAt(i);
//    }
//    return bytes;
//};
//function clearFormData() {
//    $('#txtclaimDate').val('');
//    $('#remarks').val('');
//    $('#kmOUT').val('');
//    $('#kmIN').val('');
//    $('#txtclaimAmount').val('');
//    $('#claimType').find('option:first').prop('selected', 'selected');
//    $('#claimSubType').find('option:first').prop('selected', 'selected');
//    $('#fileClaim').val('');
//}

//function getyear() {
//    debugger
//    //$('#year').empty();
//    //$('#year').append($("<option></option>").val('').html('Select year'));
//    $.ajax({
//        type: "POST",
//        url: "/Reimbursement/Getyear",
//        context: document.body,
//        success: function (data) {
//            debugger

//            if (data != '') {
//                $.each(data.yearList, function (item, value) {
//                    debugger
//                    $("#year").append($("<option></option>").val(value.encryptcountryID).html(value.text));
//                });
//                //select the current year by default //
//                var currentyear = new Date().getFullYear().toString();
//                /* $("#year").val(currentyear);*/
//                $("#year option[value='" + currentyear + "']").prop("selected", true);

//                $("#year").prop("disabled", false);

//            }
//        },
//        error: function (data, error) {
//            debugger
//            $("#spanMessage").addClass('text-danger').html(data);
//        }

//    });
//}
////$('#year').change(function () {
////    debugger
////    getmonth();
////});
//function getmonth() {
//    debugger
//    var iselectyear = $("#year option:selected").val();
//    var iselectyearS = $("#year option:selected").html();
//    //$('#month').empty();
//    //$('#month').append($("<option></option>").val('').html('Select Month'));

//    $.ajax({
//        type: "GET",
//        url: "/Reimbursement/Getmonth",
//        context: document.body,
//        data: { 'year': iselectyearS },
//        success: function (data) {
//            debugger

//            if (data != '') {
//                $.each(data.ddlMonths, function (item, value) {
//                    debugger

//                    $("#month").append($("<option></option>").val(value.value).html(value.text));
//                });
//                //select the current year by default //
//                const moonLanding = new Date('July 20, 69 00:20:18');

//                //console.log(moonLanding.getMonth());
//                var currentmonth = moonLanding.getMonth().toString();
//                /* $("#month").val(currentmonth);*/
//                $("#month option[value='" + currentmonth + "']").prop("selected", true);

//                $("#month").prop("disabled", false);
//            }
//        },
//        error: function (data, error) {
//            debugger
//            $("#spanMessage").addClass('text-danger').html(data);
//        }

//    });
//}



