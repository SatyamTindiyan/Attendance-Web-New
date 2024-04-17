
var selectedfirsttab;
$(document).ready(function () {

    /* var events = [];*/
    Regularization();

    setTimeout(
        function () {
            selectedfirsttab = 1;
        }, 2000);
});

//var timerVal = moment().format('hh:mm :s');
var flag = false;
$(function () {
    $("#txtintimeR").clockpicker({
        placement: 'bottom',
        align: 'left',
        autoclose: true,
        'default': 'now',
        twelvehour: false,
        donetext: 'Done',
        afterDone: function () {
            debugger
            $('#txtouttimeR').get(0).value = $('#txtintimeR').get(0).value;
            flag = true;
        }
    });
    $("#txtouttimeR").clockpicker({

        placement: 'bottom',
        align: 'left',
        autoclose: true,
        /* 'default': 'now',*/
        twelvehour: false,
        donetext: 'Done',
        afterDone: function () {
            debugger
            if (flag) {
                if ($('#txtouttimeR').get(0).value < $('#txtintimeR').get(0).value) {
                    //$('#txtouttimeR').get(0).value = $('#txtintimeR').get(0).value;

                }
            }
        }
    });
});

var CCC;

function Regularization() {
    debugger
    $.ajax({
        url: '../Attendance/Regularization',
        type: "POST",
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function (data) {
            debugger
            
            var regcheck = false;
            /*GenerateCalendar(events);*/
            var events = [];
            $.each(data.lstattendance, function (i, v) {
                debugger
                
                /*$('#HiddenID').val(v.id);*/
                regcheck = false;
                if ((v.rIntime != null) || (v.rOuttime != null)) {
                    regcheck = true
                }
                events.push({
                    /*start: moment(v.date, "MM/DD/YYYY HH:mm:ss"),*/
                    start: moment(v.date),
                    ID:v.id,
                    AIntime: moment(v.aIntime),
                    AOuttime: moment(v.aOuttime),
                    Regcheck: regcheck,
                    ONDUTY: v.onduty,
                    APPstatusHEAD: v.apPstatusHEAD,
                    RIntime: v.rIntime,
                    ROuttime: v.rOuttime,
                    timediffrence: v.timediffrence,
                    ShiftIntime: v.shiftIntime,
                    Shiftouttime: v.shiftouttime,
                   // ShiftINTime: v.shiftINTime,
                    Workhours: v.workhours,
                    DATE: moment(v.date),
                    ColorCode: v.colorCode,
                    /*AR_DATE1: v.date,*/
                    //INTIMEDB: v.intime,
                    //OUTTIMEDB: v.outtime,
                    
                    cache: false,
                    
                });
               /* $("#dateHidden").val(v.date);*/
            });
            GenerateCalendar(events);
        },
        error: function (hrx) {
            alert("failed");
        }

    });

}

function GenerateCalendar(events) {
    debugger
    $('#calendar').fullCalendar('destroy');
    $('#calendar').fullCalendar({
        
        timeFormat: 'h(:mm)a',
        // showNonCurrentDates: false,
        fixedWeekCount: false,
        header: {
            left: 'prev,next today',
            center: 'title',
            right: false
        },
        defaultView: 'month',
        eventLimit: false,
        eventColor: 'yellow',
        lazyLoading: false,
        events: events,

        selectable: true,
        select: function (arg ,event) {
            debugger
            var selectdate = arg._d;
            var selectedDate = arg.start;
            var intime = null;
            var outtime = null;
            //var ID = null;
            var selectedEvent = events.find(function (event) {
                debugger
                return event.start.isSame(selectdate, 'day');
            });
            if (selectedEvent) {
                debugger
                var date_formatin = new Date(selectedEvent.AIntime._i);
                var date_formatout = new Date(selectedEvent.AOuttime._i);
                //var selectedDate =  date_format.getHours() + ':' + date_format.getMinutes();


                intime = date_formatin.getHours() + ':' + date_formatin.getMinutes();
                outtime = date_formatout.getHours() + ':' + date_formatout.getMinutes();
              
            }
            $("#txtintime").val(intime);
            $("#txtouttime").val(outtime);
           // var date = String(arg._d.getDate() + "-" + (arg._d.getMonth() + 1) + "-" + arg._d.getFullYear());
            var date = arg._d.getFullYear() + "-" + ("0" + (arg._d.getMonth() + 1)).slice(-2) + "-" + ("0" + (arg._d.getDate())).slice(-2);
            $("#txtselectdate").val(date);            
            $("#calendarModal").modal('show');
            $('#HiddenID').val(selectedEvent.ID);

        },
      
        eventClick: function (calEvent, event, view,arg ) {
            debugger
            var datteee = event.DATE._i;
            var Aouttimee = event.AOuttime._i;
            var intimee = event.AIntime._i;
            
            $("#txtouttime").val();
            $("#txtintime").val();
            if (calEvent.RIntime == null && calEvent.ROuttime == null) {
                debugger
                var dt = calEvent.AIntime._i;
                $("#txtintime").val();
                $("#txtintimeR").val('');
                $("#txtouttimeR").val('');
                $('#txtintimeR').removeClass('border-danger');
                $('#txtintimeR').removeClass('border-danger');
                $('#ddlintimeR').removeClass('border-danger');
                //var d1 = Date.parse(dt);
                //var d = new Date(d1);
                //var a = d.toLocaleString();
                if (dt.toString() == 'NaN') {
                    dt = '';
                }
                $("#txtintime").val();
                debugger
                var dtt = calEvent.aOuttime._i;
                //var d11 = Date.parse(dtt);
                //var dd = new Date(d11);
                //var aa = dd.toLocaleString();
                if (dtt.toString() == 'NaN') {
                    dtt = '';
                }
                $("#txtouttime").val();
                debugger
                var dttt = calEvent.start;
                var d111 = Date.parse(dttt);
                var ddd = new Date(d111);
                var aaa = ddd.toLocaleString();
                $('#dateHidden').val(aaa);
                $('#calendarModal').modal("toggle");
            }
            else {
                debugger
                var dt = calEvent.RIntime;
                //var d1 = Date.parse(dt);
                //var d = new Date(d1);
                //var a = d.toLocaleString();
                if (dt == 'Invalid Date') {
                    dt = '';
                }
                $("#txtintimeR").val(dt);
                debugger
                var dtt = calEvent.ROuttime;
                //var d11 = Date.parse(dtt);
                //var dd = new Date(d11);
                //var aa = dd.toLocaleString();
                if (dtt == 'Invalid Date') {
                    dtt = '';
                }
                $("#txtouttimeR").val(dtt);
                debugger
                var dt = calEvent.AIntime;
            
                $("#txtintime").val();
                debugger
                var dtt = calEvent.AOuttime;
                //var d11 = Date.parse(dtt);
                //var dd = new Date(d11);
                //var aa = dd.toLocaleString();
                //if (aa == 'Invalid Date') {
                //    aa = '';
                //}
                $("#txtouttime").val();
                debugger
                var dttt = calEvent.start;
                var d111 = Date.parse(dttt);
                var ddd = new Date(d111);
                var aaa = ddd.toLocaleString();
                $('#dateHidden').val(aaa);
                $('#calendarModal').modal("toggle");
            }

        },
        
        dayRender: function (date, cell) {
            

            
        },
        viewRender: function (view, element) {
            debugger

            if (view.name == "agendaWeek" || view.name == "basicWeek")
            $('#calendar').fullCalendar('rerenderEvents');


        },
        eventRender: function (event, element) {
            debugger
            var datteee = event.DATE._i;
            var Aouttimee = event.AOuttime._i;
            var intimee = event.AIntime._i;
            var RIntime = event.RIntime;
            var ROuttime = event.ROuttime;
            var workhour = event.Workhours;
            $("#txtouttime").val();
            $("#txtintime").val();
            debugger
            if (event != null && intimee.toString() == "NaN") {
                debugger
                element.css("background-color", "red");
                element.css("height", "35px");
                element.css("width", "35px");
                element.css("border-radius", "50%");
                element.css("display", "inline-block");
                // element.css("margin", "20px");
                element.css("top", "6px;");
                element.css("left", "11px;");
                element.css("position", "absolute;");
                element.css("line-height", "28px;");
                element.css("font-size", "13px");
            }
            if (event != null && intimee != NaN) {
                debugger

                var appstatushead = event.APPstatusHEAD;//
                var onduty = event.ONDUTY;
                var Yellow = event.Regcheck;
                var date = event.start._i;
                var Aouttime = event.AOuttime._i;
                var intime = event.AIntime._i;
                var RIntime = event.RIntime;
                var ROuttime = event.ROuttime;
                var difference = event.timediffrence;
                var intimeshift = event.ShiftIntime;
                var outtimeshift = event.Shiftouttime;
                var ShiftIntime = event.ShiftIntime;//
                var Workhours = parseInt(event.Workhours);
                var colorcode = event.ColorCode;


                var hintime = intime;
                var houttime = Aouttime;

                element.find(".fc-time").remove();
                element.find(".fc-content").css('display', 'block');
                if (colorcode == 3) {
                    element.css("background-color", "blue");
                    element.css("height", "35px");
                    element.css("width", "35px");
                    element.css("border-radius", "50%");
                    element.css("display", "inline-block");
                    element.css("margin", "1px");
                    element.css("top", "6px;");
                    element.css("left", "11px;");
                    element.css("position", "absolute;");
                    element.css("line-height", "28px;");
                    element.css("font-size", "13px");
                }
                else if (colorcode == 1) {
                    debugger
                    element.css("background-color", "green");
                    element.prop('disabled', true);
                    element.css("height", "35px");
                    element.css("width", "35px");
                    element.css("border-radius", "50%");
                    element.css("display", "inline-block");
                    //element.css("margin", "20px");
                    element.css("top", "6px;");
                    element.css("left", "11px;");
                    element.css("position", "absolute;");
                    element.css("line-height", "28px;");
                    element.css("font-size", "13px");
                }
                else if (colorcode == 2) {
                    debugger
                    element.css("background-color", "yellow");
                    element.css("height", "35px");
                    element.css("width", "35px");
                    element.css("border-radius", "50%");
                    element.css("display", "inline-block");
                    //element.css("margin", "20px");
                    element.css("top", "6px;");
                    element.css("left", "11px;");
                    element.css("position", "absolute;");
                    element.css("line-height", "28px;");
                    element.css("font-size", "13px");
                }
                //else if (Yellow == true && appstatushead == 1) {
                //    debugger
                //    element.css('background-color', 'yellow');
                //    element.css('height', '35px');
                //    element.css('width', '35px');
                //    element.css('border-radius', '50%');
                //    element.css('display', 'inline-block');
                //    //element.css("margin", "20px");
                //    element.css('top', '6px;');
                //    element.css("left", '11px;');
                //    element.css('position', 'absolute;');
                //    element.css('line-height', '28px;');
                //    element.css('font-size', '13px');
                //}
                
                //else if (Workhours >8  && Yellow == false) {
                //    debugger
                //    element.css('background-color', 'green');
                //    element.css('height', '35px');
                //    element.css('width', '35px');
                //    element.css('border-radius', '50%');
                //    element.css('display', 'inline-block');
                //    //element.css("margin", "20px");
                //    element.css('top', '6px;');
                //    element.css("left", '11px;');
                //    element.css('position', 'absolute;');
                //    element.css('line-height', '28px;');
                //    element.css('font-size', '13px');
                //}
                else if (colorcode == 4) {
                    element.css("background-color", "red");
                    element.css("height", "35px");
                    element.css("width", "35px");
                    element.css("border-radius", "50%");
                    element.css("display", "inline-block");
                    //element.css("margin", "20px");
                    element.css("top", "6px;");
                    element.css("left", "11px;");
                    element.css("position", "absolute;");
                    element.css("line-height", "28px;");
                    element.css("font-size", "13px");
                }
                else if (colorcode == 5) {
                    element.css("background-color", "purple");
                    element.css("height", "35px");
                    element.css("width", "35px");
                    element.css("border-radius", "50%");
                    element.css("display", "inline-block");
                    //element.css("margin", "20px");
                    element.css("top", "6px;");
                    element.css("left", "11px;");
                    element.css("position", "absolute;");
                    element.css("line-height", "28px;");
                    element.css("font-size", "13px");
                }
                else if (colorcode == 6) {
                    element.css("background-color", "orange");
                    element.attr("disabled", "disabled");
                    element.css("height", "35px");
                    element.css("width", "35px");
                    element.css("border-radius", "50%");
                    element.css("display", "inline-block");
                    //element.css("margin", "20px");
                    element.css("top", "6px;");
                    element.css("left", "11px;");
                    element.css("position", "absolute;");
                    element.css("line-height", "28px;");
                    element.css("font-size", "13px");
                }
            }
        }
    });
}

function closeModal() {
    $('#calendarModal').modal('hide');
}

$('#btnsave').click(function () {
    debugger
    var isValid = 0;
    var isTrue = 0;
    var ID = $('#HiddenID').val();
    var intimee=$("#txtintime").val();
    var intime = $('#txtintime').html();
    var intimeR = $('#txtintimeR').val();
    var outtimeR = $('#txtouttimeR').val();
    var remarksIN = $("#ddlintimeR option:selected").text();
    var remarksOUT = $("#ddlouttimeR option:selected").text();
    var date = $('#dateHidden').val();
    var sdate = $("#txtselectdate").val();
    var ssdate = $("#txtselectdate").html();

    if (selectedfirsttab == 1) {
        debugger
        if (intimeR == "" || intimeR == null) {
            $('#txtintimeR').addClass('border-danger');
            isTrue = 1;
        } else if (remarksIN == "Select a suitable reason") {
            isValid = 1
            $('#ddlintimeR').addClass('border-danger');
        }
        else if (intimeR == "" || intimeR == null || intimeR == 'Invalid Date') {
            isTrue = 1
            $('#txtintimeR').addClass('border-danger');
        }

    } else if (selectedfirsttab == 2) {
        if (outtimeR == null || outtimeR == "") {
            $('#txtouttimeR').addClass('border-danger');
            isTrue = 1
        }
        else if (remarksOUT == "Select a suitable reason") {

            $('#ddlouttimeR').addClass('border-danger');
            isValid = 1
        }

        else if (outtimeR == null || outtimeR == "" || outtimeR == 'Invalid Date') {

            $('#txtouttimeR').addClass('border-danger');
            isTrue = 1
        }
    }
    if (isValid == 1 || isTrue == 1) {
        if (isValid == 1) {
            $('#msgsuccess').addClass('text-danger').html('Please select a reson..');
        } else {
            $('#msgsuccess').addClass('text-danger').html('Please select time..');
        }

    } else {

        $('#msgsuccess').removeClass();
        debugger
        var data = {
            INTIMETZ: intimeR,
            OUTTIMETZ: outtimeR,
            REMARKS: remarksIN,
            REMARKS_OUT: remarksOUT,
            AR_DATE1: (date),
            AR_DATE1: (sdate),
            ID:ID,

        }

        $.ajax({
            type: 'POST',
            url: "../Attendance/AttendanceRegularizationInsert",
            data: data,
            success: function (result) {
                debugger
                $('#msgsuccess').html('');
                $('#txtintimeR').removeClass('border-danger');
                $('#txtintimeR').removeClass('border-danger');
                $('#ddlintimeR').removeClass('border-danger');
                $('#spanmsg').addClass('').html("sent successfully");
              
                Regularization();
                    setTimeout(function () {
                    $('#spanmsg').html('');
                    }, 3000);
                window.location.reload();
            },
            error: function () {
                $('#spanmsg').html("something went wrong...");
                setTimeout(function () {
                    $("#spanmsg").html('');
                }, 5000);
            }
        });
    }
});

function selectedtab(ii) {
    debugger
    selectedfirsttab = ii
    //alert(selectedfirsttab);
   /* console.log(_i.val())*/
}



