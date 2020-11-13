$(document).ready(function () {

    //$.ajax({
    //    type: 'Get',
    //    url: '@(Url.Action("GetCurrentUser","User"))',
    //    contentType: "application/json",
    //    processData: false,
        
    //}).done(function (data) {
    //    console.log(data);
    //}).fail(function (err) {
    //    console.log(err);
    //})

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

function jQueryAxjaxSignInPost(form, e) {
    $.ajax({
        type: 'Post',
        contentType: false,
        processData: false,
        url: form.action,
        data: new FormData(form),
    }).done(function (data) {
        if (!data.statusCode)
            $('#modal-login').html(data);
        else {
            if (data.statusCode == '404') {
                $('.text-danger').text('');
                $('.login-fail').text('The email address or password is incorrect.');
            } else {
                UserLogined(data.userName);
                RemoveOverplay();
                $('.text-danger').text('');
                $("#login-modal").css("display", "none");
            }
        }

    }).fail(function (err) {
        alert(err);
        console.log(err);
    })
    return false;
}

function UserLogined(userName) {
    if (userName) {
        $('.buttons').removeClass('opened').addClass('closed');
        $('.account').removeClass('closed').addClass('opened');
        $('.user-name').text(userName);
    } else {
        $('.buttons').removeClass('closed').addClass('opened');
        $('.account').removeClass('opened').addClass('closed');
    }
}

function RemoveOverplay() {
    let opacity = 1;
    $('.header').css("opacity", opacity);
    $('.product').css("opacity", opacity);
    $('.footer').css("opacity", opacity);
}