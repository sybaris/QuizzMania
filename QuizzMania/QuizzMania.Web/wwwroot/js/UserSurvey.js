"use strict";
//---------------------------------------------------------------
// Ce fichier contient le javascript à destination de la vue UserSurvey
//---------------------------------------------------------------
$(function () {
    $("#sendUserAnswerButton").attr('disabled', true);
})

// On démarre SignalR pour la page UserSurvey
connection.start().then(function () {
    $("#sendUserAnswerButton").attr('disabled', false);
}).catch(function (err) {
    return console.error(err.toString());
});

// On recoit de SignalR l'info que l'admin affiche les réponses
connection.on("ReceiveAdminDisplayAllAnswers", function () {
    // Désactivation du button pour que l'utilisateur n'envoie plus de réponses
    $("#sendUserAnswerButton").attr("disabled", true);
});

// On recoit de SignalR l'info que l'admin passe à la question suivante
connection.on("ReceiveAdminNextQuestion", function () { 
    // On vide l'input pour que l'utilisateur puisse proposer une autre réponse
    $("#userAnswerInput").val("");
    // On réactive le boutton de soumission de la réponse
    $("#sendUserAnswerButton").attr("disabled", false);
});

// Click sur le boutton "Soumettre une réponse" d'un utilisateur
$("#sendUserAnswerButton").on("click", function (event) {
    // On récupère le nom de l'utilisateur
    var user = $(this).data('firstname');
    // On récupère l'id de l'utilisateur
    var id = $(this).data('id');
    // On récupère la réponse de l'utilisateur qu'il faut envoyer
    var reponse = $("#userAnswerInput").val();
    // On envoie la réponse au Hub
    connection.invoke("SendUserAnswer", user, id, reponse).catch(function (err) {
        return console.error(err.toString());
    });
    // On a traité l'événement... https://developer.mozilla.org/fr/docs/Web/API/Event/preventDefault
    event.preventDefault();
});