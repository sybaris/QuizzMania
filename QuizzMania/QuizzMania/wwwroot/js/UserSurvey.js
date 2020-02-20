"use strict";
//---------------------------------------------------------------
// Ce fichier contient le javascript à destination de la vue UserSurvey
//---------------------------------------------------------------
$(function () {
    $("#sendButton").attr('disabled', true);
})

var connection = new signalR.HubConnectionBuilder().withUrl("/QuizzHub").build();

connection.start().then(function () {
    $("#sendButton").attr('disabled', false);
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("ReceiveMessage", function (user, id, message) { // Envoi des champs 
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    document.getElementById(id).textContent = message;

    $("#icon" + id).removeClass("fa-times");
    $("#icon" + id).addClass("fa-check");
});

connection.on("ButtonDisabled", function () { // Désactivation du button
    $("#sendButton").attr("disabled", true);
});

connection.on("ButtonNext", function () { // Question suivante
    $("#messageInput").val("");
    $("#sendButton").attr("disabled", false);
});

$("#sendButton").on("click", function (event) {
    var user = $(this).data('firstname');
    var id = $(this).data('id');
    var message = $("#messageInput").val();
    connection.invoke("SendMessage", user, id, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});