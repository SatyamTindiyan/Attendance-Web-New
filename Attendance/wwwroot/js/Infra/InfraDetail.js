$(document).ready(function () {
    $("#tblInfrareport").DataTable({
        "processing": true,
        "serverSide": false,
        "filter": true,
        "orderMulti": true,
        "ajax": {
            "url": "/Infra/InfraDetails",
            "type": "POST",
            "dataType": "json",
            'data': function () {
                 
            },

        },
        "columns": [
            { "data": "EmpID", "autowidth": true },
            { "data": "EmpName", "autowidth": true },
            { "data": "Location", "autowidth": true },
            { "data": "SerialNo", "autowidth": true },
            { "data": "ServiceTag", "autowidth": true },
            { "data": "ModelNo", "autowidth": true },
            { "data": "InfraGrp", "autowidth": true }
        ]
    });
});
