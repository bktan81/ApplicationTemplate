function ShowLoadingX() {
    $.blockUI({ timeout: 60000, message: '<h1><img src="../Content/Images/ajax-loader.gif" /> </h1>', css: { border: '1px solid Aqua', height: '75px', 'width': '120px', top: '40%', left: '45%' } });
}

var codeT;

function formatNumber1(num) {
    return num.toString().replace(/,/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",")
}
function formatNumber2(num, decPoints) {
    var num2 = RoundNumber(num, decPoints);
    var num3 = num2.toFixed(decPoints);
    return num3.replace(/\B(?=(\d{3})+(?!\d))/g, ",")
}
function getGuidS4() {
    return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
}
function getGuid8()
{
    var guid = (getGuidS4() + getGuidS4() + getGuidS4() + getGuidS4()).toLowerCase();
    return guid;
}
var langCurrent;
function getLValue(codeName) {
    var langArrayName = "lang_EN.";    
    if (langCurrent == "zh-CN") {
        langArrayName = "lang_CN.";
    } //add more language options here if any

    var retStr = eval(langArrayName + codeName);
    if (typeof retStr != 'undefined') {
        return retStr;
    }
    return "";
}
function getAppCodeText(inputCode) {
    var msgContent = "";
    var arrCode = inputCode.split("|");
    if (arrCode.length == 1) {
        msgContent = "<p style='margin:5px'>" + getLValue(arrCode[0]) + "</p>";
        //alert(getLValue(arrStatus[0]));
    }
    else if (arrCode.length > 1) {
        for (i = 0; i < arrCode.length; i++) {
            msgContent += "<li>" + getLValue(arrCode[i]) + "</li>";
        }
        msgContent = "<ul>" + msgContent + "</ul>";
    }

    return msgContent;
}
function getAppCodeText2(vText) {
    var msgContent = "";
    var arrCode = vText.split("|");
    if (arrCode.length == 1) {
        msgContent = "<p style='margin:5px'>" + arrCode[0] + "</p>";
        //alert(getLValue(arrStatus[0]));
    }
    else if (arrCode.length > 1) {
        for (i = 0; i < arrCode.length; i++) {
            msgContent += "<li>" + arrCode[i] + "</li>";
        }
        msgContent = "<ul>" + msgContent + "</ul>";
    }

    return msgContent;
}
function CustomParseFloat(numValue) {
    numValue = numValue.toString();
    if (numValue == "")
        return 0;
    numValue = numValue.toString().replace(/,/g, "");
    if (isNaN(numValue))
        return 0;
    var returnValue = parseFloat(numValue.replace(",", ""));
    return returnValue;
}
function RoundNumber(rnum, rlength) {
    rnum = CustomParseFloat(rnum);
    return Math.round(rnum * Math.pow(10, rlength)) / Math.pow(10, rlength);
}
function CastDecimal2(rnum) {
    return CastDecimal(rnum, 2);
}
function CastDecimal4(rnum) {
    return CastDecimal(rnum, 4);
}
function CastDecimal(rnum, rlength) {
    rnum = CustomParseFloat(rnum);

    if (rlength == 1)
        rnum = (Math.floor((rnum * 10).toFixed(2))) / 10;

    if (rlength == 2)
        rnum = (Math.floor((rnum * 100).toFixed(2))) / 100;

    if (rlength == 3)
        rnum = (Math.floor((rnum * 1000).toFixed(2))) / 1000;

    if (rlength == 4)
        rnum = (Math.floor((rnum * 10000).toFixed(2))) / 10000;
    return rnum;
}
function isNumberInput(str) {
    var pattern = /^\d+$/;
    return pattern.test(str);  // returns a boolean
};



///////////////////////////////////////////////////////////////////////////////////////
    function ShowAppMsg(summElement, msgContent, showPopUp) {
        ShowAppMsg2(summElement, msgContent, showPopUp, "");
    }

function ShowAppMsg2(summElement, msgContent, showPopUp, strNavigateUrl) {

    if (showPopUp) {

        var summElementId = summElement.attr('id');

        if (strNavigateUrl != "") {
            msgContent += "<hr style='width:100%'/>" +
                    "<div style='width:100%;text-align:right'>" +
                    "<input id='btnKendoWindowClose2' onClick=\"CloseKendoWindow2('" + summElementId + "','" + strNavigateUrl + "')\" type='button' value='Close' />" +
                    "</div>";

        } else {
            msgContent += "<hr style='width:100%'/>" +
                    "<div style='width:100%;text-align:right'>" +
                    "<input id='btnKendoWindowClose' onClick=\"CloseKendoWindow('" + summElementId + "')\" type='button' value='Close' />" +
                    "</div>";
        }


        summElement.html(msgContent);

        summElement.kendoWindow({
            actions: ["Close"],
            modal: true,
            title: 'Message',
            width: "450px"
        }).data("kendoWindow");

        var win = summElement.data("kendoWindow");
        win.title('Message'); //to refresh title
        win.center();
        win.open();

    }
    else {
        summElement.html(msgContent);
    }
}

function CloseKendoWindow(summElementId) {
    CloseKendoWindow2(summElementId, "");
}

function CloseKendoWindow2(summElementId, strNavigateUrl) {
    $("#" + summElementId).data("kendoWindow").close();

    if (strNavigateUrl != "") {

        if (strNavigateUrl == "back") {
            history.back(1);
        }
        else if (strNavigateUrl == "login") {
            location.href = "/Account/Login";
        }
        else {
            location.href = "#/" + strNavigateUrl;
        }

    }

}

///////////////////////////////////////////////////////////////////////////////////////
var alertResult = "";
var alertWindows;
var alertCallback;

$(function () {
    alertWindows = $("#div_alert_window").kendoWindow({
        actions: ["Close"],
        draggable: true,
        modal: true,
        resizable: false,
        visible: false,
        title: "Action"
    }).data("kendoWindow");
});

function alertClose(BtnResult) {
    console.log(BtnResult);
    alertResult = BtnResult;
    alertWindows.close();
};

function alertCloseCallBack(e) {
    alertWindows.unbind("close", alertCloseCallBack);
    if (alertCallback != null)
        alertCallback(alertResult);

}

function openDialogBox(Title, Message, Type, Buttons, theFunction) {

    var runTimeData = '<table cellpadding="0" cellspacing="0"><tr><td><div        class="AKKendoAlertIcon ' + Type + '"></div></td>' +
         '<td><div class="AKKendoAlertText" style="padding:10px 5px 20px 10px;">' + Message + '</div></td></tr></table><div style=\"width:100%;text-align:right\">';
    /*
    for (var i in Buttons) {
        var s = Buttons[i];
        runTimeData += '<input class="AKKendoAlertBtn" type="button"           onclick="alertClose(\'' + s + '\')" value="' + s + '" >';
    }
    */

    for (var i in Buttons) {
        var s = Buttons[i];
        runTimeData += '<input class="AKKendoAlertBtn" type="button"           onclick="alertClose(' + i + ')" value="' + s + '" padding=\"5px\" >';
    }

    runTimeData += '</div>';
    alertResult = "Cancel";

    if (theFunction !== undefined) {
        alertCallback = theFunction;
    }
    else {
        alertCallback = null;
    }

    alertWindows.bind("close", alertCloseCallBack);
    alertWindows.title(Title);
    alertWindows.center();
    alertWindows.content(runTimeData);
    alertWindows.open();
}
///////////////////////////////////////////////////////////////////////////////////////

    $(document).ajaxStart(
        function () {
            ShowLoading();
        }
    );

$(document).ajaxStop($.unblockUI);



$.ajaxSetup({
    timeout: 60000,
    error: function (x, e) {
        if (x.responseText != "") {

            var jobj;
            try {
                jobj = JSON.parse(x.responseText);
            }
            catch (exception) {
                jobj = null;
            }


            var strErrText = "";

           
            if (x.status == 401) {
                console.log("jobj.status:401");
                strErrText = getLValue(appCode.SessionLost);
                //clearInterval(prevGetData);
            }else if (jobj) {
                console.log("jobj.status:" + jobj.status);
                strErrText = getLValue(jobj.status);
                if (jobj.status == appCode.SessionLost || jobj.status == appCode.LoginFromOtherPlace || jobj.status == appCode.UserIsBlockedOrFreezed) {
                  // clearInterval(prevGetData);
                }
            }
            else {
                console.log("jobj.status:else");
                strErrText = getLValue(appCode.SystemError);
            }

            var summElement = $("#mainsummary");
            summElement.html("<p>" + strErrText + "</p>");



            summElement.kendoWindow({
                actions: ["Close"],
                modal: true,
                title: 'Message',
                width: "450px",
                close: function (e) {
                    if (jobj) {
                        if (jobj.status == appCode.SessionLost || jobj.status == appCode.LoginFromOtherPlace || jobj.status == appCode.UserIsBlockedOrFreezed) {
                           // window.location.reload();
                        }
                    }
                    else if (x.status == 401) {
                        //window.location.reload();
                    }
                }
            }).data("kendoWindow");

            var win = summElement.data("kendoWindow");
            win.title('Message'); //to refresh title
            win.center();
            win.open();

        }
        else if (x.status == 500) {

            var summElement = $("#mainsummary");
            summElement.html("<p>" + getLValue(appCode.SystemError) + "</p>");
           // clearInterval(prevGetData);

            summElement.kendoWindow({
                actions: ["Close"],
                modal: true,
                title: 'Message',
                width: "450px"
            }).data("kendoWindow");

            var win = summElement.data("kendoWindow");
            win.title('Message'); //to refresh title
            win.center();
            win.open();

           // window.location.reload();
        }
        else if (x.status == 401) {

            var summElement = $("#mainsummary");
            summElement.html("<p>" + getLValue(appCode.SessionLost) + "</p>");
           // clearInterval(prevGetData);

            summElement.kendoWindow({
                actions: ["Close"],
                modal: true,
                title: 'Message',
                width: "450px",
                close: function (e) {
                    window.location.reload();
                }
            }).data("kendoWindow");

            var win = summElement.data("kendoWindow");
            win.title('Message'); //to refresh title
            win.center();
            win.open();

           // window.location.reload();
        }


    }
});//$.ajaxSetup Error
///////////////////////////////////////////////////////////////////////////////////////