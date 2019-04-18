$('#test').hide(100);
var token = "";
var inputImage = $('.modal-body').children().children().eq(0);
var inputDate = $('.modal-body').children().children().eq(1);
var inputName = $('.modal-body').children().children().eq(3);
var inputHours = $('.modal-body').children().children().eq(6);
var inputDescription = $('.modal-body').children().children().eq(8);
var inputId = $('.modal-body').children().children().eq(10);
var element;
var toggleModal = $('#modal');
var modalForm = $('.modal-form');
var spanError = $('.alert.alert-danger');
var clickDate = "";
var currentDate;
var saveButton = $('.modal-footer > button');
var sum = 0;
var images = new Object();
var prevHours;
var arrayTest = [];

const monthNames = [
	"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November",
	"December"
];
const weekdays = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"]

var date = new Date();
var current = "";
