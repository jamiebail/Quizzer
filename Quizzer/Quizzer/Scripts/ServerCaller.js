$(document).ready(function() {
    function sendAnswer() {
        chat.server.answer($('#team').val(), $('#answer').val());
    }
});