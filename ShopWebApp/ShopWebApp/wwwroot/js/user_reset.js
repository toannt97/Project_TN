$(function () {
    $('.user-reset__close-button').click(() => {
        $('.user-reset').remove();
        $("#login-modal").css("display", "block");
    });

    $('.user-reset__button-send').click(() => {
        $('.user-reset__validation').text('');
        console.log($('.user-reset__user-name').val().trim());
        $.ajax({
            type: 'Post',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({
                'UserName': $('.user-reset__user-name').val().trim(),
                'EmailAddress': $('.user-reset__email-address').val().trim(),
            }),
            url: '/User/ResetPasswordHandle',
        }).done(function (data) {
            $('.user-reset').replaceWith(data);    
        }).fail(function (err) {
            alert('An error occurred while performing operation');
            console.log(err.responseText);
        })
    });
});