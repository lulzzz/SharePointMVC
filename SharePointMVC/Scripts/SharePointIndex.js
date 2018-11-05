$(document).ready(function () {

    console.log("Hello im SharePointIndex.js!!");

});



$("#allLists").on("click", function() {

    console.log("troll");
    
    $.ajax({
        url: "/SharePoint/AllListsPartial",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'html',
        success: function (result) {
            $('#allListsContainer').html(result);
        },
        error: function (error) {
            //Loggade för att se varför jag fick error tidigare
            console.log(error);
        }

    });


});