String.prototype.format = function() {
    var args = arguments;    
    return this.replace(/{(\d+)}/g, function(match, number) {
        return typeof (args[number] != 'undefined') ? args[number] : '{' + number + '}';
    });
};

///*Singleton + Revealing Module Patterns*/
var Namespace = function() {
    function Create(pSrc) {
        eval("window." + pSrc + " = new Object();");
    }
    function Exists(pSrc) {
        eval("var NE = false; try{if(" + pSrc + "){NE = true;}else{NE = false;}}catch(err){NE=false;}");
        return NE;
    }
    function Register(pName) {
        var lChk = false;
        var lCob = "";
        var lSpc = pName.split(".");
        for (var i = 0; i < lSpc.length; i++) {
            if (lCob != "") { lCob += "."; }
            lCob += lSpc[i];
            lChk = Exists(lCob);
            if (!lChk) { Create(lCob); }
        }
        /*if (lChk) { throw "Namespace: " + pName + " is already defined."; }*/
    }
    return {
        Register: Register
    };
} (); /*Self executable function*/
Namespace.Register("App");
Namespace.Register("App.Config");
/*Common config entries across pages*/
App.Config = { 
    /*URLs*/
    AjaxHandler : "services/Service.ashx"
    /*CSS Classes*/
    /*Msgs*/
    /*Constants*/
};
Namespace.Register("App.Global.Functions");

App.Global.Functions = function () {
    var PopupWindow = null;
    function OpenPopup(url, width, height, top, left, scrollbars, resizable) {
        left = (screen.availWidth - width) / 2;
        top = (screen.availHeight - height) / 2;
        if (!PopupWindow || PopupWindow.closed)
            PopupWindow = window.open(url, '', 'width=' + width + ',height=' + height + ',top=' + top + ',left=' + left + ',toolbars=no,menubar=no,dependent=yes,status=no,titlebar=no,scrollbars=' + scrollbars + ',resizable=' + resizable);
        else
            PopupWindow.focus()

        if (PopupWindow.attachEvent)
            PopupWindow.attachEvent("onunload", HideModalWindow);
        else if (!PopupWindow.onunload)
            PopupWindow.onunload = HideModalWindow;
        else if (!PopupWindow.onbeforeunload)
            PopupWindow.onbeforeunload = HideModalWindow;
    }
    function ShowCalendar(pElement) {
        ShowModalWindow();
        OpenPopup('wfmCalendar.aspx?fn=' + $($("form")[0]).attr("id") + '&cn=' + pElement + '&new=1', '740', '450', '60', '60', 'no', 'no');
    }
    function ShowDealCenterCalendar(pElement) {
        ShowModalWindow();
        OpenPopup('wfmCalendar.aspx?fn=' + $($("form")[0]).attr("id") + '&cn=' + pElement + "&new=dc", '740', '450', '60', '60', 'no', 'no');
    }
    function ShowModalWindow() {
        var lOverlay = $("#parentOverlay");
        if (!lOverlay || lOverlay.length === 0) {
            $($("form")[0]).append("<div id='parentOverlay'></div>");
            lOverlay = $("#parentOverlay");
        }
        lOverlay.show();
    }
    function HideModalWindow() {
        if (document.getElementById("parentOverlay") == null)
            return;
        var lOverlay = $("#parentOverlay");
        if (lOverlay || lOverlay.length > 0)
            lOverlay.hide();
    }
    function CheckModalWindowExist() {
        var lOverlay = $("#parentOverlay");
        if (lOverlay && lOverlay.length > 0 && lOverlay.css("display") !== "none")
            return true;
        else
            return false;
    }
    function AddDays(date, days) {
        if (date == undefined || date.trim() == '')
            return '';

        var dt = new Date(date)
        dt.setDate(dt.getDate() + days)

        return GetShortDateString(dt);

    }
    function GetShortDateString(dt) {

        if (AppInfo == undefined || AppInfo.ShortDatePattern == undefined)
            return GetFormattedDate(dt, "M-d-yyyy");

        return GetFormattedDate(dt, AppInfo.ShortDatePattern);
    }

    function GetFormattedDate(dt, pattern) {

        var dString = pattern.replace(/yyyy/g, dt.getFullYear());
        dString = dString.replace(/yy/g, (dt.getFullYear() + "").substring(2));

        var month = dt.getMonth();
        /* dString = dString.replace(/MMMM/g, dateFormat.monthsLong[month].escapeDateTimeTokens());
        dString = dString.replace(/MMM/g, dateFormat.monthsShort[month].escapeDateTimeTokens()); */
        //dString = dString.replace(/MM/g, month+1<10 ? "0" + (month+1) : month+1);
        dString = dString.replace(/MM/g, (month + 1));
        dString = dString.replace(/(\\)?M/g, function ($0, $1) { return $1 ? $0 : month + 1; });

        /* var dayOfWeek = dt.getDay();
        dString = dString.replace(/dddd/g, dateFormat.daysLong[dayOfWeek].escapeDateTimeTokens());
        dString = dString.replace(/ddd/g, dateFormat.daysShort[dayOfWeek].escapeDateTimeTokens());
        */

        var day = dt.getDate();
        //dString = dString.replace(/dd/g, day < 10 ? "0" + day : day);
        dString = dString.replace(/dd/g, day);
        dString = dString.replace(/(\\)?d/g, function ($0, $1) { return $1 ? $0 : day; });

        /*var hour = dt.getHours();
        if (dateFormat.clockType == 12) {
        if (hour > 12) {
        hour -= 12;
        }
        }
    
        dString = dString.replace(/HH/g, hour < 10 ? "0" + hour : hour);
        dString = dString.replace(/(\\)?H/g, function($0, $1) { return $1 ? $0 : hour; });
    
        var minutes = dt.getMinutes();
        dString = dString.replace(/mm/g, minutes < 10 ? "0" + minutes : minutes);
        dString = dString.replace(/(\\)?m/g, function($0, $1) { return $1 ? $0 : minutes; });

        var seconds = dt.getSeconds();
        dString = dString.replace(/ss/g, seconds < 10 ? "0" + seconds : seconds);
        dString = dString.replace(/(\\)?s/g, function($0, $1) { return $1 ? $0 : seconds; });
    
        dString = dString.replace(/fff/g, this.getMilliseconds());
    
        dString = dString.replace(/tt/g, this.getHours() > 12 || this.getHours() == 0 ? dateFormat.tt["PM"] : dateFormat.tt["AM"]);
        */


        return dString.replace(/\\/g, "");
    }


    function IsValidDateField(pSelElem) {
        /*if (pSelElem && pSelElem.type == undefined) return true;
        if (fld.value == '' || fld.value == '~' || IsDateValid(fld.value)) {
        fld.className = 'FieldControl';
        if (fld.id == InValidFldName) {
        if (typeof (document.all.lblErrorList) != 'undefined') { document.all.lblErrorList.innerText = '' }
        }
        return true;
        }
        else {
        MarkFieldInValid(fld, GetLocalizedText('PleaseEnterValidDate'));
        return false;
        }*/
        var retunValue = true;
        var lVal = pSelElem.val();
        if (lVal !== "" && !IsDateValid(pSelElem.val())) {
            pSelElem.addClass("error");
            retunValue = false;
        }
        else {
            pSelElem.removeClass("error");
            FormatDate(pSelElem);
            retunValue = true;
        }
        return retunValue
    }
    function FormatDate(field) {
        var lDateSep = AppConfigRuntimeScriptConstants.DateSep;
        var lDate = $(field).val().split(lDateSep);
        var lYearPos = AppConfigRuntimeScriptConstants.YearPos;
        var lMonthPos = AppConfigRuntimeScriptConstants.MonthPos;
        var lDayPos = AppConfigRuntimeScriptConstants.DatePos;
        if (isNaN(lDate[lYearPos]) || isNaN(lDate[lMonthPos]) || isNaN(lDate[lDayPos]))
            return false;
        if (lDate[lYearPos].length === 2)
            lDate[lYearPos] = "20" + lDate[lYearPos];
        if (lDate[lMonthPos].length === 1)
            lDate[lMonthPos] = "0" + lDate[lMonthPos];
        if (lDate[lDayPos].length === 1)
            lDate[lDayPos] = "0" + lDate[lDayPos];
        $(field).val( lDate[0] + AppConfigRuntimeScriptConstants.DateSep + lDate[1] + AppConfigRuntimeScriptConstants.DateSep + lDate[2]);
    }
    function IsDateValid(pValue) {
        var lDateSep = AppConfigRuntimeScriptConstants.DateSep;
        var lDate = pValue.split(lDateSep);
        var lYearPos = AppConfigRuntimeScriptConstants.YearPos;
        var lMonthPos = AppConfigRuntimeScriptConstants.MonthPos;
        var lDayPos = AppConfigRuntimeScriptConstants.DatePos;
        if (lDate.length != 3)
            return false;
        try {
            if (isNaN(lDate[lYearPos]) || isNaN(lDate[lMonthPos]) || isNaN(lDate[lDayPos]))
                return false;
            var lYear = parseInt(lDate[lYearPos], 10);
            var lMonth = parseInt(lDate[lMonthPos], 10);
            var lDay = parseInt(lDate[lDayPos], 10);
            if (isNaN(lYear) || isNaN(lMonth) || isNaN(lDay))
                return false;
            if ((lYear > 99 && lYear < 1753) || lYear < 1 || lYear > 9999)
                return false;
            if (lYear < 50)
                lYear = lYear + 2000;
            if (lYear < 100)
                lYear = lYear + 1900;
            if (lMonth > 12 || lMonth < 1)
                return false;
            if (lDay < 1 || lDay > 31)
                return false;
            if ((lMonth == 4 || lMonth == 6 || lMonth == 9 || lMonth == 11) && lDay > 30)
                return false;
            if (lMonth == 2) {
                var lMaxDay = 28;
                if (lYear % 4 == 0 && !(lYear % 100 == 0 && lYear % 400 != 0))
                    lMaxDay = 29;
                if (lDay > lMaxDay)
                    return false;
            }
        }
        catch (e) {
            return false;
        }
        return true;
    }
    function IsValidCurrencyField(fld, scale) {
        if (IsValidCurrency(fld, scale) || IsValidPercent(fld, scale)) {
            return true;
        }
        else {
            MarkFieldInValid(fld, GetLocalizedText('ValueMustBeNumericUpToLimit').replace('{0}', scale));
            fld.focus();
        }
    }
    function IsValidCurrency(fld, scale) {
        if (fld.type == undefined) return true;
        if (scale == null) scale = 3;
        var currencyExp = RegExp(AppConfigRuntimeScriptConstants.CurrencyFrmt);
        if (fld.value == '' || fld.value == '~' || (currencyExp.test(fld.value) && parseFloat(fld.value.replace(AppConfigRuntimeScriptConstants.CurrSymbol, '')) <= 9999999999.9999)) {
            fld.className = 'FieldControlSmall';
            if (fld.id == InValidFldName) {
                try { document.all.lblErrorList.innerText = '' } catch (e) { }
            };
            if (fld.getAttribute('calcexpr') != null && fld.getAttribute('calcexpr') != '')
                return ValidateAllowAmt(fld.id);
            else
                return true;
        }
        else {
            return false;
        }
    }
    function IsValidPercent(fld, scale) {
        if (fld.type == undefined) return true;
        if (scale == null) scale = 3;
        var percentExp = RegExp(AppConfigRuntimeScriptConstants.PercentFrmt);
        if (fld.value == '' || fld.value == '~' || (percentExp.test(fld.value) && parseFloat(fld.value.replace('%','')) <= 9999999999.9999)) {
            fld.className = 'FieldControlSmall';
            if (fld.id == InValidFldName) {
                try { document.all.lblErrorList.innerText = '' } catch (e) { }
            };
            if (fld.getAttribute('calcexpr') != null && fld.getAttribute('calcexpr') != '')
                return ValidateAllowAmt(fld.id);
            else
                return true;
        }
        else {
            return false;
        }
    }
    function ToJSONKeyValueFormat(pKey, pValue, pIsString) {
        if (pIsString === undefined) pIsString = true;
        return ((pIsString) ? "\"" + pKey + "\":\"" + pValue + "\"" : "\"" + pKey + "\":" + pValue);
    }
    function ToJSONFormat() {
        var lJSONString = "";
        for (var i = 0; i < arguments.length; i++) {
            lJSONString = ((i === 0) ? "{" + arguments[i] : lJSONString = lJSONString + "," + arguments[i]);
        }
        return ((lJSONString !== "") ? lJSONString + "}" : "");
    }

    function JqAjaxCall(url, data, datatype, ajaxcallback, cache) {
        $.ajax({
            type: "GET", 		//GET or POST or PUT or DELETE verb
            url: url, 		// Location of the service
            data: data, 		//Data sent to server
            cache: cache,
            contentType: "", 	// content type sent to server
            dataType: datatype, 	//Expected data format from server
            success: ajaxcallback //,
            //error: function (json) {//On Error
            //  alert('Jquery Ajax request failed');
            //}
        });
    }

    function JQueryAsyncAjaxCall(url, data, datatype, ajaxcallback) {
        $.ajax({
            type: "GET", 		//GET or POST or PUT or DELETE verb
            url: url, 		// Location of the service
            data: data, 		//Data sent to server
            contentType: "", 	// content type sent to server
            dataType: datatype, 	//Expected data format from server
            success: ajaxcallback
        });
    }

    function JQueryAjaxCall(url, data, datatype, ajaxcallback) {
        $.ajax({
            type: "GET", 		//GET or POST or PUT or DELETE verb
            url: url, 		// Location of the service
            data: data, 		//Data sent to server
            contentType: "", 	// content type sent to server
            dataType: datatype, 	//Expected data format from server
            success: ajaxcallback//,
            //error: function (json) {//On Error
            //  alert('Jquery Ajax request failed');
            //}
        });
    }
    function ShowOverlay(overlayContent) {
        var docHeight = $(document).height(); //grab the height of the page
        var scrollTop = $(window).scrollTop(); //grab the px value from the top of the page to where you're scrolling
        var overlay = $('<div id="overlay"> <div class="overlay-bg"><div class="overlay-content">' + overlayContent + '</div></div></div>');
        overlay.appendTo(document.body)
        $('.overlay-bg').show().css({ 'height': docHeight }); //display your popup and set height to the page height
        $('.overlay-content').css({ 'top': scrollTop + 100 + 'px' }); //set the content 20px from the window top
    }
    function HideOverlay() {
        $('.overlay-bg').hide(); // hide the overlay
        $('#overlay').remove(); // remove the overlay div from body

    }
    // restrict the no of characters in a control
    function limitText(limitField, limitNum) {
        var lineBreaks = limitField.value.split('\n').length - 1;
        if ((limitField.value.length + lineBreaks) > limitNum) {
            limitField.value = limitField.value.substring(0, (limitNum - lineBreaks));
        }
    }
    return { OpenPopup: OpenPopup, ShowCalendar: ShowCalendar, ShowDealCenterCalendar: ShowDealCenterCalendar,
        ShowModalWindow: ShowModalWindow, HideModalWindow: HideModalWindow, JqAjaxCall: JqAjaxCall,
        IsValidDateField: IsValidDateField, IsValidCurrencyField: IsValidCurrencyField,
        ToJSONKeyValueFormat: ToJSONKeyValueFormat, ToJSONFormat: ToJSONFormat, CheckModalWindowExist: CheckModalWindowExist,
        JQueryAjaxCall: JQueryAjaxCall, JQueryAsyncAjaxCall: JQueryAsyncAjaxCall, AddDays: AddDays, GetShortDateString: GetShortDateString, GetFormattedDate: GetFormattedDate,
        ShowOverlay: ShowOverlay, HideOverlay: HideOverlay, limitText: limitText
    };
} ();