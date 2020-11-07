function jQueryAxjaxSignInPost(form) {
    console.log('ajax');

    $.ajax({
        type: 'Post',
        contentType: false,
        processData: false,
        url: form.action,
        data: new FormData(form),
    }).done(function (data) {
        console.log(data);
        $('#modal-login').html(data);
    }).fail(function (err) {
        console.log(err);
    }).always(function () {
        console.log('ajax');
    })
    
    return false;

}