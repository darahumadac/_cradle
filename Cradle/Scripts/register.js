$(function(){
    
    //Designer Profile Form
    var memberType = $('input[name="MemberAccountType"]');
    memberType.change(toggleDesignerDiv);

    function toggleDesignerDiv() {
        var memberTypeRadioBtn = $('input[name="MemberAccountType"]:checked').val();
        if (memberTypeRadioBtn == "Designer") {
            $('#designerDiv').collapse("show");
        }
        else {
            $('#designerDiv').collapse("hide");
        }
    }

    if ($('input[name="MemberAccountType"]:checked').val() == "Designer") {
        toggleDesignerDiv();
    }

    //Personal Profile Form
    $('#mobileNo, #businessMobileNo').val('');


});