function jQueryAxjaxSignUpPost(form) {
    $.ajax({
        type: 'Post',
        contentType: false,
        processData: false,
        url: form.action,
        data: new FormData(form),
        beforeSend: function () {
            DisableTextField();
            console.log('sended');
        },
    }).done(function (data) {
        if (!data.statusCode) {
            $('#modal-register').html(data);
        } else {
            switch (data.statusCode) {
                case 452:
                    $('.register-fail').text(data.messageError);
                    break;
                case 200:
                    $('.success-notification').css("display", "block");
                    $('.button-submit').css('display', 'none');
                    $('.button-ok').css('display', 'block');
                    break;
                default:
                // code block
            }
        }
    }).fail(function (err) {
        console.log(err);
    })
    return false;
}

function onlyNumberKey(evt) {
    // Only ASCII charactar in that range allowed
    var ASCIICode = (evt.which) ? evt.which : evt.keyCode

    if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
        return false;

    return true;
}





$(function () {
    $("input[name=PhoneNumber]")[0].oninvalid = function () {
        this.setCustomValidity("The phone number is invalid.");
        alert(CONSTANTS.name);
    };

    $('.button-ok').click(function () {
        RemoveOverplay();
        $('#RegisterForm input').val('');
        EnableTextField()
        $('.success-notification').css("display", "none");
        $("#register-modal").css("display", "none");

    });
});

function RemoveOverplay() {
    let opacity = 1;
    $('.header').css("opacity", opacity);
    $('.product').css("opacity", opacity);
    $('.footer').css("opacity", opacity);
}

function DisableTextField() {
    $('#RegisterForm input').prop("disabled", true);
    $('#RegisterForm .button-submit').prop("disabled", true);
}

function EnableTextField() {
    $('#RegisterForm input').prop("disabled", false);
    $('#RegisterForm .button-submit').prop("disabled", false);
}