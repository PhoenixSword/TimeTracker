function gantt(dataTasks) {
	google.charts.load('current', { 'packages': ['gantt'] });
    google.charts.setOnLoadCallback(drawChart);

    var arrayChart = new Array();

	function drawChart() {
        var hoursArray = new Object();
		var s = [];
		var data = new google.visualization.DataTable();
		data.addColumn('string', 'Task ID');
		data.addColumn('string', 'Task Name');
		data.addColumn('string', 'Resource');
		data.addColumn('date', 'Start Date');
		data.addColumn('date', 'End Date');
		data.addColumn('number', 'Duration');
		data.addColumn('number', 'Percent Complete');
		data.addColumn('string', 'Dependencies');
        $.each(dataTasks, function (index, value) {
            var max;
            var min;
            var id;
            $.each(value, function (index2, value2) {
	            index2 === 0 ? (id = value2.id) : null;
	            index2 === 0 ? (max = +value2.date) : null;
	            index2 === 0 ? (min = +value2.date) : null;

                if (+value2.date > max)
                    max = value2.date;
                if (+value2.date < min)
                    min = value2.date;
            });
            s.push([
                id, index, index,
                new Date(new Date().getFullYear(), new Date().getMonth(), min), new Date(new Date().getFullYear(), new Date().getMonth(), max), null, 100, null
            ]);
        });
		data.addRows(s);

		var options = {
			height: s.length*33,
			gantt: {
				trackHeight: 30
			}
		};

		var chart = new google.visualization.Gantt(document.getElementById('chart_div'));

		chart.draw(data, options);
	}
}