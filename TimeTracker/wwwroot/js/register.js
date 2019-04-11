
if (!firebase.apps.length) {
    firebase.initializeApp(config);
}
function regWithPassword() {
    var form = $('form.register').serializeArray();
    if (form[1].value === form[2].value) {
	    firebase.auth().createUserWithEmailAndPassword(form[0].value, form[1].value).then(function() {
		    window.location.replace("/");
	    }).catch(function(error) {
		    var errorMessage = error.message;
		    $('.alert.alert-danger').html(errorMessage);
		    $('.alert.alert-danger').removeAttr("hidden");
	    });
    } else {
        $('.alert.alert-danger').html("Passwords do not match");
        $('.alert.alert-danger').removeAttr("hidden");
    }
}

var singin = "";
firebase.auth().onAuthStateChanged(function (user) {
    if (user) {
        singin = `<button class="btn my-0 py-0 mx-2 text-white">${user.email}</button>
                        <button class="btn my-0 py-0 mx-2 text-white" style="height: 26px;" onclick=logOut()>Logout</button>`;
        $('#name').html(singin);
    } else {
        singin = `<span class="nav-item">
                                <a class="nav-link text-white" href="/login">Login</a>
                            </span>
                            <span class="nav-item">
                                <a class="nav-link text-white" href="/register">Register</a>
                            </span>`;
        $('#name').html(singin);
    }
});