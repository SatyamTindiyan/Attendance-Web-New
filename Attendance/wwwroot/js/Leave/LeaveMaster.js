
$(document).ready(function () {

    BindtblLeaveMaster()

});


$("#txtFromDate").datepicker({
    dateFormat: 'dd/mm/yy',
    changeMonth: true,
    changeYear: true,
    //minDate: dateText
    timeFormat: 'hh:mm TT'
});

$("#txtToDate").datepicker({
    dateFormat: 'dd/mm/yy',
    changeMonth: true,
    changeYear: true,
    //minDate: dateText
    timeFormat: 'hh:mm TT'
});


$(function () {
    $('#txtleaveCode').keyup(function () {
        $(this).val($(this).val().toUpperCase());
    });
});

$(function () {
    $('#txtleaveName').keyup(function () {
        $(this).val($(this).val().toUpperCase());
    });
});
function cleatTextBox() {
    $('#txtleaveName').val('');
    $('#leavegroup').find('option:first').prop('selected', 'selected');
    $('#ddlleavecreditType').find('option:first').prop('selected', 'selected');
    $('#txtleaveCode').val('');
    $('#txtleaveCredite').val('');
    $('#txtRemarks').val('');
    $('#txtFromDate').val('');
    $('#txtToDate').val('');
    $('#txtQuantity').val('');
}


$('#btnCancelleaveMaster').click(function () {
    cleatTextBox();
});

$('#btnSaveleaveMaster').click(function () {
    debugger
    var leaveTypegroup = $('#leavegroup').find(":selected").val()
    var leaveName = $('#txtleaveName').val();
    var recordid = $('#hiddenRecordId').val();
    // var leaveType = $('#ddlleaveType').find(":selected").val()
    var leaveTypeCode = $('#txtleaveCode').val();
    var leaveCrediteType = $('#ddlleavecreditType').find(":selected").val();
    var leaveCredite = $('#txtleaveCredite').val();
    var remarks = $('#txtRemarks').val();
    var FromDate = $('#txtFromDate').val();
    var ToDate = $('#txtToDate').val();
    var Quantity = $('#txtQuantity').val();
    var isError = 0;


    if (leaveTypegroup == '' || leaveTypegroup == 0 || leaveTypegroup == null) {
        $('#leavegroup').addClass('border-danger');
        isError = 1;
    } else {
        $('#leavegroup').removeClass('border-danger');
    }

    if (leaveName == '' || leaveName == null) {
        $('#txtleaveName').addClass('border-danger');
        isError = 1;
    } else {
        $('#txtleaveName').removeClass('border-danger');
    }


    if (leaveTypeCode == '' || leaveTypeCode == null) {
        $('#txtleaveCode').addClass('border-danger');
        isError = 1;
    } else {
        $('#txtleaveCode').removeClass('border-danger');
    }
    if (leaveCrediteType == '' || leaveCrediteType == 'Leave Credit Type') {
        $('#ddlleavecreditType').addClass('border-danger');
        isError = 1;
    } else {
        $('#ddlleavecreditType').removeClass('border-danger');
    }
    if (leaveCredite == '' || leaveCredite == null) {
        $('#txtleaveCredite').addClass('border-danger');
        isError = 1;
    } else {
        $('#txtleaveCredite').removeClass('border-danger');
    }

    if (FromDate == '' || FromDate == null) {
        $('#txtFromDate').addClass('border-danger');
        isError = 1;
    } else {
        $('#txtFromDate').removeClass('border-danger');
    }
    if (ToDate == '' || ToDate == null) {
        $('#txtToDate').addClass('border-danger');
        isError = 1;
    } else {
        $('#txtToDate').removeClass('border-danger');
    }
    if (Quantity == '' || Quantity == null) {
        $('#txtQuantity').addClass('border-danger');
        isError = 1;
    } else {
        $('#txtQuantity').removeClass('border-danger');
    }

    if (isError == 1) {


    } else {

        var data = {
            LeaveGroupType: leaveTypegroup,
            LEAVE_DESC: leaveName,
            LEAVE_CODE: leaveTypeCode,
            LeaveCredite: leaveCredite,
            Remarks: remarks,
            LeaveCrediteType: leaveCrediteType,
            FromDate: FromDate,
            ToDate: ToDate,
            LEAVE_COUNT: Quantity,
            RecordId: recordid,
        };

        $.ajax({
            type: 'POST',
            url: '../Leave/SaveLeaveMaster',
            data: data,

            success: function (result) {
                debugger
                if (result == "success") {
                    $("#validatemsgS").addClass('text-success').html('Insert data sucessfully');
                    setTimeout(function () {
                        $('#validatemsgS').html('');
                        cleatTextBox();
                        BindtblLeaveMaster();
                    }, 4000);
                }
                else {

                    $("#validatemsgE").addClass('text-danger').html(result);
                    setTimeout(function () {
                        $('#validatemsgE').html('');
                        cleatTextBox();
                    }, 4000);
                }
            },
            error: function () {
                alert('error');
            },

        });
    };
});

function BindtblLeaveMaster() {
    debugger
    var id = $('#hiddenRecordId').val();
    var data = {
        RecordId: id
    }
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: '../Leave/GetLeaveMasterDetail',
        data: data,
        dataType: "json",
        success: function (response) {

            debugger
            $("#tblLeaveMasterBody").empty();

            if (response.length > 0) {
                var tr;
                var scount = 0;
                for (var i = 0; i < response.length; i++) {
                    debugger
                    scount = scount + 1;
                    tr = $('<tr/>');
                    tr.append("<td>" + scount + "</td>");
                   /* tr.append("<td>" + response[i].leaveGroupType + "</td>");*/
                    tr.append("<td>" + response[i].leavE_DESC + "</td>");
                    tr.append("<td>" + response[i].leavE_CODE + "</td>");
                    tr.append("<td>" + response[i].leaveCrediteType + "</td>");
                    tr.append("<td>" + response[i].leaveCredite + "</td>");
                    tr.append("<td>" + response[i].leavE_COUNT + "</td>");
                    //tr.append("<td>" + response[i].FromDate + "</td>");
                    //tr.append("<td>" + response[i].ToDate + "</td>");
                    tr.append("<td>" + response[i].remarks + "</td>");
                    var editbtn = '<a class="btn btn-smm btn-danger" onclick="EditLeaveMasterDetails(' + response[i].id + ')"> <i class="fa fa-edit" aria-hidden="true"></i></a >';
                   // tr.append("<td>" + editbtn + "</td>");
                    var deletebtn = '<a class="btn btn-smm btn-info" onclick="DeleteLeaveMasterDetails(' + response[i].id + ')"> <i class="fa fa-trash" aria-hidden="true"></i></a> ';
                    tr.append("<td>" + editbtn + " " + deletebtn + "</td>");
                    $('#tblLeaveMasterBody').append(tr);
                }

            }

        },
        error: function (response) {
            //                      
        }
    });
}

function DeleteLeaveMasterDetails(RecordId) {
    debugger
    var result = confirm("Are you sure you want to delete this leave");
    if (result) {
        if (RecordId != '' || RecordId > 0) {
            $.ajax({
                type: "GET",
                url: '../Leave/DeleteLeaveMasterDetails',
                /*data: { "Empid": Empid, "RecordId": RecordId},*/
                data: { "RecordId": RecordId },
                contentType: "application/json; charset=utf-8",
                success: function (data) {


                    $('#validatemsgS').html(data);
                    BindtblLeaveMaster();

                    setTimeout(function () {
                        $('#validatemsgS').html('');
                    }, 4000);
                },
                error: function (data, error) {
                    $('#validatemsgE').html(data);
                    setTimeout(function () {
                        $('#validatemsgE').html('');
                    }, 2000);
                }
            });
        }
    }
}

function EditLeaveMasterDetails(RecordId) {
    debugger
    $('#hiddenRecordId').val(RecordId);
    if (RecordId != '' || RecordId > 0) {
        $.ajax({
            type: "GET",
            url: '../Leave/EditLeaveMasterDetail',
            /*data: { "Empid": Empid, "RecordId": RecordId},*/
            data: { "RecordId": RecordId },
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                debugger
                //$('#leavegroup').val(response[0].leaveGroupType);
                //$("#leavegroup").prop('disabled', true);
                $('#txtleaveName').val(response[0].leavE_DESC);
                $('#txtleaveCode').val(response[0].leavE_CODE);
                $('#ddlleavecreditType').val(response[0].leaveCrediteType);
                $('#txtleaveCredite').val(response[0].leaveCredite);
                $('#txtRemarks').val(response[0].remarks);
                //$('#txtFromDate').val(response[0].FromDate);
                //$('#txtToDate').val(response[0].ToDate);
                $('#txtQuantity').val(response[0].leavE_COUNT);

            },
            error: function (data, error) {
                $('#validatemsgE').html('Error.');
                setTimeout(function () {
                    $('#validatemsgE').html('');
                }, 2000);

            }
        });
    }
}





