Namespace.Register("App.Controls.CM");
App.Controls.CM = {
    reqInProcess: false,
    timerID: 0,
    hideDelay: 0,
    showDelay: 500,
    Initialize: function () {
        if ($(".cMenu").length > 0) {
            $(".cMenu").each(function (index) {
                var id = $(this).attr("id");
                var aTag = $(this).find("a");
                if ($(aTag).length > 0)
                    $(aTag).unbind("hover").bind("hover", function () { App.Controls.CM.DelayedShowContextMenuIcon(id, true); });
                else
                    $(this).unbind("hover").bind("hover", function () { App.Controls.CM.DelayedShowContextMenuIcon(id, false); });
            });
        }
    },
    ShowContextMenuIcon: function (id, isAnchor) {
        if (!App.Controls.CM.reqInProcess) {
            $(".cMenu .icon2").each(function () { $(this).removeClass("icon").html("&nbsp;&nbsp;&nbsp;&nbsp;"); });
            $(".cMenu .newCM").each(function () { $(this).hide(); });
            var control = $("#" + id + " span");
            if ($(control).length > 0) {
                var icon = $(control).find(".icon2");
                if ($(icon).length == 0) {
                    var iconTxt = "<span class='icon2'></span>";
                    $(control).html("").append(iconTxt).find(".icon2").addClass("icon").unbind("hover").hover(function () {
                        App.Controls.CM.DelayedShowContextMenu(id, isAnchor);
                    }, function () {
                        App.Controls.CM.DelayedHideContextMenuIcon(id);
                    });
                }
                else {
                    $(icon).html("").addClass("icon");
                }
            }
        }
    },
    HideContextMenuIcon: function (id) {
        var icon = $("#" + id).find(".icon2");
        if (icon.next().css("display") === "none" && icon.length > 0)
            icon.removeClass("icon").html("&nbsp;&nbsp;&nbsp;&nbsp;"); //icon.hide().parent().html("&nbsp;&nbsp;&nbsp;&nbsp;" + iConHtml);
    },
    ShowContextMenu: function (id, isAnchor) { // Get Context Menu        
        $(".cMenu .newCM").each(function () { $(this).hide(); });
        if ($("#" + id + " .newCM").length == 0 && !App.Controls.CM.reqInProcess) {
            App.Controls.CM.reqInProcess = true;
            var control = $("#" + id);
            var mName = $(control).attr("data-menu");
            var mKey = $(control).attr("data-key");
            var rpcbr = $(control).attr("data-rpcbr");
            var vt = $(control).attr("data-vt");
            var dsflag = $(control).attr("data-dsflag");
            var vType = $("#hdnViewType").val();
            $("body").css("cursor", "wait");
            $.ajax({
                //type: 'GET', //'POST'
                type: 'POST',
                cache: false,
                //url: App.Config.AjaxHandler + String.format('?act={0}&args={1}', 'CM', vt + ";" + mName + ";" + id + ";" + rpcbr + ";" + dsflag + ";" + vType),
                url: App.Config.AjaxHandler + "?act=CM&args=vt" + ";" + mName + ";" + id + ";" + rpcbr + ";" + dsflag + ";" + vType,
                data: { 'file': 'dave', 'type': 'ward' },
                success: function (result, status) {
                    App.Controls.CM.GetCentextMenuSuccess(id, result);
                }
            });
        }
        else
            $("#" + id + " .newCM").show();
    },
    GetCentextMenuSuccess: function (id, result) {
        $("body").css("cursor", "auto");
        App.Controls.CM.reqInProcess = false;
        if (result && result != "error") {
            // handle the session time out
            if (result.indexOf("AppHandler - Session") >= 0) {
                alert("Session has expired. Please login again");
                return;
            }
            var cmIcon = $("#" + id + " .icon2");
            cmIcon.parent().append(result);
            var cmSec = $("#" + id + " .newCM");
            //            cmSec.show().unbind("hover").bind("mouseout", function () {
            //                $(this).hide();
            //                if ($(this).css("display") === "none")
            //                    $(this).prev().removeClass("icon").html("&nbsp;&nbsp;&nbsp;&nbsp;"); //.parent().hide();
            //            }).bind("mouseover", function () {
            //                $(this).show().prev().html("").addClass("icon");
            //            });
            //            cmSec.show().unbind("hover").hover(function () {
            //                $(this).hide();
            //                if ($(this).css("display") === "none")
            //                    $(this).prev().removeClass("icon").html("&nbsp;&nbsp;&nbsp;&nbsp;"); //.parent().hide();
            //            }, function () {
            //                $(this).show().prev().html("").addClass("icon");
            //                        });
            cmSec.show().unbind("mouseout").bind("hover", function () {
                $(this).hide();
                if ($(this).css("display") === "none")
                    $(this).prev().removeClass("icon").html("&nbsp;&nbsp;&nbsp;&nbsp;"); //.parent().hide();
            }).bind("mouseover", function () {
                $(this).show().prev().html("").addClass("icon");
            });

        }
        else {
            $("#" + id + " a").addClass(result);
        }
    },
    ClearTimerID: function () {
        if (App.Controls.CM.timerID) {
            clearTimeout(this.timerID);
            App.Controls.CM.timerID = 1;
        }
    },
    DelayedShowContextMenu: function (id, isAnchor) {
        //App.Controls.CM.ClearTimerID();
        //App.Controls.CM.timerID = window.setTimeout(Function.createDelegate(this, function () { App.Controls.CM.ShowContextMenu(id, isAnchor); }), this.showDelay);
        App.Controls.CM.ShowContextMenu(id, isAnchor);
    },
    DelayedShowContextMenuIcon: function (id, isAnchor) {
        //App.Controls.CM.ClearTimerID();
        //App.Controls.CM.timerID = window.setTimeout(Function.createDelegate(this, function () { App.Controls.CM.ShowContextMenuIcon(id, isAnchor); }), this.showDelay);
        App.Controls.CM.ShowContextMenuIcon(id, isAnchor);
    },
    DelayedHideContextMenuIcon: function (id) {
        //App.Controls.CM.ClearTimerID();
        //App.Controls.CM.timerID = window.setTimeout(Function.createDelegate(this, function () { App.Controls.CM.HideContextMenuIcon(id); }), this.hideDelay);
        App.Controls.CM.HideContextMenuIcon(id);
    }
};

$(document).ready(function() {
    App.Controls.CM.Initialize();
});