// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//@Respinoza - 2022

// Function for MM/dd/yyyy format - need a previous validation of dateString
function numberOfYears(dateString) {
    var parts = dateString.split("/");
    var dtDOB = new Date(parts[2] + "/" + parts[0] + "/" + parts[1]);
    var dtCurrent = new Date();
    return dtCurrent.getFullYear() - dtDOB.getFullYear();
}


//set the number of years
function CalculateYears(dateString) {
    var years = numberOfYears(dateString);
    $("#Age").val(years);
}

//Get the información from the service
var responseText = '';

function getPremium(strUrl) {

    //perform some basic validations
    if (!$("#myFormId").valid()) return;
    
    var dateStr = $("#txtDoB").val();
    var stateStr = $("#SelectedState option:selected").val();
    var planStr = $("#SelectedPlan option:selected").val();
    
    if (jQuery.trim(dateStr) == '' || !isDate(dateStr) ||  numberOfYears(dateStr) == 0) {
        document.getElementById("DateOfBirthMsg").innerText = 'Enter a valid Date of Birth';
        return;
    }
    else {
        document.getElementById("DateOfBirthMsg").innerText = '';
    }


    if (stateStr == "") {
        document.getElementById("SelectedStateMsg").innerText = 'Please select a State.';
        return;
    }
    else {
        document.getElementById("SelectedStateMsg").innerText = '';
    }


    if (planStr == "") {
        document.getElementById("SelectedPlanMsg").innerText = 'Please select a Plan.';
        return;
    }
    else {
        document.getElementById("SelectedPlanMsg").innerText = '';
    }


    // and when the form is correct....

    var parts = dateStr.split("/");

    var obj = {
        doB: parts[2] + "-" + parts[0] + "-" + parts[1] + "T00:00:00.000Z",
        state: stateStr,
        age: $("#Age").val(),
        plan: planStr
    };

    var json = JSON.stringify(obj);
    

    $.ajax({
        
       // url: 'https://localhost:44346/Premium',
        url: strUrl + 'Premium',
        type: 'put',
        data: json,
        cache: 'true',
        dataType: 'json',
        async: true,
        contentType: "application/json; charset=utf-8",
        success: function (json, status, jqXHR) {
            $('#SelectedPeriod').prop('disabled', !(json.length > 0));
            $("#SelectedPeriod option").prop("selected", false).trigger("chosen:updated");
            responseText = jqXHR.responseText;
            var myjsongrid = eval(responseText);
            var grid = $('#list');
            grid.clearGridData();
            grid.setGridParam({ data: myjsongrid });
            grid.setGridParam({ datatype: 'local' });
            grid.trigger('reloadGrid');
        },
        error: function (parsedjson, textStatus, errorThrown) {

            $('body').append(
                "parsedJson status: " + parsedjson.status + '</br>' +
                "errorStatus: " + textStatus + '</br>' +
                "errorThrown: " + parsedjson.responseText);

        }
    });



}




//manage the change of period

function periodChange(o) {
    if (responseText != '') {
        var myjsongrid = eval(responseText);
        var grid = $('#list');
        grid.clearGridData();
        grid.setGridParam({ data: myjsongrid });
        grid.setGridParam({ datatype: 'local' });
        grid.trigger('reloadGrid');
    }
}



//check if date
function isDate(dateString) {
    var checkdate = dateString;
    var rawmonth = checkdate.substr(0, 2);
    var rawday = checkdate.substr(3, 2);
    var rawyear = checkdate.substr(6, 4);
    var checkdate = new Date(checkdate);
    return ((rawmonth == checkdate.getMonth() + 1) &&
        (rawday == checkdate.getDate()) &&
        (rawyear == checkdate.getFullYear())
    );
}


//Events for the page
$(document).ready(function () {


    $.validator.addMethod("AccurateDate", function (value) {
        return isDate($("#txtDoB").val());
    }, "Not a real date");
    $.validator.classRuleSettings.AccurateDate = { AccurateDate: true };



    $("#myFormId").removeAttr("novalidate");
    $("#myFormId").validate({  // initialize plugin on the form
        rules: {
            txtDoB: {
                required: true,
                dateISO: true,
                AccurateDate: true
            },
            SelectedState: {
                required: true
            },
            SelectedPlan: {
                required: true
            },
            Age: {
                    required: true
            },
        },
        message: {
            SelectedState: "Please select a State.",
            SelectedPlan: "Please select a Plan.",
            Age: "Please select an Age.",

        }
    });


    //clear grid when change any value
    $('#txtDoB, #SelectedPlan, #SelectedState').change(function () {
        clearForm();
    });

    // clear the results form
    function clearForm() {
        $('#list').clearGridData();
        $('#SelectedPeriod').prop('disabled', true);
        $("#SelectedPeriod option").prop("selected", false).trigger("chosen:updated");
    }


//initialize picker
    $("#txtDoB").datepicker({
        dateFormat: 'mm/dd/yy',
        changeMonth: true,
        changeYear: true,
        yearRange: "-100:+0",

        showOn: "both",
        buttonImageOnly: true,
        buttonImage: 'css/themes/base/images/X-office-calendar.svg',
        onSelect: function (dateString, txtDate) {
            CalculateYears(dateString);
            clearForm();
        }

    }).next('button').button({ icons: { primary: 'ui-icon-calendar' }, text: false });

    $("#txtDoB").mask('99/99/9999', { placeholder: "__/__/____" });

    //initialize combos
    $("#SelectedState").chosen();

    $("#SelectedPlan").attr('style', 'width:150px;');
    $("#SelectedPlan").chosen();

    $("#SelectedPeriod").chosen({ disable_search: true });
    $('#SelectedPeriod').prop('disabled', true).trigger("chosen:updated");
    /// jqgrid

    $("#list").jqGrid({
        datatype: 'local',
        ajaxGridOptions: { contentType: 'application/json; charset=utf-8' },

        colNames: [  'carrier',  'premium', 'Annual', 'Monthly'],
        colModel: [
            { name: 'carrier', index: 'carrier', width: 155 },
            {
                name: 'premium', index: 'premium', width: 90, align: 'right', formatter: 'number',
            formatoptions: { decimalSeparator: ".",  decimalPlaces: 2, defaultValue: '0.00' } },
            {
                name: 'premium', index: 'premium', width: 90, align: 'right', formatter: function (cellvalue, options, rowObject) {
                    var ammount = '';
                    if ($('#SelectedPeriod').val() != '') {
                        ammount = (cellvalue * 12) / $('#SelectedPeriod').val(); //Annually
                        ammount = (Math.round(ammount * 100) / 100).toFixed(2);
                    }

                    return '<input type=\'text\' readonly=\'True\' style=\'width:80px;\'  id=\'txtAnnual' + options.rowId + '\'' + ' value=\"' + ammount + '\" \>';
                }
            },
            {
                name: 'premium', index: 'premium', width: 90, formatter: function (cellvalue, options, rowObject) {
                    var ammount = '';
                    if ($('#SelectedPeriod').val() != '') {
                        ammount = cellvalue / $('#SelectedPeriod').val(); //Monthly
                        ammount = (Math.round(ammount * 100) / 100).toFixed(2);
                    }

                    return '<input type=\'text\' readonly=\'True\' style=\'width:80px;\'  id=\'txtMonthly' + options.rowId + '\'' + ' value=\"' + ammount + '\" \>';

                }
            },


        ],
        pager: '#pager',
        sortname: 'carrier',
        viewrecords: true,
        sortorder: "desc",
        pgbuttons: false,
        pgtext: "",
        pginput: false,
        caption: "Premium"
    }).navGrid('#pager', { resize: false, refresh: false, view: false, del: false, add: false, edit: false, search: false, excel: false },
        {
            width: 500,
        }, // default settings for edit
        {
            width: 350,
        }, // default settings for add
        {
            width: 350,
            beforeSubmit: function (postdata, formid) {
                //here you can add client validations
                return [true, ''];
            },
            serializeDelData: function (postdata) {
            }
        }, // delete instead that del:false we need this
        { closeOnEscape: true, closeAfterSearch: true/*, multipleSearch: true*/ }, // search options
        { width: 600 } /* view parameters*/
    );



    ////

});