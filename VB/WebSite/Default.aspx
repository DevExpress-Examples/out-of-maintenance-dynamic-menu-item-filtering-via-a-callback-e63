<%-- BeginRegion Page setup --%>
<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="ASPxperience_Menu_DynamicMenuFilteringOnCallback_DynamicMenuFilteringOnCallback" %>

<%@ Register Assembly="DevExpress.Web.v7.3" Namespace="DevExpress.Web.ASPxCallbackPanel"
	TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v7.3" Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v7.3" Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dxwm" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v7.3" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<%-- EndRegion --%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
	<title>Dynamic menu item filtering via a callback</title>
	<%-- BeginRegion CSS --%>
	<style type="text/css">
		.gameTable {
			width: 100%;
		}
		.gameTable td.label {
			padding: 4px 4px 6px 0;
		}
		.gameTable td.gamesMenu {
			padding-top: 4px;
		}
	</style>
	<%-- EndRegion --%>
	<script type="text/javascript">
		var pendingCallback = false;
var previousFilterString = "";
function OnFilterBoxKeyUp(s, e) {
	var filterString = Trim(s.GetText());
	if (pendingCallback || filterString == previousFilterString)
		return;
	pendingCallback = true;
	previousFilterString = filterString;
	window.setTimeout("callbackPanel.PerformCallback()", 300);
}
function OnCallbackPanelEndCallback(s, e) {
	pendingCallback = false;
}
function Trim(str) {
	return str.replace(/\s*((\S+\s*)*)/, "$1").replace(/((\s*\S+)*)\s*/, "$1");
}
	</script>
</head>
<body>
	<form id="form1" runat="server">

	<div>
		<dxrp:ASPxRoundPanel ID="rpGames" runat="server" Width="240px" HeaderText="Games">
			<PanelCollection>
				<dxrp:PanelContent runat="server">
					<table cellpadding="0" cellspacing="0" class="gameTable">
						<tr>
							<td class="label" valign="top">
								Search:
							</td>
							<td valign="top">
								<dxe:ASPxTextBox ID="tbFilterBox_GameName" runat="server" Width="170px">
									<ClientSideEvents KeyUp="OnFilterBoxKeyUp" />
								</dxe:ASPxTextBox>
							</td>
						</tr>
						<tr>
							<td colspan="2" class="gamesMenu" align="center" valign="top">
								<dxcp:ASPxCallbackPanel ID="cpCallbackPanel" runat="server" Width="100%" ClientInstanceName="callbackPanel" OnCallback="OnCallback" Height="150px">
									<ClientSideEvents EndCallback="OnCallbackPanelEndCallback" />
									<PanelCollection>
										<dxrp:PanelContent runat="server">
											<dxwm:ASPxMenu ID="menuGames" runat="server" Orientation="Vertical" Width="100%" BorderBetweenItemAndSubMenu="HideRootOnly">
												<ItemStyle HorizontalAlign="Left" >
													<HoverStyle BackColor="White">
													</HoverStyle>
												</ItemStyle>
												<SubMenuItemStyle HorizontalAlign="Left" />
												<SubMenuStyle GutterWidth="0px">
													<Paddings PaddingLeft="5px" PaddingRight="5px" />
												</SubMenuStyle>
											</dxwm:ASPxMenu>
											<dxe:ASPxLabel ID="lblNoGamesFound" runat="server" Text="No Games Found" ForeColor="#A0A0A0" />
										</dxrp:PanelContent>
									</PanelCollection>
								</dxcp:ASPxCallbackPanel>
							</td>
						</tr>
					</table>
				</dxrp:PanelContent>
			</PanelCollection>
		</dxrp:ASPxRoundPanel>
	</div>
	</form>
</body>
</html>