function jQueryAxjaxSignUpPost(form) {
    $.ajax({
        type: 'Post',
        contentType: false,
        processData: false,
        url: form.action,
        data: new FormData(form),
    }).done(function (data) {
        $('#modal-register').html(data);
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
});
