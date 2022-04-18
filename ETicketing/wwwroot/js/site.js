
$(document).ready(function () {
    loadCartCount();
})
function loadCartCount() {

    $.ajax({
        url: "/api/carts",
        contentType: 'application/json; charset=utf-8',
        type: 'get',
        dataType: 'json',
        success: function (result) {

            let cartCount = result.length;
            if (cartCount > 0) {
                $("#cartCount").show();
                $("#cartCount").html(cartCount);
            }
            else {
                $("#cartCount").hide();
            }

        }
    });

}