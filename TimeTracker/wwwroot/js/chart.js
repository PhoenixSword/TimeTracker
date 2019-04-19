var dateArray = [];
var dayofMonth = new Date(date.getFullYear(), date.getMonth() + 1, 0).getDate();
for (var i = 0; i < dayofMonth; i++) {
	dateArray.push(i + 1);
}

firebase.auth().onAuthStateChanged(function (user) {
	if (user) {
        token = user.ie;
        getInfo(token);
	}
});

function getRandomColor() {
	var letters = '0123456789ABCDEF'.split('');
	var color = '#';
	for (var i = 0; i < 6; i++) {
		color += letters[Math.floor(Math.random() * 16)];
	}
	return color;
}

function getInfo() {
        var arrayChart = new Array();
        var test = [];
        var hours = [];
		$.ajax({
			url: '/api/getInfo',
			type: 'GET',
            data: { date: ("0" + (date.getMonth() + 1)).slice(-2) + "." + date.getFullYear() },
			beforeSend: function (xhr) {
				xhr.setRequestHeader("Authorization", "Bearer " + token);
			},
            success: function (data) {
	            gantt(data);
                $.each(data, function (index, value) {
	                var hours = [];
	                for (var i = 0; i < dayofMonth; i++) {
		                hours.push(0);
	                }
                    $.each(value, function (index2, value2) {
                        hours[value2.date-1] = +value2.hours;
                    });
                    arrayChart.push(
                    {
	                    data: hours,
                        label: index,
                        borderColor: getRandomColor() ,
	                    fill: false,
                        //hidden: true,
                        pointRadius: 6,
	                    pointHoverRadius: 8
                        });
                });
                var titleChart = "." + ('0' + (new Date().getMonth() + 1)).slice(-2) + "." + new Date().getFullYear();
                new Chart(document.getElementById("myChart"),
	            {
		            type: 'line',
		            data: {
			            labels: dateArray,
			            datasets: arrayChart
		            },
		            options: {
			            title: {
				            display: true,
				            text: 'Tasks in this month'
                        },
                        tooltips: {
                            callbacks: {
                                label: function (tooltipItem, data) {
                                    var label = data.datasets[tooltipItem.datasetIndex].label || '';

			                        if (label) {
				                        label += ': ';
			                        }
			                        label += Math.round(tooltipItem.yLabel * 100) / 100 + " hours";
			                        return label;
                                },
                                title: function (tooltipItem, data) {
                                    var title = tooltipItem[0] ? ('0' + (tooltipItem[0].index + 1)).slice(-2) : '';

                                    if (title) {
                                        title += titleChart;
	                                }
                                    return title;
                                },

	                        }
			            }
			                
		            }
	            });            

			}
		})
	}

