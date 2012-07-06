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

//添加分享前检查
function validateAddShare() {

    if ($("#title").val().trim() == ""){
        $("#title").addClass("illegal");
        $("#title").focus();
        $(".message").html("标题不允许为空！");
        return false;
    }
    else {
        $("#title").removeClass("illegal");
        $(".message").html("");
    }

    if ($("#reason").val().trim() == "") {
        $("#reason").addClass("illegal");
        $("#reason").focus();
        $(".message").html("分享理由不允许为空！");
        return false;
    }
    else {
        $("#reason").removeClass("illegal");
        $(".message").html("");
    }

    return true;
}