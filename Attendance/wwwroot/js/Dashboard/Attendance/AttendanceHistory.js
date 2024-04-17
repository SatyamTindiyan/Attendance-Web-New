



    

//var table = $("#tblAttreport").dataTable({
//    "processing": true,
//    "serverSide": false,
//    "filter": true,
//    "orderMulti": true,
//    "ajax": {
//        "url": "/Attendance/AttendanceHistory",
//        "type": "POST",
//        "datatype": "json",
//        'data': function (d) {
//            return JSON.stringify(d);
//        }
//    },
//    "columns": [
//        { "data": "EMPID", "autowidth": true },
//        { "data": "DATE", "autowidth": true },
//        { "data": "StatusInTime", "autowidth": true },
//        { "data": "StatusOutTime", "autowidth": true },
//    ],
//});

//$("#btnSearch").click(function () {
//    debugger
//    var data = {
//        month: $("#txtmonth").find("option:selected").val(),
//        year: $("#txtyear").find("option:selected").val()
//    };

//    $.ajax({
//        url: "/Attendance/AttendanceHistory",
//        type: "POST",
//        data: data,
//        dataType: "json",
//        success: function (data) {
//            debugger
//            dataTable.fnClearTable();

//        }
//    });
//});


var table = $("#tblAttreport").DataTable({
    "processing": true,
    "serverSide": false,
    "filter": true,
    "orderMulti": true,
    "ajax": {
        "url": "/Attendance/AttendanceHistory",
        "type": "POST",
        "dataType": "json",
        'data': function (d) {
            d.month = $('#txtmonth').val(); 
            d.year = $('#txtyear').val();   
            return d;
        }
    },
    "columns": [
        { "data": "EMPID", "autowidth": true },
        { "data": "DATE", "autowidth": true },
        { "data": "StatusInTime", "autowidth": true },
        { "data": "StatusOutTime", "autowidth": true },
    ]
});

$("#btnSearch").click(function () {
    table.ajax.reload();
});

