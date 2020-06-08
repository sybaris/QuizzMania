"use strict";
//---------------------------------------------------------------
// Ce fichier contient le javascript à destination de la vue AnswersWhiteboard
//---------------------------------------------------------------
// Au chargement de la page AnswerWhiteboard
$(function () {
    // Par défaut, toutes les réponses sont cachées.
    $(".reponse").toggle();
})

// On démarre SignalR pour la page AnswersWhiteboard
connection.start().catch(function (err) {
    return console.error(err.toString());
});

// Click sur le boutton "Afficher les réponses"
$("#btnshow").on("click", function (event) {
    // On change l'état caché/affiché de la réponse
    $(".reponse").toggle();
    // On envoie à tous les utilisateurs le message SendAdminDisplayAllAnswers pour que les utilisateurs ne puissent plus répondre pendant ce temps
    // jusqu'à ce que l'on passe à la question suivante...
    connection.invoke("SendAdminDisplayAllAnswers").catch(function (err) {
        return console.error(err.toString());
    });
    // On a traité l'événement... https://developer.mozilla.org/fr/docs/Web/API/Event/preventDefault
    event.preventDefault();
});

// Click sur le boutton "Question suivante"
$("#btn_next").on("click", function (event) {
    // On cache à nouveau toutes les réponses
    $(".reponse").hide();
    // On remet tous les status des réponses à non répondu
    $(".list-icon").removeClass("fa-check");
    $(".list-icon").addClass("fa-times");
    // On vide toutes les réponses
    $(".reponse").each(
        function (index, element) { element.textContent = ''; });
    // On envoie à tous les utilisateurs le message SendAdminNextQuestion pour indiquer que l'on va changer de question
    // Chaque utilisateur va pouvoir à nouveau poster des réponses...
    connection.invoke("SendAdminNextQuestion").catch(function (err) {
        return console.error(err.toString());
    });
    // On a traité l'événement... https://developer.mozilla.org/fr/docs/Web/API/Event/preventDefault
    event.preventDefault();
});

// On recoit de SignalR l'info qu'un utilisateur a envoyé sa réponse
connection.on("ReceiveUserAnswer", function (user, id, reponse) {
    // On enlève quelques caractères qui peuvent poser problème en Html
    var reponseStr = reponse.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    // On met le texte de la réponse
    document.getElementById(id).textContent = reponseStr;
    // On retire l'icone croix rouge pour mettre la coche verte
    $("#icon" + id).removeClass("fa-times");
    $("#icon" + id).addClass("fa-check");
});