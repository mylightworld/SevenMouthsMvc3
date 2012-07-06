function logoVoteToUp(logoId, $parent) {

    $.ajax({
        url: "/LogoVotes/VoteToUp",
        type: "POST",
        data: { id: logoId },
        success: function (data) {
            $("span", $parent).html(data.upCounts);
        }
    });
}

function logoVoteToDown(logoId, $parent) {

    $.ajax({
        url: "/LogoVotes/VoteToDown",
        type: "POST",
        data: { id: logoId },
        success: function (data) {
            $("span", $parent).html(data.downCounts);
        }
    });
}