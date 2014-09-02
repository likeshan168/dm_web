using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Investigate : System.Web.UI.Page
{
    private static DataTable columnTable;
    SQL_Operate so = new SQL_Operate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            initTable();
            gvBind();
        }
    }

    private void initTable()
    {
        columnTable = new DataTable();
        columnTable.Columns.Add("QID", typeof(string));
        columnTable.Columns.Add("question", typeof(string));
        columnTable.Columns.Add("type", typeof(string));
        columnTable.Columns.Add("value", typeof(string));
    }

    protected void producted_preview(DataTable innerTable)
    {
        int index = 1;
        string controlString = "";
        foreach (DataRow mydr in innerTable.Rows)
        {
            #region 注销
            //switch (mydr[2].ToString())
            //{
            //    case "短文本":
            //        controlString += index.ToString() + ". " + mydr[1].ToString() + ":  <asp:TextBox runat='server' ID='"
            //            + mydr[0].ToString() + "'></asp:TextBox>";

            //        break;
            //    case "长文本":
            //        controlString += index.ToString() + ". " + mydr[1].ToString() + ":  <asp:TextBox runat='server' ID='"
            //            + mydr[0].ToString() + "' TextMode='MultiLine'></asp:TextBox>";
            //        break;
            //    case "单选按钮组":
            //        //controlString += index.ToString() + ". <asp:CheckBox runat=\"server\" ID=\"" + mydr[0].ToString() + "\" Text=\"" + mydr[1].ToString() + "\" /><br/>";
            //        controlString += index.ToString() + ". " + mydr[1].ToString() + ":  ";
            //        string[] Rlist = mydr[3].ToString().Split(',');
            //        int RcbIndex = 1;
            //        foreach (string insertString in Rlist)
            //        {
            //            controlString += "<asp:RadioButton runat=\"server\" GroupName=\"" + index + "\" ID=\"" + mydr[0].ToString() + "_" + RcbIndex.ToString() + "\" Text=\"" + insertString + "\" />&nbsp;&nbsp;";
            //            RcbIndex++;
            //        }
            //        //controlString += "<br/>";
            //        break;
            //    case "复选框":
            //        controlString += index.ToString() + ". " + mydr[1].ToString() + ":  ";
            //        string[] list = mydr[3].ToString().Split(',');
            //        int cbIndex = 1;
            //        foreach (string insertString in list)
            //        {
            //            controlString += "<asp:CheckBox runat=\"server\" ID=\"" + mydr[0].ToString() + "_" + cbIndex.ToString() + "\" Text=\"" + insertString + "\" />&nbsp;&nbsp;";
            //            cbIndex++;
            //        }
            //        //controlString += "<br/>";
            //        break;
            //    //default: 
            //    //    controlString += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            //    //    controlString += "<asp:ImageButton runat=\"server\" ID=\"" + mydr[0].ToString() + "_back\" ImageUrl=\"images/del.gif\" OnClick=\"delInvValue\" /><br/>";
            //    //    break; 
            //}
            #endregion
            controlString += index.ToString() + ". [问题描述] : " + mydr[1].ToString() + "   [问题类型] : " + mydr[2].ToString();
            controlString += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            controlString += "<asp:ImageButton runat=\"server\" ID=\"" + mydr[0].ToString() + "_back\" ImageUrl=\"images/XX.gif\" OnClick=\"deleteInvValue\" /><br/>";
            Control c = ParseControl(controlString);
            PH.Controls.Clear();
            PH.Controls.Add(c);
            index++;
        }
    }

    protected void deleteInvValue(object sender, ImageClickEventArgs e)
    {
        ImageButton imb = (ImageButton)sender;
        string id = imb.ID.Substring(0, imb.ID.LastIndexOf("_"));
        //Button btn = (Button)sender;
        //string id = btn.Text.Substring(0, btn.Text.LastIndexOf("_"));
        DataRow[] dr = columnTable.Select("QID='" + id.Replace("ctl00_cph_", "") + "'");
        columnTable.Rows.Remove(dr[0]);
        if (columnTable.Rows.Count > 0)
            producted_preview(columnTable);
        lblTip.Text = columnTable.Rows.Count.ToString();
    }

    protected void ask_add_Click(object sender, EventArgs e)
    {
        string name = getQID();//R5111QBWAQ
        DataRow dr = columnTable.NewRow();
        dr[0] = name;
        dr[1] = invTextDcb.Text;
        if (rb1.Checked)
            dr[2] = rb1.Text;
        else if (rb2.Checked)
            dr[2] = rb2.Text;
        else if (rb3.Checked)
            dr[2] = rb3.Text;
        else
            dr[2] = rb4.Text;
        dr[3] = inputText.Text == "" ? null : inputText.Text;
        DataRow[] sdr = columnTable.Select("question='" + dr[1].ToString() + "'");
        foreach (DataRow mdr in sdr)
        {
            if (mdr[2].ToString() == dr[2].ToString() && mdr[3].ToString() == dr[3].ToString())
            {
                ScriptManager.RegisterClientScriptBlock(up1, GetType(), "sc2", "alert('不能重复添加相同内容!!')", true);
                producted_preview(columnTable);
                return;
            }
        }
        columnTable.Rows.Add(dr);
        producted_preview(columnTable);
        lblTip.Text = columnTable.Rows.Count.ToString();
        ScriptManager.RegisterClientScriptBlock(up1, GetType(), "scTip", "tipsShow()", true);
    }

    protected string getQID()
    {
        ArrayList al = new ArrayList();
        for (int i = 65; i <= 90; i++)
            al.Add(i);
        for (int j = 48; j <= 57; j++)
            al.Add(j);
        Random r = new Random();
        string name = "";

        #region//旧版

        //for (int m = 0; m < 10; m++)
        //{
        //    int c = 0;
        //    if (m == 0)
        //        c = r.Next(25);
        //    else
        //        c = r.Next(35);
        //    name += Convert.ToChar(Convert.ToInt32(al[c]));
        //}

        //DataRow[] sdr = columnTable.Select("QID='" + name + "'");
        //int t = Convert.ToInt32(so.sqlExecuteScalar("select count(*) from Investigate_details where QID='" + name + "'"));
        //if (t == 1 || sdr.Length > 0)
        //    return getQID();//相当于循环
        //else
        //    return name;
        #endregion

        #region//新版
        int t;
        DataRow[] sdr;
        do
        {
            for (int m = 0; m < 10; m++)
            {
                int c = 0;
                if (m == 0)
                    c = r.Next(25);
                else
                    c = r.Next(35);
                name += Convert.ToChar(Convert.ToInt32(al[c]));
            }
            sdr = columnTable.Select("QID='" + name + "'");
            t = Convert.ToInt32(so.sqlExecuteScalar("select count(*) from Investigate_details where QID='" + name + "'"));
        } while (t == 1 || sdr.Length > 0);
        return name;
        #endregion
    }

    protected void tempNone(object sender, EventArgs e)
    {
        return;
    }

    protected void invAdd_Click(object sender, EventArgs e)
    {
        if (columnTable == null)
        {
            ClientScript.RegisterStartupScript(GetType(), "sc3", "<script>alert('无法建立空问卷!!')</script>");
            return;
        }
        if (columnTable.Rows.Count <= 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "sc4", "<script>alert('无法建立空问卷!!')</script>");
            return;
        }
        try
        {
            int t = getInvsID();
            ArrayList al = new ArrayList();
            al.Add("insert into Investigate values(" + t + ",'" + invDescribe.Text + "','" +
                DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "',0)");
            foreach (DataRow mydr in columnTable.Rows)
            {
                al.Add("insert into Investigate_details values('" + mydr[0].ToString() + "'," +
                    t + ",'" + mydr[1].ToString() + "','" + mydr[2].ToString() + "','" + mydr[3].ToString() + "')");
            }
            so.sqlExcuteNonQuery(al, true);
            #region 恢复至初始示状态
            initTable();
            invTextDcb.Text = "";
            inputText.Text = "";
            invDescribe.Text = "";
            #endregion
            gvBind();
            ClientScript.RegisterStartupScript(GetType(), "al", "<script>alert('新建问卷成功.请设定启用状态')</script>");
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "al", "<script>alert('" + ex.Message.Replace("\r\n", "").Replace("\"", "").Replace("'", "") + "')</script>");
        }
    }

    protected void gvBind()
    {
        DataTable dt = new DataTable();
        dt = so.sqlExcuteQueryTable("select * from Investigate order by invDate asc");
        if (dt.Rows.Count == 0)
        {
            DataTable tdt = dt.Clone();
            tdt.Rows.Add(tdt.NewRow());
            GridView1.DataSource = tdt;
            tdt.Rows[0][3] = false;
            GridView1.DataBind();
            int columnCount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columnCount - 1;
            GridView1.Rows[0].Cells[0].Text = "暂无调研问卷!     <a href='javascript:void(0)' onclick=\"divShow(1)\">添加一个新问卷</a>";
        }
        else
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int stateCount = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            if (((CheckBox)(GridView1.Rows[i].Cells[3].Controls[1])).Checked)
                stateCount++;
        }
        #region//旧版
        //if (stateCount != 1)
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "sc5", "<script>alert('同一时间只能启用一个问卷.请修改后重新提交!')</script>");
        //    return;
        //}
        #endregion
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            so.sqlExcuteNonQuery("update Investigate set insState='"
                + (((CheckBox)(GridView1.Rows[i].Cells[3].Controls[1])).Checked ? "1" : "0").ToString() + "'"
                + " where TID='" + GridView1.Rows[i].Cells[0].Text + "'", false);
        }
        ClientScript.RegisterStartupScript(GetType(), "sc6", "<script>alert('保存完成!')</script>");
    }

    private int getInvsID()
    {
        System.Random r = new Random();
        int id = r.Next(100, 65535);
        string sql = "select count(tid) from Investigate where tid=" + id;
        if (Convert.ToInt32(so.sqlExecuteScalar(sql)) > 0)
            return getInvsID();
        else
            return id;
    }
}
