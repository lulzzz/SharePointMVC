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

function urlCheck() {
    var inputFreeUrl = $("#inputFreeUrl").val();

    if (inputFreeUrl !== "") {
        $("#urlDropDown").prop("disabled", true);
        $("#urlDropDown").css("box-shadow", "inset 0 200px 1px rgba(255, 0, 0)");
        

    } else {
        $("#urlDropDown").prop("disabled", false);
        $("#urlDropDown").css("box-shadow", "inset 0 1px 1px rgba(0, 0, 0, .075)");
    }
}






