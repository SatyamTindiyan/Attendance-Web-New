$(document).ready(function () {
    getyear()
    getmonth()

});
var today = new Date();
var approvallbl = $('#myHiddenVar').val();

function datebind() {
    debugger
    $(".dtBox").DateTimePicker({
        //minDate: today
        dateFormat: "dd-MM-yyyy hh:mm"
    });
}

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
            //var _ClaimType = row.find("td").eq(0).find().val();
            var _ClaimType = row.find("td").eq(1).find('option:selected').val();
            var _ClaimTypeSub = row.find("td").eq(2).find('option:selected').val();
            //var _ClaimDate = row.find("td").eq(3).find('input[type = "text"]').val();
            var _ClaimAmt = row.find("td").eq(4).find('input[type = "text"]').val();
            var _KMIN = row.find("td").eq(5).find('input[type = "text"]').val();
            var _KMOUT = row.find("td").eq(6).find('input[type = "text"]').val();
            var _Remarks = row.find("td").eq(7).find('input[type = "text"]').val();
            var _ClimStatus = row.find("td").eq(8).find('input[type = "text"]').val();
            //var _MRemarks = row.find("td").eq(9).find('input[type = "text"]').val();
            //var _File3 = row.find("td").eq(10).find('input[type = "file"]')[0].name;
            //var filepath = $("#" + _File3).name;

            //var fileUpload = row.find("td").eq(10).find('input[type = "file"]').get(0);
            ////$("#fupload").get(0);
            //var files = fileUpload.files;
            //var _filename = files[0].name;

            var ClaimData = {};
            if (_ClaimType > 0) {
                ClaimData.ClaimType = _ClaimType;
                ClaimData.ClaimTypeSub = _ClaimTypeSub;
                //ClaimData.ClaimDate = _ClaimDate
                ClaimData.ClaimAmt = _ClaimAmt;
                ClaimData.KMIN = _KMIN;
                ClaimData.KMOUT = _KMOUT;
                ClaimData.Remarks = _Remarks;
                ClaimData.ClaimStatus = _ClimStatus;
                //ClaimData.MRemarks = _MRemarks;
                //ClaimData.FileName = _filename
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
        _jsonClaimData.PrevOutstadAmt = "1";
        _jsonClaimData.onOutstanding = "2";

        $.ajax({
            url: "../webpage/ReimbursementHeadApproval.aspx/Reimbursementt",
            type: 'post',
            contentType: 'application/json;charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(_jsonClaimData),
            success: function (data) {
                debugger;
                if (data != '') {
                    debugger;
                    $('#divMessage').html(data.d);
                    $('#divMessage').addClass(' shadow-border');

                }
            },
            error: function (xhr) {
                debugger;
                var ss = xhr.responseText;
                $('#divMessage').html('Your Reimbursement  not saved successfully.');
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


$('#btnAddMore').on('click', function (e) {
    debugger;
    var rowCount = $('#tblReimbursement tbody tr').length;
    if (rowCount > 0) {
        debugger;
        var txtDatetime = "datetime" + (rowCount + 1);
        var ClaimType = "ClaimType" + (rowCount + 1);
        var ddlClaimSubTypeId = "ClaimSubType" + (rowCount + 1);
        var txtClaimAmt = "txtClaimAmt" + (rowCount + 1)
        var txtKMIN = "txtKMIN" + (rowCount + 1);
        var txtKMOut = "txtKMOut" + (rowCount + 1);
        var txtRemarks = "txtRemarks" + (rowCount + 1);
        var txtClaimStatus = "txtClaimStatus" + (rowCount + 1);
        var txtManagerStatus = "txtManagerStatus" + (rowCount + 1);
        var txtRMStatus = "txtRMStatus" + (rowCount + 1);
        var txtFinanceStatus = "txtFinanceStatus" + (rowCount + 1);
        var txtMRemarks = "txtMRemarks" + (rowCount + 1);
        var fileClaim = "fileClaim" + (rowCount + 1);
        var currentRow = $(this).closest("tr");
        var rr = $("#tbodyRouteAssign").find('tr');
        var Count = rowCount + 1;
        var ClaimTypeOption = '';
        var ClaimSubTypeOption = ''
        var row = $(this);
    }
    //$("#" + ClaimType + " option").each(function (i, value) {
    //    DEBU
    //    var ss = this.value + ',' + this.text
    //    ClaimTypeOption = ClaimTypeOption + '<option value=' + this.value + '>' + this.text + '</option>';



    //});

    $("ClaimType3 option").each(function (i, value) {
        debugger;
        var ss = this.value + ',' + this.text
        ClaimTypeOption = ClaimTypeOption + '<option value=' + this.value + '>' + this.text + '</option>';



    });
    $("#ClaimSubType1 option").each(function (i, value) {

        var ss = this.value + ',' + this.text
        ClaimSubTypeOption = ClaimSubTypeOption + '<option value=' + this.value + '>' + this.text + '</option>';



    });
    var newRow = "<tr>" +
        "<th>" + Count + "</th>" +
        "<td><select class='form-control' required style='width:100px;'   id='" + ddlClaimTypeId + "'>" + ClaimTypeOption + "</select></td>" +
        "<td><select class='form-control' required style='width:100px;'   id='" + ddlClaimSubTypeId + "'>" + ClaimSubTypeOption + "</select></td>" +
        "<td><input type='text' required placeholder='click here' class='form-control'  data-field='datetime' readonly><div id='dtBox' class='dtBox'></td>" +
        "<td><input class='form-control' required type='text' id='" + txtClaimAmt + "' placeholder='Claim Amt'></td>" +
        "<td><input class='form-control' required type='text' id='" + txtKMIN + "' placeholder='KMIN'></td>" +
        "<td><input class='form-control' required type='text' id='" + txtKMOut + "' placeholder='KMOUT'></td>" +
        "<td><input class='form-control' required type='text' id='" + txtClaimStatus + "' placeholder='KMIN'></td>" +
        "<td><input class='form-control' required type='text' id='" + txtManagerStatus + "' placeholder='KMIN'></td>" +
        "<td><input class='form-control' required type='text' id='" + txtRMStatus + "' placeholder='KMIN'></td>" +
        //"<td><input class='form-control' required type='text' id='" + txtFinanceStatus + "' placeholder='KMIN'></td>" +
        "<td><input class='form-control' required type='text' id='" + txtRemarks + "' placeholder='Remarks'></td>" +
        "<td><input class='form-control' required type='text' id='" + txtMRemarks + "' placeholder='KMOUT'></td>" +
        "<td><input class='form-control' required type='file' id='" + fileClaim + "' ></td>" +
        "<td onclick='removerClaimRow(event,this)'><i class='fa fa-close btn btn-sm btn-danger'></i></td>" +
        "</tr>";
    $('#tblReimbursement tbody').append(newRow);
    e.preventDefault();
});
function removerClaimRow(e, control) {
    $(control).closest("tr").remove();
    e.preventDefault();
}

function bindSutbtypedisable(control) {
    debugger;
    var ind = control.closest('tr').rowIndex;
    //var ddlClaimSubTypeId = "ClaimSubType" + ind;
    //$('#' + ddlClaimSubTypeId).empty();
    //$('#' + ddlClaimSubTypeId + 'option:selected').remove();
    //var ddlid = control.id;

    // var row_index = $(this).parent().index('tr');
    var ddlClaimTypeId = "ClaimType" + ind;
    var txtClaimAmt = "txtClaimAmt" + ind;
    var txtKMIN = "txtKMIN" + ind;
    var txtKMOut = "txtKMOut" + ind;
    $("#tblReimbursement tbody tr").each(function () {
        debugger;
        var rows = $(this);
        var index = this.rowIndex;
        var _ClaimType = $(rows).find("td").eq(1).find('option:selected').val();
        var _ClaimSubType = $(rows).find("td").eq(2).find('option:selected').val();

        if (ind == index) {
            if (_ClaimSubType != '' && _ClaimSubType != "undefined") {
                if (_ClaimSubType == 12) {
                    debugger
                    $("#" + txtKMIN).prop("disabled", false);
                    $("#" + txtKMOut).prop("disabled", false);
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


function exportData() {

    debugger;
    /* Get the HTML data using Element by Id */
    var table = document.getElementById("tbodyReimbursement");


    //define the heading for each row of the data
    var columnNames = ['ID,EMPID,CLAIM TYPE,CLAIM SUBTYPE,CLAIM DATE,CLAIM AMT.,KMIN,KMOUT,REMARKS\n'];
    /* Declaring array variable */

    var rows = [];

    //Add column name to row array
    rows.push(columnNames);
    //iterate through rows of table
    for (var i = 0, row; row = table.rows[i]; i++) {
        debugger
        //rows would be accessed using the "row" variable assigned in the for loop
        //Get each cell value/column from the row
        var rowData = [];

        rowData.push(row.cells[0].innerText);
        rowData.push(row.cells[1].innerText);
        rowData.push(row.cells[2].innerText);
        rowData.push(row.cells[3].innerText);
        rowData.push(row.cells[4].innerText);
        rowData.push(row.cells[5].innerText);
        rowData.push(row.cells[6].innerText);
        rowData.push(row.cells[7].innerText);
        rowData.push(row.cells[8].innerText);

        // Add the row data to the rows array
        rows.push(rowData);

    }
    csvContent = "data:text/csv;charset=utf-8,";
    /* add the column */
    rows.forEach(function (rowArray) {
        debugger
        //var row = rowArray.map(value => '"' + value.replace(/"/g, '""') + '"').join(",");
        var row = rowArray.join(",");
        csvContent += row + '\r\n';

    });

    /* create a hidden <a> DOM node and set its download attribute */
    var encodedUri = encodeURI(csvContent);
    var link = document.createElement("a");
    link.setAttribute("href", encodedUri);
    link.setAttribute("download", "HeadApproval.csv");
    document.body.appendChild(link);
    /* download the data file named "Stock_Price_Report.csv" */
    link.click();
}

$("#btnFetchPaidreport").click(function () {
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
        //var ddlClaimSubTypeId = "ClaimSubType" + (rowCount + 1);
        var ClaimSubTypeOption = '<option value =""> Select Claim Sub Type</option>';
        var ClaimStatusOption = '';
        formData.append('Month', Month);
        formData.append('year', year);
        formData.append('EmpId', EmpId);
        $.ajax({

            url: "../Reimbursement/GetPaidReimbursement",
            type: 'POST',
            //contentType: "application/json; charset=utf-8",
            //data: JSON.stringify(_jsonFetchData),
            data: formData,
            contentType: false,
            processData: false,
            context: document.body,
            success: function (data) {
                debugger
                var tbodyCliamdata = "";
                var count = 0;
                var tsum = 0;
                var dt1 = data.dt;
                var dt2 = data.dtt;
                var _SubType = data.subType;
                var _ClaimSatus = data.claimSatus;
                let ms = Date.parse("March 21, 2012");
                //if (_ClaimSatus != '') {

                //    ClaimStatusOption = '<option value =""> Select Claim Status</option>';
                //    $(_ClaimSatus).each(function (i, value) {
                //        ClaimStatusOption = ClaimStatusOption + '<option   value=' + this.Id + '>' + this.ClaimStatuss + '</option>';
                //    });
                //}
                if (dt1.length > 0) {
                    debugger;
                    for (var i = 0; i <= dt1.length - 1; i++) {
                        var selectedId = data.dt[i].payoutTypeID;
                        var selectedSubId = data.dt[i].payoutSubTypeID;
                        var selectedClaimSatusId = /*data.dt[i].id;*/ data.dt[i].claimSatus; data.dt[i].claimStatuss;
                        //var jsonDate = data.dt[i].claimDate;
                        //var jsd = new Date(parseInt(jsonDate.substr(6))); //to js format
                        //var dateObject = new Date(jsd); //Date object
                        //var day = dateObject.getDate();
                        //var hrs = dateObject.getHours();
                        //var min = dateObject.getMinutes();
                        //var month = dateObject.getDate();
                        //var year = dateObject.getFullYear();
                        //var date = day + "-" + month + "-" + year + " " + hrs + ":" + min; //jsd
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
                        var claimId = "claimId" + rowCountS;
                        var txtempid = "txtempid" + rowCountS;
                        var txtClaimAmt = "txtClaimAmt" + rowCountS;
                        var txtKMIN = "txtKMIN" + rowCountS;
                        var txtKMOut = "txtKMOut" + rowCountS;
                        var ddlClaimTypeId = "ClaimType" + rowCountS;
                        var ddlClaimSubTypeId = "ClaimSubType" + rowCountS;
                        var txtRemarks = "txtRemarks" + rowCountS;
                        var txtMRemarks = "txtMRemarks" + rowCountS;
                        var txtClaimStatus = "txtClaimStatus" + rowCountS;
                        var txtManagerStatus = "txtManagerStatus" + rowCountS;
                        var txtRMStatus = "txtRMStatus" + rowCountS;
                        var txtFinanceStatus = "txtFinanceStatus" + rowCountS;
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

                        if (_ClaimSatus != '') {

                            ClaimStatusOption = '<option value =""> Select Claim Status</option>';
                            $(_ClaimSatus).each(function (i, value) {
                                debugger

                                if (this.id == 2)
                                    ClaimStatusOption = ClaimStatusOption + '<option  selected="selected" value=' + this.id + '>' + this.claimStatuss + '</option>';

                                else
                                    ClaimStatusOption = ClaimStatusOption + '<option   value=' + this.id + '>' + this.claimStatuss + '</option>';


                            });
                        }
                        count = count + 1;
                        tsum += dt1[i].claimAmount
                        tbodyCliamdata += "<tr>";
                        tbodyCliamdata += "<td>" + count + "</td>";
                        tbodyCliamdata += "<input type='hidden' ID='" + claimId + "' class='claimId' value='" + dt1[i].id + "'>";
                        tbodyCliamdata += "<td><input type='text' required ID='" + txtempid + "' disabled value='" + dt1[i].emP_ID + "' placeholder='KM IN' class='form-control'></td>";
                        tbodyCliamdata += "<td><select onchange='bindSutbtypeClaim(this)' required class='form-control' id='" + ddlClaimTypeId + "'>'" + ClaimTypeOption + "' </select></td>";
                        tbodyCliamdata += "<td><select onchange='bindSutbtypedisable(this)' required class='form-control' id='" + ddlClaimSubTypeId + "'>'" + ClaimSubTypeOption + "'</select></td>";
                        tbodyCliamdata += "<td><input type='text' required value='" + dt + "' placeholder='click here' class='form-control'data-field='datetime'><div id='dtBox' class='dtBox'></div></td>";
                        tbodyCliamdata += "<td><input type='text' ID='" + txtClaimAmt + "'   value='" + dt1[i].claimAmount + "' placeholder='Amount' class='form-control amt'></td>";

                        tbodyCliamdata += "<td><input type='text' required ID='" + txtKMIN + "' disabled value='" + dt1[i].kmin + "' placeholder='KM IN' class='form-control'></td>";
                        tbodyCliamdata += "<td><input type='text' required ID='" + txtKMOut + "' disabled value='" + dt1[i].kmout + "' placeholder='KM OUT' class='form-control'></td>";
                        tbodyCliamdata += "<td><select  class='form-control required  colorchg' id='" + txtClaimStatus + "'>'" + ClaimStatusOption + "' </select></td>";
                        tbodyCliamdata += "<td><select class='form-control required  colorchg' id='" + txtManagerStatus + "'>'" + ClaimStatusOption + "' </select></td>";
                        tbodyCliamdata += "<td><select class='form-control required  colorchg' id='" + txtRMStatus + "'>'" + ClaimStatusOption + "' </select></td>";
                        //tbodyCliamdata += "<td><select class='form-control required  colorchg' id='" + txtFinanceStatus + "'>'" + ClaimStatusOption + "' </select></td>";
                        tbodyCliamdata += "<td><input type='text' required ID='" + txtRemarks + "' value='" + dt1[i].remarks + "' placeholder='Remarks' class='form-control'></td>";
                        tbodyCliamdata += "<td><input type='text' required ID='" + txtMRemarks + "' value='" + dt1[i].Managerremarks + "' placeholder='Manager Remarks' class='form-control'></td>";
                        tbodyCliamdata += "<td><button type='button' required id='btnExport' value='" + dt1[i].images + "' onclick='downloadFile(this)' class='btn-success'><i class='fa fa-download' aria-hidden='true' ></i ></button > ";
                        tbodyCliamdata += "</tr>";



                    };
                    $('#tbodyReimbursement').html(tbodyCliamdata);
                    $("#txtTotal").val(tsum);
                    $("#tfooter").css("display", "table-row");
                    debugger;
                    /*datebind();*/
                    bgstatus();

                }
                else {

                }

                $('#tbodyReimbursement').html(tbodyCliamdata);
                $("#txtTotal").val(tsum);
                $("#tfooter").css("display", "table-row");
                /*datebind();*/
                bgstatus();
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


function downloadFile(control) {
    debugger
    var row = $(control).closest('tr');
    var fileName = row.find("button[id^='btnExport']").attr("data-file-name");
    var imageid = $("#images option:selected").val();

    $.ajax({
        url: "../Reimbursement/DownloadFile",
        type: 'POST',
        data: { fileName: fileName, imageid: imageid },
        success: function (data) {
            debugger
            var byteCharacters = atob(data.imageBytes); // Convert base64 to binary string
            var byteNumbers = new Array(byteCharacters.length);
            for (var i = 0; i < byteCharacters.length; i++) {
                byteNumbers[i] = byteCharacters.charCodeAt(i);
            }
            var byteArray = new Uint8Array(byteNumbers);

            var blob = new Blob([byteArray], { type: 'image/jpeg' }); // Create blob with JPEG type

            // Create a temporary URL for the Blob object
            var url = window.URL.createObjectURL(blob);

            // Create a temporary link element
            var link = document.createElement('a');
            link.href = url;
            link.download = fileName;

            // Trigger the download
            link.click();

            // Clean up the temporary URL
            window.URL.revokeObjectURL(url);
        },
        error: function (xhr, status, error) {
            // Handle error
            console.log(error);
        }
    });
}

function getFileType(fileName) {
    debugger
    // Extract the file extension from the file name
    var extension = fileName.substr(fileName.lastIndexOf('.') + 1).toLowerCase();

    //  to handle different file types
    if (extension === 'jpg' || extension === 'jpeg') {
        return 'jpeg';
    } else if (extension === 'png') {
        return 'png';
    } else {
        // Default to a generic file type
        return 'octet-stream';
    }
}

function getMimeType(fileType) {
    debugger
    // Map file types to corresponding MIME types
    var mimeTypes = {
        'jpeg': 'image/jpeg',
        'png': 'image/png',
        'octet-stream': 'application/octet-stream'
    };

    return mimeTypes[fileType] || mimeTypes['octet-stream'];
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
                //const moonLanding = new Date('July 20, 69 00:20:18');
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


// Approval Hear methods //
$('#ckbCheckAll').click(function () {
    debugger

    $('.ReportingRA').prop('checked', false);
    $('.ReportingRR').prop('checked', false);
    if ($('#ckbCheckAll').is(":checked") == true) {

        $(".ReportingRA").prop("checked", true);
    }

});

$('#ckbCheckAllOne').click(function () {
    debugger
    $('.PrejectHA').prop('checked', false);
    $('.PrejectHR').prop('checked', false);
    if ($('#ckbCheckAllOne').is(":checked") == true) {

        $(".PrejectHA").prop("checked", true);
    }


});

$('#ckbCheckAllTwo').click(function () {

    debugger
    $('.AccountA').prop('checked', false);
    $('.AccountR').prop('checked', false);
    if ($('#ckbCheckAllTwo').is(":checked") == true) {

        $(".AccountA").prop("checked", true);
    }

});


$("#btnFetchDataHead").click(function () {
    debugger
    var errors = '';
    var Month = $("#month option:selected").val();
    var year = $("#year option:selected").html();
    var EmpId = $('#txtid').val();

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
        var formData = new FormData();
        var rowCount = $('#tblReimbursement tbody tr').length;
        var ddlClaimTypeId = "ClaimType" + (rowCount + 1);
        var ClaimTypeOption = '<option value =""> Select Claim Type</option>';
        var ClaimSubTypeOption = '<option value =""> Select Claim Sub Type</option>';
        var ClaimStatusOption = '';
        formData.append('Month', Month);
        formData.append('year', year);
        formData.append('EmpId', EmpId);

        var appendrowth = "";

        $.ajax({
            url: "../Reimbursement/GetReimbursementApproval",
            type: "POST",
            data: formData,
            contentType: false,
            processData: false,
            dataType: "json",
            success: function (ResponseDt) {
                debugger
                var tsum = 0;
                if (ResponseDt.data.length > 0) {

                    var ResponseDtlength = ResponseDt.data.length;

                    for (var i = 0; i <= ResponseDtlength - 1; i++) {
                        var ReportingRA = 'ReportingRA';
                        var ReportingRR = 'ReportingRR';
                        var PrejectHA = 'PrejectHA';
                        var ProjectHR = 'ProjectHR';
                        var AccountA = 'AccountA';
                        var AccountR = 'AccountR';
                        ReportingRA = ReportingRA + i;
                        ReportingRR = ReportingRR + i;
                        PrejectHA = PrejectHA + i;
                        ProjectHR = ProjectHR + i;
                        AccountA = AccountA + i;
                        AccountR = AccountR + i;


                        let ms = Date.parse("March 21, 2012");

                        tsum += ResponseDt.data[i].claimAmount;
                        var appendrow = `<tr class="odd"><td class="sorting_1">` + ResponseDt.data[i].id + ` </td><td ><div class="hover-text">` + ResponseDt.data[i].emP_ID + `<span class="tooltip-text" id="top">` + ResponseDt.data[i].empName + `</span></div>
                        </td><td>` + ResponseDt.data[i].payoutType + `</td><td>` + ResponseDt.data[i].payoutSubType + `</td><td>` + ResponseDt.data[i].claimDate + `</td><td>` + ResponseDt.data[i].invoiceDate + `</td>
                        <td>` + ResponseDt.data[i].claimAmount + `</td><td><input class='form-control' type='text' id='" + txtRevisedAmt + "' placeholder='RevisedAmt'></td><td>` + ResponseDt.data[i].kmin + `</td><td>` + ResponseDt.data[i].kmout + `</td>
                      
                        <td class='text-center' enable><input type="checkbox" id=`+ ReportingRA + ` class="ReportingRA" name="chk" value="2" onclick="SelectOnlyOne(this,` + i + `)"><span class="text-success">
                        Approved</span> <br><input type="checkbox" id=`+ ReportingRR + ` class="ReportingRR" name="3" value="3" onclick="SelectOnlyOne(this,` + i + `)">
                        <span class="text-danger">Reject</span></td>
               
                        <td class="text-center"><input type="checkbox" id=`+ PrejectHA + ` class="PrejectHA" name="chk" value="2" onclick="SelectOnlyOne(this,` + i + `)">
                        <span class="text-success">Approved</span> <br><input type="checkbox" id=`+ ProjectHR + ` class="ProjectHR" name="3" value="3" onclick="SelectOnlyOne(this,` + i + `)" >
                        <span class="text-danger">Reject</span></td>
              
                        <td>` + ResponseDt.data[i].remarks + `</td>
                        <td><input class='form-control' type='text' id='" + txtRemarks + "' placeholder='Remarks'></td>
                        <td><button type='button' required id='btnExport' data-file-name='` + ResponseDt.data[i].images + `' onclick='downloadFile(this)'><i class='fa fa-download' aria-hidden='true'></button></td>
                        </tr>`;

                        appendrowth = appendrowth + appendrow;
                    }
                    debugger
                    $("#tbodyReimbursement").append(appendrowth);
                    $("#txtTotal").val(tsum);
                    $("#tfooter").css("display", "table-row");
                    SetDataTable();
                    setTimeout(function () {
                        debugger
                        for (var i = 0; i < ResponseDtlength; i++) {

                            var ReportingRAchk = 'ReportingRA' + i;
                            var ReportingRRchk = 'ReportingRR' + i;
                            var PrejectHAchk = 'PrejectHA' + i;
                            var ProjectHRchk = 'ProjectHR' + i;
                            var AccountAchk = 'AccountA' + i;
                            var AccountRchk = 'AccountR' + i;
                            var ManagerRemarks = 'txtRemarks';


                            if (ResponseDt.data[i].approvalLevel == 3) {
                                $('#' + ReportingRAchk).prop('disabled', true);
                                $('#' + ReportingRRchk).prop('disabled', true);
                                $('#' + PrejectHAchk).prop('disabled', true);
                                $('#' + ProjectHRchk).prop('disabled', true);
                                $('#' + ManagerRemarks).prop('disabled', true);
                            }
                            if (ResponseDt.data[i].approvalLevel == 4) {
                                $('#' + ReportingRAchk).prop('disabled', true);
                                $('#' + ReportingRRchk).prop('disabled', true);
                                $('#' + PrejectHAchk).prop('disabled', true);
                                $('#' + ProjectHRchk).prop('disabled', true);
                                $('#' + ManagerRemarks).prop('disabled', true);
                            }

                            if (ResponseDt.data[i].disableOption == 'head') {
                                $('#' + PrejectHAchk).prop('disabled', true);
                                $('#' + ProjectHRchk).prop('disabled', true);
                            }
                            if (ResponseDt.data[i].disableOption == 'manager') {
                                $('#' + ReportingRAchk).prop('disabled', true);
                                $('#' + ReportingRRchk).prop('disabled', true);

                            }

                            if (ResponseDt.data[i].projectHeadID == 1) {
                                $('#' + PrejectHAchk).prop('checked', true);
                            }
                            if (ResponseDt.data[i].projectHeadID == 2) {
                                $('#' + PrejectHAchk).prop('checked', true);
                            }
                            if (ResponseDt.data[i].projectHeadID == 3) {
                                $('#' + ProjectHRchk).prop('checked', true);
                            }

                            if (ResponseDt.data[i].finance_approval == 1) {
                                $('#' + AccountAchk).prop('checked', true);
                            }
                            if (ResponseDt.data[i].finance_approval == 2) {
                                $('#' + AccountAchk).prop('checked', true);
                            }
                            if (ResponseDt.data[i].finance_approval == 3) {
                                $('#' + AccountRchk).prop('checked', true);
                            }
                        }

                    }, 2000);
                }
            }
        });
    }
    else {
        $('#divMessage').html(errors);
        $('#divMessage').addClass(' shadow-border');
        return false;
    }
})


var isDataTableInitialized = false;
var dataTable;
function SetDataTable() {
    // Check if DataTable is already initialized
    if (!isDataTableInitialized) {
        // DataTable is not initialized, so initialize it
        dataTable = $('#tblReimbursement').DataTable({
            "bDestroy": false,
            "searching": false,
            "paging": false,
            "bInfo": false,
            "aLengthMenu": [[15, 25, 50, -1], [15, 25, 50, "All"]],
            "iDisplayLength": 15
        });

        isDataTableInitialized = true; // Update initialization flag
    } else {
        // DataTable is already initialized, so destroy and reinitialize it
        dataTable.destroy();
        dataTable = $('#tblReimbursement').DataTable({
            "bDestroy": false,
            "searching": false,
            "paging": false,
            "bInfo": false,
            "aLengthMenu": [[15, 25, 50, -1], [15, 25, 50, "All"]],
            "iDisplayLength": 15
        });
    }
}
//function SetDataTable() {
//    debugger
//    $('#tblReimbursement').DataTable({
//        //order: [[2, 'asc']],
//        //rowGroup: {
//        //    dataSrc: 2
//        //}
//        "bDestroy": false,
//        "searching": false,
//        "paging": false,
//        // "bFilter": false,
//        "bInfo": false,
//        "aLengthMenu": [[15, 25, 50, -1], [15, 25, 50, "All"]],
//        "iDisplayLength": 15,
//        //"pageLength": 25,
//        // "pagingType": "simple" //"full_numbers"
//        //language: {
//        //    paginate: {
//        //        previous: 'Prev',
//        //        next: 'Next'
//        //    },
//        //    aria: {
//        //        paginate: {
//        //            previous: 'Previous',
//        //            next: 'Next'
//        //        }
//        //    }
//        //}


//    });
//}
function SelectOnlyOne(e, i) {
    debugger
    $('#ReportingRA' + i).prop('checked', false);
    $('#ReportingRR' + i).prop('checked', false);
    $('#PrejectHA' + i).prop('checked', false);
    $('#ProjectHR' + i).prop('checked', false);
    $('#AccountA' + i).prop('checked', false);
    $('#AccountR' + i).prop('checked', false);
    $('#' + e.id).prop('checked', true);


    //debugger
    //alert(e.id)


}

$("#btnsubmitdata").click(function () {
    debugger
    var ClaimDetail = [];
    var formData = new FormData();

    var Month = $("#month option:selected").val();
    var year = $("#year option:selected").html();
    var EmpId = $('#txtid').val();


    $('#tblReimbursement tbody tr').each(function () {
        debugger
        var row = $(this);
        // get current row 2nd TD
        var appList = {};

        var RevisedAmount = row.find("td").eq(7).find('input[type="text"]').val();
        var MangerRemarks = row.find("td").eq(13).find('input[type="text"]').val();
        var ReportingRA = row.find(".ReportingRA").is(':checked');
        var ReportingRR = row.find(".ReportingRR").is(':checked');
        var PrejectHA = row.find(".PrejectHA").is(':checked');
        var PrejectHR = row.find(".PrejectHR").is(':checked');
        var AccountA = row.find(".AccountA").is(':checked');
        var AccountR = row.find(".AccountR").is(':checked');

        //appList.MangerRemarks = MangerRemarks;
        //appList.RevisedAmount = RevisedAmount;
        if (ReportingRA || ReportingRR || PrejectHA || PrejectHR || AccountA || AccountR) {

            var Name = row.find("td:eq(0)").text();
            var EMPID = row.find("td:eq(1)").text();
            var _date = row.find("td:eq(4)").text();
            //var ManagerRemark = row.find("td:eq(12)").text();

            //  var valHead = row.find("td").eq(8).find('input[type="checkbox"]').val()

            if (ReportingRA == true) {
                var valReportingRA = row.find(".ReportingRA").val();
                appList.ReportingManager = valReportingRA;
                appList.ProjectHeadID = 1;
                /* appList.Finance_Approval = 1;*/

            };
            if (ReportingRR == true) {
                var valReportingRR = row.find(".ReportingRR").val();
                appList.ReportingManager = valReportingRR;
                appList.ProjectHeadID = 1;
                appList.Finance_Approval = 1;
            }

            if (PrejectHA == true) {
                var valPrejectHA = row.find(".PrejectHA").val();
                appList.ProjectHeadID = valPrejectHA;
                appList.Finance_Approval = 1;
                appList.ReportingManager = 1;
            }
            if (PrejectHR == true) {
                var valPrejectHR = row.find(".PrejectHR").val();
                appList.ProjectHeadID = valPrejectHR;
                appList.Finance_Approval = 1;
                appList.ReportingManager = 1;
            }

            if (AccountA == true) {
                var valAccountA = row.find(".AccountA").val();
                appList.Finance_Approval = valAccountA;
                appList.ReportingManager = 1;
                appList.ProjectHeadID = 1;
            }
            if (AccountR == true) {
                var valAccountR = row.find(".AccountR").val();
                appList.Finance_Approval = valAccountR;

            }


            if (MangerRemarks != null) {
                debugger
                var claim = {
                    RevisedAmount: RevisedAmount,
                    MangerRemarks: MangerRemarks
                };
                ClaimDetail.push(claim);

            }


            //ClaimDetail.push(appList);



        }
    });

    formData.append('ClaimDetail', JSON.stringify(ClaimDetail));
    formData.append('year', year);
    formData.append('Month', Month);
    formData.append('EmpId', EmpId);
    //formData.append('ARDATE', _date);
    //formData.append('ManagerRemark', MangerRemarks);


    $.ajax({
        type: 'POST',
        url: "../Reimbursement/SaveHeadReimbursementApprovalDtl",
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        data: formData,
        contentType: false,
        processData: false,
        document: document.body,
        success: function (result) {
            debugger
            $('#msgSuccess').addClass('label-success').html('Reimbursemnet Approved Successfully');
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
});

var strtxt = "";
$('#btnExport').on('change', function () {
    debugger
    var fileReader = new FileReader();
    fileReader.onload = function () {
        debugger
        var data = fileReader.result;
        strtxt = data.toString();
    };
    fileReader.readAsDataURL($('#btnExport').prop('files')[0]);
});

$('#month').on('change', function () {

    $("#tbodyReimbursement").empty();
    $("#txtTotal").val('');
});
