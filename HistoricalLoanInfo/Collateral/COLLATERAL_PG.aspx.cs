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

namespace SME.HistoricalLoanInfo.Collateral
{
	/// <summary>
	/// Summary description for COLLATERAL_PG.
	/// </summary>
	public partial class COLLATERAL_PG : System.Web.UI.Page
	{
		//protected Connection conn = new Connection("Data Source=10.123.12.30;Initial Catalog=SMEDEV;uid=sa;pwd=");
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
				string CL_TYPE	= Request.QueryString["coltypeid"];
							
				DDL_CL_CURRENCY.Items.Add(new ListItem("- PILIH -", ""));
				DDL_CL_COLCLASSIFY.Items.Add(new ListItem("- PILIH -", ""));
				DDL_CL_RELATIONSHIP.Items.Add(new ListItem("- PILIH -", ""));
				DDL_CL_CONTRACTDATEMONTH.Items.Add(new ListItem("- PILIH -", ""));
				DDL_CL_DUEDATEMONTH.Items.Add(new ListItem("- PILIH -", ""));
				DDL_CL_VALACCRDTO.Items.Add(new ListItem("- PILIH -", ""));
				DDL_CL_JNSAGUNAN.Items.Add(new ListItem("- PILIH -", ""));

				for (int i=1; i<=12; i++)
				{
					DDL_CL_CONTRACTDATEMONTH.Items.Add(new ListItem(DateAndTime.MonthName(i, false), i.ToString()));
					DDL_CL_DUEDATEMONTH.Items.Add(new ListItem(DateAndTime.MonthName(i, false), i.ToString()));
				}

				//--- Mata Uang
				conn.QueryString = "select CURRENCYID, CURRENCYID + ' - ' + CURRENCYDESC as CURRENCYDESC from RFCURRENCY where ACTIVE = '1' order by CURRENCYID";
				conn.ExecuteQuery();
				for (int i=0; i< conn.GetRowCount(); i++)
					DDL_CL_CURRENCY.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));
				
				conn.QueryString = "select COLCLASSID, COLCLASSDESC from RFCOLLCLASS where  active='1'";
				conn.ExecuteQuery();
				for (int i=0; i< conn.GetRowCount(); i++)
					DDL_CL_COLCLASSIFY.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));

				//--- Hubungan
				conn.QueryString = "select RELTYPEID, RELTYPEID+' - '+RELTYPEDESC from RFRELATIONSHIP where active='1'";
				conn.ExecuteQuery();
				for (int i=0; i< conn.GetRowCount(); i++)
					DDL_CL_RELATIONSHIP.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));

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
				
				string CL_SEQ = LBL_CL_SEQ.Text;
				if (CL_SEQ != "")
					ViewData();
				this.SecureData();
			}
			else
			{
				Update();
				this.updateCollStatus();
			}
		}

		private void updateCollStatus() 
		{
			//------ Jika ingin melakukan appraisal ----------
			//------ status collateral ubah jadi '3' --------
			if (Request.QueryString["appr"] == "1") 
			{
				conn.QueryString = "exec VA_APPR_COLL_STATUS '" + Request.QueryString["regno"] + "','" + Request.QueryString["curef"] + "','" + Request.QueryString["CL_SEQ"] + "'";
				conn.ExecuteNonQuery();
			}
			//------------------------------------------------
		}

		private void SecureData() 
		{
			// Jika yang memanggil bukan dalam scope DataEntry, maka disable semua control input
			// Flag :
			//		Nama  = de
			//		Value ==  1 --> Parent DataEntry
			//			  !=  1 --> Parent non-DataEntry
			string de = Request.QueryString["de"];
			if (de != "1") 
			{
				System.Web.UI.ControlCollection coll = this.Page.Controls;
				for (int i = 0; i < coll[4].Controls.Count; i++) 
				{
					if (coll[4].Controls[i] is TextBox) 
					{
						TextBox txt = (TextBox) coll[4].Controls[i];
						txt.ReadOnly = true;
						//txt.Enabled = false;
					}
					else if (coll[4].Controls[i] is DropDownList) 
					{
						DropDownList ddl = (DropDownList) coll[4].Controls[i];
						ddl.Enabled = false;
					}
					else if (coll[4].Controls[i] is Button)
					{
						Button btn = (Button) coll[4].Controls[i];
						//btn.Enabled = false;
						btn.Visible = false;
					}
					else if (coll[4].Controls[i] is RadioButtonList) 
					{
						RadioButtonList rbl = (RadioButtonList) coll[4].Controls[i];
						rbl.Enabled = false;
					}
					else if (coll[4].Controls[i] is RadioButton) 
					{
						RadioButton rb = (RadioButton) coll[4].Controls[i];
						rb.Enabled = false;
					}
					else if (coll[4].Controls[i] is CheckBox)
					{
						CheckBox cb = (CheckBox) coll[4].Controls[i];
						cb.Enabled = false;
					}					
					else if (coll[4].Controls[i] is DataGrid) 
					{						
						DataGrid dg = (DataGrid) coll[4].Controls[i];						
						/*
						try 
						{
							for(int j=0; j < dg.Items.Count; j++) 
							{
								dg.Items[i].Enabled	= false;
							}
						}
						catch (ArgumentOutOfRangeException ex) 
						{
							// ignore...
						}
						*/
					}
					else if (coll[4].Controls[i] is HtmlTableRow) 
					{
						HtmlTableRow htr = (HtmlTableRow) coll[4].Controls[i];
						//htr.Disabled = true;	

						for (int j=0; j < htr.Controls.Count; j++) 
						{							
							for (int jj = 0; jj < htr.Controls[j].Controls.Count; jj++) 
							{
								if (htr.Controls[j].Controls[jj] is TextBox) 
								{
									TextBox txt = (TextBox) htr.Controls[j].Controls[jj];
									txt.ReadOnly = true;
									//txt.Enabled = false;
								}
								else if (htr.Controls[j].Controls[jj] is DropDownList) 
								{
									DropDownList ddl = (DropDownList) htr.Controls[j].Controls[jj];
									ddl.Enabled = false;
								}
								else if (htr.Controls[j].Controls[jj] is Button)
								{
									Button btn = (Button) htr.Controls[j].Controls[jj];
									//btn.Enabled = false;
									btn.Visible = false;
								}
								else if (htr.Controls[j].Controls[jj] is RadioButtonList) 
								{
									RadioButtonList rbl = (RadioButtonList) htr.Controls[j].Controls[jj];
									rbl.Enabled = false;
								}
								else if (htr.Controls[j].Controls[jj] is RadioButton) 
								{
									RadioButton rb = (RadioButton) htr.Controls[j].Controls[jj];
									rb.Enabled = false;
								}
								else if (htr.Controls[j].Controls[jj] is CheckBox)
								{
									CheckBox cb = (CheckBox) htr.Controls[j].Controls[jj];
									cb.Enabled = false;
								}					
							}
						}
					}
				}
			}
		}

		private void ViewData()
		{
			conn.QueryString = "select * from VW_BACKUP_COLL_PG "+
				"where CUREF ='" + LBL_CUREF.Text + "' and CLSEQ = "+ LBL_CL_SEQ.Text;
			conn.ExecuteQuery();

			TXT_CL_VALUE.Text			= tool.MoneyFormat(conn.GetFieldValue("CL_PROPVALUE"));
			TXT_CL_DESC.Text			= conn.GetFieldValue("CL_DESC");
			try{DDL_CL_CURRENCY.SelectedValue	= conn.GetFieldValue("CL_CURRENCY");}
			catch{}
			DDL_CL_COLCLASSIFY.SelectedValue	= conn.GetFieldValue("CL_COLCLASSIFY");
			TXT_SIBS_COLID.Text			= conn.GetFieldValue("SIBS_COLID");
			DDL_CL_RELATIONSHIP.SelectedValue	= conn.GetFieldValue("CL_RELATIONSHIP");
			string CL_CONTRACTDATE		= conn.GetFieldValue("CL_CONTRACTDATE");
			TXT_CL_CONTRACTDATEDAY.Text	= tool.FormatDate_Day(CL_CONTRACTDATE);
			TXT_CL_CONTRACTDATEYEAR.Text= tool.FormatDate_Year(CL_CONTRACTDATE);
			DDL_CL_CONTRACTDATEMONTH.SelectedValue	= tool.FormatDate_Month(CL_CONTRACTDATE);
			string CL_DUEDATE		= conn.GetFieldValue("CL_DUEDATE");
			TXT_CL_DUEDATEDAY.Text	= tool.FormatDate_Day(CL_DUEDATE);
			TXT_CL_DUEDATEYEAR.Text	= tool.FormatDate_Year(CL_DUEDATE);
			DDL_CL_DUEDATEMONTH.SelectedValue	= tool.FormatDate_Month(CL_DUEDATE);
			TXT_CL_LOGBOOKNO.Text	= conn.GetFieldValue("CL_LOGBOOKNO");
			TXT_CL_GUARANTEEVAL.Text	= tool.MoneyFormat(conn.GetFieldValue("CL_GUARANTEEVAL"));
			TXT_CL_GUARANTEENAME.Text	= conn.GetFieldValue("CL_GUARANTEENAME");
			//DDL_CL_VALACCRDTO.SelectedValue	= conn.GetFieldValue("CL_VALACCRDTO");
			try{DDL_CL_JNSAGUNAN.SelectedValue	= conn.GetFieldValue("CL_JNSAGUNAN");}
			catch{}


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
			DDL_CL_VALACCRDTO.SelectedValue	= CL_VALACCRDTO.ToString().Trim();
		}

		private void Update()
		{
			if (DDL_CL_RELATIONSHIP.SelectedValue.Trim() != "" && TXT_CL_GUARANTEENAME.Text.Trim() == "")
				Tools.popMessage(this,"Nama Pemilik Tidak Boleh Kosong !!");
			else
			{
				try 
				{
					conn.QueryString = "exec DE_COLL_PG '"+ LBL_CUREF.Text +"', "+ LBL_CL_SEQ.Text +", 0, "+
						tool.ConvertFloat(TXT_CL_VALUE.Text) +", '"+ TXT_CL_DESC.Text +"', "+ tool.ConvertNull(DDL_CL_CURRENCY.SelectedValue) +", "+
						tool.ConvertNull(DDL_CL_COLCLASSIFY.SelectedValue) +", '"+ TXT_SIBS_COLID.Text +"', "+ tool.ConvertNull(DDL_CL_RELATIONSHIP.SelectedValue) +", '"+
						TXT_CL_LOGBOOKNO.Text +"', "+ tool.ConvertFloat(TXT_CL_GUARANTEEVAL.Text) +", "+
						tool.ConvertDate(TXT_CL_CONTRACTDATEDAY.Text, DDL_CL_CONTRACTDATEMONTH.SelectedValue, TXT_CL_CONTRACTDATEYEAR.Text) +", "+
						tool.ConvertDate(TXT_CL_DUEDATEDAY.Text, DDL_CL_DUEDATEMONTH.SelectedValue, TXT_CL_DUEDATEYEAR.Text) +", "+
						tool.ConvertNull(DDL_CL_VALACCRDTO.SelectedValue) +", '"+ TXT_CL_GUARANTEENAME.Text +"', "+
						tool.ConvertNull(DDL_CL_JNSAGUNAN.SelectedValue)+", '"+LBL_REGNO.Text+ "'";
					conn.ExecuteNonQuery();
				} 
				catch 
				{
					Tools.popMessage(this, "Input tidak valid !");
					return;
				}
				ViewData();
			}
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
	}
}
