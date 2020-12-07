$(function () {

    $('#btn-login').click(() => {
        $.ajax({
            type: 'Get',
            contentType: false,
            processData: false,
            url: '/User/SignInIndex',
        }).done(function (data) {
            $('.backdrop').addClass("backdrop--open");
            $('body').append(data);
        }).fail(function (err) {
            alert('An error occurred while performing operation');
            console.log(err.responseText);
        })
    });

    $('#btn-register').click(() => {
        $.ajax({
            type: 'Get',
            contentType: false,
            processData: false,
            url: '/User/SignUpIndex',
        }).done(function (data) {
            $('.backdrop').addClass("backdrop--open");
            $('body').append(data);
        }).fail(function (err) {
            alert('An error occurred while performing operation');
            console.log(err.responseText);
        })
    });

    $('.user__item--change-password').click(() => {
        $.ajax({
            type: 'Get',
            contentType: false,
            processData: false,
            url: '/User/ChangePasswordIndex',
        }).done(function (data) {
            $('.user__menu').removeClass('user__menu--opened');
            $('.backdrop').addClass("backdrop--open");
            $('body').append(data);
        }).fail(function (err) {
            alert('An error occurred while performing operation');
            console.log(err.responseText);
        })
    });

    $('.user__item--sign-out').click(() => {
        $.ajax({
            type: 'Post',
            contentType: false,
            processData: false,
            url: '/User/SignOut',
        }).done(function (data) {
            switch (data.statusCode) {
                case 200: {
                    $('.user-name').html('');
                    $('.buttons').removeClass('closed').addClass('opened');
                    $('.account').removeClass('opened').addClass('closed');
                    break;
                }
                default: {
                    alert('An error occurred while performing operation');
                    console.log(err.responseText);
                    break;
                }

            }
        }).fail(function (err) {
            alert('An error occurred while performing operation');
            console.log(err.responseText);
        })
    });
});