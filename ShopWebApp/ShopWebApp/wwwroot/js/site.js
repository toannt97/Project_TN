$(document).ready(function () {
    function SetOverplay() {
        let opacity = 0.3;
        $('.header').css("opacity", opacity);
        $('.product').css("opacity", opacity);
        $('.footer').css("opacity", opacity);
    }

    function RemoveOverplay() {
        let opacity = 1;
        $('.header').css("opacity", opacity);
        $('.product').css("opacity", opacity);
        $('.footer').css("opacity", opacity);
    }
    // When the user clicks the button, open the modal 
    $('#btn-login').click(function () {
        SetOverplay();
        $("#login-modal").css("display", "block");
    });

    $('#btn-register').click(function () {
        SetOverplay();
        $("#register-modal").css("display", "block");
    });

    // When the user clicks on <span> (x), close the modal
    $('.close-modal').click(function () {
        RemoveOverplay();
        $('.text-danger').text('');
        $("#login-modal").css("display", "none");
        $("#register-modal").css("display", "none");
    });

});
