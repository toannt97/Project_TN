$(document).ready(function () {
    function SetOverplay() {
        let opacity = 0.3;
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
function jQueryAxjaxSignInPost(form) {
    $.ajax({
        type: 'Post',
        contentType: false,
        processData: false,
        url: form.action,
        data: new FormData(form),
    }).done(function (data) {
        let regex = RegExp('/\w+/g');
        if (regex.test(data))
            $('#modal-login').html(data);
        else {
            RemoveOverplay();
            $('.text-danger').text('');
            $("#login-modal").css("display", "none");
        }

    }).fail(function (err) {
        alert(err);
        console.log(err);
    })
    return false;
}

function RemoveOverplay() {
    let opacity = 1;
    $('.header').css("opacity", opacity);
    $('.product').css("opacity", opacity);
    $('.footer').css("opacity", opacity);
}