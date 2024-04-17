
$(document).ready(function () {
    debugger
    //SetDataTable();
});

$("#searchData").keyup(function () {
    debugger
    var searchText = $(this).val();
    $("#Employeelist tr").each(function () {
        debugger
        var found = "false";
        //$(this).find('td:nth(0)').each(function () {     /////// when you want to searching on particuler column.
        $(this).each(function () {
            if ($(this).text().toLowerCase().indexOf(searchText.toLowerCase()) >= 0) {
                found = "true";
            }
        });
        if (found == "true") {
            $(this).show();
        }
        else {
            $(this).hide();
        }
    });
});

function OnclickSelectAllCheckbox(e) {
    e.preventDefault();
    //if ($('#ChkAll').is(":checked") == true) {
    //    //$(".EmplAppchk").prop('checked', false);
    //    $(':checkbox[class=EmplAppchk]:not(:disabled)').prop("checked", false).trigger("change");
    //}
    //else {
    //    //$(".EmplAppchk").prop('checked', true);
    //    $(':checkbox[class=EmplAppchk]:not(:disabled)').prop("checked", true).trigger("change");
    //}
}

$("#ChkAll").on("change", function (e) {
    e.preventDefault();
    $("input:checkbox").not(this).not(":disabled").prop("checked", this.checked);
});

function SaveAllCheckbox(status, e) {
    debugger
    /*showloader();*/
    e.preventDefault();
    var IsMapped = "";
    var objValue = {};
    var ChkSelectAll = []; var _flag = 0;
    var tblResourceInsert = "#Employeelist > tr";

    $(tblResourceInsert).each(function (InsertResourceindex, tr) {
        debugger
        if ($(tr).find("input:checkbox").is(":checked")) {
         debugger
            _flag = 1;
            var empdetails = {};
            //alert($(tr).find("#btnMapped").text());
            empdetails.EmpId = $(tr).find("input:checkbox").val();
            IsMapped = $(tr).find("#btnMapped").text();
            if (status == 1) {
             debugger
                $(tr).find("#btnMapped").addClass("border-danger");
                return false;
            }
            else { $(tr).removeClass("border-danger"); }

            var CompanyId = $("#CompanyID").val();
            var projectId = $("#ProjectID").val();
            var VendorId = $("#VendorID").val();
            var EmpId = $("#EmpID").val();
            empdetails.VendorID = VendorId;
            empdetails.ManagerID = EmpId;
            empdetails.ProjectID = projectId;
            empdetails.Mapped = IsMapped;
            ChkSelectAll.push(empdetails);
        }
    });
    debugger
    var PID = $("#ProjectID").val();
    if (PID == "") {
        hideloader();
        alert("Please Select Project");
        //$('#ProjectID').addClass('border-danger');
        return false;
    }
    if (_flag == 0) {
        hideloader();
        alert("Please Select any employee")
        return false;
    }
    if (status == 1 && IsMapped == "Mapped") {
        /*hideloader();*/
        alert("Mapped employee select")
        return false;
    }

    var objValue = {};
    //var CompanyId = $('#CompanyID').val();
    //var projectId = $('#ProjectID').val();
    //var VendorId = $('#VendorID').val();
    //var EmpId = $('#EmpID').val();
    objValue.ChkSelectAll = ChkSelectAll;
    //objValue.VendorId = VendorId;
    //objValue.ManagerID = EmpId;
    //objValue.ProjectId = projectId

    $.ajax({
        type: "POST",
        url: "/EngineeringTeamMapping/SaveRecord",
        //data: '{objAlarm: ' + JSON.stringify(objValue) + '}',
        data: "{objModal: " + JSON.stringify(objValue) + ",status: " + status + "}",
        contentType: " application/json; charset=utf-8",
        dataType: "text",
        success: function (data) {
debugger
            if (data == "success") {
                $("#spanMessage").addClass("text-success").html("Data Updated successfully .");
                BindEmployeeDetail();
                hideloader();
            }
            else {
                hideloader();
                $("#spanMessage").addClass("text-danger").html(data);
            }
        },
        error: function (data, error) {
            hideloader();
            $("#spanMessage").addClass("text-danger").html(data);
        }
    });
    hideloader();

}

function SetDataTable() {

    table = $("#tblEmpList").dataTable({
        //order: [[2, 'asc']],
        //rowGroup: {
        //    dataSrc: 2
        //}
        "searching": false,
        "paging": true,
        // "bFilter": false,
        "bInfo": false,
        "aLengthMenu": [[10, 20, 50, -1], [10, 20, 50, "All"]],
        "iDisplayLength": 10,
        //"pageLength": 25,
        // "pagingType": "simple" //"full_numbers"
        language: {
            paginate: {
                previous: "Prev",
                next: "Next"
            },
            aria: {
                paginate: {
                    previous: "Previous",
                    next: "Next"
                }
            }
        }
    });
    //table.destroy();
}

$("#CompanyID").on("change", function () {
    debugger
    var companyId = $("#CompanyID option:selected").val();
    if (companyId != "") {
        $("#VendorID").empty();
        $("#VendorID").append($("<option></option>").val('').html("Select Vendor"));
        $.ajax({
            type: "POST",
            url: "/EngineeringTeamMapping/GetVendorListBasedOnCompanyIdvendor",
            data: JSON.stringify({ "company": companyId }),
            contentType: "application/json; charset=utf-8",
            context: document.body,
            success: function (data) {
                debugger
                if (data != "") {
                    $.each(data, function (item, value) {
                        debugger
                        $("#VendorID").append($("<option></option>").val(value.VendorID).html(value.Vendor));
                    })
                    $("#VendorID").prop("disabled", false);
                }
            },
            error: function (xhr) {
                $("#VendorID").empty();
            }
        });
    }
    else {
        $("#VendorID").empty();
        $("#VendorID").prop("disabled", true);
    }
});

$("#VendorID").change(function () {
    debugger
    var VendorID = $("#VendorID option:selected").val();
    var companyId = $("#CompanyID option:selected").val();
    if (VendorID != "") {
        //BindEmployeeDetailForApprovelAdmin();
        BindProjectBasedonVendor(VendorID);
    }
});


function BindEmployeeDetail() {
    debugger
    /*showloader();*/
    $("#ChkAll").prop("checked", false);

    $("#Employeelist").empty();
    var VendorId = $("#VendorID option:selected").val();
    var CompanyID = $("#CompanyID option:selected").val();
    var ProjectID = $("#ProjectID option:selected").val();
    var EmpID = $("#EmpID option:selected").val();
    if (typeof ProjectID === "undefined" || ProjectID == "") {
        ProjectID = 0
    }
    $.ajax({
        type: "GET",// disabled="disabled"
        url: "/EngineeringTeamMapping/GetEmpRiggerTechnicianList/",
        data: { "VendorId": VendorId, "ProjectID": ProjectID, "CompanyID": CompanyID, "EmpId": EmpID },
        context: document.body,
        success: function (data) {
            debugger
            var Sncount = 0;
            //<div style="color:blue;">(`+ value.PERSONALEMAIL + `)</div>
            $.each(data, function (item, value) {
                var btnstatustext = "";
                var checkBoxShow = '<input type="checkbox" value="' + value.EMP_ID + '" class="EmplAppchk">';
                var desig = "";

                if (value.AppRejStatus == 1) {
                    btnstatustext = `<button class="btn btn-smm btn-info" id="btnMapped" name="Mapped">Mapped</button>`;
                    checkBoxShow = '<input type="checkbox" value="' + value.EMP_ID + '"  Class="EmplAppchk">';
                }

                if (value.Designation == null)
                    desig = "";
                else
                    desig = value.Designation;

                Sncount = Sncount + 1;
                $("#Employeelist").append(`<tr class="odd">
                        <th scope="row" class="sorting_1">`+ Sncount + `</th>
                           <td>`+ value.EmployeeName + `</td>
                        <td>
                        `+ value.EMP_ID + `<br>
                        
                        </td>
                        <td Class="designation">`+ desig + `</td>
                        <td>`+ btnstatustext + `</td>
                        <td class="project-checkbox">
                        <label class="CCcontainer mb-0 text-smm">`+ checkBoxShow + `
                         <span class="checkmark"></span>
                        </label>
                        </td>
                        </tr>`);
                //<input type="checkbox" value=`+ value.EMP_ID + ` class="EmplAppchk">
            })
            SetDataTable();
            hideloader();
        },
        error: function (xhr) {
            hideloader();
            // console.log(xhr);

        }
    });
    /*hideloader();*/
}

function BindProjectBasedonVendor(VendorId) {

    $("#ProjectID").empty();
    $("#ProjectID").append($("<option></option>").val("").html("Select Project"));
    if (VendorId > 0) {
        debugger
        $.ajax({
            type: "GET",
            url: "/EngineeringTeamMapping/GetProjectBasedOnVendor/",
            data: { "vendor": VendorId },
            contentType: "application/json; charset=utf-8",
            context: document.body,
            success: function (data) {
                debugger
                if (data != '') {

                    $.each(data, function (item, value) {
                        debugger
                        $("#ProjectID").append($("<option></option>").val(value.ProjectId).html(value.Name));
                    })
                    $("#ProjectID").prop("disabled", false);
                }
            },
            error: function (xhr) {
                hideloader();
                $("#ProjectID").empty();
            }
        });
    }
    /* hideloader();*/
}

$("#ProjectID").change(function () {
    debugger
    //showloader();
    $("#EmpID").empty();
    $("#EmpID").append($("<option></option>").val('').html("Select Employee"));
    var ProjectID = $("#ProjectID option:selected").val();
    if (ProjectID != "") {
        debugger
        $.ajax({
            type: "GET",
            url: "/EngineeringTeamMapping/GetEmployeeProjectbase/",
            data: { "ProjectId": ProjectID },
            contentType: "application/json; charset=utf-8",
            context: document.body,
            success: function (data) {
                debugger
                if (data != "") {
                    $.each(data, function (item, value) {
                        debugger
                        $("#EmpID").append($("<option></option>").val(value.EMP_ID).html(value.EmployeeName));
                    })
                    $("#EmpID").prop("disabled", false);
                }
            },
            error: function (xhr) {
                $("#EmpID").empty();
            }
        });

    }
    //hideloader();
});

$("#EmpID").change(function () {
    debugger
    var EmpID = $("#EmpID option:selected").val();
    BindEmployeeDetail();
});

