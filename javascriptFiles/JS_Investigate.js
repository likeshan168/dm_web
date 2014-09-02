function move(dir) {
    if (dir == "next") {
        $("#rightControl").click();
        inputType();
    }

    else if (dir == "preview") {
        $("#leftControl").click();
        inputType();
    }

}

function inputType() {
    if ($('#ctl00_cph_rb1').attr('checked') == true || $('#ctl00_cph_rb2').attr('checked') == true) {
        $('#inputTxt').css('display', 'none');
    } else
        $('#inputTxt').css('display', 'block');
}

function delInvValue(object) {
    var id = object.id;
    var x = $('#ctl00_cph_hidDIN');
    x.val = id;
}

function tipsShow() {
    $('#PTIPS').fadeTo("normal", 1, function () {
        $('#PTIPS').fadeOut("fast");
    });
}

function divShow(state) {
    if (state == 1)
        $('#pageContainer').css('display', 'block');
    else
        $('#pageContainer').css('display', 'none');
}
