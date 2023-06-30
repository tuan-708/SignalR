// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
"use strict";


var connection = new signalR.HubConnectionBuilder().withUrl("/deleteProduct").build();


connection.on("DeletedProduct", function (pid) {
    var row = document.getElementById("row-" + pid);
    row.remove();

    var x = document.getElementById("snackbar");

    // Add the "show" class to DIV
    x.className = "show";

    // After 3 seconds, remove the show class from DIV
    x.innerText = "Đã xóa Product ID: " + pid;
    setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
});


function newWindownLoadedOnClient() {
    connection.send("DeleteProduct");
}

function fulfilled() {
    console.log("Connection to Product Successfully");
    newWindownLoadedOnClient();
}

function rejected() {

} 

connection.start().then(fulfilled,rejected);