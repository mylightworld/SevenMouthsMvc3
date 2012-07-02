/***********************************************************/
/*************通用方法**************************************/
/***********************************************************/

//截取一段代码
function cutOutCode(html, startStr, endStr) {

    var startInt;
    var endInt;

    if (startStr == "")
        startInt = 0;
    else
        startInt = html.indexOf(startStr);
    if (endStr == "")
        endInt = html.length;
    else
        endInt = html.indexOf(endStr);

    return html.substring(startInt, endInt);

}

//验证邮箱格式
function checkEmail(email) {
    if (email.length <= 0) {
        return false;
    }
    else {
        var pattern = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(\.[a-zA-Z0-9_-])+/;
        if (pattern.test(email)) {
            return true;
        }
        else {
            return false;
        }
    }
}