$(function () {
    $('.card-layout__button-close').click(() => {
        $(".backdrop").removeClass("backdrop--open");
        $('.card-layout').remove();
    });

    $('.sign-in__forgot-link').click(() => {
        $.ajax({
            type: 'Get',
            contentType: false,
            processData: false,
            url: '/User/ResetPasswordIndex',
        }).done(function (data) {
            $('.card-layout').remove();
            $('body').append(data);
        }).fail(function (err) {
            alert('An error occurred while performing operation');
            console.log(err.responseText);
        })
    });

    //$("input[name=PhoneNumber]")[0].oninvalid = function () {
    //    this.setCustomValidity("The phone number is invalid.");
    //    alert(CONSTANTS.name);
    //};

    $('.card-layout__button-submit').click((event) => {
        let idElement = event.target.id;
        if (idElement === 'sign-in__button-submit') {
            var data = {
                EmailAddress: $('.sign-in__email-address').val(),
                Password: $('.sign-in__password').val(),
            };
            let url = '/User/SignInHandle';
            SubmitForm(data, url);
        }
        else if (idElement === 'user-reset__button-submit') {
            var data = {
                EmailAddress: $('.user-reset__email-address').val().trim(),
            };
            let url = '/User/ResetPasswordHandle';
            let successMessage = 'Your request has been sent successfully.Please check your email to get password!';
            SubmitForm(data, url, successMessage);
        }
        else if (idElement === 'sign-up__button-submit') {
            let data = {
                UserName: $('.sign-up__user-name').val().trim(),
                EmailAddress: $('.sign-up__email-address').val().trim(),
                Password: $('.sign-up__password').val().trim(),
                ConfirmPassword: $('.sign-up__confirm-password').val().trim(),
                PhoneNumber: $('.sign-up__phone-number').val().trim(),
                Address: $('.sign-in__address').val().trim(),
            };
            let url = 'User/SignUpHandle';
            let successMessage = 'Your account has been successfully registered. Please check your email to activate your account!';
            SubmitForm(data, url, successMessage);
        }
        else {
            GetSessionUser().then(respone => {
                let idSessionUser = respone ? respone.id : undefined;
                if (!idSessionUser) {
                    $('.buttons').removeClass('closed').addClass('opened');
                    $('.account').removeClass('opened').addClass('closed');
                }
                else {
                    switch (idElement) {
                        case 'user-change-password__button-submit': {
                            let data = {
                                CurrentPassword: $('.user-change-password__current-password').val().trim(),
                                NewPassword: $('.user-change-password__new-password').val().trim(),
                                PasswordComfirmation: $('.user-change-password__password-confirmation').val().trim(),
                            };
                            let url = 'User/ChangePasswordHandle';
                            let successMessage = 'Your password has been updated successfully!';
                            SubmitForm(data, url, successMessage);
                            break;
                        }
                        case 'update-profile__button-submit': {
                            let data = {
                                Id: idSessionUser,
                                EmailAddress: $('.update-profile__email-address').val(),
                                PhoneNumber: $('.update-profile__phone-number').val(),
                                Address: $('.update-profile__address').val(),
                                UserName: $('.update-profile__user-name').val(),
                            };
                            let url = 'User/UpdateProfileHandle';
                            let successMessage = 'Your profile has been updated successfully!';
                            console.log($('.update-profile__user-name').val());
                            SubmitForm(data, url, successMessage);
                            break;
                        }
                    }
                }
            });
        }
    });
});

function GetSessionUser() {
    return $.ajax({
        type: 'Get',
        url: '/User/GetCurrentUser',
        dataType: 'json',
    }).done(function (data) {
    }).fail(function (err) {
        alert('An error occurred while performing operation');
    })
}

function SubmitForm(dataRequest, url, successMessage) {
    let userName = dataRequest.UserName;
    $.ajax({
        type: 'Post',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({
            Id: dataRequest.Id,
            EmailAddress: dataRequest.EmailAddress,
            Password: dataRequest.Password,
            ConfirmPassword: dataRequest.ConfirmPassword,
            PhoneNumber: dataRequest.PhoneNumber,
            Address: dataRequest.Address,
            UserName: dataRequest.UserName,
            CurrentPassword: dataRequest.CurrentPassword,
            NewPassword: dataRequest.NewPassword,
            PasswordComfirmation: dataRequest.PasswordComfirmation,
        }),
        beforeSend: handleBeforeSend(),
        url: url,
    }).done(function (dataResponse) {
        if (!dataResponse.statusCode)
            $('.card-layout').replaceWith(dataResponse);
        else {
            switch (dataResponse.statusCode) {
                case 200: {
                    if (dataResponse.userName) {
                        handleLoginSuccess(dataResponse.userName, dataResponse.itemsInCart);
                    }
                    else {
                        console.log(dataResponse.statusCode);
                        handleSuccessElse(userName, successMessage);
                    }
                    break;
                }
                default: {
                    console.log(dataResponse.statusCode);
                    handleErrorHTTP(dataResponse.messageError)
                    break;
                }
            }
        }
    }).fail(function (err) {
        console.log(err);
        alert('An error occurred while performing operation');
    })
}

function onlyNumberKey(evt) {
    // Only ASCII charactar in that range allowed
    var ASCIICode = (evt.which) ? evt.which : evt.keyCode
    if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
        return false;
    return true;
}

function handleBeforeSend() {
    $('.card-layout__input-text').prop('disabled', true);
    $('.card-layout__button-submit').prop('disabled', true);
}

function handleErrorHTTP(messageError) {
    $('.card-layout__validation').text('');
    $('.card-layout__notification').text(messageError);
    $('.card-layout__notification').removeClass('card-layout__notification--success');
    $('.card-layout__notification').addClass('card-layout__notification--fail');
    $('.card-layout__notification').addClass('card-layout__notification--open');
    $('.card-layout__input-text').prop('disabled', false);
    $('.card-layout__button-submit').prop('disabled', false);
    $('.card-layout__input-text--disabled').prop('disabled', true);
}

function handleLoginSuccess(userName, NoOfItemsInCart) {
    $('.card-layout__validation').text('');
    $('.user-name').text(userName);
    $('.cart-count').text(NoOfItemsInCart);
    $('.buttons').removeClass('opened');
    $('.account').removeClass('closed');
    $('.account').removeClass('opened');
    $('.account').removeClass('opened');
    $('.card-layout').remove();
    $('.backdrop').removeClass('backdrop--open');
    $('.buttons').addClass('closed');
    $('.card-layout__notification').addClass('card-layout__notification--open');
}

function handleSuccessElse(userName, successMessage) {
    if (userName)
        $('.user-name').text(userName);
    $('.card-layout__validation').text('');
    $('.card-layout__notification').text(successMessage);
    $('.card-layout__notification').removeClass('card-layout__notification--fail');
    $('.card-layout__notification').addClass('card-layout__notification--success');
    $('.card-layout__notification').addClass('card-layout__notification--open');
}