$(function () {
    $('.product-detail__button-add-cart').click(() => {
        if ($('.user-name').html() === '') {
            $("#btn-login").trigger("click");
        } else {
            $.ajax({
                type: 'post',
                contentType: 'application/json; charset=UTF-8',
                data: JSON.stringify({
                    'ProductId': $('.product-detail__id').text(),
                }),
                //beforesend: () => {
                //    $('.user-reset__email-address').prop('disabled', true);
                //    $('.user-reset__button-send').prop('disabled', true);
                //},
                url: '/cart/addtocart',
            }).done(function (data) {
                alert(data.messageError);
                if (data.statusCode == 200) {
                    var cartItemsCurrent = parseInt($('.cart-count').text());
                    $('.cart-count').text(cartItemsCurrent +1);
                } else if (data.statusCode == 401) {
                    $("#btn-login").trigger("click");
                }
            }).fail(function (err) {
                alert('an error occurred while performing operation');
                console.log(err.responsetext);
            })
        }
    });
});
