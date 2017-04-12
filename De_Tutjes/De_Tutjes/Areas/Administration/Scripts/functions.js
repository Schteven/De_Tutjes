// MAIN CREATE/CHILD
var readyStart = "test";
var readyEnd = "test";

function readyDatesAJAX() {
    $(function () {
        $.ajax({
            type: "POST",
            url: "/Children/CalculateReadyDateAJAX",
            data: '{birthdate: "' + $('#toddler_Person_BirthDate').datepicker('getDate') + '" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $("#agreedDays_StartDate").val(response.readyForDaycare);
                $("#agreedDays_EndDate").val(response.readyForSchool);
                readyStart = response.readyForDaycare;
                readyEnd = response.readyForSchool;
            }
        });
    });
}

function submitToddlerForm() {

    readyDatesAJAX();

    $(function () {
        $('#addToddlerModal').modal('hide');
    });

}

function submitParentForm() {
    $(function () {
        $('#addParentModal').modal('hide');
    });
}

function submitAgreedDaysForm() {
    $(function () {
        $('#addAgreedDaysModal').modal('hide');
    });
}

function submitPickupForm() {
    $(function () {
        $('#addPickupModal').modal('hide');
    });
}

$(function () {
    $('#addToddlerModal').on('hidden.bs.modal', function () {
        $(this).find("input,textarea,select").val('').end();
    });

    $('#addParentModal').on('hidden.bs.modal', function () {
        $(this).find("input,textarea,select").val('').end();
    });

    $('#addAgreedDaysModal').on('hidden.bs.modal', function () {
        $(this).find("input,textarea,select").val('').end();
    });

    $('#addPickupModal').on('hidden.bs.modal', function () {
        $(this).find("input,textarea,select").val('').end();
    });
});

$(function () {
    $('[name="step1"]').click(function (e) {
        e.preventDefault();

        //var addToddlerModal = $("#addToddlerModal").html();
        //$("#addToddlerModal").html(addToddlerModal);

        $('#steps a[href="#a"]').tab('show');

    });
    $('[name="step2"]').click(function (e) {
        e.preventDefault();

        //var addParentModal = $("#addParentModal").html();
        //$("#addParentModal").html(addParentModal);

        $('#steps a[href="#b"]').tab('show');
    });
    $('[name="step3"]').click(function (e) {
        e.preventDefault();

        /*var addAgreedDaysModal = $("#addAgreedDaysModal").html();
        $("#addAgreedDaysModal").html(addAgreedDaysModal);

        var addPickupModal = $("#addPickupModal").html();
        $("#addPickupModal").html(addPickupModal);
        */

        setDatePickerAgreedDaysAndPickups();

        readyDatesAJAX();

        $('#steps a[href="#c"]').tab('show');
    });
    $('[name="step4"]').click(function (e) {
        e.preventDefault();
        $('#steps a[href="#d"]').tab('show');
    });
    $('[name="step5"]').click(function (e) {
        e.preventDefault();
        $('#steps a[href="#e"]').tab('show');
    });
    $('[name="step6"]').click(function (e) {
        e.preventDefault();
        $('#steps a[href="#f"]').tab('show');
    });
});

// CREATE/CHILD/TODDLER

$(function () {
    $('#toddler_Person_BirthDate').datepicker({
        format: "dd/mm/yyyy",
        language: "nl",
        autoclose: true
    });
});

// CREATE/CHILD/PARENT

$(function () {
    $('#parent_Person_BirthDate').datepicker({
        format: "dd/mm/yyyy",
        language: "nl",
        autoclose: true
    });
});

// CREATE/CHILD/AGREEDDAYS

function CalculateFreePlacesOnDayAJAX(id) {
    $(function () {
        $.ajax({
            type: "POST",
            url: "/Children/CalculateFreePlacesOnDayAJAX",
            data: '{day: "' + id + '", startdate: "' + readyStart + '", enddate: "' + readyEnd + '" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                //$("#check" + id).html(response.freeplace);
            }
        });
    });
}

function CalculateAll() {

    CalculateFreePlacesOnDayAJAX("Monday");
    CalculateFreePlacesOnDayAJAX("Tuesday");
    CalculateFreePlacesOnDayAJAX("Wednesday");
    CalculateFreePlacesOnDayAJAX("Thursday");
    CalculateFreePlacesOnDayAJAX("Friday");

}

function setDatePickerAgreedDaysAndPickups() {
    $(function () {
        $('#agreedDays_StartDate').datepicker({
            format: "dd/mm/yyyy",
            language: "nl",
            daysOfWeekDisabled: "0,6",
            autoclose: true
        });

        $('#agreedDays_EndDate').datepicker({
            format: "dd/mm/yyyy",
            language: "nl",
            daysOfWeekDisabled: "0,6",
            autoclose: true
        });
    });
}


$(function () {
    /*
    $("#Monday").click(function () {
        CalculateFreePlacesOnDayAJAX(this.id);
    });
    $("#Tuesday").click(function () {
        CalculateFreePlacesOnDayAJAX(this.id);
    });
    $("#Wednesday").click(function () {
        CalculateFreePlacesOnDayAJAX(this.id);
    });
    $("#Thursday").click(function () {
        CalculateFreePlacesOnDayAJAX(this.id);
    });
    $("#Friday").click(function () {
        CalculateFreePlacesOnDayAJAX(this.id);
    });
    */

    $("#agreedDays_StartDate").change(function () {
        readyStart = $("#agreedDays_StartDate").datepicker('getDate');
    });

    $("#agreedDays_EndDate").change(function () {
        readyEnd = $("#agreedDays_StartDate").datepicker('getDate');
        CalculateAll();
    });

    $('#addAgreedDaysModal').on('shown.bs.modal', function () {
        CalculateAll();
    });

        $('.button-checkbox').each(function () {
            var $widget = $(this),
                $button = $widget.find('button'),
                $checkbox = $widget.find('input:checkbox'),
                color = $button.data('color'),
                settings = {
                    on: {
                        icon: 'glyphicon glyphicon-check pull-left'
                    },
                    off: {
                        icon: 'glyphicon glyphicon-unchecked pull-left'
                    }
                };
            $button.on('click', function () {
                $checkbox.prop('checked', !$checkbox.is(':checked'));
                $checkbox.triggerHandler('change');
                updateDisplay();
            });
            $checkbox.on('change', function () {
                updateDisplay();
            });
            function updateDisplay() {
                var isChecked = $checkbox.is(':checked');
                $button.data('state', isChecked ? "on" : "off");
                $button.find('.state-icon')
                    .removeClass()
                    .addClass('state-icon ' + settings[$button.data('state')].icon);
                if (isChecked) {
                    $button
                        .removeClass('btn-default')
                        .addClass('btn-' + color + ' active');
                }
                else {
                    $button
                        .removeClass('btn-' + color + ' active')
                        .addClass('btn-default');
                }
            }
            function init() {
                updateDisplay();
                if ($button.find('.state-icon').length === 0) {
                    $button.prepend('<i class="state-icon ' + settings[$button.data('state')].icon + '"></i> ');
                }
            }
            init();
        });
    });
