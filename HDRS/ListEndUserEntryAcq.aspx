<%@ Page language="c#" Codebehind="ListEndUserEntryAcq.aspx.cs" AutoEventWireup="True" Inherits="SME.HDRS.ListEndUserEntryAcq" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ListEndUserEntryAcq</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<CENTER>
				<table width="100%" border="0">
					<tr>
						<td align="left">
							<TABLE id="Table31">
								<TR>
									<TD class="tdBGColor2" style="WIDTH: 400px" align="center"><B>APPLICATION PENDING LIST</B></TD>
								</TR>
							</TABLE>
						</td>
						<TD class="tdNoBorder" align="right"><A href="../Body.aspx"><IMG src="../Image/MainMenu.jpg"></A><A href="../Logout.aspx" target="_top"><IMG src="../Image/Logout.jpg"></A></TD>
					</tr>
					<TR>
						<TD class="tdHeader1" colSpan="2">ACQUIRE INFO LIST</TD>
					</TR>
					<TR>
						<TD colSpan="2"><ASP:DATAGRID id="DGR_ACQ" runat="server" AutoGenerateColumns="False" CellPadding="1" AllowPaging="True"
								Width="100%">
								<AlternatingItemStyle CssClass="TblAlternating"></AlternatingItemStyle>
								<Columns>
									<asp:BoundColumn DataField="HTH_HRS#" HeaderText="HRS#">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="H_RECEIVED_DATE" HeaderText="HRS DATE">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="200px"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="HTH_TRACKDATE" HeaderText="TRACK DATE">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="H_APP#" HeaderText="NO. APLIKASI">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="SU_FULLNAME" HeaderText="SEND BY">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="FUNCTION">
										<HeaderStyle CssClass="tdSmallHeader"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:LinkButton id="edit_cab" runat="server" CommandName="Continue">Continue</asp:LinkButton>&nbsp;
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</ASP:DATAGRID></TD>
					</TR>
				</table>
			</CENTER>
		</form>
	</body>
</HTML>
