document.onkeydown = checkKey;
var temp = false;
function checkKey(e) {
    e = e || window.event;
    if (e.keyCode == '37') {
	    getCalendar(1);
    } else if (e.keyCode == '39') {
	    getCalendar(2);
    }
}

$('body').delegate('.fas.fa-chart-line', 'click', function() {
    $('.flip-container').addClass("hover");
    $('.chart-line').removeClass('back-hide');
    $('.gantt').addClass('back-hide');
    $('.gantt').removeClass('back');
    $('.chart-line').addClass('back');
})

$('body').delegate('.fas.fa-chart-bar', 'click', function () {
    $('.flip-container').addClass("hover");
    $('.gantt').removeClass('back-hide');
    $('.chart-line').addClass('back-hide');
    $('.chart-line').removeClass('back');
    $('.gantt').addClass('back');
})

$('body').delegate('.far.fa-calendar-alt', 'click', function () {
	$('.flip-container').removeClass("hover");
})