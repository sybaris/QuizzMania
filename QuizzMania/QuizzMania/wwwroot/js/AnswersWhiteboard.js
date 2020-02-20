"use strict";
//---------------------------------------------------------------
// Ce fichier contient le javascript à destination de la vue AnswersWhiteboard
//---------------------------------------------------------------
$(function () {
    // Par défaut, toutes les réponses sont cachées.
    $(".reponse").toggle();
})

// Click sur le boutton "Afficher les réponses"
$("#btnshow").on("click", function (event) {
    // On change l'état caché/affiché de la réponse
    $(".reponse").toggle();
    // On envoie à tous les utilisateurs le message DisabledButton pour que les utilisateurs ne puissent plus répondre pendant ce temps
    // jusqu'à ce que l'on passe à la question suivante...
    connection.invoke("DisabledButton").catch(function (err) {
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
    // On envoie à tous les utilisateurs le message NextQuestion pour indiquer que l'on va changer de question
    // Chaque utilisateur va pouvoir à nouveau poster des réponses...
    connection.invoke("NextQuestion").catch(function (err) {
        return console.error(err.toString());
    });
    // On a traité l'événement... https://developer.mozilla.org/fr/docs/Web/API/Event/preventDefault
    event.preventDefault();
});