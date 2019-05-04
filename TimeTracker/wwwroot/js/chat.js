function chat() {

	const hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("/chat", { accessTokenFactory: () => token })
        .configureLogging(signalR.LogLevel.None)
		.build();

	var chatResult = $("#chatroomSection");
	hubConnection.on('Receive', function (message, connectionId) {

		var type = "ext";
		if (connectionId !== username) {
			type = "int";
        }
	
        

        var elem = $(`<p class="${type}"><span class="d-flex"> ${connectionId}</span><span class="d-flex flex-wrap"> ${message}</span></p>`);
		chatResult.append(elem);
        chatResult.scrollTop(chatResult.prop('scrollHeight'));
	});


	hubConnection.on('Notify', function (message) {
        var elem = $(`<p class="notify"> ${message}</p>`);
        chatResult.append(elem);
        chatResult.scrollTop(chatResult.prop('scrollHeight'));
	});

    document.getElementById("sendBtn").addEventListener("click", function (e) {
        if ($('#message').val() !== "") {
		    let message = document.getElementById("message").value;
		    hubConnection.invoke('Send', message, username);
		    $('#message').val("");
	    }
	});

	hubConnection.start();
}

