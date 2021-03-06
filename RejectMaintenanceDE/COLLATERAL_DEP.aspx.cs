using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Microsoft.VisualBasic;
using DMS.CuBESCore;
using DMS.DBConnection;

namespace SME.RejectMaintenanceDE
{
	/// <summary>
	/// Summary description for COLLATERAL_DEP.
	/// </summary>
	public partial class COLLATERAL_DEP : System.Web.UI.Page
	{
		protected Tools tool = new Tools();
		protected Connection conn;
				
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = (Connection) Session["Connection"];

			if (!IsPostBack)
			{
				LBL_REGNO.Text	= Request.QueryString["regno"];
				LBL_CUREF.Text	= Request.QueryString["curef"];
				LBL_TC.Text		= Request.QueryString["tc"];
				LBL_CL_SEQ.Text	= Request.QueryString["CL_SEQ"];
				int CL_TYPE	= Convert.ToInt16(Request.QueryString["coltypeid"]);

				DDL_CL_CURRENCY.Items.Add(new ListItem("- PILIH -", ""));
				DDL_CL_COLCLASSIFY.Items.Add(new ListItem("- PILIH -", ""));
				DDL_CL_VALACCRDTO.Items.Add(new ListItem("- PILIH -", ""));
				DDL_CL_JNSAGUNAN.Items.Add(new ListItem("- PILIH -", ""));
				DDL_CL_IKATTYPE.Items.Add(new ListItem("- PILIH -", ""));
				DDL_CL_BANK.Items.Add(new ListItem("- PILIH -", ""));
				DDL_CL_REKTYPE.Items.Add(new ListItem("- PILIH -", ""));
				DDL_CL_REKCURRENCY.Items.Add(new ListItem("- PILIH -", ""));
				DDL_CL_FDRISSUEDDATEMONTH.Items.Add(new ListItem("- PILIH -", ""));
				DDL_CL_FDREXPIREDDATEMONTH.Items.Add(new ListItem("- PILIH -", ""));
				DDL_CL_RELATIONSHIP.Items.Add(new ListItem("- PILIH -", ""));
				DDL_CL_REVIEWDATEMONTH.Items.Add(new ListItem("- PILIH -", ""));
				DDL_CL_GUARANTEEDUEDATEMONTH.Items.Add(new ListItem("- PILIH -", ""));

				for (int i=1; i<=12; i++)
				{
					DDL_CL_FDRISSUEDDATEMONTH.Items.Add(new ListItem(DateAndTime.MonthName(i, false), i.ToString()));
					DDL_CL_FDREXPIREDDATEMONTH.Items.Add(new ListItem(DateAndTime.MonthName(i, false), i.ToString()));
					DDL_CL_REVIEWDATEMONTH.Items.Add(new ListItem(DateAndTime.MonthName(i, false), i.ToString()));
					DDL_CL_GUARANTEEDUEDATEMONTH.Items.Add(new ListItem(DateAndTime.MonthName(i, false), i.ToString()));
				}

				//--- Mata Uang
				conn.QueryString = "select CURRENCYID, CURRENCYID + ' - ' + CURRENCYDESC as CURRENCYDESC from RFCURRENCY where ACTIVE = '1' order by currencyid";
				conn.ExecuteQuery();
				for (int i=0; i< conn.GetRowCount(); i++)
				{
					DDL_CL_CURRENCY.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));
					DDL_CL_REKCURRENCY.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));
				}

				conn.QueryString = "select COLCLASSID, COLCLASSDESC from RFCOLLCLASS where active='1'";
				conn.ExecuteQuery();
				for (int i=0; i< conn.GetRowCount(); i++)
					DDL_CL_COLCLASSIFY.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));

				conn.QueryString = "select ACCRDTOID, ACCRDTODESC from RFVALUEACCORDING where active='1'";
				conn.ExecuteQuery();
				for (int i=0; i< conn.GetRowCount(); i++)
					DDL_CL_VALACCRDTO.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));

				//--- Jenis Agunan
				conn.QueryString = "select AGUNANID, AGUNANID + ' - ' + AGUNANDESC AS AGUNANDESC from RFJENISAGUNAN WHERE ACTIVE='1' order by AGUNANID";
					//"where COLTYPEID = '"+ CL_TYPE +"'";
				conn.ExecuteQuery();
				for (int i=0; i< conn.GetRowCount(); i++)
					DDL_CL_JNSAGUNAN.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));

				//--- Jenis Pengikatan
				conn.QueryString = "select IKATID, IKATID + ' - ' + IKATDESC AS IKATDESC from RFIKAT where active = '1' order by IKATID";
				conn.ExecuteQuery();
				for (int i=0; i< conn.GetRowCount(); i++)
					DDL_CL_IKATTYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));

				//--- Hubungan
				conn.QueryString = "select RELTYPEID, RELTYPEID+' - '+RELTYPEDESC from RFRELATIONSHIP where active='1'";
				conn.ExecuteQuery();
				for (int i=0; i< conn.GetRowCount(); i++)
					DDL_CL_RELATIONSHIP.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));
				
				string CL_SEQ = LBL_CL_SEQ.Text;
				if (CL_SEQ != "")
					ViewData();
			}
			else
			{
				Update();
			}
		}

		private void ViewData()
		{
			conn.QueryString = "select * from VW_COLL_DEP "+
				"where CUREF ='" + LBL_CUREF.Text + "' and CLSEQ = "+ Convert.ToInt16(LBL_CL_SEQ.Text);
			conn.ExecuteQuery();
			TXT_CL_VALUE.Text			= tool.MoneyFormat(conn.GetFieldValue("CL_PROPVALUE"));
			TXT_CL_VALUE2.Text			= tool.MoneyFormat(conn.GetFieldValue("CL_VALUE2"));
			TXT_CL_VALUEINS.Text		= tool.MoneyFormat(conn.GetFieldValue("CL_VALUEINS"));
			TXT_CL_VALUEIKAT.Text		= tool.MoneyFormat(conn.GetFieldValue("CL_VALUEIKAT"));
			TXT_CL_VALUEPPA.Text		= tool.MoneyFormat(conn.GetFieldValue("CL_VALUEPPA"));
			TXT_CL_VALUELIQ.Text		= tool.MoneyFormat(conn.GetFieldValue("CL_VALUELIQ"));
			TXT_CL_DESC.Text			= conn.GetFieldValue("CL_DESC");
			try
			{
				DDL_CL_CURRENCY.SelectedValue		= conn.GetFieldValue("CL_CURRENCY");
			} 
			catch {}
			TXT_CL_EXCHANGERATE.Text		= tool.MoneyFormat(conn.GetFieldValue("CL_EXCHANGERATE"));
			TXT_CL_FOREIGNVAL.Text			= tool.MoneyFormat(conn.GetFieldValue("CL_FOREIGNVAL"));
			try
			{
				DDL_CL_COLCLASSIFY.SelectedValue	= conn.GetFieldValue("CL_COLCLASSIFY");
			} 
			catch {}
			TXT_SIBS_COLID.Text			= conn.GetFieldValue("SIBS_COLID");
			//DDL_CL_VALACCRDTO.SelectedValue		= conn.GetFieldValue("CL_VALACCRDTO");
			string V_CL_ISCASHEDVALUE;
			V_CL_ISCASHEDVALUE = conn.GetFieldValue("CL_ISCASHEDVALUE");
			if (V_CL_ISCASHEDVALUE == "1")
				CHB_CL_ISCASHEDVALUE.Checked = true;
			try
			{
				DDL_CL_JNSAGUNAN.SelectedValue		= conn.GetFieldValue("CL_JNSAGUNAN"); 
			} 
			catch {}
			try
			{
				DDL_CL_IKATTYPE.SelectedValue		= conn.GetFieldValue("CL_IKATTYPE");
			} 
			catch {}
			try
			{
				DDL_CL_BANK.SelectedValue	= conn.GetFieldValue("CL_BANK"); 
			} 
			catch {}
			TXT_CL_REKNUM.Text			= conn.GetFieldValue("CL_REKNUM");
			try
			{
				DDL_CL_REKTYPE.SelectedValue		= conn.GetFieldValue("CL_REKTYPE"); 
			} 
			catch {}
			try
			{
				DDL_CL_REKCURRENCY.SelectedValue	= conn.GetFieldValue("CL_REKCURRENCY");
			} 
			catch {}
			TXT_CL_FDRRATE.Text			= conn.GetFieldValue("CL_FDRRATE");
			TXT_CL_FDRISSUEDBY.Text		= conn.GetFieldValue("CL_FDRISSUEDBY"); 
			string CL_FDRISSUEDDATE		= conn.GetFieldValue("CL_FDRISSUEDDATE");
			TXT_CL_FDRISSUEDDATEDAY.Text	= tool.FormatDate_Day(CL_FDRISSUEDDATE);
			try
			{
				DDL_CL_FDRISSUEDDATEMONTH.SelectedValue = tool.FormatDate_Month(CL_FDRISSUEDDATE);
			} 
			catch {}
			TXT_CL_FDRISSUEDDATEYEAR.Text	= tool.FormatDate_Year(CL_FDRISSUEDDATE);
			string CL_FDREXPIREDDATE		= conn.GetFieldValue("CL_FDREXPIREDDATE");
			TXT_CL_FDREXPIREDDATEDAY.Text	= tool.FormatDate_Day(CL_FDREXPIREDDATE);
			try
			{
				DDL_CL_FDREXPIREDDATEMONTH.SelectedValue = tool.FormatDate_Month(CL_FDREXPIREDDATE);
			} 
			catch {}
			TXT_CL_FDREXPIREDDATEYEAR.Text	= tool.FormatDate_Year(CL_FDREXPIREDDATE);
			TXT_CL_TENORTERM.Text		= conn.GetFieldValue("CL_TENORTERM");
			TXT_CL_SPRDRATE.Text		= conn.GetFieldValue("CL_SPRDRATE");
			TXT_CL_REKBOOKNUM.Text		= conn.GetFieldValue("CL_REKBOOKNUM"); 
			try
			{
				DDL_CL_RELATIONSHIP.SelectedValue = conn.GetFieldValue("CL_RELATIONSHIP");
			} 
			catch {}
			TXT_CL_OWNERNMFIRST.Text	= conn.GetFieldValue("CL_OWNERNMFIRST"); 
			TXT_CL_OWNERNMMID.Text		= conn.GetFieldValue("CL_OWNERNMMID");
			TXT_CL_OWNERNMLAST.Text		= conn.GetFieldValue("CL_OWNERNMLAST"); 
			string CL_REVIEWDATE		= conn.GetFieldValue("CL_REVIEWDATE");
			TXT_CL_REVIEWDATEDAY.Text	= tool.FormatDate_Day(CL_REVIEWDATE);
			try
			{
				DDL_CL_REVIEWDATEMONTH.SelectedValue = tool.FormatDate_Month(CL_REVIEWDATE);
			} 
			catch {}
			TXT_CL_REVIEWDATEYEAR.Text	= tool.FormatDate_Year(CL_REVIEWDATE);
			TXT_CL_GUARANTEEVAL.Text	= tool.MoneyFormat(conn.GetFieldValue("CL_GUARANTEEVAL"));
			string CL_GUARANTEEDUEDATE		= conn.GetFieldValue("CL_GUARANTEEDUEDATE");
			TXT_CL_GUARANTEEDUEDATEDAY.Text	= tool.FormatDate_Day(CL_GUARANTEEDUEDATE);
			try
			{
				DDL_CL_GUARANTEEDUEDATEMONTH.SelectedValue = tool.FormatDate_Month(CL_GUARANTEEDUEDATE);
			} 
			catch {}
			TXT_CL_GUARANTEEDUEDATEYEAR.Text	= tool.FormatDate_Year(CL_GUARANTEEDUEDATE);
			TXT_CL_INSTDUE.Text			= conn.GetFieldValue("CL_INSTDUE");

			string CL_VALACCRDTO = conn.GetFieldValue("CL_VALACCRDTO");
			conn.QueryString = "select isnull(LA_APPRTYPE,'0') from LISTASSIGNMENT where AP_REGNO = '"+Request.QueryString["regno"]+
				"' and CU_REF = '" +Request.QueryString["curef"]+ "' and CL_SEQ = "+Convert.ToInt16(LBL_CL_SEQ.Text);
			conn.ExecuteQuery();
			if (conn.GetRowCount() != 0)
			{
				if (conn.GetFieldValue(0,0) == "1")
					CL_VALACCRDTO = "2";
				else if (conn.GetFieldValue(0,0) == "0" || conn.GetFieldValue(0,0) == "2")
					CL_VALACCRDTO = "1";
			}
			try
			{
				DDL_CL_VALACCRDTO.SelectedValue	= CL_VALACCRDTO.ToString().Trim();
			} 
			catch {}
		}

		private void Update()
		{
			conn.QueryString = "exec DE_COLL_DEP '"+ LBL_CUREF.Text +"', "+ LBL_CL_SEQ.Text +", 0, "+
				tool.ConvertFloat(TXT_CL_VALUE.Text) +", "+ 
				tool.ConvertFloat(TXT_CL_VALUE2.Text) +", "+ tool.ConvertFloat(TXT_CL_VALUEINS.Text) +", "+ tool.ConvertFloat(TXT_CL_VALUEIKAT.Text) +", "+ tool.ConvertFloat(TXT_CL_VALUEPPA.Text) +", "+ tool.ConvertFloat(TXT_CL_VALUELIQ.Text) +", '"+ 
				TXT_CL_DESC.Text +"', "+ tool.ConvertNull(DDL_CL_CURRENCY.SelectedValue) +", "+ 
				tool.ConvertNull(DDL_CL_COLCLASSIFY.SelectedValue) +", '"+ TXT_SIBS_COLID.Text +"', "+
				tool.ConvertFloat(TXT_CL_FOREIGNVAL.Text) +", "+ tool.ConvertFloat(TXT_CL_EXCHANGERATE.Text) +", "+ 
				tool.ConvertNull(DDL_CL_VALACCRDTO.SelectedValue) +", '"+
				tool.ConvertFlag(CHB_CL_ISCASHEDVALUE.Checked) +"', "+ tool.ConvertNull(DDL_CL_JNSAGUNAN.SelectedValue) +", "+
				tool.ConvertNull(DDL_CL_IKATTYPE.SelectedValue) +", "+ tool.ConvertNull(DDL_CL_BANK.SelectedValue) +", '"+
				TXT_CL_REKNUM.Text +"', "+ tool.ConvertNull(DDL_CL_REKTYPE.SelectedValue) +", "+
				tool.ConvertNull(DDL_CL_REKCURRENCY.SelectedValue) +", "+
				tool.ConvertFloat(TXT_CL_FDRRATE.Text) +", '"+ TXT_CL_FDRISSUEDBY.Text +"', "+
				tool.ConvertDate(TXT_CL_FDRISSUEDDATEDAY.Text, DDL_CL_FDRISSUEDDATEMONTH.SelectedValue, TXT_CL_FDRISSUEDDATEYEAR.Text) +", "+
				tool.ConvertDate(TXT_CL_FDREXPIREDDATEDAY.Text, DDL_CL_FDREXPIREDDATEMONTH.SelectedValue, TXT_CL_FDREXPIREDDATEYEAR.Text) +", "+ 
				tool.ConvertNull(TXT_CL_TENORTERM.Text) +", "+ tool.ConvertFloat(TXT_CL_SPRDRATE.Text) +", '"+ TXT_CL_REKBOOKNUM.Text +"', "+
				tool.ConvertNull(DDL_CL_RELATIONSHIP.SelectedValue) +", '"+ TXT_CL_OWNERNMFIRST.Text +"', '"+
				TXT_CL_OWNERNMMID.Text +"', '"+ TXT_CL_OWNERNMLAST.Text +"', "+
				tool.ConvertDate(TXT_CL_REVIEWDATEDAY.Text, DDL_CL_REVIEWDATEMONTH.SelectedValue, TXT_CL_REVIEWDATEYEAR.Text) +", "+
				tool.ConvertFloat(TXT_CL_GUARANTEEVAL.Text) +", "+
				tool.ConvertDate(TXT_CL_GUARANTEEDUEDATEDAY.Text, DDL_CL_GUARANTEEDUEDATEMONTH.SelectedValue, TXT_CL_GUARANTEEDUEDATEYEAR.Text) +", '"+
				TXT_CL_INSTDUE.Text +"', '"+LBL_REGNO.Text+ "'";
			conn.ExecuteNonQuery();
			ViewData();
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		protected void Button1_ServerClick(object sender, System.EventArgs e)
		{
		
		}
	}
}
