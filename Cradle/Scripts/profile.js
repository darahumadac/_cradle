$(function () {
    $("body").tooltip({ selector: '[data-toggle=tooltip]' });
    $("#manageProfileDiv").scrollspy(this, { offset: 100 });

    $('#IsRTW, #IsCustomMade').change(displayDeliveryTimeOptions);

    function displayDeliveryTimeOptions()
    {
        if ($(this).attr('id') == "IsRTW")
        {
            if ($(this).prop('checked') == false) {
                $('#rtwOption').addClass("hidden");
            }
            else {
                $('#rtwOption').removeClass("hidden");
            }
        }

        if ($(this).attr('id') == "IsCustomMade") {
            if ($(this).prop('checked') == false)
            {
                $('#customMadeOption').addClass("hidden");
            }
            else {
                $('#customMadeOption').removeClass("hidden");
            }
        }

        if($('#IsRTW').prop('checked') == false && $('#IsCustomMade').prop('checked') == false)
        {
            $('#noMadeTypeSelected').removeClass('hidden');
        }
        else
        {
            $('#noMadeTypeSelected').addClass('hidden');
        }
    }

})
