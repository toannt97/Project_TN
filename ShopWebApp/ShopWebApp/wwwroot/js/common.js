$(function () {
    $.ajax({
        type: 'Get',
        contentType: false,
        processData: false,
        url: '/Cart/GetNumOfCartItems',
    }).done(function (data) {
        $('.cart-count').text(data.Quantity);
    }).fail(function (err) {
        alert('An error occurred while performing operation');
        console.log(err.responseText);
    })
});