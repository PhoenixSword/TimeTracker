var element;
var toggleModal = $('#modal');
var modalForm = $('.modal-form');
var spanError = $('.modal-body > span');
var current = ""
var clickDate = "";
var currentDate;

$('body').delegate('.calendar__day.active', 'click',
    function () {
        if (current !== "") {
	        current.attr("id", "");
        }
        current = $(this);
        clickDate = current.children().eq(1).children().eq(0).attr("id");
        current.attr("id", "current");
        var dayOfWeekNumber = weekdays[new Date(new Date().getFullYear(), new Date().getMonth(), clickDate).getDay()];
        if (dayOfWeekNumber == 0) dayOfWeekNumber = 7;
        $('.sidebar__heading').html(`${dayOfWeekNumber}<br>${monthNames[new Date().getMonth()]} ${clickDate}`);
        $.ajax({
	        url: '/api/getTasks',
	        contentType: "application/json; charset=utf-8",
            data: { "date": `${date.getFullYear()}.${date.getMonth() + 1}.${clickDate}` },
	        beforeSend: function (xhr) {
		        xhr.setRequestHeader("Authorization", "Bearer " + token);
	        },
            success: function (data) {
                $(`.sidebar__list`).html(``);
                $.each(data, function (index, value) {
                    var url = value.downloadUrl ? value.downloadUrl : "https://cdn4.iconfinder.com/data/icons/business-ash-gray-set-2/64/b-01-512.png";
                    $(`.sidebar__list`).append(`<li class="sidebar__list-item d-flex"><span class="list-item__time d-flex"><div class="taskImage" style="background: url(${url}) no-repeat; background-size: contain;"></div> ${value.hours} hours</span><span class="taskName" data-tooltip="${value.description}" data-tooltip-position="top">${value.name}<span></li>`);
	            });
            }
        });
	});

	$('body').delegate('.button-edit', 'click', function () {
	    spanError.html("");
	    element = $(this);
	    toggleModal.click();
	    inputDate.val(date.getFullYear() + "-" + ("0" + (date.getMonth() + 1)).slice(-2) + "-" + ("0" + (element.children().eq(0).html())).slice(-2));
	    inputHours.val(element.children().eq(1).children().eq(0).html());
    });
    $('body').delegate('.modal-form button', 'click', function () {
    if (modalForm[0].checkValidity()) {
        var form = modalForm.serializeArray();
        var data = new FormData();
        console.log($('input[type=file]')[0].files[0]);
        data.append("date", form[0].value);
        data.append("name", form[1].value);
        data.append("hours", form[2].value);
        data.append("description", form[3].value);
        data.append("image", $('input[type=file]')[0].files[0]);
        $.ajax({
            url: '/api',
            type: 'POST',
            processData: false,
            contentType: false,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + token);
            },
            success: function (data) {
                if (!data) {
                    console.log(data);
                    var tempDate = new Date(form[0].value).getDate();
                    $(`span [id=${tempDate}]`).html(form[2].value);
                    spanError.html("");
                    toggleModal.click();
                    $(`span [id=${tempDate}]`).parent().parent().removeClass("green deep-orange stylish-color grey").addClass(checkColor(form[2].value));
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
