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

namespace SME.CBI
{
	/// <summary>
	/// Summary description for SiteVisitInfo.
	/// </summary>
	public partial class SiteVisitInfo : System.Web.UI.Page
	{
		protected Connection conn;
		private string regno, curef;
		private string userid;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			conn = (Connection) Session["Connection"];

			conn.QueryString = "SELECT CU_RM FROM CUSTOMER WHERE CU_REF = '" + Request.QueryString["curef"] + "'";
			conn.ExecuteQuery();
			userid = conn.GetFieldValue("CU_RM");

			if (!Logic.AllowAccess(Session["GroupID"].ToString(), Request.QueryString["mc"], conn))
				Response.Redirect("../Restricted.aspx");

			if (!IsPostBack)
			{
				ViewData();
			}

			ViewMenu();
		}

		private void ViewMenu() 
		{
			string strtemp = "";
			try 
			{
				//--- Membuat menu dari DATABASE
				conn.QueryString = "select * from SCREENMENU where menucode = '" + Request.QueryString["mc"] + "'";
				conn.ExecuteQuery();
				for (int i = 0; i < conn.GetRowCount(); i++) 
				{
					HyperLink t = new HyperLink();
					t.Text = conn.GetFieldValue(i, 2);
					t.Font.Bold = true;
					if (conn.GetFieldValue(i, 3).Trim()!= "") 
					{
						if (conn.GetFieldValue(i,3).IndexOf("mc=") >= 0)
							strtemp = "regno=" + Request.QueryString["regno"] + "&curef="+Request.QueryString["curef"]+"&tc="+Request.QueryString["tc"];
						else	strtemp = "regno=" + Request.QueryString["regno"] + "&curef="+Request.QueryString["curef"]+"&mc="+Request.QueryString["mc"]+"&tc="+Request.QueryString["tc"];
					}
					else 
					{
						strtemp = "";
						t.ForeColor = Color.Red; 
					}
					t.NavigateUrl = conn.GetFieldValue(i, 3)+strtemp;
					Menu.Controls.Add(t);
					Menu.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;"));
				}
			} 
			catch (Exception ex) 
			{
				Console.Write(ex.Message);
			}			
		}

		private void ViewData()
		{
			conn.QueryString = "SELECT * FROM VW_CUSTINFO_SITEVISITINFO_CBI WHERE CU_REF = '" + Request.QueryString["curef"] + "'";
			conn.ExecuteQuery();

			DataTable dt = new DataTable();
			dt = conn.GetDataTable().Copy();
			DG_SITEVISIT.DataSource = dt;
			try 
			{
				DG_SITEVISIT.DataBind();
			}
			catch 
			{
				DG_SITEVISIT.CurrentPageIndex = 0;
				DG_SITEVISIT.DataBind();
			}

			for (int i=0;i<DG_SITEVISIT.Items.Count;i++)
			{
				LinkButton lbdel = (LinkButton)DG_SITEVISIT.Items[i].Cells[6].FindControl("LB_DELETE");
				lbdel.Attributes.Add("onclick","if(!deleteconfirm()){return false;};");
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
			this.DG_SITEVISIT.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DG_SITEVISIT_ItemCommand);
			this.DG_SITEVISIT.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DG_SITEVISIT_PageIndexChanged);

		}
		#endregion

		private void DG_SITEVISIT_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (((LinkButton)e.CommandSource).CommandName)
			{
				case "view":
					//Tools.popMessage(this, e.Item.Cells[5].Text.ToString().Replace("~/VerificationAssignment/SiteVisit.aspx?","SiteVisitInfo.aspx?"));
					//Tools.popMessage(this, e.Item.Cells[5].Text.ToString().Replace("~/VerificationAssignment/SiteVisit.aspx?","SiteVisitInfo.aspx?"));
					string url = e.Item.Cells[5].Text.ToString().Replace("~/VerificationAssignment/SiteVisit.aspx?","SiteVisit.aspx?") + "&mc=" + Request.QueryString["mc"] + "&scr=" + Request.QueryString["scr"] + "&lkkn=" + Request.QueryString["lkkn"]+ "&regno=" + Request.QueryString["regno"];
					Response.Redirect(url);
					break;

				case "delete":
					try
					{
						conn.QueryString = "EXEC CUSTINFO_SITEVISITINFO_DELETEVISIT '" +
							e.Item.Cells[0].Text.ToString() + "'";
						conn.ExecuteNonQuery();

						ViewData();
					}
					catch (Exception ex)
					{
						Response.Write("<!--" + ex.ToString() + "-->");
						string errmsg = ex.Message.Replace("'","");
						if (errmsg.IndexOf("Last Query:") > 0)
							errmsg = errmsg.Substring(0, errmsg.IndexOf("Last Query:"));
						GlobalTools.popMessage(this, errmsg);
						return;
					}
					break;

				default:
					break;
			}
		}

		private void DG_SITEVISIT_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DG_SITEVISIT.CurrentPageIndex = e.NewPageIndex;
			ViewData();
		}

		protected void BTN_NEW_Click(object sender, System.EventArgs e)
		{
			try
			{
				conn.QueryString = "exec CUSTINFO_SITEVISITINFO_NEWVISITCBI '" + 
					Request.QueryString["curef"] + "', '" +
					userid + "'";
				conn.ExecuteQuery();

				string url = conn.GetFieldValue(0,0).Replace("~/CBI/SiteVisit.aspx?","VASiteVisit.aspx?") + "&curef=" + Request.QueryString["curef"] + "&mc=" + Request.QueryString["mc"] + "&scr=" + Request.QueryString["scr"]+ "&regno=" + Request.QueryString["regno"];
				Response.Redirect(url);
			}
			catch (Exception ex)
			{
				Response.Write("<!--" + ex.Message + "-->");
			}
		}
	}
}
