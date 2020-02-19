"use strict";
$(function () {
    $(".reponse").toggle();
})

$("#btnshow").on("click", function (event) {
    $(".reponse").toggle();
    connection.invoke("DisabledButton").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

$("#btn_next").on("click", function (event) {
    $(".reponse").hide();
    $(".list-icon").removeClass("fa-check");
    $(".list-icon").addClass("fa-times");
    connection.invoke("NextQuestion").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});