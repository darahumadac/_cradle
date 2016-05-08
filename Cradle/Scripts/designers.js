$(function () {
    function formatPrice() {
        parseFloat(this.val());
    }

    $('#budgetLimit').formatPrice();
})