$(document).ready(function () {
   
});

//var dataTable = $("#tblAttendancedtl").dataTable({
   
//   /* "dom": "Bfrtip",*/
//    "processing": true,
//    "serverSide": false,
//    "filter": true,
//    "orderMulti": true,

//    "ajax": {
//        "url": "../Attendance/AttendanceDetail",
//        "type": "POST",
//        "contentType": 'application/json;charset=utf-8',
//        "dataType": "json",
//        "dataSrc": "data"
//    },

    
//    "columns": [
        
//        { "data": "empid", "autowidth": true },
//        { "data": "emp_Full_Name", "autowidth": true },
//        { "data": "date", "autowidth": true },
//        { "data": "intimedb", "autowidth": true },
//        { "data": "outtimedb", "autowidth": true },
//        { "data": "radialindistance", "autowidth": true },
//        { "data": "radialoutdistance", "autowidth": true },
//        { "data": "country", "autowidth": true },
//        { "data": "cordinator", "autowidth": true },
//        { "data": "manager", "autowidth": true },
//        { "data": "head", "autowidth": true }
//    ],

    

//    "buttons": [
//        "copyHtml5", "excelHtml5", "pdfHtml5", "csvHtml5"
//        //'copy',
//        //'csv',
//        //'excel',
//        //'pdf',
//        //'print'
//    ]
//});


//$('#btnsearch').click(function () {
//    debugger
//    var data = {
//        fromdate: $("#txtto").val(),
//        todate: $("#txtfrom").val(),
//        counid: $("#CountryID").find("option selected").val()

//    };

//    $.ajax({
//        url: "/Attendance/AttendanceDetail",
//        type: "POST",
//        data: data,
//        dataType: "json",
//        success: function (data) {
//            debugger

//            var jsonObject1 = data;
//            var jsonObject = data.d;
//            dataTable.clear().draw();
//            dataTable.rows.add(jsonObject1); // add to DataTable instance
//            dataTable.draw(); // always redraw
//        }
//    });
//});


var table = $("#tblAttendancedtl").DataTable({
    "processing": true,
    "serverSide": false,
    "filter": true,
    "orderMulti": true,
    "ajax": {
        "url": "/Attendance/AttendanceDetail",
        "type": "POST",
        "dataType": "json",
        'data': function (d) {
            // You can add additional parameters here if needed
            d.fromdate = $('#txtfromdate').val();
            d.todate = $('#txttodate').val(),// Add the month parameter
            d.counid = $('#CountryID').val();    // Add the year parameter
            return d;
        }
    },
    "columns": [

        { "data": "EMPID", "autowidth": true },
        { "data": "Emp_Full_Name", "autowidth": true },
        { "data": "DATE", "autowidth": true },
        { "data": "INTIMEDB", "autowidth": true },
        { "data": "OUTTIMEDB", "autowidth": true },
        { "data": "RADIALINDISTANCE", "autowidth": true },
        { "data": "RADIALOUTDISTANCE", "autowidth": true },
        { "data": "Country", "autowidth": true },
        { "data": "CORDINATOR", "autowidth": true },
        { "data": "MANAGER", "autowidth": true },
        { "data": "HEAD", "autowidth": true }
    ],
});


$("#btnsearch").click(function () {
    table.ajax.reload();
});


var Countries = []
// fetch Cordinator FORM DATABASE
function LoadCountry(element) {
    debugger
    if (Countries.length == 0) {

        $.ajax({
            "type": "GET",
           "url": "/Attendance/GetCountry",

            success: function (data) {
                debugger

                Countries = data;

                //render cordinator
                renderCountry(element);
            }

        })
    }
    else {
        //render cordinator to the element
        renderCountry(element);
    }
}

function renderCountry(element) {
    debugger
    var $ele = $(element);
    $ele.empty();
    $ele.append($("<option/>").val("0").text("-Select Country-"));
    $.each(Countries, function (i, val) {
        debugger
        $ele.append($("<option>").val(val.CountryID).text(val.Country));
        /* $ele.append($('<option>').val(val.CountryName).text(val.CountryID));*/
    })
}



LoadCountry($("#ddlCountry"));

function exportData() {
    debugger
    var table = document.getElementById("tblAttendancedtl");

    // Declaring array variable 
    var rows = [];

    //iterate through rows of table
    for (var i = 0, row; row = table.rows[i]; i++) {
        //rows would be accessed using the "row" variable assigned in the for loop
        //Get each cell value/column from the row
        column1 = row.cells[0].innerText;
        column2 = row.cells[1].innerText;
        column3 = row.cells[2].innerText;
        column4 = row.cells[3].innerText;
        column5 = row.cells[4].innerText;
        column6 = row.cells[5].innerText;
        column7 = row.cells[6].innerText;
        column8 = row.cells[7].innerText;
        column9 = row.cells[8].innerText;
        column10 = row.cells[9].innerText;
        column11 = row.cells[10].innerText;


        // add a new records in the array /
        rows.push(
            [
                column1,
                column2,
                column3,
                column4,
                column5,
                column6,
                column7,
                column8,
                column9,
                column10,
                column11
            ]
        );

    }
    csvContent = "data:text/csv;charset=utf-8,";
    // add the column delimiter as comma(,) and each row splitted by new line character (\n) 
    rows.forEach(function (rowArray) {
        row = rowArray.join(",");
        csvContent += row + "\r\n";
    });

    // create a hidden <a> DOM node and set its download attribute 
    var encodedUri = encodeURI(csvContent);
    var link = document.createElement("a");
    link.setAttribute("href", encodedUri);
    link.setAttribute("download", "AttendanceDetail.csv");
    document.body.appendChild(link)
        ;
    // download the data file named "Stock_Price_Report.csv" /
    link.click();
}

$("#btndetailExport").click(function () {
    exportData();
});


$("#txtfromdate").datepicker({
    dateFormat: 'dd/mm/yy',
    changeMonth: true,
    changeYear: true,
    //minDate: dateText
    timeFormat: 'hh:mm TT'
});

//fromdate: $('#txtfrom').val(),
//    todate: $('#txtto').val(),

$("#txttodate").datepicker({
    dateFormat: 'dd/mm/yy',
    changeMonth: true,
    changeYear: true,
    //minDate: dateText
    timeFormat: 'hh:mm TT'
});