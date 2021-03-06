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
using DMS.CuBESCore;
using DMS.DBConnection;
using Microsoft.VisualBasic;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using System.Net;
using SME.SourceSMEReport;

namespace SME.SourceSMEReport
{
	/// <summary>
	/// Summary description for RptOverallSLA.
	/// </summary>
	public partial class RptOverallSLA : System.Web.UI.Page
	{

		protected Connection Conn = new Connection();
		
		protected Tools tools = new Tools();
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string vRMCODE ="";
			Conn = (Connection) Session["Connection"];

			//if (!Logic.AllowAccess(Session["GroupID"].ToString(), Request.QueryString["mc"], Conn))
			//Response.Redirect("/SME/Restricted.aspx");

			LBL_BU.Text = Request.QueryString["BU"];
			Conn.QueryString = "select rmcode from app_parameter ";
			Conn.ExecuteQuery();
			if (Conn.GetRowCount()>0)
			{
				vRMCODE = Conn.GetFieldValue(0,0);
			}

			if (!IsPostBack)
			{
				DDL_BLN1.Items.Add(new ListItem("-- PILIH --",""));
				DDL_BLN2.Items.Add(new ListItem("-- PILIH --",""));
				for (int i = 1; i <= 12; i++)
				{
					DDL_BLN1.Items.Add(new ListItem(DateAndTime.MonthName(i, false), i.ToString()));
					DDL_BLN2.Items.Add(new ListItem(DateAndTime.MonthName(i, false), i.ToString()));
				}

				TXT_TGL1.Text=DateAndTime.Today.Day.ToString();
				DDL_BLN1.SelectedValue=DateAndTime.Today.Month.ToString();
				TXT_THN1.Text=DateAndTime.Today.Year.ToString();
				TXT_TGL2.Text=DateAndTime.Today.Day.ToString();
				DDL_BLN2.SelectedValue=DateAndTime.Today.Month.ToString();
				TXT_THN2.Text=DateAndTime.Today.Year.ToString();


				LBL_RM.Text = "'0002','0004'";
				Conn.QueryString = "select rmcode from app_parameter ";
				Conn.ExecuteQuery();
				if (Conn.GetRowCount()>0)
				{
					LBL_RM.Text = Conn.GetFieldValue(0,0);
				}

				Label1.Text = Posisi_User().ToString();
				fillRegion();
				FillProgram();
				
				fillRM();

				/* // Nama-nama RM berdasarkan region dan branch yg terpilih
				 * 
				Conn.QueryString = "select userid, su_fullname from scuser where groupID in (" + vRMCODE + ")";
				Conn.ExecuteQuery();
				for (int i = 0; i < Conn.GetRowCount(); i++)
				{
					DDL_RM.Items.Add(new ListItem(Conn.GetFieldValue(i,1),Conn.GetFieldValue(i,0)));
				}*/
			}

		}

		////////////////////////////////////////////////////////////////////////////////////////////////////
		#region My Method 

		private void fillRegion()
		{
			DDL_REGION.Items.Clear();
			switch(Label1.Text)
			{
				case "1": case "2": case "3":
					Conn.QueryString = "select AreaID, AREANAME from rfarea where AreaID='" + Session["AreaID"].ToString() + "' ";
					break;
					/*
				case "3": 
					Conn.QueryString = "select AreaID, AREANAME from rfarea where arearegmanager='" + Session["UserID"].ToString() + "' ";
					break;
					*/
				default:
					Conn.QueryString = "select AreaID, AREANAME from rfarea";
					DDL_REGION.Items.Add(new ListItem("-- PILIH --",""));
					break;
			}
			Conn.ExecuteQuery();
			for (int i = 0; i < Conn.GetRowCount(); i++)
			{
				DDL_REGION.Items.Add(new ListItem(Conn.GetFieldValue(i,1),Conn.GetFieldValue(i,0)));
			}
			fillCBC();
		}

		private void fillCBC()
		{
			DDL_CBC.Items.Clear();
			switch(Label1.Text)
			{
				case "1": 
					/*
					Conn.QueryString = "select distinct cbc_code, (select branch_name from rfbranch A where A.branch_code=b.cbc_code) branch_name "+
						"from rfbranch b left join rfcity c on b.cityid=c.cityid where cbc_Code is not null and cbc_code <>'' and b.areaid = '" + DDL_REGION.SelectedValue + "' "+
						"and b.Branch_CODE='" + Session["BranchID"].ToString() + "' ";
						*/
					Conn.QueryString = "select cbc.branch_code as cbc_code, cbc.branch_name as branch_name  from rfbranch b " +
						" left join rfbranch cbc on cbc.branch_code = b.cbc_code " +
						" where b.Branch_CODE='" + Session["BranchID"].ToString() + "' ";

					break;

				case "2":
					/* 
					Conn.QueryString = "select distinct cbc_code, (select branch_name from rfbranch A where A.branch_code=b.cbc_code) branch_name "+
						"from rfbranch b left join rfcity c on b.cityid=c.cityid where cbc_Code is not null and cbc_code <>'' and b.areaid = '" + DDL_REGION.SelectedValue + "' "+
						"and b.CBC_code='" + Session["CBC"].ToString() + "' ";
						*/
					Conn.QueryString = "select branch_code as cbc_code, branch_name as branch_name  from rfbranch  " +
						" where branch_code = cbc_code and CBC_code='" + Session["CBC"].ToString() + "' ";
					break;

				case "3":
					Conn.QueryString = "select branch_code as cbc_code, branch_name as branch_name  from rfbranch  " +
						" where branch_code = cbc_code and areaid  ='" + Session["AreaID"].ToString() + "' ";
					break;
					/*case "3": 
						Conn.QueryString = "select distinct cbc_code, (select branch_name from rfbranch A where A.branch_code=b.cbc_code) branch_name "+
							"from rfbranch b left join rfcity c on b.cityid=c.cityid where cbc_Code is not null and c.areaid = '" + DDL_REGION.SelectedValue + "' "+
							"and b.AreaID='" + Session["AreaID"].ToString() + "' ";
						DDL_CBC.Items.Add(new ListItem("-- PILIH --",""));
						break;*/

				default:
					Conn.QueryString = "select branch_code as cbc_code, branch_name as branch_name  from rfbranch  " +
						" where branch_code = cbc_code and areaid ='" +  DDL_REGION.SelectedValue  + "' ";
					/*Conn.QueryString = "select distinct cbc_code, (select branch_name from rfbranch A where A.branch_code=b.cbc_code) branch_name "+
						"from rfbranch b left join rfcity c on b.cityid=c.cityid where cbc_Code is not null and cbc_code <>'' and b.areaid = '" + DDL_REGION.SelectedValue + "' ";
						*/
					DDL_CBC.Items.Add(new ListItem("-- PILIH --",""));
					break;
			}

			if(DDL_REGION.SelectedValue != "")
			{
				Conn.ExecuteQuery();
				for (int i = 0; i < Conn.GetRowCount(); i++)
				{
					DDL_CBC.Items.Add(new ListItem(Conn.GetFieldValue(i,1),Conn.GetFieldValue(i,0)));
				}
			}
			fillBranch();
		}

		private void fillBranch()
		{
			DDL_BRANCH.Items.Clear();
			switch(Label1.Text)
			{
				case "1": 
					Conn.QueryString = "SELECT BRANCH_CODE, BRANCH_NAME FROM RFBRANCH where cbc_code='" +
						DDL_CBC.SelectedValue + "' and Branch_Code='" + Session["BranchID"].ToString() + "' ";
					break;
					/*case "2":
						Conn.QueryString = "SELECT BRANCH_CODE, BRANCH_NAME FROM RFBRANCH where cbc_code='" + 
							DDL_CBC.SelectedValue + "' and CBC_Code='" + Session["CBC"].ToString() + "' ";
						DDL_BRANCH.Items.Add(new ListItem("-- PILIH --",""));
						break;
					case "3": 
						Conn.QueryString =  "SELECT BRANCH_CODE, BRANCH_NAME FROM RFBRANCH where cbc_code='" +
							DDL_CBC.SelectedValue + "' ";//and areaid='" + Session["AreaID"].ToString() + "' ";
						DDL_BRANCH.Items.Add(new ListItem("-- PILIH --",""));
						break;*/
				default:
					Conn.QueryString =  "SELECT BRANCH_CODE, BRANCH_NAME FROM RFBRANCH where cbc_code='" + 
						DDL_CBC.SelectedValue + "'";
					DDL_BRANCH.Items.Add(new ListItem("-- PILIH --",""));
					break;
			}
			if(DDL_CBC.SelectedValue != "")
			{
				Conn.ExecuteQuery();
				for (int i = 0; i < Conn.GetRowCount(); i++)
				{
					DDL_BRANCH.Items.Add(new ListItem(Conn.GetFieldValue(i,1),Conn.GetFieldValue(i,0)));
				}
			}

			//fillTeamLeader();
			//fillRM();
		}

		private void fillTeamLeader()
		{
			GlobalTools.fillRefList(DDL_TEAM, "exec RPT_GETTEAMLEADER '" + DDL_BRANCH.SelectedValue + "' ", false, Conn);

			/**
			DDL_TEAM.Items.Clear();
			Conn.QueryString = "select distinct a.su_teamleader, b.su_fullname "+
				"from scuser A left join scuser B on b.userid = a.su_teamleader "+
				"where a.su_teamleader is not null and A.su_branch='" + DDL_BRANCH.SelectedValue + "' order by b.su_fullname";
			DDL_TEAM.Items.Add(new ListItem("-- PILIH --",""));
			if(DDL_BRANCH.SelectedValue != "")
			{
				Conn.ExecuteQuery();
				for (int i = 0; i < Conn.GetRowCount(); i++)
				{
					DDL_TEAM.Items.Add(new ListItem(Conn.GetFieldValue(i,1),Conn.GetFieldValue(i,0)));
				}
			}**/
		}

		private int Posisi_User()
		{
			string area="";
			int Posisi;
            if ((Session["BranchID"].ToString() == "000") || (Session["BranchID"].ToString() == "001"))
			{ 
				//Head Office
				Posisi = 0;
			}
			else
			{
				// Area Manager ....
				//Conn.QueryString = "select * from rfarea where arearegmanager='" + Session["UserID"].ToString() + "' ";
				Conn.QueryString = "select * from scuser where userid ='" + Session["UserID"].ToString() + "' " +
					"and groupid in ( select groupid from scgroup_init2 where GR_KEY = 'AREA_MGR')";
				Conn.ExecuteQuery();
				if (Conn.GetRowCount()>0)
					area="yup";
				else
					area="nop";

				if (area=="yup")
				{
					Posisi=3;
				}//aa
				else
				{
					if (Session["BranchID"].ToString()==Session["CBC"].ToString()) //(Session["GroupID"].ToString().StartsWith("01"))
					{
						//CBC
						Posisi=2;
					}
					else
					{
						//Branch
						Posisi = 1;
					}
				}
			}
			return Posisi;
		}

		private void fillRM()
		{
			DDL_RM.Items.Clear();
			Conn.QueryString = "select userid, su_fullname from scuser "+
				"left join rfbranch on scuser.su_branch=rfbranch.branch_code "+
				"where groupid in (" + LBL_RM.Text + ") and areaid='" + DDL_REGION.SelectedValue +
				"' and scuser.su_branch='" + DDL_BRANCH.SelectedValue + "' order by su_fullname asc";
			DDL_RM.Items.Add(new ListItem("-- PILIH --",""));
			if(DDL_BRANCH.SelectedValue != "")
			{
				Conn.ExecuteQuery();
				for (int i = 0; i < Conn.GetRowCount(); i++)
				{
					DDL_RM.Items.Add(new ListItem(Conn.GetFieldValue(i,1),Conn.GetFieldValue(i,0)));
				}
			}
		}

		#endregion
		////////////////////////////////////////////////////////////////////////////////////////////////////

		private void LoadReport_Load(string action,string tgl1, string tgl2, string branch, string rm, string area, string cbc, string teamleader, string program)
		{	
			string kriterianya="", tanggal1_k="", tanggal2_k="";
			/*
			string tanggal1	= tools.ConvertDate(tgl1);
			string tanggal2	= tools.ConvertDate(tgl2);
			*/
			string tanggal1	= tgl1;
			string tanggal2	= tgl2;
			 
			tanggal1		= tanggal1.Replace("'","");
			tanggal2		= tanggal2.Replace("'","");

            IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.AppSettings["ReportUser"].ToString(), WebConfigurationManager.AppSettings["ReportPassword"].ToString(), WebConfigurationManager.AppSettings["DomainName"].ToString());

            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            ReportViewer1.ServerReport.ReportServerUrl = new Uri(WebConfigurationManager.AppSettings["ServerUrl"].ToString());
            
            /*
			if (!Information.IsDate(tanggal1))
			{
				tanggal1	= DateTime.Today.ToString();
				tanggal1_k = Tools.toISODate(DateTime.Today.Day.ToString(),DateTime.Today.Month.ToString() ,DateTime.Today.Year.ToString());
			}
			else
			*/
			{
				tanggal1_k = Tools.toISODate(DateTime.Parse(tanggal1.ToString()).Day.ToString(),DateTime.Parse(tanggal1.ToString()).Month.ToString(), DateTime.Parse(tanggal1.ToString()).Year.ToString());
			}
			/*
			if (!Information.IsDate(tanggal2))
			{
				tanggal2	= DateTime.Today.ToString();
				tanggal2_k = Tools.toISODate(DateTime.Today.Day.ToString(),DateTime.Today.Month.ToString() ,DateTime.Today.Year.ToString());
			}
			else
			*/
			{
				tanggal2_k = Tools.toISODate(DateTime.Parse(tanggal2.ToString()).Day.ToString(), DateTime.Parse(tanggal2.ToString()).Month.ToString(), DateTime.Parse(tanggal2.ToString()).Year.ToString());
			}
            kriterianya += "AND (convert(nvarchar, TgApplikasi, 112) between " + tanggal1_k + " and " + tanggal2_k + ") ";

			if (!rm.Equals(""))
			{
				kriterianya += "AND (ap_relmngr = '" + rm + "') ";
			}
			if (!area.Equals(""))     
			{
				kriterianya += " AND (areaid = '" + area + "') ";
			}
			if (!cbc.Equals(""))
			{
				kriterianya += " AND (CBC_CODE = '" + cbc + "') ";
			}
			if (!branch.Equals(""))
			{
				kriterianya += "and (branch_code = '" + branch + "') ";
			}
			if (!teamleader.Equals(""))
			{
				kriterianya += " AND (ap_teamleader = '" + teamleader + "') ";
			}
			if (!program.Equals(""))
			{
				kriterianya += " AND (prog_code = '" + program + "') ";
			}

            if (!action.Equals("PRINT"))
            {
                //ReportViewer1.ReportPath = "/SMEReports/RptOverallSLA&sql_kondisi=" + kriterianya + "&Start_Date=" + tanggal1 + "&End_Date=" + tanggal2 + "&area=" + area + "&cbc=" + cbc + "&branch=" + branch + "&rm=" + rm + "&teamleader=" + teamleader + "&program=" + program + "&rs:Command=Render&rc:Toolbar=True";
                ReportViewer1.ServerReport.ReportPath = "/SMEReports/RptOverallSLA";

                List<ReportParameter> paramList = new List<ReportParameter>();

                paramList.Add(new ReportParameter("sql_kondisi", kriterianya, false));
                paramList.Add(new ReportParameter("area", area, false));
                paramList.Add(new ReportParameter("cbc", cbc, false));
                paramList.Add(new ReportParameter("branch", branch, false));
                paramList.Add(new ReportParameter("rm", rm, false));
                paramList.Add(new ReportParameter("Start_Date", tanggal1, false));
                paramList.Add(new ReportParameter("End_Date", tanggal2, false));
                paramList.Add(new ReportParameter("teamleader", teamleader, false));
                paramList.Add(new ReportParameter("program", program, false));

                ReportViewer1.ServerReport.SetParameters(paramList);
                ReportViewer1.ServerReport.Refresh();
            }
            else
                Response.Redirect("RptOverallSLAPrint.aspx?sql_kondisi=" + kriterianya.Replace("'", "''") + "&Start_Date=" + tanggal1 + "&End_Date=" + tanggal2 + "&area=" + area + "&cbc=" + cbc + "&branch=" + branch + "&rm=" + rm + "&teamleader=" + teamleader + "&program=" + program);
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

		protected void BTN_SAVE_Click(object sender, System.EventArgs e)
		{
			LoadSql("");
		}

		private void LoadSql(string action)
		{
			string branch	= DDL_BRANCH.SelectedValue;
			string rm		= DDL_RM.SelectedValue;
			string teamleader = DDL_TEAM.SelectedValue;
			string area		= DDL_REGION.SelectedValue;
			string cbc      = DDL_CBC.SelectedValue;
			string program  = DDL_PROGRAM.SelectedValue;

			string tanggal1 = "";
			string tanggal2 = "";
			
			if (Tools.isDateValid(this,TXT_TGL1.Text,DDL_BLN1.SelectedValue,TXT_THN1.Text)&&Tools.isDateValid(this, TXT_TGL2.Text,DDL_BLN2.SelectedValue,TXT_THN2.Text))
			{
				tanggal1 = tools.ConvertDate(TXT_TGL1.Text,DDL_BLN1.SelectedValue,TXT_THN1.Text);
				tanggal2 = tools.ConvertDate(TXT_TGL2.Text,DDL_BLN2.SelectedValue,TXT_THN2.Text);

				/*
				if (!Information.IsDate(tanggal1) || !Information.IsDate(tanggal2))
				{
					Tools.popMessage(this,"Invalid date");
					if (!Information.IsDate(tanggal1))
						Tools.SetFocus(this,TXT_TGL1);
					else
						Tools.SetFocus(this,TXT_TGL2);
				}
				else
				*/
					LoadReport_Load(action,tanggal1, tanggal2, branch, rm, area, cbc, teamleader, program);
			}
			else
			{
				Response.Write("<script language='javascript'>alert('Invalid Date !')</script>");
			}
		}

		protected void DDL_REGION_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fillCBC(); 
			fillTeamLeader();
			fillRM();
		}

		protected void DDL_CBC_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fillBranch();
			fillTeamLeader();
			fillRM();
		}

		protected void DDL_BRANCH_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			fillTeamLeader();
			fillRM();
		}

		private void FillProgram()
		{
			string BusinessUnit ="";
			try 
			{
				BusinessUnit = Request.QueryString["BU"];
			}
			catch{ BusinessUnit =""; }

			if (BusinessUnit.ToString().Trim() == "CB100")
				Conn.QueryString = "SELECT DISTINCT PROGRAMID, PROGRAMDESC FROM RFPROGRAM " + 
					" where businessunit = 'CB100' ORDER BY PROGRAMDESC";
			else 
				Conn.QueryString = "SELECT DISTINCT PROGRAMID, PROGRAMDESC FROM RFPROGRAM " + 
					" where businessunit <> 'CB100' ORDER BY PROGRAMDESC";

			Conn.ExecuteQuery();
			DDL_PROGRAM.Items.Clear();
			DDL_PROGRAM.Items.Add(new ListItem("-- PILIH --",""));
			for (int i = 0; i < Conn.GetRowCount(); i++)
			{
				String s0 = Conn.GetFieldValue(i,0),
					s1 = Conn.GetFieldValue(i,1);
				ListItem li = new ListItem(s1,s0);
				DDL_PROGRAM.Items.Add(li);
			}
		}

		protected void Btn_Print_Click(object sender, System.EventArgs e)
		{
			LoadSql("PRINT");
		}


		protected void btn_back_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
				Response.Redirect("MainReportSLA.aspx?mc=" + Request.QueryString["mc"] + "&BU=" + LBL_BU.Text);
		}
	}
}
