
//注册前的验证
function validateReigster() {

    if ($("#emailOrName").val() == "") {
        $(".message").html("邮箱或用户名不允许为空！");
        $("#emailOrName").addClass("illegal");
        $("#emailOrName").focus();
        return false;
    }
    else {

        if ($("#emailOrName").val().indexOf('@') >= 0 && !checkEmail($("#emailOrName").val())) {
            $("#emailOrName").removeClass("illegal");
            $(".message").html("邮箱格式不正确！");
            return false;
        }
        /**********由于异步执行的问题，此处暂时不进行邮箱或用户名是否重名的js异步验证******/
//        $.ajax({
//            url: "/Users/CheckEmailOrName",
//            type: "POST",
//            data: { emailOrName: $("#emailOrName").val() },
//            success: function (data) {
//                if (data.isReduplicate) {
//                    $("#emailOrName").addClass("illegal");
//                    $(".message").val(data.message);
//                    return false;
//                }
//                else {
//                    $("#emailOrName").removeClass("illegal");
//                    $(".message").html("");
//                }
//            }
//        });
    }

    if ($("#password").val().length < 6 || $("#pwdConfirm").val().length < 6) {
        
        if ($("#password").val().length < 6) {
            $("#password").addClass("illegal");
            $("#password").focus();
            $(".message").html("密码长度不能小于6位");
            return false;
        }
        else {
            $("#password").removeClass("illegal");
            $(".message").html("");
        }
        if ($("#pwdConfirm").val().length < 6) {
            $("#pwdConfirm").addClass("illegal");
            $("#pwdConfirm").focus();
            $(".message").html("密码长度不能小于6位");
            return false;
        }
        else {
            $("#pwdConfirm").removeClass("illegal");
            $(".message").html("");
        }
    }
    else {
        if ($("#password").val() != $("#pwdConfirm").val()) {
            $("#pwdConfirm").addClass("illegal");
            $("#pwdConfirm").focus();
            $(".message").html("两次输入的密码不一样！");
            return false;
        }
        else {
            return true;
        }
    }
}