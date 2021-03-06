<%@ Page language="c#" Codebehind="COLLATERAL_VEH.aspx.cs" AutoEventWireup="True" Inherits="SME.RejectMaintenanceDE.Jaminan_VEH" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Jaminan_VEH</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../style.css" type="text/css" rel="stylesheet">
		<!-- #include  file="../include/cek_all.html" -->
		<!-- #include  file="../include/cek_entries.html" -->
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<center>
				<TABLE id="Table1" cellSpacing="2" cellPadding="2" width="100%">
					<TR>
						<TD class="td" vAlign="top" width="50%">
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%">
								<TR>
									<TD class="TDBGColor1" width="200">
										Keterangan&nbsp;Jaminan</TD>
									<TD width="15"></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_CL_DESC" onKeypress="return kutip_satu()" runat="server" Columns="25" MaxLength="50"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Mata Uang</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_CL_CURRENCY" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Klasifikasi Jaminan</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_CL_COLCLASSIFY" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Core Collateral ID</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:TextBox ID="TXT_SIBS_COLID" Runat="server" MaxLength="35" Columns="30" onKeypress="return kutip_satu()"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Dealer</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_CL_DEALER" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">No. Mesin</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_CL_MACHINENO" runat="server" Columns="20" MaxLength="20" onKeypress="return kutip_satu()"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Number of Unit</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_CL_NOOFUNITS" runat="server" Columns="8" MaxLength="8" onKeypress="return numbersonly()"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Manufactured Year</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_CL_MANUFACTUREYY" runat="server" onKeypress="return numbersonly()" Columns="4"
											MaxLength="4"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">No. Chasis/ Seri</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_CL_CHASISNO" runat="server" Columns="20" MaxLength="20" onKeypress="return kutip_satu()"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Jenis Kendaraan</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:TextBox id="TXT_CL_JNSMOBIL" runat="server" MaxLength="20" Columns="20" onKeypress="return kutip_satu()"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Type / Jenis Unit</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_CL_CARTYPE" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Brand / Model</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:TextBox id="TXT_CL_CARBRAND" runat="server" MaxLength="60" Columns="30" onKeypress="return kutip_satu()"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Owner / Nama BPKB</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_CL_OWNER" runat="server" Columns="25" MaxLength="100" onKeypress="return kutip_satu()"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1" width="200">Hubungan</TD>
									<TD width="15"></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_CL_RELATIONSHIP" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">No. BPKB</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_CL_BPKBNO" runat="server" Columns="20" MaxLength="20" onKeypress="return kutip_satu()"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">No. Polisi</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_CL_PLATEID" runat="server" Columns="20" MaxLength="20" onKeypress="return kutip_satu()"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Dinilai Oleh</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_CL_APPRBY" runat="server" Columns="25" MaxLength="100" onKeypress="return kutip_satu()"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Tgl. Penerbitan</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_CL_ISSUEDDATEDAY" runat="server" Columns="2" MaxLength="2" onKeypress="return numbersonly()"></asp:textbox><asp:dropdownlist id="DDL_CL_ISSUEDDATEMONTH" runat="server"></asp:dropdownlist><asp:textbox id="TXT_CL_ISSUEDDATEYEAR" runat="server" Columns="4" MaxLength="4" onKeypress="return numbersonly()"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Nilai Bank</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:TextBox id="TXT_CL_VALUE" runat="server" onKeypress="return numbersonly()" Columns="25"
											MaxLength="21" onblur="FormatCurrency(this)"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Nilai Pasar</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:TextBox id="TXT_CL_VALUE2" runat="server" onKeypress="return numbersonly()" Columns="25"
											MaxLength="21" onblur="FormatCurrency(this)"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Nilai Asuransi</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:TextBox id="TXT_CL_VALUEINS" runat="server" onKeypress="return numbersonly()" Columns="25"
											MaxLength="21" onblur="FormatCurrency(this)"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Nilai Pengikatan</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:TextBox id="TXT_CL_VALUEIKAT" runat="server" onKeypress="return numbersonly()" Columns="25"
											MaxLength="21" onblur="FormatCurrency(this)"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Nilai Pengurang PPA</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:TextBox id="TXT_CL_VALUEPPA" runat="server" onKeypress="return numbersonly()" Columns="25"
											MaxLength="21" onblur="FormatCurrency(this)"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Nilai Likuidasi</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:TextBox id="TXT_CL_VALUELIQ" runat="server" onKeypress="return numbersonly()" Columns="25"
											MaxLength="21" onblur="FormatCurrency(this)"></asp:TextBox></TD>
								</TR>
								<!--<TR>
									<TD class="TDBGColor1">Jumlah Penilaian</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_CL_APPRVAL" runat="server" Columns="25" MaxLength="21" onKeypress="return numbersonly()"
											onblur="FormatCurrency(this)"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Nilai Pasar</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_CL_MARKETVAL" runat="server" Columns="25" MaxLength="21" onKeypress="return numbersonly()"
											onblur="FormatCurrency(this)"></asp:textbox></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Nilai Agunan Untuk PPAP</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:textbox id="TXT_CL_PPAPVAL" runat="server" MaxLength="21" Columns="25" onkeypress="return numbersonly()"
											onblur="FormatCurrency(this)"></asp:textbox></TD>
								</TR>-->
								<TR>
									<TD class="TDBGColor1">Lokasi Agunan</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_CL_COLLOC" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Penilaian Menurut</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_CL_VALACCRDTO" runat="server"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD class="TDBGColor1">Jenis Agunan</TD>
									<TD></TD>
									<TD class="TDBGColorValue"><asp:dropdownlist id="DDL_CL_JNSAGUNAN" runat="server"></asp:dropdownlist></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD class="TDBGColor2" vAlign="top" align="center" width="50%" colSpan="2">
							<input type="button" id="Button1" name="Button1" Value="Save" Class="Button1" runat="server" onserverclick="Button1_ServerClick">&nbsp;
							<asp:label id="LBL_CL_SEQ" runat="server" Visible="False"></asp:label>
							<asp:label id="LBL_REGNO" runat="server" Visible="False"></asp:label>
							<asp:label id="LBL_CUREF" runat="server" Visible="False"></asp:label>
							<asp:label id="LBL_TC" runat="server" Visible="False"></asp:label>
						</TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
