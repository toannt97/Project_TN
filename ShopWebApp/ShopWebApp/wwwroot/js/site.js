$(document).ready(function () {
    function SetOverplay() {
        $('.backdrop').addClass("backdrop--open");
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
        $('.validation').text('');
        $("#login-modal").css("display", "none");
        $("#register-modal").css("display", "none");
    });

    $('.user__info').click(function () {
        $('.user__menu').addClass('user__menu--opened');
    });

    $(document).mouseup(function (e) {
        var container = $('.user__menu');
        if (!container.is(e.target) && container.has(e.target).length === 0 ) {
            $('.user__menu').removeClass('user__menu--opened');
        }
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
            $('.text-danger').text('');
            switch (data.statusCode) {
                case 404:
                    $('.login-fail').text(data.messageError);
                    break;
                case 200:
                    UserLogined(data.userName);
                    RemoveOverplay();
                    $("#login-modal").css("display", "none");
                    break;
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
    $(".backdrop").removeClass("backdrop--open");
}