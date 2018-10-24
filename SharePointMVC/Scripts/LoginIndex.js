$(document).ready(function () {

    console.log("Hello im LoginIndex.js!!");

});

function emailCheck() {
   
    var email = $("#inputEmail").val();
    var password = $("#inputPassword").val();

    if (email !== "" && password !== "" ) {
        $("#loginBtn").prop("disabled", false);
    } else {
        $("#loginBtn").prop("disabled", true);
    }
}





