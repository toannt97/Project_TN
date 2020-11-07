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