
var Countries = []
function LoadCountry() {
    debugger
    $.ajax({
        type: "POST",
        url: "/Attendance/GetCountry",
        context: document.body,
        success: function (data) {
            debugger

            if (data != '') {
                $.each(data.lstCoun, function (item, value) {
                    $("#ddlcountry").append($("<option></option>").val(value.countryID).html(value.country));
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
LoadCountry($("#ddlcountry"));
   

//var dataTable = $("#tblAttSummary").dataTable({

//    "processing": true,
//    "serverSide": false,
//    "filter": true,
//    "orderMulti": true,
//    "ajax": {
//        "url": "../Attendance/AttendanceSummary",
//        "type": "POST",
//        "datatype": "json",
//        "dataSrc": "data"
//    },
//    "columns": [
//        { "data": "empid", "autowidth": true },
//        { "data": "emp_Full_Name", "autowidth": true },
//        { "data": "calendarDays", "autowidth": true },
//        { "data": "present", "autowidth": true },
//        { "data": "leave", "autowidth": true },
//        { "data": "absent", "autowidth": true },
//    ]
//});

var table = $("#tblAttSummary").DataTable({
    "processing": true,
    "serverSide": false,
    "filter": true,
    "orderMulti": true,
    "ajax": {
        "url": "/Attendance/AttendanceSummary",
        "type": "POST",
        "dataType": "json",
        'data': function (d) {
            // You can add additional parameters here if needed
            d.fromdate = $('#txtfromdate').val();// Add the month parameter
            d.counid = $('#CountryID').val();    // Add the year parameter
            return d;
        }
    },
    "columns": [

        { "data": "EMPID", "autowidth": true },
        { "data": "Emp_Full_Name", "autowidth": true },
        { "data": "CalendarDays", "autowidth": true },
        { "data": "Present", "autowidth": true },
        { "data": "Leave", "autowidth": true },
        { "data": "Absent", "autowidth": true },
    ],
});

$("#btnSearch").click(function () {
    table.ajax.reload();
});

var Countries = []
// fetch Cordinator FORM DATABASE



/*LoadCountry($("#ddlcountry"));*/


/*New for Export*/
function exportData() {
    debugger;
    // Get the HTML data using Element by Id 
    var table = document.getElementById("tblAttSummary");
    //document.getElementById("#tblAttSummary")
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


        // add a new records in the array /
        rows.push(
            [
                column1,
                column2,
                column3,
                column4,
                column5,
                column6
            ]
        );

    }
    csvContent = "data:text/csv;charset=utf-8,";
    // add the column delimiter as comma(,) and each row splitted by new line character (\n) 
    rows.forEach(function (rowArray) {
        debugger
        row = rowArray.join(",");
        csvContent += row + "\r\n";
    });

    // create a hidden <a> DOM node and set its download attribute 
    var encodedUri = encodeURI(csvContent);
    var link = document.createElement("a");
    link.setAttribute("href", encodedUri);
    link.setAttribute("download", "AttendanceSummary.csv");
    document.body.appendChild(link)
        ;
    // download the data file named "Stock_Price_Report.csv" /
    link.click();
}

$("#btnexport").click(function () {
    debugger

    exportData();
});




$("#txtfromdate").datepicker({
    dateFormat: 'dd/mm/yy',
    changeMonth: true,
    changeYear: true,
    //minDate: dateText
    timeFormat: 'hh:mm TT'
});