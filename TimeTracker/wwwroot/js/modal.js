var element;
var toggleModal = $('#modal');
var inputDate = $('.modal-body').children().children().eq(0);
var inputHours = $('.modal-body').children().children().eq(2);
var modalForm = $('.modal-form');
var spanError = $('.modal-body > span');

$('body').delegate('.calendar__day.active', 'click', function () {
	spanError.html("");
	element = $(this);
	toggleModal.click();
	inputDate.val(date.getFullYear() + "-" + ("0" + (date.getMonth() + 1)).slice(-2) + "-" + ("0" + (element.children().eq(0).html())).slice(-2));
	inputHours.val(element.children().eq(1).children().eq(0).html());
});
$('body').delegate('.modal-form button', 'click', function () {
	if (modalForm[0].checkValidity()) {
		var form = modalForm.serializeArray();
		$.ajax({
			url: '/api',
			type: 'POST',
			contentType: "application/json; charset=utf-8",
			data: JSON.stringify({ "date": form[0].value, "hours": form[1].value }),
			beforeSend: function (xhr) {
				xhr.setRequestHeader("Authorization", "Bearer " + token);
			},
			success: function (data) {
                if (!data) {
                    var tempDate = new Date(form[0].value).getDate();
                    $(`span [id=${tempDate}]`).html(form[1].value);
					spanError.html("");
					toggleModal.click();
					$(`span [id=${tempDate}]`).parent().parent().removeClass("green deep-orange stylish-color grey").addClass(checkColor(form[1].value));
                } else {
                    if (data.length > 1) {
	                    spanError.html(data[0] + "<br/>" + data[1]);
                    } else {
	                    spanError.html(data);
                    }
					
				}
			}
		});
	}
})

