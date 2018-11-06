$("#allLists").on("click", function() {

   
    
    $.ajax({
        url: "/SharePoint/PartialAllLists",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'html',
        success: function (result) {
            $('#partialContentContainer').html(result);
        },
        error: function (error) {
            //Loggade för att se varför jag fick error tidigare
            console.log(error);
        }

    });


});

$("#webTitle").on("click", function() {

    $.ajax({
        url: "/SharePoint/PartialWebTitle",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function(result) {
            $("#partialContentContainer").html(result);
        },
        error: function(error) {
            console.log(error);
        }
    });

});