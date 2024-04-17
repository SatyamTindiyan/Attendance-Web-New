

$(document).ready(function () {
    getyear();
    getmonth();

   


});
$(document).on('input', '.invoiceamt, .taxid', function () {
    debugger
    var $row = $(this).closest('tr');
    var invoiceAmount = parseFloat($row.find('.invoiceamt').val()) || 0;
    var tax = parseFloat($row.find('.taxid').val()) || 0;
    var totalAmount = invoiceAmount + tax;
    $row.find('.totalAmt').val(totalAmount.toFixed(2));
});

var today = new Date();
var approvallbl = $('#myHiddenVar').val();
// Approval Hear methods //
$('#ckbCheckAllreject').click(function () {
    debugger
    $('.AccountA').prop('checked', false);
    $('.AccountR').prop('checked', false);
    if ($('#ckbCheckAllreject').is(":checked") == true) {

        $('.AccountR').prop('checked', true);
    }


});

$('#ckbCheckAllapprove').click(function () {

    debugger
    $('.AccountA').prop('checked', false);
    $('.AccountR').prop('checked', false);
    if ($('#ckbCheckAllapprove').is(":checked") == true) {

        $(".AccountA").prop("checked", true);
      
    }

});
//function to select only one approve or reject radio btn//
function SelectOnlyOne(e, i) {
    debugger
    $('#ReportingRA' + i).prop('checked', false);
    $('#ReportingRR' + i).prop('checked', false);
    $('#PrejectHA' + i).prop('checked', false);
    $('#ProjectHR' + i).prop('checked', false);
    $('#AccountA' + i).prop('checked', false);
    $('#AccountR' + i).prop('checked', false);
    $('#' + e.id).prop('checked', true);
}

//function set table //
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

//function to export data in Excel sheet//
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
//Function to get sum of claim amt. //
function TotalAmt(control) {

    $("#txtTotal").val('');
    var sum = 0;
    $('.totalprice').each(function () {

        sum += parseFloat($(this).val());  // Or this.innerHTML, this.innerText

    })

    $("#txtTotal").val(sum);
}
//function to get year in Dropdown//
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
//function to get month in Dropdown//
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
                const moonLanding = new Date('July 20, 69 00:20:18');

                //console.log(moonLanding.getMonth());
                var currentmonth = (moonLanding.getMonth() + 1).toString();
                /* $("#month").val(currentmonth);*/
                $("#month option[value='" + currentmonth + "']").prop("selected", true);

                $("#month").prop("disabled", false);
            }
        },
        error: function (data, error) {
            debugger
            $("#spanMessage").addClass('text-danger').html(data);
        }

    });
}
//function to Download the bill or invoice//
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

// function to fetch the data in table //
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
    if (errors == '') {
        var formData = new FormData();
        //var rowCount = $('#tblReimbursement tbody tr').length;
        formData.append('Month', Month);
        formData.append('year', year);
        formData.append('EmpId', EmpId);
       
        $.ajax({
            url: "../Reimbursement/GetAcReimbursementApproval",
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
                    var appendrowth = "";
                    for (var i = 0; i < ResponseDtlength; i++) {
                        var invoiceamt = "invoiceamt_" + i;
                        var taxid = "taxid_" + i;
                        var totalAmt = "totalAmt_" + i;
                        var invoiceAmount = parseFloat($("#" + invoiceamt).val()) || 0;
                        var tax = parseFloat($("#" + taxid).val()) || 0;
                        var totalAmount = invoiceAmount + tax;
                        var txtDatetime = "txtDatetime" + i;
                        var AccountA = 'AccountA';
                        var AccountR = 'AccountR';
                        AccountA = AccountA + i;
                        AccountR = AccountR + i;
                        totalAmt = totalAmount;
                        
                        $("#" + totalAmt).val(totalAmount.toFixed(2));

                        let ms = Date.parse("March 21, 2012");

                        tsum += ResponseDt.data[i].claimAmount
                        var appendrow = `<tr>
                        <td class="sorting_1">` + ResponseDt.data[i].id + `</td>
                        <td>`+ ResponseDt.data[i].empName + `</td>
                        <td>` + ResponseDt.data[i].payoutType + `</td>
                        <td>` + ResponseDt.data[i].claimDate + `</td>
                        <td>` + ResponseDt.data[i].invoiceDate + `</td>
                        <td>` + ResponseDt.data[i].claimAmount + `</td>
                        <td>` + ResponseDt.data[i].revisedAmount + `</td>
                        <td>` + ResponseDt.data[i].remarks + `</td>
                        <td>` + ResponseDt.data[i].managerRemarks + `</td>

                         <td class='form-control' enable><input type="checkbox" id=`+ AccountA + ` class="AccountA" name="chk" value="2" onclick="SelectOnlyOne(this,` + i + `)">
                         <span class="text-success">Approved</span> <br>
                         <input type="checkbox" id=`+ AccountR + ` class="AccountR" name="3" value="3" onclick="SelectOnlyOne(this,` + i + `)">
                         <span class="text-danger">Reject</span></td>
                         <td><input type='datetime'  placeholder='click here' class='form-control datepicker' data-field='datetime'  id="${txtDatetime}" ></td> 

                        <td><input class='form-control' type='text' id='" + invoicenumber + "' placeholder='Invoice Number'></td>
                        <td><input class='form-control' type='text' id='" + trnNumber + "' placeholder='TRN Number'></td>
                        <td><input class='form-control other invoiceamt'  type='number' id=`+ invoiceamt +` placeholder='Invoice Amount'></td>
                        <td><input class='form-control other taxid' type='number' id=`+ taxid +` placeholder='Tax'></td>
                        <td><input class='form-control totalAmt' type='number' id=`+ totalAmt + ` placeholder='Total Amount' readonly></td>
                        <td><select class='form-control'  id='" +  + "'>" + creditTax + " <option>Yes</option><option>No</option></select></td>
                        <td><input class='form-control' type='text' id='" + accountRemark + "' placeholder='Account Remark'></td>

                        <td><button type='button' required id='btnExport' data-file-name='` + ResponseDt.data[i].images + `' onclick='downloadFile(this)'><i class='fa fa-download' aria-hidden='true'></button></td>
                        </tr>`;

                        appendrowth = appendrowth + appendrow;

                    }

                    $("#tbodyReimbursement").append(appendrowth);
                    debugger
                    $("#txtTotal").val(tsum);
                    $("#tfooter").css("display", "table-row");
                    $("#btndiv").css("display", "block");
                    $(".datepicker").datepicker({
                        dateFormat: "yy-mm-dd",
                        changeMonth: true,
                        changeYear: true,
                        //minDate: dateText
                        timeFormat: "hh:mm TT"
                    });

                    SetDataTable();
                    setTimeout(function () {
                        debugger
                        for (var i = 0; i < ResponseDtlength; i++) {

                          
                            var AccountAchk = 'AccountA' + i;
                            var AccountRchk = 'AccountR' + i;
                            var ManagerRemarks = 'txtRemarks';
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

//function to save the updated account record //
$("#btnsubmitdata").click(function () {
    debugger
    var ClaimDetail = new Array();
    var formData = new FormData();
    $('#tblReimbursement tbody tr').each(function () {
        debugger
        var row = $(this);
        var appList = {};
        var ClaimDetail = new Array();
       
        var VerificationDate_ = row.find("td").eq(10).find('input[type="datetime"]').val();
        var InvoiceNumber_ = row.find("td").eq(11).find('input[type="text"]').val();
        var TRNnumber_ = row.find("td").eq(12).find('input[type="text"]').val();
        var InvoiceAmount_ = row.find("td").eq(13).find('input[type="number"]').val();
        var TaxAmount_ = row.find("td").eq(14).find('input[type="number"]').val();
        var TotalAmount_ = row.find("td").eq(15).find('input[type="number"]').val();
        var CreditTax_ = row.find("td").eq(16).find('option:selected').val();
        var AccountRemark_ = row.find("td").eq(17).find('input[type="text"]').val();
        var AccountA_ = row.find(".AccountA").is(':checked');
        var AccountR_ = row.find(".AccountR").is(':checked');

        if (AccountA || AccountR) {

            var Month = $("#month option:selected").val();
            var year = $("#year option:selected").html();
            var EmpId = $('#txtid').val();

            var Name = row.find("td:eq(0)").text();
            var EMPID = row.find("td:eq(2)").text();
            var _date = row.find("td:eq(4)").text();

            if (AccountA == true) {
                var valAccountA = row.find(".AccountA").val();
                appList.Finance_Approval = valAccountA;
                //appList.ReportingManager = 1;
                //appList.ProjectHeadID = 1;
            }
            if (AccountR == true) {
                var valAccountR = row.find(".AccountR").val();
                appList.Finance_Approval = valAccountR;

            }
            appList = {
                VerificationDate= VerificationDate_,
                InvoiceNumber   = InvoiceNumber_,
                TRNNumber = TRNnumber_,
                InvoiceAmount = InvoiceAmount_,
                TaxAmount = TaxAmount_,
                TotalAmount = TotalAmount_,
                CreditTax = CreditTax_,
                AccountRemark = AccountRemark_,
               
            };
            ClaimDetail.push(appList);
            formData.append('ClaimDetail', JSON.stringify(ClaimDetail));
            formData.append('year', year);
            formData.append('Month', Month);
            formData.append('EmpId', EmpId);
            formData.append('ARDATE', _date);

        }
    });
    $.ajax({
        type: 'POST',
        url: "../Reimbursement/SaveAccountReimbursementApproval",
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
//function to search data in table//
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


