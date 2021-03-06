﻿

// MAIN CREATE/CHILD
var readyStart = "test";
var readyEnd = "test";

function readyDatesAJAX() {

    $(function () {
        $.ajax({
            type: "POST",
            url: "/children/CalculateReadyDateAJAX",
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

    $('#buttonStep2').prop('disabled', false);

    $(function () {
        $('#addToddlerModal').modal('hide');
    });
}

function submitParentForm() {

    $('#buttonStep3').prop('disabled', false);

    $(function () {
        $('#addParentModal').modal('hide');
    });

}

function submitAgreedDaysForm() {

    $('#buttonStep4').prop('disabled', false);

    $(function () {
        $('#addAgreedDaysModal').modal('hide');
    });

}

function submitPickupForm() {
    $(function () {
        $('#addPickupModal').modal('hide');
    });
}

function submitMedicalInformationForm() {
    $(function () {
        $('#addMedicalInformationModal').modal('hide');
    });
}

function submitFoodInformationForm() {
    $(function () {
        $('#addFoodInformationModal').modal('hide');
    });
}

function submitSleepInformationForm() {
    $(function () {
        $('#addSleepInformationModal').modal('hide');
    });
}

function submitDailyRoutineForm() {
    $(function () {
        $('#addDailyRoutineModal').modal('hide');
    });
}

function submitImportantNoticeForm() {
    $(function () {
        $('#addImportantNoticeModal').modal('hide');
    });
}

$(function () {
    $('#addParentModal').on('hidden.bs.modal', function () {
        $(this).find("input,textarea,select,radio").val('').end();
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
        $('#steps a[href="#step1"]').tab('show');
    });
    $('[name="step2"]').click(function (e) {
        e.preventDefault();
        $('#steps a[href="#step2"]').tab('show');
    });
    $('[name="step3"]').click(function (e) {
        e.preventDefault();

        setDatePickerAgreedDaysAndPickups();
        readyDatesAJAX();

        $('#steps a[href="#step3"]').tab('show');
    });
    $('[name="step4"]').click(function (e) {
        e.preventDefault();
        $('#steps a[href="#step4"]').tab('show');
    });
    $('[name="step5"]').click(function (e) {
        e.preventDefault();
        $('#steps a[href="#step5"]').tab('show');
    });
    $('[name="step6"]').click(function (e) {
        e.preventDefault();
        $('#steps a[href="#step6"]').tab('show');
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

    $('#parent_Person_Address_PostalCode').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/shared/GetPostalCodes",
                type: "POST",
                dataType: "json",
                data: { input: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Postalcode + ' (' + item.Borough + ')', value: item.Postalcode };
                    }));
                }
            });
        },
        select: function (e, ui) {
            $.ajax({
                url: "/shared/GetCity",
                type: "POST",
                dataType: "json",
                data: { input: ui.item.value },
                success: function (data) {
                    $("#parent_Person_Address_City").val(data);
                }
            });
        }
    });
    $('#parent_Person_Address_Street').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/shared/GetStreet",
                type: "POST",
                dataType: "json",
                data: { postal: $('#parent_Person_Address_PostalCode').val(), input: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Street, value: item.Street };
                    }));
                }
            });
        }
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

    $('#pickup_Person_Address_PostalCode').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/shared/GetPostalCodes",
                type: "POST",
                dataType: "json",
                data: { input: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Postalcode + ' (' + item.Borough + ')', value: item.Postalcode };
                    }));
                }
            });
        },
        select: function (e, ui) {
            $.ajax({
                url: "/shared/GetCity",
                type: "POST",
                dataType: "json",
                data: { input: ui.item.value },
                success: function (data) {
                    $("#pickup_Person_Address_City").val(data);
                }
            });
        }
    });
    $('#pickup_Person_Address_Street').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/shared/GetStreet",
                type: "POST",
                dataType: "json",
                data: { postal: $('#pickup_Person_Address_PostalCode').val(), input: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Street, value: item.Street };
                    }));
                }
            });
        }
    });

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

//CREATE/CHILD/MEDICAL
$(function () {

    $('#medical_Doctor_Person_Address_PostalCode').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/shared/GetPostalCodes",
                type: "POST",
                dataType: "json",
                data: { input: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Postalcode + ' (' + item.Borough + ')', value: item.Postalcode };
                    }));
                }
            });
        },
        select: function (e, ui) {
            $.ajax({
                url: "/shared/GetCity",
                type: "POST",
                dataType: "json",
                data: { input: ui.item.value },
                success: function (data) {
                    $("#medical_Doctor_Person_Address_City").val(data);
                }
            });
        }
    });
    $('#medical_Doctor_Person_Address_Street').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/shared/GetStreet",
                type: "POST",
                dataType: "json",
                data: { postal: $('#medical_Doctor_Person_Address_PostalCode').val(), input: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Street, value: item.Street };
                    }));
                }
            });
        }
    });

    $("#DoctorForm").hide();
    $("#HasDoctor").click(function () {
        if ($(this).is(":checked")) {
            $("#DoctorForm").show();
        } else {
            $("#DoctorForm").hide();
        }
    });

    $("#MedicationForm").hide();
    $("#HasMedication").click(function () {
        if ($(this).is(":checked")) {
            $("#MedicationForm").show();
        } else {
            $("#MedicationForm").hide();
        }
    });

    $("#AllergiesForm").hide();
    $("#HasAllergies").click(function () {
        if ($(this).is(":checked")) {
            $("#AllergiesForm").show();
        } else {
            $("#AllergiesForm").hide();
        }
    });

    $("#ChildDiseasesForm").hide();
    $("#HadChildDiseases").click(function () {
        if ($(this).is(":checked")) {
            $("#ChildDiseasesForm").show();
        } else {
            $("#ChildDiseasesForm").hide();
        }
    });

});


// OVERVIEW


$(function () {

    $("#toddlerOverview").imagepicker({
        show_label: true
    });

    $('#toddlerOverview').change(function () {
        var id = $('#toddlerOverview').val();
        //if ($('#toddlerOverview').is(':selected')) {
            $("#EditLink").prop("href", "/Administration/Children/Edit/" + id + "");
        //}
        $(this).closest('form').trigger('submit');
    });

});

function updatePhoto() {
    if ($("#toddlerPhoto").val() === null) {
        $("#toddlerPhotoShow").attr('src', '/Images/baby-boy.png');
    } else {
        $("#toddlerPhotoShow").attr('src', '/Images/Photos/' + $("#toddlerPhoto").val());
    }
}

// EDIT

$(document).ready(function () {

    function hideElements(name) {
        $("#content").children().hide();
        $("div[name='" + name + "']").show();
        $("div[name='submitter']").show();
    }

    hideElements("personal");

    $('#editBar a').click(function () {
        hideElements($(this).attr('name'));
    });

    $('#personalDiv #postalCode').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/shared/GetPostalCodes",
                type: "POST",
                dataType: "json",
                data: { input: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Postalcode + ' (' + item.Borough + ')', value: item.Postalcode };
                    }));
                }
            });
        },
        select: function (e, ui) {
            $.ajax({
                url: "/shared/GetCity",
                type: "POST",
                dataType: "json",
                data: { input: ui.item.value },
                success: function (data) {
                    $("#city").val(data);
                }
            });
        }
    });
    $('#personalDiv #street').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/shared/GetStreet",
                type: "POST",
                dataType: "json",
                data: { postal: $('#postalCode').val(), input: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Street, value: item.Street };
                    }));
                }
            });
        }
    });

});

    /**********************************************************/

    // CALENDAR OVERVIEW

    // <![CDATA[
    $(document).ready(function () {
        $('#calendar').fullCalendar({
            hiddenDays: [ 6, 0 ],
            locale: 'nl-be',
            header: {
                left: '',
                center: '',
                right: ''
            },
            editable: false,
            events: '/calendar/GetToddlersOfPeriod',
            /*eventClick: function (calEvent, jsEvent, view) {

                alert('Event: ' + calEvent.title);

            },*/
            eventRender: function (event, element, view) {
                if (view.name === "agendaWeek") {
                    if (event.allDay === true) {
                        //element.find(".fc-title").append("<br/> <b>..</b>" + event.description);
                    } else {
                        //element.find(".fc-title").append("<br/>" +event.description);
                    }
                }
            },
            viewRender: function(currentView){
                if (currentView.name === "agendaWeek") {
                    $("#buttonCalZoomIn").addClass("disabled");
                    $("#buttonCalZoomOut").removeClass("disabled");
                } else if (currentView.name === "month") {
                    $("#buttonCalZoomOut").addClass("disabled");
                    $("#buttonCalZoomIn").removeClass("disabled");
                }
            },
            views: {
                month: {
                    eventLimit: 2
                }
            },
            loading: function (isLoading, view) {
                if (isLoading) {
                    $("#buttonCalZoomIn").addClass("disabled");
                } else {
                    $("#buttonCalZoomIn").removeClass("disabled");
                }
            }
        });
    });
    // ]]>

    function sendDemoMail() {
        $(function () {
            $.ajax({
                type: "POST",
                url: "/Shared/sendDemoMailAJAX",
                data: '{id: "0" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    alert("mail send");
                }
            });
        });
    }
