$(document).ready(function () {
    $('.user__info').click(function () {
        $('.user__menu').addClass('user__menu--opened');
    });

    $(document).mouseup(function (e) {
        var container = $('.user__menu');
        
        if (!container.is(e.target) && container.has(e.target).length === 0) {
            $('.user__menu').removeClass('user__menu--opened');
        } else if (!target.is(e.target) && container.has(e.target).length === 0) {
            $('.search-result__items').css('display','none');

        }
    });
});
