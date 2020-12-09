$(function () {
    $('.card-layout__button-close').click(() => {
        $(".backdrop").removeClass("backdrop--open");
        $('.card-layout').remove();
    });

    $('.card-layout__button-submit').click((event) => {
        GetSessionUser().then(respone => {
            let idSessionUser = respone.id;
            if (!idSessionUser) {
                $('.buttons').removeClass('closed').addClass('opened');
                $('.account').removeClass('opened').addClass('closed');
            } else {
                let idElement = event.target.id;
                switch (idElement) {
                    case 'update-profile__button-submit': {
                        var data = {
                            Id: idSessionUser,
                            EmailAddress : $('.update-profile__email-address').val(),
                            PhoneNumber: $('.update-profile__phone-number').val(),
                            Address: $('.update-profile__address').val(),
                            UserName : $('.update-profile__user-name').val(),
                        };
                        let url = 'User/UpdateProfileHandle';
                        SubmitForm(data, url);
                        break;
                    }
                }
            }
        });

        

        $("input[name=PhoneNumber]")[0].oninvalid = function () {
            this.setCustomValidity("The phone number is invalid.");
            alert(CONSTANTS.name);
        };

        //if (!idSessionUser) {
        //    $('.buttons').removeClass('closed').addClass('opened');
        //    $('.account').removeClass('opened').addClass('closed');
        //}
        //let idElement = event.target.id;
        //switch (idElement) {
        //    case 'update-profile__button-submit': {
        //        let data = {
        //            Id = $('.update-profile__id').val(),
        //            EmailAddress = $('update-profile__email-address').val(),
        //            PhoneNumber = $('update-profile__email-address').val(),
        //            Address = $('update-profile__address').val(),
        //            UserName = $('update-profile__user-name').val(),
        //        };
        //        let url = 'User/UpdateProfileHandle';
        //        SubmitForm(data, url);
        //        break;
        //    }
        //}
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

function SubmitForm(dataRequest, url) {
    //ResetToDefault();
    $.ajax({
        type: 'Post',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({
            Id: dataRequest.Id,
            EmailAddress: dataRequest.EmailAddress,
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
            $('.user-reset').replaceWith(dataResponse);
        else {
            switch (dataResponse.statusCode) {
                case 200: {
                    $('.user-reset_notification').text('Your request has been sent successfully! Please check your email to get new password.');
                    $('.user-reset_notification').addClass('user-reset_notification--success');
                    break;
                }
                default: {
                    $('.user-reset_notification').text(dataResponse.errorMessage);
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
    })
}

function onlyNumberKey(evt) {
    // Only ASCII charactar in that range allowed
    var ASCIICode = (evt.which) ? evt.which : evt.keyCode
    if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
        return false;
    return true;
}