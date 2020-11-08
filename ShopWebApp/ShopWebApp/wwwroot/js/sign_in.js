function jQueryAxjaxSignInPost(form) {
    $.ajax({
        type: 'Post',
        contentType: false,
        processData: false,
        url: form.action,
        data: new FormData(form),
    }).done(function (data) {
        $('#modal-login').html(data);
    }).fail(function (err) {
    })
    return false;
}
