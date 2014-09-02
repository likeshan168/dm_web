using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class invsAnswer : System.Web.UI.Page
{
    SQL_Operate so = new SQL_Operate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                #region 原始版本
                // if (Request.QueryString["vipID"] != null && Request.QueryString["invsID"] != null)
                //{
                //    ViewState["vid"] = Request.QueryString["vipID"];
                //    ViewState["invsID"] = Request.QueryString["invsID"].ToString();
                //}
                //else
                //{
                //    ClientScript.RegisterClientScriptBlock(GetType(), "tips2", "alert('参数格式不正确');window.history.back();", true); return;
                //}
                //if (!vipValidate(ViewState["vid"].ToString(), ViewState["invsID"].ToString()))
                //{
                //    ClientScript.RegisterClientScriptBlock(GetType(), "tips1", "alert('您已参与过此调查,不能重复参与.');window.history.back();", true);
                //    return;
                //}
                //txtvipID.Text = ViewState["vid"].ToString();//设置vip卡号
                #endregion
                #region 新版本
                if ( Request.QueryString["invsID"] != null)
                {
                    ViewState["invsID"] = Request.QueryString["invsID"].ToString();
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "tips2", "alert('参数格式不正确');window.history.back();", true); return;
                }
                txtvipID.Text = Request.QueryString["vipID"] == null ? string.Empty : Request.QueryString["vipID"];
                #endregion
            }
            catch (Exception ex) { ClientScript.RegisterClientScriptBlock(GetType(), "tips2", "alert('" + ex.Message.Replace("'", "").Replace("\"", "") + "');window.history.back();", true); return; }
        }
        initPage();
    }

    private void initPage()
    {
        try
        {
            DataTable innerTable = so.sqlExcuteQueryTable("select * from Investigate_details where TID='" + ViewState["invsID"].ToString() + "'");
            int index = 1;
            string controlString = "";
            phInvs.Controls.Clear();
            foreach (DataRow mydr in innerTable.Rows)
            {
                switch (mydr[3].ToString())
                {
                    case "短文本":
                        controlString += index.ToString() + ". " + mydr[2].ToString() + ":  <asp:TextBox runat='server' ID='"
                            + mydr[0].ToString() + "'></asp:TextBox><br/><br/>";

                        break;
                    case "长文本":
                        controlString += index.ToString() + ". " + mydr[2].ToString() + ":  <asp:TextBox runat='server' ID='"
                            + mydr[0].ToString() + "' TextMode='MultiLine'></asp:TextBox><br/><br/>";
                        break;
                    case "单选按钮组":
                        controlString += index.ToString() + ". " + mydr[2].ToString() + ":  ";
                        string[] Rlist = mydr[4].ToString().Split(',');
                        int RcbIndex = 1;
                        foreach (string insertString in Rlist)
                        {
                            controlString += "<asp:RadioButton runat=\"server\" GroupName=\"" + mydr[0].ToString() + "\" ID=\"" + mydr[0].ToString() + "_" + RcbIndex.ToString() + "\" Text=\"" + insertString + "\" />&nbsp;&nbsp;";
                            RcbIndex++;
                        }
                        controlString += "<br/><br/>";
                        break;
                    case "复选框":
                        controlString += index.ToString() + ". " + mydr[2].ToString() + ":  ";
                        string[] list = mydr[4].ToString().Split(',');
                        int cbIndex = 1;
                        foreach (string insertString in list)
                        {
                            controlString += "<asp:CheckBox runat=\"server\" ID=\"" + mydr[0].ToString() + "_" + cbIndex.ToString() + "\" Text=\"" + insertString + "\" />&nbsp;&nbsp;";
                            cbIndex++;
                        }
                        controlString += "<br/><br/>";
                        break;
                }
                index++;
            }
            Control c = ParseControl(controlString);
            phInvs.Controls.Add(c);
        }
        catch (Exception ex)
        { throw ex; }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (!vipValidate(ViewState["vid"].ToString(), ViewState["invsID"].ToString()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "tips5", "alert('您已参与过此次调查,不能重复参与');window.close();", true);
                return;
            }
            DataTable ansTable = getAnswer();
            ArrayList al = new ArrayList();
            foreach (DataRow dr in ansTable.Rows)
            {
                al.Add("insert into Investigate_vip values('" + ViewState["vid"].ToString() + "','" + dr[0].ToString() + "','" + dr[1].ToString() + "')");
            }
            so.sqlExcuteNonQuery(al, true);
            ClientScript.RegisterClientScriptBlock(GetType(), "tips4", "alert('感谢您参与此次调查.');window.close();", true);
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(GetType(), "tips3", "alert('" + ex.Message.Replace("'", "").Replace("\"", "") + "');", true);
        }
    }

    private DataTable getAnswer()
    {
        DataTable ansTable = new DataTable("ans");
        ansTable.Columns.Add("QID", typeof(string));
        ansTable.Columns.Add("answer", typeof(string));
        foreach (Control c in phInvs.Controls[0].Controls)
        {
            switch (c.GetType().Name)
            {
                case "TextBox":
                    if (((TextBox)c).Text.Trim() != "")
                    {
                        string tid = c.ClientID.Replace("ctl00_cph_", "");
                        string txt = ((TextBox)c).Text.Trim();
                        if (txt != "")
                        {
                            DataRow dr = ansTable.NewRow();
                            dr[0] = tid;
                            dr[1] = txt;
                            ansTable.Rows.Add(dr);
                        }
                    }
                    break;
                case "CheckBox":
                    if (((CheckBox)c).Checked)
                    {
                        string tid = c.ClientID.Replace("ctl00_cph_", "");
                        tid = tid.Substring(0, tid.LastIndexOf("_"));
                        string txt = ((CheckBox)c).Text.Trim();
                        if (txt != "")
                        {
                            //判断是否已存在此问题,是则追加答案,否则新建
                            DataRow[] dr = ansTable.Select("QID='" + tid + "'");
                            if (dr != null && dr.Length > 0)
                            {
                                dr[0][1] += txt + ",";
                            }
                            else
                            {
                                DataRow dr1 = ansTable.NewRow();
                                dr1[0] = tid;
                                dr1[1] = txt + ",";
                                ansTable.Rows.Add(dr1);
                            }
                        }
                    }
                    break;
                case "RadioButton":
                    if (((RadioButton)c).Checked)
                    {
                        string tid = c.ClientID.Replace("ctl00_cph_", "");
                        tid = tid.Substring(0, tid.LastIndexOf("_"));
                        string txt = ((RadioButton)c).Text.Trim();
                        if (txt != "")
                        {
                            DataRow dr = ansTable.NewRow();
                            dr[0] = tid;
                            dr[1] = txt;
                            ansTable.Rows.Add(dr);
                        }
                    }
                    break;
            }
        }

        return ansTable;
    }

    private bool vipValidate(string vid, string tid)
    {
        string sql = "select count(inv.TID) from Investigate as inv inner join Investigate_details as ide on inv.tid=ide.tid inner join Investigate_vip as iv on ide.QID=iv.QID where " +
            "inv.TID=" + tid + " and iv.CID='" + vid + "'";
        if (Convert.ToInt16(so.sqlExecuteScalar(sql)) == 0)
            return true;
        else
            return false;
    }
}