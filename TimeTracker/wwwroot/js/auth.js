
var date = new Date();
if (!firebase.apps.length) {
	firebase.initializeApp(config);
}
firebase.auth().onAuthStateChanged(function (user) {
	if (user) {
        token = user.ie;
        singin = `<button class="btn my-0 py-0 mx-2 text-white">${user.displayName ? user.displayName : user.email}</button>
                        <button class="btn my-0 py-0 mx-2 text-white" style="height: 26px;" onclick=logOut()>Logout</button>`;
		document.getElementById('name').innerHTML = singin;
	} else {

		window.location.replace("/login");
		singin = `<span class="nav-item">
                            <a class="nav-link text-white" href="/login">Login</a>
                        </span>
                        <span class="nav-item">
                            <a class="nav-link text-white" href="/register">Register</a>
                        </span>`;
		$('#name').html(singin);
	}
});