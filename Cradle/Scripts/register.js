﻿$(function(){
    
    var memberType = $('input[name="MemberAccountType"]');
    memberType.change(toggleDesignerDiv);

    function toggleDesignerDiv() {
        var memberTypeRadioBtn = $('input[name="MemberAccountType"]:checked').val();
        if (memberTypeRadioBtn == "Designer") {
            $('#designerDiv').collapse("show");
        }
        else
        {
            $('#designerDiv').collapse("hide");
        }
    }
    

});