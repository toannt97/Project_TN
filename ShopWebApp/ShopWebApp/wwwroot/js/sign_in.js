$(function () {
    $('.sign-in__close-button').click(() => {
        $(".backdrop").removeClass("backdrop--open");
        $('.sign-in').remove();
    });

    $('.sign-in__forgot-text').click(() => {
        $.ajax({
            type: 'Get',
            contentType: false,
            processData: false,
            url: '/User/ResetPasswordIndex',
        }).done(function (data) {
            $('.sign-in').css('display','none');
            $('body').append(data);
        }).fail(function (err) {
            alert('An error occurred while performing operation');
            console.log(err.responseText);
        })
    });

    $('.sign-in__button-submit').click(() => {
        //ResetToDefault();
        $.ajax({
            type: 'Post',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({
                'EmailAddress': $('.sign-in__email-address').val().trim(),
                'Password': $('.sign-in__password').val().trim(),
            }),
            //beforeSend: () => {
            //    $('.user-reset__email-address').prop('disabled', true);
            //    $('.user-reset__button-send').prop('disabled', true);
            //},
            url: '/User/SignInHandle',
        }).done(function (data) {
            if (!data.statusCode)
                $('.sign-in').replaceWith(data);
            else {
                switch (data.statusCode) {
                    case 200: {
                        $(".backdrop").removeClass("backdrop--open");
                        $('.sign-in').remove();
                        UserLogined(data);
                        break;
                    }
                    default: {
                        $('.sign-in__status').text(data.messageError);
                        break;
                    }
                }
                $('.user-reset_notification').addClass('user-reset_notification--opened');
            }
        }).fail(function (err) {
            alert('An error occurred while performing operation');
            console.log(err.responseText);
        })
    });
});

// TODO: Consider remove else case
function UserLogined(user) {
    if (user.userName) {
        $('.buttons').removeClass('opened').addClass('closed');
        $('.account').removeClass('closed').addClass('opened');
        $('.user-name').text(user.userName);
        $('.cart-count').text(user.itemsInCart);
    } else {
        $('.buttons').removeClass('closed').addClass('opened');
        $('.account').removeClass('opened').addClass('closed');
    }
}