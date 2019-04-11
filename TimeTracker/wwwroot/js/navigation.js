
document.onkeydown = checkKey;

function checkKey(e) {

	e = e || window.event;

	if (e.keyCode == '38') {
		getCalendar(1);
	}
	else if (e.keyCode == '40') {
		getCalendar(2);
	}

}