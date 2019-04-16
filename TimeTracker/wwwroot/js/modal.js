var element;
var toggleModal = $('#modal');
var modalForm = $('.modal-form');
var spanError = $('.alert.alert-danger');
var current = ""
var clickDate = "";
var currentDate;
var saveButton = $('.modal-footer > button');
var sum = 0;
var images = new Object();
var prevHours;

$('body').delegate('.calendar__day.active', 'click', function() {
	getTasks($(this));
});

$('body').delegate('.sidebar__list-item', 'click', function () {
    Edit($(this));
});


function getTasks(element) {
	if (current !== "") {
		current.attr("id", "");
    }
    if (element !== undefined) {
	    current = element;
	    clickDate = current.children().eq(1).children().eq(0).attr("id");
	    current.attr("id", "current");
	    var dayOfWeekNumber = weekdays[new Date(new Date().getFullYear(), new Date().getMonth(), clickDate).getDay()];
	    if (dayOfWeekNumber == 0) dayOfWeekNumber = 7;
	    $('.sidebar__heading').html(`${dayOfWeekNumber}<br>${monthNames[new Date().getMonth()]} ${clickDate}`);
    }
	$.ajax({
		url: '/api/getTasks',
		contentType: "application/json; charset=utf-8",
		data: { "date": `${date.getFullYear()}.${date.getMonth() + 1}.${clickDate}` },
		beforeSend: function(xhr) {
			xhr.setRequestHeader("Authorization", "Bearer " + token);
		},
		success: function(data) {
			$(`.sidebar__list`).html(``);
            sum = 0;

            images = new Object();
			$.each(data,
				function(index, value) {
					//var url = value.downloadUrl ? value.downloadUrl : "/task.png";
                    //console.log(value.name);
                    //console.log(value.downloadUrl);
                    images[value.name] = value.downloadUrl;

					sum += value.hours;
					var url = "./task.png";
					$(`.sidebar__list`).append(
						`<a><li class="sidebar__list-item d-flex"><span class="list-item__time d-flex"><div class="taskImage" style="background: url(${
                        url}) no-repeat; background-size: contain;"></div><span>${value.hours}</span> <span> hours</span></span><span class="taskName" data-tooltip="${value.description
                        }" data-tooltip-position="top">${value.name}<span></li></a>`);
                });
            //console.log(images);
		}
	});
}

$("body").delegate('input[type=file]', 'change', function () {
    $('div[class=imagePreview]').css("background", `transparent`);
    if (this.files[0]) {
        var fr = new FileReader();

        var width = 0;
        fr.onload = function () {
	        var image = new Image();

	        image.src = fr.result;

            image.onload = function () {
                width = image.width;
                height = image.height;
                $('.example-1 i').css("display", "none");
                $('.example-1 span').css("display", "none");
                $('div.imagePreview').css("background", `url(${fr.result}) no-repeat`);
                $('div.imagePreview').css("background-size", "contain");
                $('div.imagePreview').css("height", width > 394 ? (height / (width / 394)) : height);
                
                $('div.imagePreview').css("width", width > 394 ? 394 : width);
                $('div.imagePreview').addClass("mx-auto");
	        };
        };
	    fr.readAsDataURL(this.files[0]);
    } else {
	    $('.example-1 i').css("display", "block");
        $('.example-1 span').css("display", "block");
        $('div.imagePreview').css("height", 0);
        $('div.imagePreview').css("width", 0);
        $('div.imagePreview').css("background", `transparent`);
    }
	});

$('body').delegate('.button-edit', 'click', function() {
	Edit();
});

function Edit(element) {
    if (element !== undefined) {
	    spanError.html("");
	    $('.alert.alert-danger').attr("hidden", true);
        inputHours.val(element.children().eq(0).children().eq(1).html());
        prevHours = element.children().eq(0).children().eq(1).html();
        inputName.val(element.children().eq(1).text());
        inputHours.attr("max", 24 - sum + +element.children().eq(0).children().eq(1).html());
        inputDescription.val(element.children().eq(1).attr("data-tooltip"));
        if (images[inputName.val()] !== '') {

	        var img = new Image();
	        img.onload = function() {
		        var width = this.width;
		        var height = this.height;
		        $('.example-1 i').css("display", "none");
		        $('.example-1 span').css("display", "none");
		        $('div.imagePreview').css("background", `url(${images[inputName.val()]}) no-repeat`);
		        $('div.imagePreview').css("background-size", "contain");
		        $('div.imagePreview').css("height", width > 394 ? (height / (width / 394)) : height);

		        $('div.imagePreview').css("width", width > 394 ? 394 : width);
		        $('div.imagePreview').addClass("mx-auto");
	        }
	        img.src = images[inputName.val()];

        } else {
	        $('.example-1 i').css("display", "block");
	        $('.example-1 span').css("display", "block");
	        $('div.imagePreview').css("height", 0);
	        $('div.imagePreview').css("width", 0);
	        $('div.imagePreview').css("background", `transparent`);
        }
        
    } else {
	    $('.example-1 i').css("display", "block");
	    $('.example-1 span').css("display", "block");
	    $('div.imagePreview').css("height", 0);
	    $('div.imagePreview').css("width", 0);
	    $('div.imagePreview').css("background", `transparent`);
	    if (sum >= 24) {
            spanError.html("HOURS>=24");
            $('.alert.alert-danger').removeAttr("hidden");
	    } else {
            spanError.html("");
            $('.alert.alert-danger').attr("hidden", true);
	    }
	    inputHours.val("");
	    inputName.val("");
        inputDescription.val("");
        inputHours.attr("max", 24 - sum);
    }

    inputDate.val(date.getFullYear() + "-" + ("0" + (date.getMonth() + 1)).slice(-2) + "-" + ("0" + (current.children().eq(0).html())).slice(-2));
    
    toggleModal.click();
    
}

$('body').delegate('.modal-form button', 'click', function() {
	if (modalForm[0].checkValidity()) {
		var form = modalForm.serializeArray();
		var data = new FormData();
		data.append("date", form[0].value);
		data.append("name", form[1].value);
		data.append("hours", form[2].value);
        data.append("description", form[3].value);
        if ($('input[type=file]')[0].files.length !== 0) {
	        data.append("image", $('input[type=file]')[0].files[0]);
        }
        else if (images[form[1].value]) {
            data.append("downloadUrl", images[form[1].value]);
        } 
		$.ajax({
			url: '/api',
			type: 'POST',
			processData: false,
			contentType: false,
			data: data,
			beforeSend: function(xhr) {
				xhr.setRequestHeader("Authorization", "Bearer " + token);
				saveButton.attr("disabled", true);
				saveButton.html(
					`<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...`);
			},
			success: function(data) {
                if (!data) {
                    var tempDate = new Date(form[0].value).getDate();
                    $(`span [id=${tempDate}]`).html(+sum - +prevHours + +form[2].value);
					spanError.html("");
					$(`span [id=${tempDate}]`).parent().parent().removeClass("green deep-orange stylish-color grey")
						.addClass(checkColor(form[2].value));
					setTimeout(function() {
							toggleModal.click();
							getTasks();
							saveButton.attr("disabled", false);
							saveButton.html("Save");
						},
						1000);

                } else {
					saveButton.attr("disabled", false);
					saveButton.html("Save");
                    if (data.length > 1) {
	                    $('.alert.alert-danger').removeAttr("hidden");
						spanError.html(data[0] + "<br/>" + data[1]);
                    } else {
	                    $('.alert.alert-danger').removeAttr("hidden");
						spanError.html(data);
					}

				}
			}
		});
	}
});