$(document).ready(() => {

    var notification = $('.notification');

    notification.children().click(function (e) {
        e.preventDefault();
        notification.attr('style', 'right:-250px');
    });

});
