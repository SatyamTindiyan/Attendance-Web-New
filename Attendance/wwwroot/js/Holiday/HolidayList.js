$(document).ready(function () {
    $("#tblHolidayDetails").DataTable({
        "processing": true,
        "serverSide": false,
        "filter": true,
        "orderMulti": true,
        "ajax": {
            "url": "/Holiday/HolidayDetails",
            "type": "POST",
            "dataType": "json",
            'data': function () {

            },

        },
        "columns": [
            { "data": "HOLIDAY_DATE", "autowidth": true },
            { "data": "WEEKDAY", "autowidth": true },
            { "data": "HOLIDAY_NAME", "autowidth": true },
            { "data": "Country", "autowidth": true }
        ]
    });
});
