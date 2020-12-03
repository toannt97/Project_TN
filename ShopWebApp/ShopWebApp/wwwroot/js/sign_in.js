$(function () {
    $('.user-sign-in__forgot').click(() => {
        $.ajax({
            type: 'Get',
            contentType: false,
            processData: false,
            url: '/User/ResetPasswordIndex',
        }).done(function (data) {
            $('.close-modal').trigger('click');
            $('.backdrop').addClass("backdrop--open");
            $('body').append(data);
        }).fail(function (err) {
            alert('An error occurred while performing operation');
            console.log(err.responseText);
        })
    });
});