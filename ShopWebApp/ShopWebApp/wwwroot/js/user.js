$(function () {
    $('.card-layout__button-close').click(() => {
        $(".backdrop").removeClass("backdrop--open");
        $('.card-layout').remove();
    });

    $('.sign-in__forgot-link').click(() => {
        console.log('click');
        $.ajax({
            type: 'Get',
            contentType: false,
            processData: false,
            url: '/User/ResetPasswordIndex',
        }).done(function (data) {
            $('.sign-in').remove();
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
            console.log('yo');
            var data = {
                EmailAddress: $('.sign-in__email-address').val(),
                Password: $('.sign-in__password').val(),
            };
            let url = '/User/SignInHandle';
            SubmitForm(data, url);
        }
        else {
            GetSessionUser().then(respone => {
                let idSessionUser = respone ? respone.id : undefined;
                if (!idSessionUser) {
                    $('.buttons').removeClass('closed').addClass('opened');
                    $('.account').removeClass('opened').addClass('closed');
                }
                else {
                    //switch (idElement) {
                    //    case 'sign-in__button-submit': {
                    //        //var data 
                    //        break;
                    //    }
                    //    case 'update-profile__button-submit': {
                    //        let data = {
                    //            Id: idSessionUser,
                    //            EmailAddress : $('.update-profile__email-address').val(),
                    //            PhoneNumber: $('.update-profile__phone-number').val(),
                    //            Address: $('.update-profile__address').val(),
                    //            UserName : $('.update-profile__user-name').val(),
                    //        };
                    //        let url = 'User/UpdateProfileHandle';
                    //        let successMessage = 'Your profile has been updated successfully!';
                    //        SubmitForm(data, url, successMessage);
                    //        break;
                    //    }
                    //}
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
            PhoneNumber: dataRequest.PhoneNumber,
            Address: dataRequest.Address,
            UserName: dataRequest.UserName,
            FullName: dataRequest.FullName,
            Age: dataRequest.Age,
        }),
        beforeSend: () => {
            $('.card-layout__input-text').prop('disabled', true);
            $('.card-layout__button-submit').prop('disabled', true);
        },
        url: url,
    }).done(function (dataResponse) {
        if (!dataResponse.statusCode)
            $('.card-layout').replaceWith(dataResponse);
        else {
            switch (dataResponse.statusCode) {
                case 200: {
                    $('.card-layout__notification').text(successMessage);
                    $('.card-layout__notification').removeClass('card-layout__notification--fail');
                    $('.card-layout__notification').addClass('card-layout__notification--success');
                    $('.user-name').text(userName);
                    break;
                }
                default: {
                    $('.card-layout__notification').text(dataResponse.errorMessage);
                    $('.card-layout__notification').removeClass('card-layout__notification--success');
                    $('.card-layout__notification').addClass('card-layout__notification--fail');
                    $('.user-reset__button-send').prop('disabled', false);
                    $('.card-layout__input-text').prop('disabled', false);
                    $('.card-layout__button-submit').prop('disabled', false);
                    $('.card-layout__input-text--disabled').prop('disabled', true);
                    console.log(dadataResponse.statusCode);
                    break;
                }
            }
            $('.card-layout__notification').addClass('card-layout__notification--open');
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