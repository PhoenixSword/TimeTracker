
function checkColor(hours) {
    if ((hours > 0) && (hours <= 4)) {
        return "green";
    }
    else if (hours > 8) {
        return "deep-orange";
    }
    else if ((hours > 4) && (hours <= 8))
        return "stylish-color";
    return "";
}



var calendar = "";
var singin = "";


singin = "";
var date = new Date();
var today = date.getDate();
const monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
const weekdays = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"]

var month = monthNames[date.getMonth()];
var year = date.getFullYear();
var days = new Date(year, date.getMonth() + 1, 0).getDate();
var dayOfWeekNumber = new Date(date.getFullYear(), date.getMonth(), 1).getDay();
var prevMonth = new Date(year, date.getMonth(), 0).getDate();
var dayOfWeekName = weekdays[date.getDay()];

if (dayOfWeekNumber == 0) dayOfWeekNumber = 7;
calendar = '<section class="calendar__week">';
for (var k = prevMonth - dayOfWeekNumber + 2; k <= prevMonth; k++) {
    calendar += `<div class="calendar__day inactive"><span class="calendar__date">${k}</span></div>`;
}
for (var k = 1; k <= 7 - dayOfWeekNumber + 1; k++) {
    if (k == today) {
        calendar +=
            `<div class="calendar__day active today ${checkColor(hours)}">
                        <span class="calendar__date">${k}</span>
                            <span class="calendar__task calendar__task--today">
                                <span id="${k}">0</span>  <span>hours</span>
                            </span>
                        </div>`;
    }
    else {
        calendar +=
            `<div class="calendar__day active ${checkColor(hours)}">
                        <span class="calendar__date">${k}</span>
                        <span class="calendar__task"><span id="${k}">0</span>
                            <span>hours</span></span>
                    </div>`;
    }
}

calendar += `</section><section class="calendar__week">`;


for (var i = 0, j = 7 - dayOfWeekNumber + 2; i < 35; i++ , j++) {
	var tempDate = new Date(date.getFullYear(), date.getMonth(), 7 - dayOfWeekNumber + 2);
    var sectionDate = new Date(tempDate.setDate(j)).getDate();
    if (i % 7 == 0 && i > 0) {
        calendar += `</section><section class="calendar__week">`;
    }
    if (j > days) {
        calendar += `
                    <div class="calendar__day inactive">
                        <span class="calendar__date">${sectionDate}</span>
                    </div>`;
    }
    else {
        if (sectionDate == today) {
            calendar += `
                        <div class="calendar__day active today ${checkColor(hours)}">
                            <span class="calendar__date">${sectionDate}</span>
                            <span class="calendar__task calendar__task--today"><span id="${sectionDate}">0</span> <span>hours</span></span>
                        </div>`;
        }
        else {
            calendar += `
                        <div class="calendar__day active ${checkColor(hours)}" >
                            <span class="calendar__date">${sectionDate}</span>
                            <span class="calendar__task"><span id="${sectionDate}">0</span> <span>hours</span></span>
                        </div>`;
        }
    }
}
calendar += '</section>';
$('.calendar__days').append(calendar);
$('.sidebar__heading').html(`${dayOfWeekName}<br>${month} ${today}`);
$('.title-bar__year').html(`Calendar > ${month} ${year}`);
   