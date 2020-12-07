$(function () {
    $('.user-change-password__close-button').click(() => {
        $(".backdrop").removeClass("backdrop--open");
        $('.user-change-password').remove();
    });

    $('.user-change-password__button-submit').click(() => {
        $.ajax({
            type: 'Post',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({
                'CurrentPassword': $('.user-change-password__current-password').val().trim(),
                'NewPassword': $('.user-change-password__new-password').val().trim(),
                'PasswordComfirmation': $('.user-change-password__password-confirmation').val().trim(),
            }),
            beforeSend: BeforSend(),
            url: '/User/ChangePasswordHandle',
        }).done(function (data) {
            if (!data.statusCode)
                $('.user-change-password').replaceWith(data);
            else {
                switch (data.statusCode) {
                    case 200: {
                        $('.user-change-password_notification').text('Your password has been updated successfully!');
                        $('.user-change-password_notification').addClass('user-change-password_notification--success');
                        break;
                    }
                    case 404: {
                        $('.user-change-password_notification').text('Your password is not correct. Please check it again!');
                        $('.user-change-password_notification').addClass('user-change-password_notification--fail');
                        EnableFileds();
                        break;
                    }
                    case 512: {
                        $('.user-change-password_notification').text('Database connection occurs error. Please contact to administrator!');
                        $('.user-change-password_notification').addClass('user-change-password_notification--fail');
                        EnableFileds();
                        break;
                    }
                    default: {
                        $('.user-change-password_notification').text('An error occurred while performing operation. Please contact to administrator!');
                        $('.user-change-password_notification').addClass('user-change-password_notification--fail');
                        EnableFileds();
                        break;
                    }
                }
                $('.user-change-password_notification').addClass('user-change-password_notification--opened');
            }

        }).fail(function (err) {
            $('.user-change-password_notification').text('An error occurred while performing operation. Please contact to administrator!');
            $('.user-change-password_notification').addClass('user-change-password_notification--fail');
            EnableFileds();
        })
    });
})

function BeforSend() {
    ClearValidationResult();
    DisableFileds();
}

function ClearValidationResult() {
    $('.user-change-password__validation').text('');
    $('.user-change-password_notification').removeClass('user-change-password_notification--fail');
    $('.user-change-password_notification').removeClass('user-change-password_notification--success');

}

function DisableFileds() {
    $('.user-change-password__current-password').prop('disabled', true);
    $('.user-change-password__new-password').prop('disabled', true);
    $('.user-change-password__password-confirmation').prop('disabled', true);
    $('.user-change-password__button-submit').prop('disabled', true);
}

function EnableFileds() {
    $('.user-change-password__current-password').prop('disabled', false);
    $('.user-change-password__new-password').prop('disabled', false);
    $('.user-change-password__password-confirmation').prop('disabled', false);
    $('.user-change-password__button-submit').prop('disabled', false);
}