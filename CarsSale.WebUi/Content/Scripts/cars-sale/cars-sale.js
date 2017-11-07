$(document).ajaxError(() => {
    $(".modal").hide();
    $("#error-placement .message").text("Ajax request error. Please contact to support.");
    $("#error-placement").show();
});