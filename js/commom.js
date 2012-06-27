
function voteToUp(shareId) {

    $.ajax({
        url: "/Shares/Up",
        type: "POST",
        data: { id: shareId },
        success: function (data) {
           
        }
    });
}

function voteToDown(shareId) { 
    
}