$(function () {
    function initPayPalButton() {
        paypal.Buttons({
            style: {
                shape: 'rect',
                color: 'blue',
                layout: 'horizontal',
                label: 'paypal',
                tagline: true
            },

            createOrder: function (data, actions) {
                return actions.order.create({
                    purchase_units: [{ "amount": { "currency_code": "USD", "value": 1 } }]
                });
            },

            onApprove: function (data, actions) {
                return actions.order.capture().then(function (details) {
                    alert('Transaction completed by ' + details.payer.name.given_name + '!');
                });
            },

            onError: function (err) {
                console.log(err);
            }
        }).render('#paypal-button-container');
    }
    initPayPalButton();
});
