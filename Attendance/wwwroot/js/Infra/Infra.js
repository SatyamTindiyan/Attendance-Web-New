
$(document).ready(function () {
    GetInfraValues();
    tablebind();
    GetPreviousDateInfra();
    //setinfradatatable();
});

var infraGrpid = '';
$("#infraType").autocomplete({
    source: function (request, response) {
        debugger
        $.ajax({
            url: "/Infra/GetInfraType",
            type: "POST",
            dataType: "json",
            data: { SearchValue: request.term },
            success: function (data) {
                debugger
                response($.map(data.lstInfra, function (item) {
                    debugger

                    return {
                        label: item.infraname,
                        value: item.infraname,
                        InfraGrpID: item.infraGrpID
                    };
                }))
            }
        })
    },
    messages: {
        noResults: "", results: ""
    },
    select: function (event, ui) {
        debugger
        $("#infraType").val(ui.item.infraGrp);
        //infraGrpid = ui.item.InfraGrpID;
        localStorage.setItem("infraGrpid", ui.item.InfraGrpID);
        $("#hdinfragrpid").val(ui.item.InfraGrpID);

        $("#infraSerialNumber").val('');
        $("#serviceTagNumber").val('');
        $("#modelnumber").val('');
        $("#imeinumber").val('');
        $("#infraimage").val(null);
        var hval = $("#hdinfragrpid").val();
        if (hval == '14') {

            $("#infraSerialNumber").val('OTHER');
            $("#hdinfragrpid").val('14');
            $("#serviceTagNumber").val('OTHER');
            $("#modelnumber").val('OTHER');
            $("#imeinumber").val('OTHER');
            $("#infraimage").val(null);
            $("#infraStatus").val('1');

            $("#infraSerialNumber").prop('disabled', true);
            $("#serviceTagNumber").prop('disabled', true);
            $("#infraimage").prop('disabled', true);
            $("#infraStatus").prop('disabled', true);
        }

        else {
            $("#infraSerialNumber").prop('disabled', false);
            $("#serviceTagNumber").prop('disabled', false);
            $("#infraimage").prop('disabled', false);
            $("#infraStatus").prop('disabled', false);


        }

    },
    change: function (event, ui) {
        debugger
        if ((ui.item) == null) {
            event.target.value = "";

            $("#txtwar").html('Option must be selected from the list only.');
            $("#warningModal").modal('show');
        }
    }
});
var infraGrpid = localStorage.getItem("infraGrpid");
//var infraGrpid = 12;//$('#hdinfragrpid').val();
// var infraGrpid = 12; //$('#hdinfragrpid').val();

$("#infraSerialNumber").autocomplete({
    source: function (request, response) {
        debugger
        infraGrpid = $("#hdinfragrpid").val();
        $.ajax({
            url: "../Infra/GetInfraStockSerialNumber",
            type: "POST",
            dataType: "json",
            data: {
                SearchValue: request.term,
                infraGrpid: infraGrpid,
                type: 1
            },
            success: function (data) {
                debugger
                response($.map(data.lstSerialNumber, function (item) {
                    debugger
                    return {
                        label: item.seriaL_NUMBER,
                        value: item.seriaL_NUMBER,
                        InfraID: item.InfraID,
                        SERVICE_TAG_NUMBER: item.servicE_TAG_NUMBER,
                        MODEL_NUMBER: item.modeL_NUMBER,
                        IMEI_NUMBER: item.imeI_NUMBER
                    };
                }))
            }
        })
    },
    messages: {
        noResults: "", results: ""
    },
    select: function (event, ui) {
        debugger
        $("#infraSerialNumber").val(ui.item.label);
        $("#serviceTagNumber").val(ui.item.SERVICE_TAG_NUMBER);
        $("#modelnumber").val(ui.item.MODEL_NUMBER);
        $("#imeinumber").val(ui.item.IMEI_NUMBER);
    },
    change: function (e, ui) {
        debugger
        if ((ui.item) == null) {
            e.target.value = "";
            $('#txtwar').html('Option must be selected from the list only.');
            $('#warningModal').modal('show');
        }
    }
});

//$("#txtfromdate").datepicker({
//    dateFormat: 'dd/mm/yy',
//    changeMonth: true,
//    changeYear: true,
//    //minDate: dateText
//    timeFormat: 'hh:mm TT'
//});

//$("#infraSerialNumber").autocomplete({
//    source: function (request, response) {
//        debugger
//        infraGrpid = $("#hdinfragrpid").val();
//        $.ajax({
//            url: "../Infra/GetInfraStockSerialNumber",
//            type: "POST",
//            dataType: "json",
//            data: {
//                SearchValue: request.term,
//                infraGrpid: infraGrpid, type: 2
//            },
//            success: function (data) {
//                debugger
//                response($.map(data.lstSerialNumber, function (item) {
//                    debugger
//                    return {
//                        label: item.servicE_TAG_NUMBER,
//                        value: item.servicE_TAG_NUMBER,
//                        InfraID: item.infraID,
//                        MODEL_NUMBER: item.modeL_NUMBER,
//                        SERIAL_NUMBER: item.seriaL_NUMBER,
//                        IMEI_NUMBER: item.imeI_NUMBER
//                    };
//                }))
//            }
//        })
//    },
//    messages: {
//        noResults: "",
//        results: ""
//    },
//    select: function (event, ui) {
//        debugger
//        $("#serviceTagNumber").val(ui.item.SERVICE_TAG_NUMBER);
//        $("#infraSerialNumber").val(ui.item.SERIAL_NUMBER);
//        $("#modelnumber").val(ui.item.MODEL_NUMBER);
//        $("#imeinumber").val(ui.item.IMEI_NUMBER);
//    },
//    change: function (e, ui) {
//        debugger
//        if ((ui.item) == null) {
//            e.target.value = "";
//            $("#txtwar").html('Option must be selected from the list only.');
//            $("#warningModal").modal('show');
//        }
//    }
//});

$('#btnSaveInfra').click(function () {
    debugger
    // var infratypeid = $('#hdinfragrpid').val();
    var infratypeid = infraGrpid;
    var infratype = $('#infraType').val();
    var serialnumber = $("#infraSerialNumber").val();
    var serTagnumber = $("#serviceTagNumber").val();
    var modelNumber = $("#modelnumber").val();
    var imeiNumber = $("#imeinumber").val();
    var infraStatus = $("#infraStatus").val();//$("#infraStatus :selected").text();
    var infraStatustext = $("#infraStatus option:selected").html();
    var infraImage = $("#infraimage").val();
    var remarks = $("#remarks").val();
    var recordid = $("#hdinrecordid").val();
    var isError = 0;
    //if (infraImage) {
    //    var allowedExtensions = /(\.jpg|\.jpeg|\.png)$/i;
    //    if (!allowedExtensions.exec(infraImage.name)) {
    //        $('#infraimage').addClass('border-danger').html("Image is not in correct format");
    //        isError = 1;
    //    } else {
    //        $('#infraimage').removeClass('border-danger');
    //    }
    //}

    if (serialnumber == '') {
        $('#infraSerialNumber').addClass('border-danger');
        isError = 1;
    }
    else {
        $('#infraSerialNumber').removeClass('border-danger');
    }
    if (infratype == '') {
        $('#infraType').addClass('border-danger');
        isError = 1;
    }
    else {
        $('#infraType').removeClass('border-danger');
    }
    if (serTagnumber == '') {
        $('#serviceTagNumber').addClass('border-danger');
        isError = 1;
    }
    else {
        $('#serviceTagNumber').removeClass('border-danger');
    }

    if (infraStatus == 0) {
        $('#infraStatus').addClass('border-danger');
        isError = 1;
    }
    else {
        $('#infraStatus').removeClass('border-danger');
    }
    if ((infraStatus == 2) || (infraStatus == 14) || (infraStatus == 15)) {
        if (infraImage == "") {
            $('#infraimage').addClass('border-danger');
            isError = 1;
        }
        else {
            $('#infraimage').removeClass('border-danger');
        }
    }
    if ((infraStatus == 2) || (infraStatus == 14) || (infraStatus == 15)) {
        if (remarks == "") {
            $('#remarks').addClass('border-danger');
            isError = 1;
        }
        else {
            $('#remarks').removeClass('border-danger');
        }
    }
    if (isError) {
        $('#msgsuccess').addClass('text-danger').html('Please fill all highlighted fields');
    }
    else {

        $('#msgsuccess').removeClass();
        var data = {

            INFRAGRP_ID: infratypeid,
            INFRA_TYPE: infratype,
            SERIAL_NUMBER: serialnumber,
            SERVICE_TAG_NUMBER: serTagnumber,
            MODEL_NUMBER: modelNumber,
            IMEI_NUMBER: imeiNumber,
            INFRA_STATUS: infraStatustext,
            INFRA_IMAGE: strtxt,
            REMARKS: remarks,
            RecordId: recordid,
        };

        $.ajax({
            type: 'POST',
            url: "../Infra/SaveInfraType",
            data: data,

            success: function (result) {
                debugger
                if (result == "success") {
                    $("#msgerror").addClass('text-danger').html('');
                    $('#txt').addClass('text-success').html("Infra saved successfully..");
                    $('#successModal').modal('show');
                    //  $("#msgsuccess").addClass('text-success').html('Infra saved successfully');
                    //window.location.reload();
                }
                else if (result == "Updated") {
                    $('#txt').addClass('text-success').html("Infra Updated successfully..");
                    $('#successModal').modal('show');

                } else if (result == "Record already exist.") {
                    $('#txt').addClass('text-danger').html("You Don't have permision to change infra after marking the Attendance");
                    $('#successModal').modal('show');
                }
                else {
                    $("#msgerror").addClass('text-danger').html('');
                    $('#txtwar').html(result);
                    $('#warningModal').modal('show');
                    // $("#msgsuccess").addClass('text-danger').html(result);

                }
            },
            error: function () {
                $("#msgerror").addClass('text-danger').html('');
                $('#txtwar').html('something went wrong...');
                $('#warningModal').modal('show');

            }
        });
    }
});

$('#succ').click(function () {
    debugger
    tablebind();
    $('#msgsuccess').val('');
    $('#infraType').val('');
    $("#infraSerialNumber").val('');
    $("#serviceTagNumber").val('');
    $("#modelnumber").val('');
    $("#imeinumber").val('');
    //$("#infraStatus :selected").val('');
    $('#infraStatus').find('option:first').prop('selected', 'selected');
    //$("#infraStatus:selected").text('');
    $("#infraimage").val('');
    $("#infraimage").val(null);
    $("#remarks").val('');
    $("#msgsuccess").html('');

});
$('#btnAddInfra').click(function () {
    debugger
    $('#msgsuccess').val('');
    $('#infraType').val('');
    $("#infraSerialNumber").val('');
    $("#serviceTagNumber").val('');
    $("#modelnumber").val('');
    $("#imeinumber").val('');
    //$("#infraStatus :selected").val('');
    $('#infraStatus').find('option:first').prop('selected', 'selected');
    //$("#infraStatus:selected").text('');
    $("#infraimage").val('');
    $("#remarks").val('');
});

//$('#uploadexcel').on('click', function () {

//    $("#btnSaveInfra").prop('disabled', true);
//});

var hid = '';
function tablebind() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "../Infra/GetEmpAttdenceInfraDetails",
        data: "{}",
        dataType: "json",
        success: function (response) {
            var tr = '';

            $("#infraTbale").empty();
            if (response != null) {
                if (response.lstinfradtl.length > 0) {
                    debugger
                    var scount = 0;
                    for (var i = 0; i < response.lstinfradtl.length; i++) {
                        hid = response.lstinfradtl[i].recordId;
                        scount = scount + 1;

                        tr += '<tr>';
                        tr += "<td>" + scount + "</td>";
                        tr += "<td>" + response.lstinfradtl[i].emP_ID + "</td>";
                        tr += "<td>" + response.lstinfradtl[i].infrA_TYPE + "</td>";
                        tr += "<td>" + response.lstinfradtl[i].seriaL_NUMBER + "</td>";
                        // tr += "<td>" + response.lstinfradtl[i].servicE_TAG_NUMBER + "</td>";
                        tr += "<td>" + response.lstinfradtl[i].modeL_NUMBER + "</td>";
                        tr += "<td>" + response.lstinfradtl[i].infrA_STATUS + "</td>";
                        tr += "<td>" + response.lstinfradtl[i].remarks + "</td>";
                        if (response.lstinfradtl[i].infrA_IMAGE != null && response.lstinfradtl[i].infrA_IMAGE != '')
                            tr += "<td><button data-toggle='modal' data-target='#imageModal' class='btn btn-sm btn-info fa fa-eye'>test</button></td>";
                        else
                            var imagebutton = '<a data-toggle="modal" data-target="#imageModal"onclick="GetInfraImage(' + response.lstinfradtl[i].recordId + ')" class="btn btn-sm btn-info fa fa-eye">test</a>'
                        tr += "<td>" + imagebutton + "</td>";
                        var deletebtn = '<a onclick="EditDetailsView(' + response.lstinfradtl[i].recordId + ')" class="btn btn-smm btn-success"><i class="fa text-white fa-pencil-square-o" aria-hidden="true"></i></a>'
                        //    < button type = "button" onclick = "deleteReport(' + response.lstinfradtl[i].recordId + ')" class="btn btn-smm btn-danger" > <i class="fa fa-trash" aria-hidden="true"></i></button >
                        tr += "<td>" + deletebtn + "</td>";
                        tr += '</tr>';
                    }
                    $('#infraTbale').append(tr);
                    //SetDataTable();
                }
            }
        },
        error: function (response) {
            //                      
        }
    });

}

var strtxt = "";
$('#infraimage').on('change', function () {
    debugger
    var fileReader = new FileReader();
    fileReader.onload = function () {
        debugger
        var data = fileReader.result;
        strtxt = data.toString();
    };
    fileReader.readAsDataURL($('#infraimage').prop('files')[0]);
});

$('#infraStatus').change(function () {
    debugger
    var infraStatus = $("#infraStatus").val();
    if ((infraStatus == 2) || (infraStatus == 14) || (infraStatus == 15)) {
        $("#imgdiv").show();
    }
    else if ((infraStatus == 1)) {

        $("#imgdiv").hide();
        $('#infraimage').val('');
        $('#infraimage').removeAttr('src')
        strtxt = "";
    }
});

function DeleteInfraRecordDetails(recordId) {
    debugger
    var result = confirm("Are you sure you want to delete this Infra");

    if (result) {
        if (recordId != '' || recordId > 0) {
            $.ajax({
                type: "GET",
                url: "../Infra/DeleteInfraRecordDetails/",
                data: { "RecordId": recordId },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    debugger
                    $("#msgerror").addClass('text-danger').html(data);
                    window.location.reload();
                    setTimeout(function () {
                        tablebind();
                        $("#msgerror").html('');
                    }, 2000);

                },
                error: function (data, error) {
                    $("#msgerror").addClass('text-danger').html(data);
                    setTimeout(function () {
                        $("#msgerror").html('');
                    }, 4000);
                }
            });
        }
    }
}

function EditDetailsView(RecordId) {
    debugger
    if (RecordId > 0) {
        $.ajax({
            type: "GET",
            url: "../Infra/EditBindInfraTable/",
            data: { "RecordId": RecordId },
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                debugger
                var status = data.infraTypes[0].infrA_STATUS;
                if (status == '1') {

                    $("#imgdiv").hide();
                    $('#infraimage').val('');
                    $('#infraimage').removeAttr('src')
                    strtxt = "";
                }
                $("#infraType").val(data.infraTypes[0].infrA_TYPE);
                $("#hdinrecordid").val(data.infraTypes[0].id);
                $("#infraSerialNumber").val(data.infraTypes[0].seriaL_NUMBER);
                $('#infraStatus option:selected').remove();
                $('#infraStatus option[value=' + status + ']').attr("selected", "selected");
                $("#modelnumber").val(data.infraTypes[0].modeL_NUMBER);
                $("#imeinumber").val(data.infraTypes[0].imeI_NUMBER);
                $("#remarks").val(data.infraTypes[0].remarks);

            }

        });
    }
}
function SetDataTable() {
    $('#infraTbaleth').DataTable({
        //order: [[2, 'asc']],
        //rowGroup: {
        //    dataSrc: 2
        //}
        //"bDestroy": true,
        //"searching": true,
        //"paging": true,
        // "bFilter": false,
        //"bInfo": false,
        //"aLengthMenu": [[15, 25, 50, -1], [15, 25, 50, "All"]],
        "iDisplayLength": 10,
        //"pageLength": 25,
        // "pagingType": "simple" //"full_numbers"
        //language: {
        //    paginate: {
        //        previous: 'Prev',
        //        next: 'Next'
        //    },
        //    aria: {
        //        paginate: {
        //            previous: 'Previous',
        //            next: 'Next'
        //        }
        //    }
        //}


    });
}

function GetInfraValues() {
    debugger
    $.ajax({
        type: "GET",
        url: "../Infra/Getinfrastatus/",
        data: {},
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            debugger

            $.each(data.data, function (item, value) {
                debugger
                $('#infraStatus').append($("<option></option>").val(value.id).html(value.infraStatus));
            })
            //$("#infraStatus").append(selectCircle);


        },
        error: function (data, error) {
            $("#msgerror").addClass('text-danger').html(data);
            setTimeout(function () {
                $("#msgerror").html('');
            }, 4000);
        }
    });

}

function GetInfraImage(RecordId) {
    debugger
    if (RecordId > 0) {
        $.ajax({
            type: "GET",
            url: "../Infra/GetInfraImage/",
            data: { "RecordId": RecordId },
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                debugger;

                var Image = data.imageInfras[0].infrA_IMAGE;
                var imgHtml = '<img src="' + Image + '" class="img-fluid" alt="Infra Image">';

                $('#infraimage2').html(imgHtml);
                $('#imageModal').modal('show');
            }
        });
    }

}

function GetPreviousDateInfra() {
    debugger
    $.ajax({
        type: "GET",
        url: "../Infra/GetInfraPreviousDate/",
        data: {},
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            debugger
            if (data.getdate[0].result == 103) {

                $("#dateinfra").hide();
            }
            else {
                $("#dateinfra").show();
                $("#txtfromdate").val(data.getdate[0].previousAttendanceDate);
            }
            
        },
        error: function (data, error) {
            $("#msgerror").addClass('text-danger').html(data);
            setTimeout(function () {
                $("#msgerror").html('');
            }, 4000);
        }
    });
}


$('#btnUploadExcel').on("click", function () {
    debugger
    $('#spanMessage').html('');
    var errors = '';
    var formData = new FormData();
    var ExcelFileTypes = ["xls", "xlsx"];
    var ExcelFiles = $('#uploadexcel')[0].files;
    if (ExcelFiles.length > 0) {
        var fileName = $("#uploadexcel").val();
        var fileType = $("#uploadexcel").val().split('.').pop();
        var fileSize = $('#uploadexcel')[0].files[0].size;
        if ($.inArray(fileType, ExcelFileTypes) < 0 && fileSize > 0) {
            errors += '<div class="col-sm-12 text-left">Employee detail file must be xls/xlsx</div>';
        }
        else {
            formData.append('uploadEmployeeExcel', $('#uploadexcel')[0].files[0]);
        }
    }
    else {
        errors += '<div class="col-sm-12 text-left">Employee detail file must be xls/xlsx</div>';
    }

    if (errors != '') {
        $('#spanMessage').addClass('text-danger').html(errors);
    }
    else {
        $('#spanMessage').removeClass();
        $('#spanMessage').html('');

        $.ajax({
            type: "POST",
            url: "../Infra/SaveUploadExcelItems",
            data: formData,
            contentType: false,
            processData: false,
            context: document.body,
            success: function (data) {

                if (data == "success") {
                    $("#spanMessage").addClass('text-success').html('<div class="col-sm-12 text-left">File successfully saved</div>');
                    BindQuatations();
                }
                else {
                    $("#spanMessage").addClass('text-danger').html('<div class="col-sm-12 text-left">' + data + '</div>');
                }
            },
            error: function (xhr) {
                $("#spanMessage").addClass('text-danger').html('<div class="col-sm-12 text-left">File not saved.</div>');
            }
        });
    }

});




