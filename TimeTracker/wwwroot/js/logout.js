function logOut() {
	firebase.auth().signOut().then(function() {
		singin = `<span class="nav-item">
                            <a class="nav-link text-white" href="/login">Login</a>
                        </span>
                        <span class="nav-item">
                            <a class="nav-link text-white" href="/register">Register</a>
                        </span>`;
		document.getElementById('name').innerHTML = singin;

		window.location.replace("/login");
	});
}