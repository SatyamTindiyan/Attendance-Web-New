$(document).ready(function () {
   /* tablebind();*/

});

var Countries = []
function LoadCountry() {
    debugger
    $.ajax({
        type: "POST",
        url: "/Holiday/GetCountry",
        context: document.body,
        success: function (data) {
            debugger
            $('#ddlcountry').append($("<option></option>").val('').html('Select Country'));
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


LoadCountry($('#ddlcountry'));
$('#btnholidaymasteruplod').on("click", function (e) {
    debugger

    e.preventDefault();

    debugger

    var errors = '';
    var formData = new FormData();
    debugger;
    var countryid = $("#ddlcountry option:selected").val();
    var country = $("#ddlcountry option:selected").html();

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

    if (errors == '') {
        $.ajax({
            type: "POST",
            url: "../Holiday/HolidayUploadFromExcel",
            /*dataType: "json",*/
            /*enctype: 'multipart/form-data',*/
            data: formData,country,
            contentType: false,
            processData: false,
            context: document.body,
            success: function (data) {

                // alert('123');
                debugger
                //hideloader();
                $('#divMessage').hide();
                if (data == "success") {
                    $('#txt').html("Holiday Master upload Data successfully saved.");
                    $('#successModal').modal('show');
                    //alert('Holiday Successfully saved');
                    //location.reload(true);

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
        //return false;
    }
});
//var countrylst = $("#ddlcountry option:selected").val();


function tablebind() {
    debugger
    var countrylst = $("#ddlcountry option:selected").val();
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "../Holiday/BindHolidayTable",
        data :{ 'countryId': countrylst },
       /* data: "{}",*/
        dataType: "json",
        success: function (response) {
            var tr = '';
            debugger
          
                $("#holidaytable").empty();
                if (response != null) {
                    if (response.lstholidaytable.length > 0) {

                        var scount = 0;
                        for (var i = 0; i < response.lstholidaytable.length; i++) {
                            debugger
                            scount = scount + 1;
                            tr += '<tr>';
                            tr += "<td>" + scount + "</td>";
                            tr += "<td>" + response.lstholidaytable[i].country + "</td>";
                            tr += "<td>" + response.lstholidaytable[i].holidaY_NAME + "</td>";
                            tr += "<td>" + response.lstholidaytable[i].holidaY_DESC + "</td>";
                            //tr += "<td></td>";
                            //var deletebtn = '<a class="btn btn-smm btn-danger text-white" onclick="DeleteholidayRecordDetails(' + response.lstholidaytable[i].RecordId + ')"> <i class="fa fa-trash" aria-hidden="true"></i></a >';
                            //tr += "<td>" + deletebtn + "</td>";
                            //tr += "<td><button class='btn btn-info btn-xs btn-edit'>Edit</button><button class='btn btn-danger btn-xs btn-delete'>Delete</button></td>";
                            tr += '</tr>';
                        }
                        $('#holidaytable').append(tr);
                    }
                }
            
        },
        error: function (response) {
            //                      
        }
    });

}
    
function download_csv_file() {
    debugger
    //define the heading for each row of the data
    var csv = 'country,HOLIDAY_NAME,HOLIDAY_DESC\n';

    var hiddenElement = document.createElement('a');
    hiddenElement.href = 'data:text/csv;charset=utf-8,' + encodeURI(csv);
    hiddenElement.target = '_blank';

    //provide the name for the CSV file to be downloaded
    hiddenElement.download = 'Holiday.csv';
    hiddenElement.click();
}


$("#ddlcountry").on('change',function () {
    debugger
    tablebind()
   /* countryid = $(this).val(); //this id is global variable of country..*/
    

});
