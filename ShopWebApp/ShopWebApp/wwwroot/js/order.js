$(function () {
    //function initPayPalButton() {
    //    paypal.Buttons({
    //        style: {
    //            shape: 'rect',
    //            color: 'blue',
    //            layout: 'horizontal',
    //            label: 'paypal',
    //            tagline: true
    //        },

    //        createOrder: (data, actions) => {

    //            return actions.order.create({
    //                purchase_units: [
    //                    {
    //                        "amount":
    //                        {
    //                            "currency_code": "USD",
    //                            "value": totalPrice,
    //                        },
    //                        "payee": {
    //                            "email_address": "noreply.newstore@gmail.com"
    //                        }
    //                    }
    //                ]
    //            });
    //        },

    //        onApprove: function (data, actions) {
    //            return actions.order.capture().then((details) => {
    //                console.log('details', details);
    //                console.log('data', data)
    //                alert('Transaction completed by ' + details.payer.name.given_name + ' ' + details.payer.name.given_name + '!');
    //            });
    //        },

    //        onError: function (err) {
    //            console.log(err);
    //        }
    //    }).render('#paypal-button-container');
    //}
    //initPayPalButton();

})