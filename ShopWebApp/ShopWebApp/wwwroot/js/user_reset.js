$(function () {
    $('.user-reset__close-button').click(() => {
        $(".backdrop").removeClass("backdrop--open");
        $('.user-reset').remove();
    });

    $('.user-reset__button-send').click(() => {
        ResetToDefault();
        $.ajax({
            type: 'Post',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({
                'EmailAddress': $('.user-reset__email-address').val().trim(),
            }),
            beforeSend: () => {
                $('.user-reset__email-address').prop('disabled', true);
                $('.user-reset__button-send').prop('disabled', true);
            },
            url: '/User/ResetPasswordHandle',
        }).done(function (data) {
            if (!data.statusCode)
                $('.user-reset').replaceWith(data);
            else {
                console.log(data);
                switch (data.statusCode) {
                    case 200: {
                        $('.user-reset_notification').text('Your request has been sent successfully! Please check your email to get new password.');
                        $('.user-reset_notification').addClass('user-reset_notification--success');
                        break;
                    }
                    default: {
                        $('.user-reset_notification').text(data.errorMessage);
                        $('.user-reset_notification').addClass('user-reset_notification--fail');
                        $('.user-reset__button-send').prop('disabled', false);
                        $('.user-reset__email-address').prop('disabled', true);
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

function ResetToDefault() {
    $('.user-reset_notification').removeClass('user-reset_notification--opened');
    $('.user-reset_notification').removeClass('user-reset_notification--fail');
    $('.user-reset_notification').removeClass('user-reset_notification--success');
    $('.user-reset_notification').text('');
    $('.user-reset__validation').text('');
};