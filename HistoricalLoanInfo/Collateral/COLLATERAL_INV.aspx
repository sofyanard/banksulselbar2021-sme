<%@ Page language="c#" Codebehind="COLLATERAL_INV.aspx.cs" AutoEventWireup="True" Inherits="SME.HistoricalLoanInfo.Collateral.COLLATERAL_INV" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>COLLATERAL_INV</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../style.css" type="text/css" rel="stylesheet">
		<!-- #include file="../../include/cek_all.html" -->
		<!-- #include file="../../include/cek_entries.html" -->
			<script language="javascript">
		function update(regno, curef) {
			parent.document.Form1.action = "../../VerificationAssignment/AppraisalAssignment.aspx?regno=" + regno + "&curef=" + curef;
			parent.document.Form1.submit();
			return false;
		}		
		</script>
		
		<script language=vbscript>
		function HitungTotal()
			SetLocale("in")
			set obj = document.Form1
			if isnumeric(obj.TXT_CL_NOOFUNITS.value) then
				CL_NOOFUNITS = cdbl(obj.TXT_CL_NOOFUNITS.value)
			else
				CL_NOOFUNITS = 0
			end if
			
			if isnumeric(obj.TXT_CL_PRICEPERUNIT.value) then
				CL_PRICEPERUNIT = cdbl(obj.TXT_CL_PRICEPERUNIT.value)
			else
				CL_PRICEPERUNIT = 0
			end if			
			
			obj.TXT_CL_TOTALAMOUNT.value = CL_NOOFUNITS * CL_PRICEPERUNIT
			obj.TXT_CL_TOTALAMOUNT.value = replace(obj.TXT_CL_TOTALAMOUNT.value, ".", ",")
		end function			
		</script>
</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<CENTER>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" width="200">
										Keterangan&nbsp;Jaminan</TD>
									<TD width="15"></TD>
									<TD class="TDBGColorValue">
									<asp:textbox id="TXT_CL_DESC" runat="server" MaxLength="50" Columns="25" CssClass="mandatory" Width=400
											onKeypress="return kutip_satu()" Enabled=False ></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Mata Uang</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_CL_CURRENCY" runat="server" CssClass="mandatory" Enabled=False ></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Klasifikasi Jaminan</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_CL_COLCLASSIFY" runat="server" CssClass="mandatory" Enabled=False ></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">SIBS Collateral ID</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:TextBox ID="TXT_SIBS_COLID" readonly Runat="server" MaxLength="35" Columns="30" onKeypress="return kutip_satu()" Enabled=False ></asp:TextBox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Jumlah Unit</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:TextBox id="TXT_CL_NOOFUNITS" runat="server" MaxLength="10" Columns="10" CssClass="mandatory"
											onKeypress="return numbersonly()" onkeyup="HitungTotal()" Enabled=False ></asp:TextBox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Berat / Jenis Berat</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:TextBox id="TXT_CL_WEIGHT" runat="server" MaxLength="10" Columns="10" onKeypress="return numbersonly()" Enabled=False ></asp:TextBox>
										<asp:TextBox id="TXT_CL_WEIGHTTYPE" runat="server" MaxLength="10" Columns="10" onKeypress="return kutip_satu()" Enabled=False ></asp:TextBox> (gram, kg, ...)
									</TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Nilai Jaminan</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:TextBox id="TXT_CL_VALUE" runat="server" Columns="25" MaxLength="21" CssClass="mandatory"
											onKeypress="return digitsonly()" onblur = "FormatCurrency(this)" Enabled=False ></asp:TextBox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Harga Per Unit</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:TextBox id="TXT_CL_PRICEPERUNIT" runat="server" MaxLength="21" Columns="25" CssClass="mandatory"
											onKeypress="return digitsonly()" onblur = "FormatCurrency(this)"  onkeyup="HitungTotal()" Enabled=False ></asp:TextBox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Tanggal Review</TD>
									<TD></TD>
									<TD class="TDBGColorValue">
										<asp:TextBox id="TXT_CL_REVIEWDATEDAY" runat="server" MaxLength="2" Columns="2" onKeypress="return numbersonly()" Enabled=False ></asp:TextBox>
										<asp:DropDownList id="DDL_CL_REVIEWDATEMONTH" runat="server" Enabled=False ></asp:DropDownList>
										<asp:TextBox id="TXT_CL_REVIEWDATEYEAR" runat="server" MaxLength="4" Columns="4" onKeypress="return numbersonly()" Enabled=False ></asp:TextBox>
									</TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Total Amount</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:TextBox id="TXT_CL_TOTALAMOUNT" runat="server" MaxLength="21" Columns="25" CssClass="mandatory"
											onKeypress="return digitsonly()" onblur = "FormatCurrency(this)" Enabled=False ></asp:TextBox></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="center" width="50%" colSpan="2">&nbsp;
						<% if (Request.QueryString["appr"] == "1") { %>
							
							<INPUT type="button" value="Update" class="button1" onclick="return update('<%=Request.QueryString["regno"]%>','<%=Request.QueryString["curef"]%>');">&nbsp;
							
							<% } %>
							<% if (Request.QueryString["de"] == "1") { %>
							<input type="button" id="Button1" name="Button1" Value="Save" Class="Button1" onClick="return cek_mandatory(document.Form1);">&nbsp;
							<% } %>
							<asp:Label id="LBL_CL_SEQ" runat="server" Visible="False"></asp:Label>
							<asp:Label id="LBL_REGNO" runat="server" Visible="False"></asp:Label>
							<asp:Label id="LBL_CUREF" runat="server" Visible="False"></asp:Label>
							<asp:Label id="LBL_TC" runat="server" Visible="False"></asp:Label>
						</TD>
					</TR>
				</TABLE>
			</CENTER>
		</form>
	</body>
</HTML>
