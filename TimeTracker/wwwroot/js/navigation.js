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
})

$('body').delegate('.far.fa-calendar-alt', 'click', function () {
    $('.flip-container').removeClass("hover");
})
