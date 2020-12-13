var totalPrice = 0;
const FLG_DELETE = 1;
$(function () {
    totalPrice = parseInt($('.total-price').val());
    function initPayPalButton() {
        paypal.Buttons({
            style: {
                shape: 'rect',
                color: 'blue',
                layout: 'horizontal',
                label: 'paypal',
                tagline: true
            },

            createOrder:(data, actions) => {
                return actions.order.create({
                    purchase_units: [{ "amount": { "currency_code": "USD", "value": 3 } }]
                });
            },

            onApprove: function (data, actions) {
                return actions.order.capture().then((details) => {
                    console.log(details);
                    alert('Transaction completed by ' + details.payer.name.given_name + '!');
                });
            },

            onError: function (err) {
                console.log(err);
            }
        }).render('#paypal-button-container');
    }
    initPayPalButton();

    $('.cart_quantity').keyup(debounce(() => {
        var id = $(this).attr('id');
        var value = this.value;
        updateCart(id, value, 0, 1);
    }, 500));

    $('.cart__checkout-button').click(() => {
        $.ajax({
            type: 'Post',
            contentType: false,
            processData: false,
            url: '/Cart/CheckOutCart',
        }).done(function (dataResponse) {
            console.log(dataResponse.statusCode);
            if (dataResponse.statusCode === 200) {

                $('#group-button-payment').css('display', 'block');
            } else if (dataResponse.statusCode === 606) {
                console.log(dataResponse.data);
                var text = 'Check out fail!\n-------------------------------------------------------------------\n';
                dataResponse.data.map((item) => {
                    text += item.status == 1 ? item.productName + 'is out of stock.\n' : item.productName + ' only has ' + item.quantityAvailable + ' item(s).\n';
                });
                text += '==============================================\nPlease re-check your cart and check out again!';
                alert(text);
            } else {
                alert('An error occurred while performing operation');
            }
        }).fail(function (err) {
            console.log(err);
            alert('An error occurred while performing operation');
        })
    });
});

function increment(productId, unitPrice) {
    const quantityNumber = +$('#' + productId).val() +1; 
    $('#' + productId).val(quantityNumber);
    totalPrice += unitPrice;
    $('.amount').text((currencyFormat(totalPrice)));
    updateCart(productId, quantityNumber, 0, 0);
}

function decrement(productId, unitPrice) {
    const quantityNumber = +$('#' + productId).val() - 1;
    if (quantityNumber > 0) {
        $('#' + productId).val(quantityNumber);
        totalPrice -= unitPrice;
        $('.amount').text((currencyFormat(totalPrice)));
        updateCart(productId, quantityNumber, 0, 0);
    }
}

function removeCart(productId) {
    $('.backdrop').addClass("backdrop--open");
    let select = confirm("Do you want to remove this item from your cart!");
    if (select == true) {
        updateCart(productId, 0, FLG_DELETE,1);
    }
}

function updateCart(productId, quantity, status, isReload) {
    $.ajax({
        type: 'Put',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({
            ProductId: productId,
            Quantity: quantity,
            Status: status,
        }),
        url: '/Cart/UpdateItem',
    }).done(function (dataResponse) {
        if (dataResponse.statusCode)
            alert(dataResponse.messageError);
        else if (isReload == 1) {
            $('.cart-container').replaceWith(dataResponse);
        }
    }).fail(function (err) {
        console.log(err);
        alert('An error occurred while performing operation');
    })
}

function currencyFormat(num) {
    return num.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,') + '$'
}

var debounce = (func, wait, immediate) => {
    var timeout;
    return function () {
        var context = this, args = arguments;
        var later = function () {
            timeout = null;
            if (!immediate) func.apply(context, args);
        };
        var callNow = immediate && !timeout;
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
        if (callNow) func.apply(context, args);
    };
};