
if (!firebase.apps.length) {
    firebase.initializeApp(config);
}

function loginEmail() {
    var form = $('form.login').serializeArray();

    firebase.auth().signInWithEmailAndPassword(form[0].value, form[1].value).then(function (result) {
        window.location.replace("/");
    }).catch(function (error) {
        var errorCode = error.code;
        var errorMessage = error.message;
        $('.alert.alert-danger').html(errorMessage);
        $('.alert.alert-danger').removeAttr("hidden");
    });

   
}
function login(type) {
	var provider = ""
    switch (type) {
    case "Google":
		provider = new firebase.auth.GoogleAuthProvider();
		break;
    case "Facebook":
	    provider = new firebase.auth.FacebookAuthProvider();
	    break;
    case "Microsoft":
        provider = new firebase.auth.OAuthProvider('microsoft.com');
	    break;
    case "Github":
	    provider = new firebase.auth.GithubAuthProvider();
	    break;
    }
    firebase.auth().signInWithPopup(provider).then(function (result) {

	    window.location.replace("/");
	}).catch(function (error) {
        var errorCode = error.code;
        var errorMessage = error.message;
        $('.alert.alert-danger').html(errorMessage);
        $('.alert.alert-danger').removeAttr("hidden");
	});
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

