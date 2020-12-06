$(function () {
    $('.sign-up__close-button').click(() => {
        $(".backdrop").removeClass("backdrop--open");
        $('.sign-up').remove();
    });

    $("input[name=PhoneNumber]")[0].oninvalid = function () {
        this.setCustomValidity("The phone number is invalid.");
        alert(CONSTANTS.name);
    };

    $('.sign-up__button-submit').click(() => {
        $.ajax({
            type: 'Post',
            contentType: 'application/json; charset=utf-8',
            url: 'User/SignUpHandle',
            data: JSON.stringify({
                'UserName': $('.sign-up__user-name').val().trim(),
                'EmailAddress': $('.sign-up__email-address').val().trim(),
                'Password': $('.sign-up__password').val().trim(),
                'ConfirmPassword': $('.sign-up__confirm-password').val().trim(),
                'PhoneNumber': $('.sign-up__phone-number').val().trim(),
                'Address': $('.sign-in__address').val().trim(),
            }),
            beforeSend: function () {
                DisableTextField();
            },
        }).done(function (data) {
            if (!data.statusCode) {
                $('.sign-up').replaceWith(data);
            } else {
                switch (data.statusCode) {
                    case 200: {
                        $('.sign-up__notification--success').css("display", "block");
                        break;
                    }
                    default: {
                        $('.sign-in__register-fail').text(data.messageError);
                        EnableTextField();
                        break;
                    }
                }
            }
        }).fail(function (err) {
            console.log(err);
        })
    });
})

function onlyNumberKey(evt) {
    // Only ASCII charactar in that range allowed
    var ASCIICode = (evt.which) ? evt.which : evt.keyCode

    if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
        return false;

    return true;
}

function RemoveOverplay() {
    let opacity = 1;
    $('.header').css("opacity", opacity);
    $('.product').css("opacity", opacity);
    $('.footer').css("opacity", opacity);
}

function DisableTextField() {
    $('.sign-in input').prop("disabled", true);
    $('.sign-up__button-submit').prop("disabled", true);
}

function EnableTextField() {
    $('.sign-in input').prop("disabled", false);
    $('sign-up__button-submit').prop("disabled", false);
}