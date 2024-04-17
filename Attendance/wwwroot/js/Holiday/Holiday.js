$(document).ready(function () {
    tablebind()
    $("#Holydaydesc").attr('disabled', true);

});

$('#btnSaveholiday').click(function () {
    debugger
    var countryid = $('#ddlcountry').find(":selected").val();
    var holidayDesc = $('#Holydaydesc').find(":selected").html();
    var holydayName = $('#holidaydescription').find(':selected').html();
    var holydayNamev = $('#holidaydescription').find(':selected').val();
    var holidaydate = $('#holidaydate').val();
    var weekday = $("#weekday").find(":selected").val();
    var remarks = $("#remarks").val();
    var recordid = $('#hiddenRecordId').val();
    var country = $('#ddlcountry').find(":selected").html();
    var state = $('#ddlstateholiday').find(":selected").html();
    var statev = $('#ddlstateholiday').find(":selected").val();
    var isError = 0;

    if (countryid == '') {
        $('#ddlcountry').addClass('border-danger');
        isError = 1;
    }
    else {
        $('#ddlcountry').removeClass('border-danger');
    }
    if (holydayNamev == '') {
        $('#holidaydescription').addClass('border-danger');
        isError = 1;
    }
    else {
        $('#holidaydescription').removeClass('border-danger');
    }
    if (holidayDesc == '') {
        $('#Holydaydesc').addClass('border-danger');
        isError = 1;
    }
    else {
        $('#Holydaydesc').removeClass('border-danger');
    }

    if (holidaydate == '') {
        $('#holidaydate').addClass('border-danger');
        isError = 1;
    }
    else {
        $('#holidaydate').removeClass('border-danger');
    }
    if (state == '') {
        $('#ddlstateholiday').addClass('border-danger');
        isError = 1;
    }
    else {
        $('#ddlstateholiday').removeClass('border-danger');
    }
    if (weekday == '') {
        $('#weekday').addClass('border-danger');
        isError = 1;
    }
    else {
        $('#weekday').removeClass('border-danger');
    }
    if (remarks == '') {
        $('#remarks').addClass('border-danger');
        isError = 1;
    }
    else {
        $('#remarks').removeClass('border-danger');
    }



    if (isError) {
        $('#msgsuccess').addClass('text-danger').html('Please fill all highlighted fields');
    }
    else {

        $('#msgsuccess').removeClass();
        var data = {

            countryid: countryid,
            HOLIDAY_NAME: holydayName,
            HOLIDAY_DESC: holidayDesc,
            HOLIDAY_DATE: holidaydate,
            WEEKDAY: weekday,
            REMARKS: remarks,
            Country: country,
            State: state,
            RecordId: recordid,
        };

        $.ajax({
            type: 'POST',
            url: "../Holiday/SaveHolidayType",
            data: data,
            // processData: false,
            ///* dataType:'json',*/
            // contentType: false,
            success: function (result) {
                debugger
                if (result == "success") {
                    $('#txt').html("Record sucessfully Saved.");
                    $('#successModal').modal('show');
                }
                else {
                    $("#txterror").addClass('text-danger').html(data);
                    $('#dangerModal').modal('show');


                }
            },
            error: function () {
                $('#txterror').html('Format is not match.');
                $('#dangerModal').modal('show');


            },
        });
    };
});
var Countries = []
function LoadCountry() {
    debugger
    $('#ddlcountry').empty();
    $('#ddlcountry').append($("<option></option>").val('').html('Select Country'));
    $.ajax({
        type: "POST",
        url: "/Holiday/GetCountry",
        context: document.body,
        success: function (data) {
            debugger

            if (data != '') {
                $.each(data.lstCoun, function (item, value) {

                    $("#ddlcountry").append($("<option></option>").val(value.encryptcountryID).html(value.country));
                })
                $("#ddlcountry").prop("disabled", false);
            }
        },
        error: function (data, error) {
            debugger
            $("#spanMessage").addClass('text-danger').html(data);
        }

    });
}
$('#ddlcountry').change(function () {
    debugger
    tablebind();
    GetStateList();
    GetHolidaytype();
});

LoadCountry($('#ddlcountry'));

function GetHolidaytype() {
    debugger
    var countryidlist = $('#ddlcountry option:selected').val();
    $('#holidaydescription').empty();
    $('#holidaydescription').append($("<option></option>").val('').html('Select Holiday Name'));
    $.ajax({
        type: "GET",
        url: "../Holiday/HolidayDesclist",
        context: document.body,
        data: { 'country': countryidlist },
        success: function (data) {
            debugger

            if (data != '') {
                $.each(data.lstholidaytype, function (item, value) {
                    debugger
                    $("#holidaydescription").append($("<option></option>").val(value.encryptid).html(value.holidaY_NAME));
                    //$("#HolidaytypeID").append($("<option></option>").val(value.id).html(value.holidaY_TYPE));
                })
                $("#holidaydescription").prop("disabled", false);
            }
        },
        error: function (data, error) {
            debugger
            $("#spanMessage").addClass('text-danger').html(data);
        }

    });
}
$("#holidaydescription").on('change', function () {
    debugger
    var holidaydec = $("#holidaydescription").val();
    if (holidaydec != '' && holidaydec != 'undefined') {
        $("#Holydaydesc").attr('disabled', false);

    }
    else {
        $("#Holydaydesc").attr('disabled', true);
    }

})

$("#holidaydate").datepicker({
    dateFormat: "dd/mm/yy",
    changeMonth: true,
    changeYear: true,
    //minDate: dateText
    timeFormat: "hh:mm TT"
});
function GetStateList() {
    debugger
    var countryidlist = $('#ddlcountry option:selected').val();
    $('#ddlstateholiday').empty();
    $('#ddlstateholiday').append($("<option></option>").val('').html('Select state'));
    $.ajax({
        type: "GET",
        url: "../Holiday/GetStateListBasedOnCompanyId",
        context: document.body,
        data: { 'country': countryidlist },
        success: function (data) {
            debugger

            if (data != '') {
                $.each(data.statelist, function (item, value) {
                    debugger
                    $("#ddlstateholiday").append($("<option></option>").val(value.encryptstateid).html(value.state));

                })
                $("#ddlstateholiday").prop("disabled", false);
            }
        },
        error: function (data, error) {
            debugger
            $("#spanMessage").addClass('text-danger').html(data);
        }

    });
}
GetStateList($('#ddlstateholiday'));
GetHolidaytype($('#holidaydescription'));
function cleatTextBox() {
    $('#ddlcountry').find("option:first").prop('selected', 'selected');
    $('#holidaytype').find("option:first").prop('selected', 'selected');
    $('#Holydaydesc').val('');
    $('#holidaydate').val('');
    $("#weekday").find("option:first").prop('selected', 'selected');
    $("#holidaydescription").find("option:first").prop('selected', 'selected');
    $("#ddlstateholiday").find("option:first").prop('selected', 'selected');
    $("#remarks").val('');

}

function tablebind() {
    debugger
    var countrylst = $("#ddlcountry option:selected").val();
  
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "../Holiday/GetHolidayMasterDetails",
        data: { 'countryId': countrylst },
        dataType: "json",
        success: function (response) {
            var tr = '';
            debugger
            $("#tblHolidayMasterBody").empty();
            if (response != null) {
                
                if (response.lstholidaydetails.length > 0) {

                    var scount = 0;
                    for (var i = 0; i < response.lstholidaydetails.length; i++) {
                        debugger
                        scount = scount + 1;
                        tr += '<tr>';
                        tr += "<td>" + scount + "</td>";
                        /* tr += "<td>" + response.lstholidaydetails[i].organizationid + "</td>";*/
                        /* tr += "<td>" + response.lstholidaydetails[i].company + "</td>";*/
                        tr += "<td>" + response.lstholidaydetails[i].country + "</td>";
                        tr += "<td>" + response.lstholidaydetails[i].state + "</td>";
                        tr += "<td>" + response.lstholidaydetails[i].holidaY_NAME + "</td>";
                        tr += "<td>" + response.lstholidaydetails[i].holidaY_DESC + "</td>";
                        tr += "<td>" + response.lstholidaydetails[i].holidaY_DATE + "</td>";
                        tr += "<td>" + response.lstholidaydetails[i].weekday + "</td>";
                        tr += "<td>" + response.lstholidaydetails[i].remarks + "</td>";
                        /*tr += "<td>" + response.lstholidaydetails[i].ACTION + "</td>";*/
                        //var editbtn = '';
                        //var deletebtn = '<a class="btn btn-smm btn-info" onclick="EditHolidayMasterDetails(' + response.lstholidaydetails[i].recordId + ')"> <i class="fa fa-edit" aria-hidden="true"></i></a><a class="btn btn-smm btn-danger text-white" onclick="DeleteHolidayMasterDetails(' + response.lstholidaydetails[i].recordId + ')"> <i class="fa fa-trash" aria-hidden="true"></i></a >';
                        //tr += "<td>" + deletebtn + ""+editbtn+"</td>";
                        /*tr += "<td></td>";*/

                        tr += '</tr>';
                    }
                    $('#tblHolidayMasterBody').append(tr);
                }
            }
        },
        error: function (response) {
            //                      
        }
    });

}

$('#btnSubmitholidayuplod').on("click", function (e) {
    debugger

    e.preventDefault();

    debugger

    var errors = '';
    var formData = new FormData();
    debugger;
    var countryid = $("#ddlcountry option:selected").val();
    var country = $("#ddlcountry option:selected").html();
    var holidayname = $("#holidaydescription option:selected").html();
    //var state = $("#ddlstateholiday option:selected").val();
    //var Holidaytype = $("#holidaydescription option:selected").val();
    var Excelfile = $("#Excelfile").val();

    if (typeof countryid === "undefined" || countryid == '' || countryid == '0')
        errors += '<div class="col-sm-12">Please select country.</div>';
    if (typeof Excelfile === "undefined" || Excelfile == '')
        errors += '<div class="col-sm-12">please select excel file.</div>';

    var ExcelFileTypes = ["xls", "xlsx"];
    var HolidayTypeFiles = $('#Excelfile')[0].files;
    if (HolidayTypeFiles.length > 0) {
        var fileName = $("#Excelfile").val();
        var fileType = $("#Excelfile").val().split('.').pop();
        var fileSize = $('#Excelfile')[0].files[0].size;

        if ($.inArray(fileType, ExcelFileTypes) < 0 && fileSize > 0) {
            errors += '<div class="col-sm-12">  file must be xls/xlsx</div>';
        }
        else {
            formData.append('Excelfile', $('#Excelfile')[0].files[0]);
        }
    }
    formData.append('Country', country);
    formData.append("Encryptedcountryid", countryid);
    formData.append("holidayname", holidayname);
    /*formData.append('EncrpytedCountry', country);*/

    if (errors == '') {
        $.ajax({
            type: "POST",
            url: "../Holiday/SaveSiteUploadFromExcel",
            /*dataType: "json",*/
            /*enctype: 'multipart/form-data',*/
            data: formData,
            contentType: false,
            processData: false,
            context: document.body,
            success: function (data) {

                // alert('123');
                debugger
                //hideloader();
                $('#divMessage').hide();
                if (data == "success") {
                    // $("#spanMessage").addClass('text-success').html('Site upload Data successfully saved.');
                    $('#txt').html("Holiday Data upload successfully saved");
                    $('#successModal').modal('show');
                    //location.reload();

                }
                else {
                    $("#txterror").addClass('text-danger').html(data);
                    $('#dangerModal').modal('show');

                }
                // ClearSiteUploadForm();

            },
            error: function (xhr) {
                debugger
                //alert('1234');
                //hideloader();
                var ss = xhr.responseText;
                // alert('Format is not match.');
                $('#txterror').html('Format is not match.');
                $('#dangerModal').modal('show');
            }
        });
    }
    else {
        $('#txtwar').addClass('shadow-border').html(errors);
        $('#warningModal').modal('show');
    }
});

$('#btnholidaymasteruplod').on("click", function (e) {
    debugger

    e.preventDefault();

    debugger

    var errors = '';
    var formData = new FormData();
    debugger;
    var country = $("#ddlcountry option:selected").val();

    var Excelfile = $("#Excelfile").val();

    if (typeof country === "undefined" || country == '' || country == '0')
        errors += '<div class="col-sm-12">Please select country.</div>';
    if (typeof Excelfile === "undefined" || Excelfile == '')
        errors += '<div class="col-sm-12">please select excel file.</div>';

    var ExcelFileTypes = ["xls", "xlsx"];
    var HolidayTypeFiles = $('#Excelfile')[0].files;
    if (HolidayTypeFiles.length > 0) {
        var fileName = $("#Excelfile").val();
        var fileType = $("#Excelfile").val().split('.').pop();
        var fileSize = $('#Excelfile')[0].files[0].size;

        if ($.inArray(fileType, ExcelFileTypes) < 0 && fileSize > 0) {
            errors += '<div class="col-sm-12">  file must be xls/xlsx</div>';
        }
        else {
            formData.append('Excelfile', $('#Excelfile')[0].files[0]);
        }
    }


    formData.append('EncrpytedCountry', country);

    if (errors == '') {
        $.ajax({
            type: "POST",
            url: "../Holiday/HolidayUploadFromExcel",
            /*dataType: "json",*/
            /*enctype: 'multipart/form-data',*/
            data: formData,
            contentType: false,
            processData: false,
            context: document.body,
            success: function (data) {

                // alert('123');
                debugger
                //hideloader();
                $('#divMessage').hide();
                if (data == "success") {
                    // $("#spanMessage").addClass('text-success').html('Holiday Master upload Data successfully saved.');
                    $('#txt').html("Site upload Data successfully saved");
                    $('#successModal').modal('show');

                }
                else {
                    $("#txterror").addClass('text-success').html(data);
                    $('#dangerModal').modal('show');

                }
                // ClearSiteUploadForm();

            },
            error: function (xhr) {
                debugger
                alert('1234');
                //hideloader();
                var ss = xhr.responseText;
                // alert('Format is not match.');
                $('#txterror').html('Format is not match.');
                $('#dangerModal').modal('show');
            }
        });
    }
    else {
        $('#divMessage').html(errors);
        $('#divMessage').addClass(' shadow-border');
        return false;
    }
});

function DeleteHolidayMasterDetails(RecordId) {
    debugger
    var result = confirm("Are you sure you want to delete this Holiday");
    if (result) {
        if (RecordId != '' || RecordId > 0) {
            $.ajax({
                type: "GET",
                url: '../Holiday/DeleteBindHolidayTable',
                /*data: { "Empid": Empid, "RecordId": RecordId},*/
                data: { "RecordId": RecordId },
                contentType: "application/json; charset=utf-8",
                success: function (data) {


                    $('#validatemsgS').html(data);
                    tablebind();

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
function EditHolidayMasterDetails(RecordId) {
    debugger
    $('#hiddenRecordId').val(RecordId);
    if (RecordId != '' || RecordId > 0) {

        $.ajax({
            type: "GET",
            url: "../Holiday/EditBindHolidayTable", 
            data: { "RecordId": RecordId },
            contentType: "application/json; charset=utf-8",
            success: function (response) {

                debugger

                $('#ddlcountry').val(response[0].country);
                $('#ddlstateholiday').val(response[0].state);
                $('#Holydaydesc').val(response[0].holidaY_TYPE);
                $('#holidaydescription').val(response[0].holidaY_DESC);
                $('#remarks').val(response[0].remarks);
            },
            error: function (data, error) {
                $('#validatemsgE').html('Error.');
                setTimeout(function () {
                    $('#validatemsgE').html('');
                }, 2000);

            }
        })

    }
}

/* function for export CSV format */
var csvFileData = '';

function download_csv_file() {
    debugger
    //define the heading for each row of the data
    var csv = 'Country,State,HOLIDAY_NAME,HOLIDAY_DESC,HOLIDAY_DATE,WEEKDAY,REMARKS\n';

    var hiddenElement = document.createElement('a');
    hiddenElement.href = 'data:text/csv;charset=utf-8,' + encodeURI(csv);
    hiddenElement.target = '_blank';

    //provide the name for the CSV file to be downloaded
    hiddenElement.download = 'Holiday.csv';
    hiddenElement.click();
}


/* function for get Holiday desc */
function GetHolidaydesc() {
    debugger
    var holidayname = $('#holidaydescription option:selected').html();
    $('#Holydaydesc').empty();
    $('#Holydaydesc').append($("<option></option>").val('').html('Select Holiday Name'));
    $.ajax({
        type: "GET",
        url: "../Holiday/HolidayDList",
        context: document.body,
        data: { 'holidayname': holidayname },
        success: function (data) {
            debugger

            if (data != '') {
                $.each(data.lstholidaydesc, function (item, value) {
                    debugger
                    $("#Holydaydesc").append($("<option></option>").val(value.encryptid).html(value.holidaY_DESC));
                    
                })
                $("#Holydaydesc").prop("disabled", false);
            }
        },
        error: function (data, error) {
            debugger
            $("#spanMessage").addClass('text-danger').html(data);
        }

    });
}
$('#holidaydescription').change(function () {
    debugger
    GetHolidaydesc();
});



