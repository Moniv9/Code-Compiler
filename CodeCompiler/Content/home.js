
$(document).ready(function () {

    var connection = $.connection('/signalr');
    var editor = ace.edit("code-editor");

    connection.received(function (message) {

        if (message === "true")
            console.log("Connected");
        else {
            editor.setValue(message);
        }

    });

    connection.start().done(function () {

        $('#code-editor').keyup(function () {
            var codeValue = editor.getValue();
            connection.send(codeValue);

        });

    });


});