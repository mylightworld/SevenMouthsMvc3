function voteToUp(shareId) {

    $.ajax({
        url: "/Shares/VoteToUp",
        type: "POST",
        data: { id: shareId },
        success: function (data) {

        }
    });
}

function voteToDown(shareId) {

    $.ajax({
        url: "/Shares/VoteToDown",
        type: "POST",
        data: { id: shareId },
        success: function (data) {

        }
    });
}

//添加评论
function submitComment(shareId,parentId) {

    var commentContent = $("commentContent" + shareId).val();
    $.ajax({
        url: "/Shares/VoteToUp",
        type: "POST",
        data: { shareId: shareId,parentId:parentId,commentContent:commentContent },
        success: function (data) {
            
        }
    });

}