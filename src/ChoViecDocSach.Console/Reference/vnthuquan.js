http: //vnthuquan.net/truyen/truyen.aspx?tid=2qtqv3m3237nnn2ntn1n31n343tq83a3q3m3237nvn

    noidung1('tuaid=3452&chuongid=1');

function noidung1(request) {
    if (url != '') {
        Goi_fun(cho_NOIDUNG_moi("tieude"), khong_gi("fontchu")); // xoa het va co loading sign
        SendQuery('chuonghoi_moi.aspx?', 'Displaylan0("solan"), Displaylan1("tieude"), Displaylan2("fontchu"), Displaylan3("nguon")', 'POST', 0, request);
    }
}

Displaylan0 // assign name with req and split "--!!tach_noi_dung!!--" at element 0


function SendQuery(url, callbackFunction, method, cache, request) {
    Initialize(); // can url va reg o global
    if ((req != null)) {
        req.onreadystatechange = function () {
            // only if req shows "complete"
            if (req.readyState == 4) {
                // only if "OK"
                if (req.status == 200) {
                    // Process
                    eval(callbackFunction); // Displaylan0
                }
            }
        };


        // Cache data or not , default is yes(1)


        if (cache != 1) {
            url += "&rand=" + Math.random() * 1000;

        }

        // Use POST or GET method , default is GET
        if (method == 'POST') {
            req.open("POST", url, true);
            req.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            req.send(request);
        } else {
            req.open("GET", url, true);
            //req.setRequestHeader("Pragma", "no-cache");// xoa cache 
            //req.setRequestHeader("Cache-Control", "must-revalidate");// xoa cache 
            //req.setRequestHeader("If-Modified-Since", document.lastModified);// xoa cache 
            req.send(null);
        }
    }
}


function khong_gi(name) {
    obj = document.getElementById(name);
    obj.innerHTML = '';
} // xoa het

function cho_NOIDUNG_moi(name) {
    obj = document.getElementById(name);
    obj.innerHTML = '<div align="center" class="chochut">chờ Một Chút...</div>';
} // hien len dong thong bao cho

function Goi_fun(callbackFunction) {} //




function Truyendulieu_timkiem() {
    var theoObj = document.getElementById("theo");
    var chuObj = document.getElementById("chu").value;
    if (chuObj.length > 2) {
        thong_so = "theo=";
        thong_so += theoObj.options[theoObj.selectedIndex].value;
        thong_so += "&chu=";
        thong_so += encodeURI(chuObj.split(' ').join('+'));
        Timkiemtruyen_moi('./timkiem_ajax.aspx?', 'DisplayHTML("khungchinh")', 'POST', 0, thong_so);
    }
    return true;
}

function Truyendulieu_diem(data) {
    var diemm = data;
    vnthuquandiem3452 = getCookie('vnthuquandiem3452');
    if (vnthuquandiem3452 != "3452") {
        if (diemm != "0") {
            thong_so = "diem=" + diemm;
            thong_so += "&tuaid=";
            thong_so += "3452";
            ADD_DIEM('chodiemtruyen.aspx?' + thong_so, 'DisplayHTML("diem")');
            vnthuquandiem3452 = "3452";
            setCookie('vnthuquandiem3452', vnthuquandiem3452, 365)
        }

    } else {
        alert("Bạn đã cho điểm rồi!");
    }
    selectObj.selectedIndex = 0;

}

//var win=null;
var firstload = false;

function goi_youtube(url) {
    if (url != '') {
        Cho_chodiem('youtube');
        SendQuery(url, 'DisplayHTML("youtube")');
    }

}

function GOI_PHIM(url) {
    if (url != '') {
        Cho_chodiem('phimdaly');
        SendQuery(url, 'DisplayHTML("phimdaly")');
    }

}

function GOI_PHIMflv(url) {
    if (url != '') {
        Cho_chodiem('phimflv');
        SendQuery(url, 'DisplayHTML("phimflv")');
    }

}

function ADD_DIEM(url) {
    if (url != '') {
        Cho_chodiem('diem');
        SendQuery(url, 'DisplayHTML("diem")');
    }

}

function Timkiem(url) {
    if (url != '') {
        cho_NOIDUNG('khungchinh');
        SendQuery(url, 'DisplayHTML("khungchinh")');
    }

}

function Timkiemtruyen(url, callbackFunction, method, cache, request) {
    if (url != '') {
        cho_NOIDUNG('khungchinh');
        SendQuery(url, callbackFunction, method, cache, request);
    }

}

function Timkiemtruyen_moi(url, callbackFunction, method, cache, request) {
    if (url != '') {
        cho_NOIDUNG_moi('khungchinh');
        SendQuery(url, callbackFunction, method, cache, request);
    }

}

function Timkiem_noidung(url) {
    if (url != '') {
        cho_NOIDUNG('fontchu');
        SendQuery(url, 'DisplayHTML("fontchu")');
    }

}

function Goi_NOIDUNG(url) {
    if (url != '') {
        Goi_fun(cho_NOIDUNG("fontchu"), Cho_chodiem("solan"), Cho_chodiem("diem"));
        SendQuery(url, 'DisplayLANHAI("fontchu"), DisplayLANDAU("solan"), DisplayLANBA("diem")');
    }

}

function ChangeFontSize(size) {
    document.getElementById('fontchu').style.fontSize = size + "px";
}

function doitrang(data) {
    document.location.href = "theloai.aspx?theloaiid=" + data;
}


function waitPreloadPage() { //DOM
    var prep;
    var fs;
    if (document.getElementById) {
        prep = document.getElementById('prepage');
        if (prep) prep = prep.style;
        fs = document.getElementById('FontSize');
        if (fs) fs = fs.style;
    } else {
        if (document.layers) { //NS4
            //document.prepage.visibility = 'hidden';
            prep = document.prepage;
            fs = document.FontSize
        } else { //IE4
            prep = document.all.prepage; //.style.visibility = 'hidden';
            if (prep) prep = prep.style;
            fs = document.all.FontSize;
            if (fs) fs = fs.style;
        }
    }
    if (prep) prep.visibility = 'hidden';
    if (fs) fs.display = '';
}

function openWindow1(url) {
    popupWin = window.open(url, '_blank', 'width=660,height=560,scrollbars=no')
}

function getCookie(NameOfCookie) {
    if (document.cookie.length > 0) {
        begin = document.cookie.indexOf(NameOfCookie + "=");
        if (begin != -1) {
            begin += NameOfCookie.length + 1;
            end = document.cookie.indexOf(";", begin);
            if (end == -1) end = document.cookie.length;
            return unescape(document.cookie.substring(begin, end));
        }
    }
    return null;
}

function setCookie(NameOfCookie, value, expiredays) {
    var ExpireDate = new Date();
    ExpireDate.setTime(ExpireDate.getTime() + (expiredays * 24 * 3600 * 1000));

    document.cookie = NameOfCookie + "=" + escape(value) +
        ((expiredays == null) ? "" : "; expires=" + ExpireDate.toGMTString());
}

function delCookie(NameOfCookie) {
    if (getCookie(NameOfCookie)) {
        document.cookie = NameOfCookie + "=" +
            "; expires=Thu, 01-Jan-70 00:00:01 GMT";
    }
}

function mobilCookieStuff() {
    m = getCookie('m');
    mobile = (/iphone|ipad|ipod|android|blackberry|mini|windows\sce|palm/i.test(navigator.userAgent.toLowerCase()));
    if (m != null) {} else {

        if (mobile) {
            box = window.confirm("Bạn có muốn vào trang truyện dành cho Mobil không? (nếu màn hình của bạn từ 8 inch trở xuống thì nên sử dụng) ")
            if (box == true) {
                location.replace("./mobil/");
            } else if (box == false) {}

        } else {}
        m = "mobil";
        setCookie('m', m, 365)
    }
}





function cho_NOIDUNG(name) {
    obj = document.getElementById(name);
    obj.innerHTML = '<center><br><br><br><br><br><img src="../images/choooo.gif"><br><span class="buttonblue">Cho Chut...</span></center><br>';
}


function Cho_chodiemanh(name) {
    obj = document.getElementById(name);
    obj.innerHTML = 'cho chut ....';
}

function Cho_chodiem(name) {
    obj = document.getElementById(name);
    obj.innerHTML = '<center><img src="../images/choooo.gif"></center>';
}



function DisplayHTML(name) {
    obj = document.getElementById(name);
    obj.innerHTML = req.responseText;

}

function DisplayLANDAU(name) {
    obj = document.getElementById(name);
    var nhanve_s = req.responseText;
    var chiahai_s = nhanve_s.split("--!!tach_noi_dung!!--");
    obj.innerHTML = chiahai_s[0];

}

function DisplayLANHAI(name) {
    obj = document.getElementById(name);
    var nhanve_t = req.responseText;
    var chiahai_t = nhanve_t.split("--!!tach_noi_dung!!--");
    var bai = chiahai_t[1];
    obj.innerHTML = bai.split('.<br>').join('.<z>').split('!<br>').join('!<z>').split(':<br>').join(':<z>').split('?<br>').join('?<z>').split('"<br>').join('"<z>').split('. <br>').join('.<z>').split('! <br>').join('!<z>').split(': <br>').join(':<z>').split('? <br>').join('?<z>').split('" <br>').join('"<z>').split('.<BR>').join('.<z>').split('!<BR>').join('!<z>').split(':<BR>').join(':<z>').split('?<BR>').join('?<z>').split('"<BR>').join('"<z>').split('. <BR>').join('.<z>').split('! <BR>').join('!<z>').split(': <BR>').join(':<z>').split('? <BR>').join('?<z>').split('" <BR>').join('"<z>').split('.<BR>').join('.<z>').split('!<BR>').join('!<z>').split(':<BR>').join(':<z>').split('?<BR>').join('?<z>').split('"<BR>').join('"<z>').split('.  <BR>').join('.<z>').split('!  <BR>').join('!<z>').split(':  <BR>').join(':<z>').split('?  <BR>').join('?<z>').split('"  <BR>').join('"<z>').split('<BR>').join(' ').split('<br>').join(' ').split('<z>').join('<BR>');
}

function DisplayLANBA(name) {
    obj = document.getElementById(name);
    var nhanve_b = req.responseText;
    var chiahai_b = nhanve_b.split("--!!tach_noi_dung!!--");
    obj.innerHTML = chiahai_b[2];
}

