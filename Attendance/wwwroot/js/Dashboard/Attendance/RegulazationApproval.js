
$(document).ready(function () {
    debugger
    setTimeout(
        function () {
            tblbindregularApproval();
        }, 1000);

});

var approvallbl = $("#myHiddenVar").val();

$("#ckbCheckAll").click(function () {
    debugger
    $(".CordinatorA").prop("checked", false);
    $(".CordinatorR").prop("checked", false);
    if ($("#ckbCheckAll").is(":checked") == true) {

        $(".CordinatorA").prop("checked", true);
    }

});

$("#ckbCheckAllOne").click(function () {
    debugger
    $(".ManagerA").prop("checked", false);
    $(".ManagerR").prop("checked", false);
    if ($("#ckbCheckAllOne").is(":checked") == true) {

        $(".ManagerA").prop("checked", true);
    }

});

$("#ckbCheckAllTwo").click(function () {
    debugger
    $(".HeadA").prop("checked", false);
    $(".HeadR").prop('checked', false);
    if ($("#ckbCheckAllTwo").is(":checked") == true) {

        $(".HeadA").prop("checked", true);
    }

});


function tblbindregularApproval() {
    debugger
    var data = {
        fromdate: $("#txtfromdate").val(),
        todate: $("#txttodate").val(),
    };

    var appendrowth = "";

    $.ajax({
        url: "/Attendance/RegulazationApproval",
        type: "POST",
        data: data,
        dataType: "json",
        success: function (ResponseDt) {
            debugger
            if (ResponseDt.data.length > 0) {
                var ResponseDtlength = ResponseDt.data.length; txtfromdate
                for (var i = 0; i < ResponseDtlength; i++) {
                    debugger
                    var CordinatorA = "CordinatorA";
                    var CordinatorR = "CordinatorR";
                    var ManagerA = "ManagerA";
                    var ManagerR = "ManagerR";
                    var HeadA = "HeadA";
                    var HeadR = "HeadR";
                    CordinatorA = CordinatorA + i;
                    CordinatorR = CordinatorR + i;
                    ManagerA = ManagerA + i;
                    ManagerR = ManagerR + i;
                    HeadA = HeadA + i;
                    HeadR = HeadR + i;
                    var manager = ResponseDt.data[i].apPstatusMANAGER;
                    var head = ResponseDt.data[i].apPstatusHEAD;
                    var cordinator = ResponseDt.data[i].apPstatusCORDINATOR;

                    var appendrow = `<tr class="odd"><td class="sorting_1">` + ResponseDt.data[i].emp_full_Name + ` </td><td>` + ResponseDt.data[i].empid + `</td><td>` + ResponseDt.data[i].date + `</td><td>` + ResponseDt.data[i].intime + `</td><td>` + ResponseDt.data[i].outtime + `</td><td>` + ResponseDt.data[i].workingHour + `</td>




                 <td><input type="checkbox" id=`+ ManagerA + ` class="ManagerA" name="chk" value="2" onclick="SelectOnlyOne(this,` + i + `)"><input type="hidden" id ="manager" value = ` + manager +`>
                 <span class="text-success">Approved</span> <br><input type="checkbox" id=`+ ManagerR + ` class="ManagerR" name="3" value="3" onclick="SelectOnlyOne(this,` + i + `)"><input type="hidden" id ="manager" value = ` + manager +`>
                 <span class="text-danger">Reject</span></td><td><input type="checkbox" id=`+ HeadA + ` class="HeadA" name="chk" value="2" onclick="SelectOnlyOne(this,` + i + `)"><input type="hidden" id ="head" value = ` + head +`>
                 <span class="text-success">Approved</span> <br><input type="checkbox" id=`+ HeadR + ` name="chk" class="HeadR" value="3" onclick="SelectOnlyOne(this,` + i + `)"><input type="hidden" id ="head" value = ` + head +`>
                 <span class="text-danger">Reject</span></td></tr>`;


                    appendrowth = appendrowth + appendrow;


                    //});
                }
                //debugger;
                $("#tblRegapprovalth").append(appendrowth);
                // SetDataTable();

                setTimeout(function () {
                    debugger
                    for (var i = 0; i < ResponseDtlength; i++) {
                        //debugger
                        var CordinatorAchk = "CordinatorA" + i;
                        var CordinatorRchk = "CordinatorR" + i;
                        var ManagerAchk = "ManagerA" + i;
                        var ManagerRchk = "ManagerR" + i;
                        var HeadAchk = "HeadA" + i;
                        var HeadRchk = "HeadR" + i;

                        //alert(2);

                        if (approvallbl == 2) {

                            $("#" + ManagerAchk).prop("disabled", true);
                            $("#" + ManagerRchk).prop("disabled", true);
                            $("#" + HeadAchk).prop("disabled", true);
                            $("#" + HeadRchk).prop("disabled", true);
                        }

                        if (approvallbl == 3) {
                            $("#" + CordinatorAchk).prop("disabled", true);
                            $("#" + CordinatorRchk).prop("disabled", true);
                            $("#" + HeadAchk).prop("disabled", true);
                            $("#" + HeadRchk).prop("disabled", true);
                        }
                        if (approvallbl == 4) {
                            $("#" + CordinatorAchk).prop("disabled", true);
                            $("#" + CordinatorRchk).prop("disabled", true);
                            $("#" + ManagerAchk).prop("disabled", true);
                            $("#" + ManagerRchk).prop("disabled", true);
                        }

                        if (ResponseDt.data[i].disableOption == 'head') {
                            $('#' + HeadAchk).prop('disabled', true);
                            $('#' + HeadRchk).prop('disabled', true);
                        }
                        if (ResponseDt.data[i].disableOption == 'manager') {
                            $('#' + ManagerAchk).prop('disabled', true);
                            $('#' + ManagerRchk).prop('disabled', true);
                        }

                        if (ResponseDt.data[i].apPstatusMANAGER == 2) {
                            $("#" + ManagerAchk).prop("checked", true);
                        }
                        if (ResponseDt.data[i].apPstatusMANAGER == 3) {
                            $("#" + ManagerRchk).prop("checked", true);
                        }

                        if (ResponseDt.data[i].apPstatusHEAD == 2) {
                            $("#" + HeadAchk).prop("checked", true);
                        }
                        if (ResponseDt.data[i].apPstatusHEAD == 3) {
                            $("#" + HeadRchk).prop("checked", true);
                        }
                    }

                }, 2000); // How
            }
        }
    });
}



$("#txtfromdate").datepicker({
    dateFormat: "dd/mm/yy",
    changeMonth: true,
    changeYear: true,
    //minDate: dateText
    timeFormat: "hh:mm TT"
});

$("#txttodate").datepicker({
    dateFormat: "dd/mm/yy",
    changeMonth: true,
    changeYear: true,
    //minDate: dateText
    timeFormat: "hh:mm TT"
});

$("#btnSearch").click(function () {
    debugger
    $("#tblRegapprovalth").empty();
    tblbindregularApproval();
});



var appdata = [];

$("#btnregapproval").click(function () {
    debugger
    // var indextbl = 0;
    $('#tblRegapproval tbody tr').each(function () {
        debugger
        var row = $(this);
        // get current row 2nd TD
        var appList = {};

        var CordinatorA = row.find(".CordinatorA").is(':checked');
        var CordinatorR = row.find(".CordinatorR").is(':checked');
        var ManagerA = row.find(".ManagerA").is(':checked');
        var ManagerR = row.find(".ManagerR").is(':checked');
        var manager = row.find("#manager").val();
        var HeadA = row.find(".HeadA").is(':checked');
        var HeadR = row.find(".HeadR").is(':checked');
        var head = row.find("#head").val();

        if (CordinatorA || CordinatorR || ManagerA || ManagerR || HeadA || HeadR) {
            debugger
            var Name = row.find("td:eq(0)").text();
            var EMPID = row.find("td:eq(1)").text();
            var _date = row.find("td:eq(2)").text();

            if (CordinatorA == true) {
                debugger
                var valCordnatorA = row.find(".CordinatorA").val();
                appList.APPstatusCORDINATOR = valCordnatorA;
            };
            if (CordinatorR == true) {
                var valCordnatorR = row.find(".CordinatorR").val();
                appList.APPstatusCORDINATOR = valCordnatorR;
                appList.APPstatusMANAGER = 1;
                appList.APPstatusHEAD = 1;
            }

            if (ManagerA == true) {
                debugger
                var valManagerA = row.find(".ManagerA").val();
                var valHeadA = row.find(".HeadA").val();
                var valCordinatorA = row.find(".CordinatorA").val();
                appList.APPstatusMANAGER = valManagerA;
                appList.APPstatusHEAD = head;
                appList.APPstatusCORDINATOR = valCordinatorA;
            }
            if (ManagerR == true) {
                var valManagerR = row.find(".ManagerR").val();
                appList.APPstatusMANAGER = ManagerR;
                appList.APPstatusHEAD = 1;
                appList.APPstatusCORDINATOR = 1;
            }

            if (HeadA == true) {
                var valHeadA = row.find(".HeadA").val();
                var valManagerA = row.find(".ManagerA").val();
                var valCordinatorA = row.find(".CordinatorA").val();
                appList.APPstatusHEAD = valHeadA;
                appList.APPstatusCORDINATOR = 1;
                appList.APPstatusMANAGER = manager;
            }
            if (HeadR == true) {
                var valHeadR = row.find(".HeadR").val();
                appList.APPstatusHEAD = valHeadR;
                appList.APPstatusCORDINATOR = 1;
                appList.APPstatusMANAGER = manager;
            }

            appList.EMPID = EMPID;
            appList.DATE = _date;
            appdata.push(appList);
        }
    });
    if (appdata.length > 0) {
        $.ajax({
            type: "POST",
            url: "/Attendance/ApprovedRegularization",
            /*data: JSON.stringify(appdata),*/
            data: { 'appdata': appdata },
            document: document.body,
            success: function (result) {
                debugger
                $("#msgSuccess").addClass('label-success').html('Regularization Approved Successfully');
                setTimeout(function () {
                    location.reload();
                }, 4000);

            },
            error: function () {
                $("#msgError").addClass('label-danger').html('Regularization Approved Successfully');
                setTimeout(function () {
                    debugger
                    location.reload();
                }, 4000);

            }

        });
    }

});


$("#searchData").keyup(function () {
    debugger
    var searchText = $(this).val();
    $("#tblRegapprovalth tr").each(function () {
        var found = 'false';
        //$(this).find('td:nth(0)').each(function () {     /////// when you want to searching on particuler column.
        $(this).each(function () {
            debugger
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


function SetDataTable() {
    debugger
    $("#tblRegapproval").DataTable({
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

function SelectOnlyOne(e, i) {
    debugger
    $("#CordinatorA" + i).prop('checked', false);
    $("#CordinatorR" + i).prop('checked', false);
    $("#ManagerA" + i).prop('checked', false);
    $("#ManagerR" + i).prop('checked', false);
    $("#HeadA" + i).prop('checked', false);
    $("#HeadR" + i).prop('checked', false);
    $("#" + e.id).prop('checked', true);
    //debugger
    //alert(e.id)


}