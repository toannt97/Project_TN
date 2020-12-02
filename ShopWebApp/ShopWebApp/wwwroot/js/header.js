$(function () {
    $('.user__item--sign-out').click(() => {
        $.ajax({
            type: 'Post',
            contentType: false,
            processData: false,
            url: '/User/SignOut',
        }).done(function (data) {
                switch (data.statusCode) {
                    case 500: {
                        alert('An error occurred while performing operation');  
                        console.log(err.responseText);
                        break;
                    }
                    case 200: {
                        UserLogined(data);
                        break;
                    }
                }
        }).fail(function (err) {
            alert('An error occurred while performing operation');
            console.log(err.responseText);
        })
    });
});