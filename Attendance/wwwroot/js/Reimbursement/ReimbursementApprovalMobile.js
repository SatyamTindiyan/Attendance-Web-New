
$(document).ready(function () {

    getyear()
});

var today = new Date();
function HideModelPopup() {

    $('#requestClaimModalApproval').modal('hide');
}
function GetCalaimRequestDetails(control) {

    /*var ind = control.closest('tr').rowIndex;*/
    var id = $(control).val();
    $('#hdnClaimTypeId').val(id);

    var row = $(control).closest("tr");
    // anything you want to do

    $('#claimType').html(row.find("td:eq(2)").text());
    $('#cliamSubType').html(row.find("td:eq(3)").text());
    $('#claimDate').html(row.find("td:eq(4)").text());
    $('#claimAmount').html(row.find("td:eq(5)").text());
    $('#kmIN').html(row.find("td:eq(6)").text());
    $('#kmOUT').html(row.find("td:eq(7)").text());
    $('#remarks').html(row.find("td:eq(8)").text());
    $('#ClaimStatus').html(row.find("td:eq(9)").text());
    $('#requestClaimModalApproval').modal('show');

}
$("#btnFetchApprovalData").click(function () {
    debugger
    var errors = '';
    var Month = $("#ddlmounth option:selected").val();
    var year = $("#ddlYears option:selected").val();
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

        formData.append('Month', Month);
        formData.append('year', year);
        formData.append('EmpId', EmpId);
        var rowCount = $('#tblReimbursementApproval tbody tr').length;
        var ddlClaimTypeId = "ClaimType" + (rowCount + 1);
        var ClaimTypeOption = '<option value =""> Select Claim Type</option>';
        //var ddlClaimSubTypeId = "ClaimSubType" + (rowCount + 1);
        var ClaimSubTypeOption = '<option value =""> Select Claim Sub Type</option>';
        var ClaimStatusOption = '';
        $.ajax({

            url: "../Reimbursement/GetReimbursementApproval",
            type: 'POST',
            //contentType: "application/json; charset=utf-8",
            //data: JSON.stringify(_jsonFetchData),
            //context: document.body,
            data: formData,
            contentType: false,
            processData: false,
            success: function (data) {
                debugger
                //$('#btnFileDownload').show();
                //$('#btnApproveClaimType').show();
                var tbodyCliamdata = "";
                var count = 0;
                var tsum = 0;
                var dt1 = data.dt;
                var dt2 = data.dtt;
                var _SubType = data.SubType;
                var _ClaimSatus = data.ClaimSatus;


                if (dt1.length > 0) {
                    for (var i = 0; i <= data.dt.length - 1; i++) {

                        var selectedId = data.dt[i].payoutTypeID;
                        var selectedSubId = data.dt[i].payoutSubTypeID;
                        var selectedClaimSatusId = 2;//data.dt[i].ID;
                        var jsonDate = data.dt[i].claimDate;
                        var jsd = new Date(parseInt(jsonDate.substr(6))); //to js format
                        var dateObject = new Date(jsd); //Date object
                        var day = jsd.getDate();
                        var hrs = jsd.getHours();
                        var min = jsd.getMinutes();
                        var month = jsd.getDate();
                        var year = jsd.getFullYear();
                        var date = day + "-" + month + "-" + year + " " + hrs + ":" + min; //jsd
                        var rowCountS = i + 1;
                        var txtClaimAmt = "txtClaimAmt" + rowCountS;
                        var txtKMIN = "txtKMIN" + rowCountS;
                        var txtKMOut = "txtKMOut" + rowCountS;
                        var ddlClaimTypeId = "ClaimType" + rowCountS;
                        var ddlClaimSubTypeId = "ClaimSubType" + rowCountS;
                        var txtRemarks = "txtRemarks" + rowCountS;
                        var txtClaimStatus = "txtClaimStatus" + rowCountS;
                        var fileClaim = "fileClaim" + rowCountS;

                        count = count + 1;
                        tsum += dt1[i].claimAmount
                        tbodyCliamdata += "<tr>";
                        tbodyCliamdata += "<td>" + count + "</td>";
                        tbodyCliamdata += "<td hidden><input type='hidden' value='" + dt1[i].id + "' id='ClaimTypeId'/></td>";
                        tbodyCliamdata += "<td hidden><input type='hidden' value='" + dt1[i].images + "' id='fileClaimId'/></td>";
                        tbodyCliamdata += "<td>" + dt1[i].payoutType + "</td>";
                        /* tbodyCliamdata += "<td><select onchange='bindSutbtypeClaim(this)' required class='form-control' id='" + ddlClaimTypeId + "'>'" + ClaimTypeOption + "' </select></td>";*/

                        tbodyCliamdata += "<td class='col-desktop'>" + dt1[i].payoutSubType + "</td>";
                        /*  *//* tbodyCliamdata += "<td class='col-desktop'><select onchange='bindSutbtypedisable(this)' required class='form-control' id='" + ddlClaimSubTypeId + "'>'" + ClaimSubTypeOption + "'</select></td>";*/

                        tbodyCliamdata += "<td>" + dt1[i].claimDate + "</td>";
                        /*tbodyCliamdata += "<td><input type='text' required value='" + date + "' placeholder='click here' class='form-control'data-field='datetime'><div id='dtBox' class='dtBox'></div></td>";*/


                        //tbodyRouteAssign += "<td><input type='text' placeholder='click here' class='form-control'  data-field='datetime'><div id='dtBox' class='dtBox'></div></td>";

                        tbodyCliamdata += "<td>" + dt1[i].claimAmount + "</td>";
                        /*tbodyCliamdata += "<td><input type='text' ID='" + txtClaimAmt + "'   value='" + dt1[i].ClaimAmount + "' placeholder='Amount' class='form-control amt'></td>";*/

                        tbodyCliamdata += "<td class='col-desktop'>" + dt1[i].kmin + "</td>";
                        /*tbodyCliamdata += "<td class='col-desktop'><input type='text' required ID='" + txtKMIN + "' disabled value='" + dt1[i].KMIN + "' placeholder='KM IN' class='form-control'></td>";*/

                        tbodyCliamdata += "<td class='col-desktop'>" + dt1[i].kmout + "</td>";
                        /*tbodyCliamdata += "<td class='col-desktop'><input type='text' required ID='" + txtKMOut + "' disabled value='" + dt1[i].KMOUT + "' placeholder='KM OUT' class='form-control'></td>";*/

                        tbodyCliamdata += "<td class='col-desktop'>" + dt1[i].remarks + "</td>";

                        tbodyCliamdata += "<td class='col-desktop'>" + dt1[i].claimStatuss + "</td>";
                        tbodyCliamdata += "<td><button type='button' required id='btnExport'title='Download' value='" + dt1[i].images + "' onclick='DownloadFile(this)' class='btn-info btn-smm col-desktop'><i class='fa fa-download' aria-hidden='true' ></i ></button> <button type='button' required id='btnApproved' title='Approve' value='" + dt1[i].ID + "' onclick='ApproveClaimType(" + dt1[i].ID + ")' class='btn btn-smm btn-success col-desktop'><i class='fa fa-check'></i></button > <button type='button' required id='btnReject' title='Approve' value='" + dt1[i].ID + "' onclick='RejectClaimType(" + dt1[i].ID + ")' class='btn btn-smm btn-danger col-desktop'><i class='fa fa-close'></i></button ><button type='button' required id='btnApproved" + dt1[i].ID + "' title='Approve' value='" + dt1[i].ID + "' onclick='GetCalaimRequestDetails(this)' class='btn btn-smm btn-info col-mobile'><i class='fa fa-eye'></i></button></td> ";
                        tbodyCliamdata += "</tr>";
                        //$('#ClaimType1 option[value=' + dt1[i].PayoutTypeID + ']').attr("selected", 'true');//PayoutSubTypeID
                    };
                    $('#tbodyReimbursementApproval').html(tbodyCliamdata);
                    $("#txtTotal").val(tsum);
                    $("#tfooter").css("display", "table-row");

                }
                else {
                    debugger;
                    var Count = 1;
                    var newRow = "<tr>" +
                        "<th>" + Count + "</th>" +
                        "<td colspan='9'><h6>No Records Found</h6></td>" +
                        "</tr>";
                    $('#tblReimbursementApproval tbody').append(newRow);

                }

                $('#tbodyReimbursement').html(tbodyCliamdata);
                $("#txtTotal").val(tsum);
                $("#tfooter").css("display", "table-row");

            },
            error: function (xhr) {
                debugger;
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
function DownloadFile(control) {
    debugger;
    if (control != null && control == undefined) {
        var ind = control.closest('tr').rowIndex;
        $("#tblReimbursementApproval tbody tr").each(function () {
            debugger;
            var rows = $(this);
            var index = this.rowIndex;
            if (ind == index) {

                var fileName = $(control).val();
                $.ajax({

                    url: "../Reimbursement/DownloadFile",
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    data: '{fileName: "' + fileName + '"}',
                    context: document.body,
                    success: function (data) {

                        //Convert Base64 string to Byte Array.
                        var img = new Image();
                        var bytes = Base64ToBytes(data.fileName);
                        var fileName = data.fileName;
                        ///*img.src = img;*/
                        //var _filename = img.src = img;
                        ////Convert Byte Array to BLOB.
                        //img.src = "data:image/png;base64," + data.d;
                        //$("#Image1").src = "data:image/png;base64," + data.d;
                        //var aa = img.outerHTML;
                        //"<img src='data:image/png;base64," + data.d;
                        var blob = new Blob([bytes], { type: "application/octetstream" });

                        //Check the Browser type and download the File.
                        var isIE = false || !!document.documentMode;
                        if (isIE) {
                            window.navigator.msSaveBlob(blob, fileName);
                        } else {
                            //var url = window.URL || window.webkitURL;
                            //link = url.createObjectURL(blob);
                            //var a = $("<a/>");
                            //a.attr("download", fileName);
                            //a.attr("href", link);
                            //$("body").append(a);
                            //a[0].click();
                            //var bytes = Base64ToBytes(data.d);
                            var a = document.createElement("a"); //Create <a>
                            a.href = "data:image/png;base64," + fileName; //Image Base64 Goes here
                            a.download = "Download"; //File name Here
                            a.click(); //Downloaded file
                            $("body").remove(a);
                        }
                    }
                });
            }
        });
    } else {
        var fileName = $('#fileClaimId').val();
        $.ajax({

            url: "../Reimbursement/DownloadFile",
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: '{fileName: "' + fileName + '"}',
            context: document.body,
            success: function (data) {

                //Convert Base64 string to Byte Array.
                var img = new Image();
                var bytes = Base64ToBytes(data.fileName);
                var fileName = data.fileName;
                ///*img.src = img;*/
                //var _filename = img.src = img;
                ////Convert Byte Array to BLOB.
                //img.src = "data:image/png;base64," + data.d;
                //$("#Image1").src = "data:image/png;base64," + data.d;
                //var aa = img.outerHTML;
                //"<img src='data:image/png;base64," + data.d;
                var blob = new Blob([bytes], { type: "application/octetstream" });

                //Check the Browser type and download the File.
                var isIE = false || !!document.documentMode;
                if (isIE) {
                    window.navigator.msSaveBlob(blob, fileName);
                } else {
                    //var url = window.URL || window.webkitURL;
                    //link = url.createObjectURL(blob);
                    //var a = $("<a/>");
                    //a.attr("download", fileName);
                    //a.attr("href", link);
                    //$("body").append(a);
                    //a[0].click();
                    //var bytes = Base64ToBytes(data.d);
                    var a = document.createElement("a"); //Create <a>
                    a.href = "data:image/png;base64," + fileName; //Image Base64 Goes here
                    a.download = "Download"; //File name Here
                    a.click(); //Downloaded file
                    $("body").remove(a);
                }
            }
        });
    }
};
function Base64ToBytes(base64) {
    var s = window.atob(base64);
    var bytes = new Uint8Array(s.length);
    for (var i = 0; i < s.length; i++) {
        bytes[i] = s.charCodeAt(i);
    }
    return bytes;
};
function SetDataTable() {
    debugger
    $('#tblReimbursement').DataTable({
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


function ApproveClaimType(control) {
    debugger
    var ClaimTypeId = control;
    if (ClaimTypeId == "undefined") {
        var selectedClaimTypeId = 1;
        // var ClaimTypeId = $('#ClaimTypeId').val();
        $.ajax({

            type: 'GET',
            url: "../Reimbursement/SaveHeadReimbursementApproval",
            contentType: "application/json; charset=utf-8",
            /* data: JSON.stringify(_jsonFetchData),*/
            data: { "ApprovalId": ClaimTypeId, "Status": selectedClaimTypeId },
            context: document.body,
            success: function (data) {
                debugger
                if (data == "success") {
                    setTimeout(function () {
                    }, 1000);
                }
                else {
                    alert('Something Went Wrong......');
                }
            },
            error: function (xhr) {
                debugger;
                var ss = xhr.responseText;
                $('#divMessage').html('Your Reimbursement not  Fetch Data.');
                $('#divMessage').addClass(' shadow-border');
            }
        });
    }
    else {

        var ClaimTypeId = $('#hdnClaimTypeId').val();
        var selectedClaimTypeId = 1;
        $.ajax({

            type: 'GET',
            url: "../Reimbursement/SaveHeadReimbursementApproval",
            contentType: "application/json; charset=utf-8",
            /* data: JSON.stringify(_jsonFetchData),*/
            data: { "ApprovalId": ClaimTypeId, "Status": selectedClaimTypeId },
            context: document.body,
            success: function (data) {

                if (data == "success") {
                    setTimeout(function () {

                    }, 1000);
                }
                else {
                    alert('Something Went Wrong......');
                }
            },
            error: function (xhr) {
                debugger;
                var ss = xhr.responseText;
                $('#divMessage').html('Your Reimbursement not  Fetch Data.');
                $('#divMessage').addClass(' shadow-border');
            }
        });
    }
}


var appdata = [];
function ApproveCliamTypeDtl() {
    debugger;
    // var indextbl = 0;
    $('#tblReimbursementApproval tbody tr').each(function () {
        debugger;
        var row = $(this);
        // get current row 2nd TD
        var appList = {};

        //var CordinatorA = row.find(".CordinatorA").is(':checked');
        //var CordinatorR = row.find(".CordinatorR").is(':checked');
        //var ManagerA = row.find(".ManagerA").is(':checked');
        //var ManagerR = row.find(".ManagerR").is(':checked');
        //var HeadA = row.find(".HeadA").is(':checked');
        //var HeadR = row.find(".HeadR").is(':checked');



        var claimTypeId = row.find("td:eq(0)").text();
        var Id = row.find("td:eq(1)").text();
        //var claimTypeIdd = row.find("td:eq(2)").val();
        //var claimTypeIddd = row.find("td:eq(3)").val();
        //var claimTypeIdddd = row.find("td:eq(4)").val();
        //var claimTypeIddddd = row.find("td:eq(5)").val();
        //var claimTypeIdddddd = row.find("td:eq(6)").val();
        //var claimTypeIddddddd = row.find("td:eq(7)").val();
        //var ClaimStatuss = row.find("td:eq(9)").val();
        //var ClaimStatusss = row.find("td:eq(8)").val();
        //var _date = row.find("td:eq(2)").text();

        //var data1 = row.find("td:eq(6) input[type='text']").val();
        //var data2 = row.find("td:eq(7) input[type='text']").val();
        //var data3 = $(this).find("td").eq(2).find('option:selected').val();
        var ClaimStatuss = $(this).find("td").eq(9).find('option:selected').val();

        //var valCordnatorA = row.find(".CordinatorA").val();
        //appList.APPstatusCORDINATOR = valCordnatorA;
        //appList.APPstatusMANAGER = 1;
        //appList.APPstatusHEAD = 1;

        appList.Id = Id;
        appList.ClaimStatuss = ClaimStatuss;
        appdata.push(appList);

    });
    if (appdata.length > 0) {
        $.ajax({
            type: 'POST',
            url: "../Reimbursement/SaveHeadReimbursementApprovalDtl",
            /*data: JSON.stringify(appdata),*/
            data: { 'appdata': appdata },
            document: document.body,
            success: function (result) {
                $('#msgSuccess').addClass('label-success').html('Regularization Approved Successfully');
                setTimeout(function () {
                    location.reload();
                }, 4000);

            },
            error: function () {
                $('#msgError').addClass('label-danger').html('Regularization Approved Successfully');
                setTimeout(function () {
                    location.reload();
                }, 4000);

            }

        });
    }


}
function RejectClaimType(control) {
    debugger;
    var ClaimTypeId = control;


    if (ClaimTypeId == "undefined") {

        var selectedClaimTypeId = 3;
        $.ajax({

            type: 'GET',
            url: "../Reimbursement/SaveHeadReimbursementApproval",
            contentType: "application/json; charset=utf-8",
            /* data: JSON.stringify(_jsonFetchData),*/
            data: { "ApprovalId": ClaimTypeId, "Status": selectedClaimTypeId },
            context: document.body,
            success: function (data) {

                if (data == "success") {
                    alert('data submitted successfully.');

                    setTimeout(function () {
                    }, 1000);
                }
                else {
                    alert('Something Went Wrong......');
                }
            },
            error: function (xhr) {
                debugger;
                var ss = xhr.responseText;
                $('#divMessage').html('Your Reimbursement not  Fetch Data.');
                $('#divMessage').addClass(' shadow-border');
            }
        });
    }
    else {
        var ClaimTypeId = $('#hdnClaimTypeId').val();
        var selectedClaimTypeId = 3;
        $.ajax({

            type: 'GET',
            url: "../Reimbursement/SaveHeadReimbursementApproval",
            contentType: "application/json; charset=utf-8",
            /* data: JSON.stringify(_jsonFetchData),*/
            data: { "ApprovalId": ClaimTypeId, "Status": selectedClaimTypeId },
            context: document.body,
            success: function (data) {

                if (data == "success") {
                    alert('data submitted successfully.');

                    setTimeout(function () {
                    }, 1000);
                }
                else {
                    alert('Something Went Wrong......');
                }
            },
            error: function (xhr) {
                debugger;
                var ss = xhr.responseText;
                $('#divMessage').html('Your Reimbursement not  Fetch Data.');
                $('#divMessage').addClass(' shadow-border');
            }
        });
    }
}
$('#searchData').keyup(function () {

    var searchText = $(this).val();
    $('#tbodyReimbursementApproval tr').each(function () {

        var found = 'false';

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

