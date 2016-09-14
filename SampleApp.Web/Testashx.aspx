<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Testashx.aspx.cs" Inherits="SampleApp1.Testashx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test Handler Service</title>
    <link rel="stylesheet" href="css/contextmenu.css"/>
</head>
<body>
    <form id="form1" runat="server">
   <%-- <span><span class="icon2">CM&nbsp;&nbsp;&nbsp;&nbsp;</span>
        <span class="newCM" style="display: none;">
            <a href="javascript:OpenPopup('wfmPopUpReport.aspx?rtype=MDEAL&amp;param1=1987168','800', '520', '20', '20', 'yes', 'yes');" class="CMEnabled">View Deal Sheet</a>
        <a href="javascript:OpenPopup('wfmPopUpCopyDeal.aspx?linkkey=1987168', '780','600','20','20','no','yes');" class="CMEnabled">Copy Deal</a>
        <a href="#">content</a> href="javascript:__doPostBack('CHANGE_STATUS', '1987168;445;ContextMenu');" class="CMEnabled">Change Status</a>
        <span class="CMDisabled">View Errors/Warnings</span>
            <a href="wfmBillingSearch.aspx?offer=710101526" class="CMEnabledSep">View Bills</a>
        <span class="CMDisabled">Find in Archive History</span>
            <a href="javascript:OpenPopup('wfmPopupFundAmount.aspx?linkKey=1987168&amp;type=EDIT_PERCENTAGE', '600', '400', '0','0', 'yes', 'yes');" class="CMEnabled">Assign To Funds</a>
            <a href="javascript:OpenPopup('wfmPopUpReport.aspx?rtype=CDEAL&amp;param1=1987168&amp;CrossDiv=true','800', '520', '20', '20', 'yes', 'yes');" class="CMEnabledSep">View Cross Div Report</a>
            <a href="javascript:OpenPopup('wfmPopUpReport.aspx?rtype=CDEAL&amp;param1=1987168&amp;DealHistory=true','800', '520', '20', '20', 'yes', 'yes');" class="CMEnabled">View Dialog History Report</a>
            <a href="javascript:OpenPopup('wfmPopUpReport.aspx?rtype=DETAIL&amp;param1=\'710101526\'','800', '520', '20', '20', 'yes', 'yes');" class="CMEnabled">View Deal Detail</a>
        </span></span>--%>
        
    <span class="cMenu" id="id_2138501_91285" data-rpcbr="true" data-menu="DealList_OfferNumber" data-vt="MASTER" data-dsflag="true"><a href="somepage.aspx?linkKey=2138501&amp;">Deal#</a>
        <span>
            
        </span>
    </span>
    
    <script src="lib/jquery-1.7.2.js" type="text/javascript"></script>
    <script src="js/global.js" type="text/javascript"></script>
    <script src="js/contextmenu.js" type="text/javascript"></script>
    </form>
</body>
</html>
