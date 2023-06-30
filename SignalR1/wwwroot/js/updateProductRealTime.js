// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/updateProduct").build();

connection.on("UpdateTotalViews", function (value) {
    
    var newCountSpan = document.getElementById("totalViewsCounter");
    newCountSpan.innerText = value.toString();
});

connection.on("UpdateTotalUsers", function (value) {

    var newCountSpan = document.getElementById("totalUsersCounter");
    newCountSpan.innerText = value.toString();
});


connection.on("UpdatedProduct", function (total) {

    var InStockElement = document.getElementById("ProductItem_UnitsInStock");
    var instockValue = InStockElement.value;

    InStockElement.value = instockValue - total;

    var x = document.getElementById("snackbar");

    // Add the "show" class to DIV
    x.className = "show";

    // After 3 seconds, remove the show class from DIV
    x.innerText = "Hiện tại sản phẩm còn: " + total;
    setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);

});


function newWindownLoadedOnClient() {
    connection.send("NewWindowLoaded");
    connection.send("UpdateProduct");
    connection.send("DeleteProduct");
}

function fulfilled() {
    console.log("Connection to Product Successfully");
    newWindownLoadedOnClient();
}

function rejected() {

} 

connection.start().then(fulfilled,rejected);