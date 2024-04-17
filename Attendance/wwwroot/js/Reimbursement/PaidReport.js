$(document).ready(function () {
    getyear()
    
});
$('#btnFetchData').on('click', function () {
    debugger
    tablebind();
});
function tablebind() {
    debugger
    debugger;
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
        debugger
        formData.append('Month', Month);
        formData.append('year', year);
        formData.append('EmpId', EmpId);

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../Reimbursement/GetPaidReimbursement",
            data: formData,
            dataType: "json",
            contentType: false,
            processData: false,
            context: document.body,
            success: function (response) {
                var tr = '';
                debugger
                $("#claimTable").empty();
                if (response != null) {
                    if (response.lstReimbursement.length > 0) {

                        var scount = 0;
                        for (var i = 0; i < response.lstReimbursement.length; i++) {
                            debugger
                            scount = scount + 1;
                            tr += '<tr>';
                            tr += "<td>" + scount + "</td>";
                            tr += "<td>" + response.lstReimbursement[i].emP_ID + "</td>";
                            tr += "<td>" + response.lstReimbursement[i].payoutTypeID + "</td>";
                            tr += "<td>" + response.lstReimbursement[i].payoutSubTypeID + "</td>";
                            tr += "<td>" + response.lstReimbursement[i].claimDate + "</td>";
                            tr += "<td>" + response.lstReimbursement[i].claimAmount + "</td>";
                            tr += "<td>" + response.lstReimbursement[i].kmin + "</td>";
                            tr += "<td>" + response.lstReimbursement[i].kmout + "</td>";
                            tr += "<td>" + response.lstReimbursement[i].remarks + "</td>";
                            tr += "<td>" + response.lstReimbursement[i].claimStatus + "</td>";
                            tr += "<td>" + response.lstReimbursement[i].managerApproval + "</td>";
                            tr += "<td>" + response.lstReimbursement[i].rM_Approval + "</td>";
                            tr += "<td>" + response.lstReimbursement[i].finance_Approval + "</td>";
                            if (response.lstReimbursement[i].images != null && response.lstReimbursement[i].images != '')
                                tr += "<td> <img data-toggle='modal' data-target='#infraFullImg' id='infatmgId''name='Infra' style='max-width:50px; max-height:50px;' src=" + response.lstReimbursement[i].images + "></td>";
                            else
                                tr += "<td></td>";
                            var deletebtn = '<a class="btn btn-smm btn-danger text-white" onclick="DeleteInfraRecordDetails(' + response.lstReimbursement[i].RecordId + ')"> <i class="fa fa-trash" aria-hidden="true"></i></a >';
                            tr += "<td>" + deletebtn + "</td>";
                            tr += '</tr>';
                        }
                        $('#claimTable').append(tr);
                    }
                }
            },

            error: function (response) {
                //                      
            }
        });
    }

}

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