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

    $('.search-result__input-text').keyup(debounce(function () {
        const keyword = $(this).val();
        if (keyword === '') {
            $('.search-result__items').css('display', 'none');
            return;
        }
            
        // The following function will be executed every one and a half seconds
        $.ajax({
            url: "/Product/Search",
            type: "Post",
            data: { "Keyword": keyword },
            success: function (data) {
                $(".search-result__items").html(data);
                $('.search-result__items').css('display', 'block');
            },
            error: function (data) {
                alert("Error: " + data);
            }
        });
    }, 1000));

    $(document).mouseup(function (e) {
        var target = $('.search-result__items');

        if (!target.is(e.target) && target.has(e.target).length === 0) {
            $('.search-result__items').css('display', 'none');
        }
    });
    
});

var debounce = function (func, wait, immediate) {
    var timeout;
    return function () {
        var context = this, args = arguments;
        var later = function () {
            timeout = null;
            if (!immediate) func.apply(context, args);
        };
        var callNow = immediate && !timeout;
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
        if (callNow) func.apply(context, args);
    };
};