function voteToUp(shareId,$parent) {

    $.ajax({
        url: "/Shares/VoteToUp",
        type: "POST",
        data: { id: shareId },
        success: function (data) {
            $(".upCounts", $parent).html(data.upCounts);
        }
    });
}

function voteToDown(shareId, $parent) {

    $.ajax({
        url: "/Shares/VoteToDown",
        type: "POST",
        data: { id: shareId },
        success: function (data) {
            $(".downCounts", $parent).html(data.downCounts);
        }
    });
}

//添加评论
function submitComment(shareId,parentId,$button) {

    var commentContent = $("#commentContent" + shareId).val();

    $.ajax({
        url: "/Shares/SubmitComment",
        type: "post",
        data: { shareId: shareId, parentId: parentId, commentContent: commentContent },
        success: function (data) {
            //更新评论列表
            $button.parent().parent().parent().parent().html(data);
        }
    });

}