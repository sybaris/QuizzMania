"use strict";
//---------------------------------------------------------------
// Ce fichier contient le javascript disponible sur tout le site.
// Toutes les pages incluront ce script...
//---------------------------------------------------------------
$(document).ready(function () {
    // Initialization des Tooltip - Voir : https://getbootstrap.com/docs/4.0/components/tooltips/
    // Ceci active le style des tooltip via bootstrap
    $('[data-toggle="tooltip"]').tooltip();
})

// variable globale utilisé pour SignalR
var connection = new signalR.HubConnectionBuilder().withUrl("/QuizzHub").build();