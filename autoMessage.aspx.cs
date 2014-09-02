using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class autoMessage : System.Web.UI.Page
{
    SQL_Operate so = new SQL_Operate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            initFiledList();
            initCouponList();
            initGVmsg();
        }
    }

    private void initFiledList()
    {
        DataTable dt = so.sqlExcuteQueryTable("select ENname,CNname,[type],Udefined from DataInfo");
        ViewState["filedDt"] = dt;
        ListItem fli; ListItem lis;
        ddlField.Items.Clear();
        ddlFields.Items.Clear();
        foreach (DataRow dr in dt.Rows)
        {
            fli = new ListItem();
            fli.Text = dr[1].ToString();
            fli.Value = "[%" + dr[1].ToString() + "%]";
            ddlField.Items.Add(fli);

            lis = new ListItem();
            lis.Text = dr[1].ToString();
            if (dr[3].ToString().ToLower() == "true")
                lis.Value = "UD_Fileds." + dr[0].ToString();
            else
                lis.Value = "cardInfo." + dr[0].ToString();
            ddlFields.Items.Add(lis);
        }
        fli = new ListItem();
        fli.Text = "优惠券编号";
        fli.Value = "[%优惠券ID%]";
        ddlField.Items.Add(fli);
        fli = new ListItem();
        fli.Text = "赠送积分";
        fli.Value = "[%赠送积分%]";
        ddlField.Items.Add(fli);
    }

    private void initCouponList()
    {
        DataTable dt = so.sqlExcuteQueryTable("select tid,typeDetails from couponType where usingState=1");
        ListItem li;
        ddlCouponList.Items.Clear();
        foreach (DataRow dr in dt.Rows)
        {
            li = new ListItem();
            li.Text = dr[1].ToString();
            li.Value = dr[0].ToString();
            ddlCouponList.Items.Add(li);
        }
    }

    protected void btnAddcdt_Click(object sender, EventArgs e)
    {
        ListItem li = new ListItem();
        string liText = "", liValue = "";
        if (lbCdt.Items.Count != 0)//listbox这个 是在已经有了项之后的条件
        {
            if (rbCdt1.Checked)
            {
                liText += "并且  ";
                liValue += "and ";
            }
            else
            {
                liText += "或者  ";
                liValue += "or  ";
            }
        }
        liText += ddlFields.Items[ddlFields.SelectedIndex].Text + " ";
        liValue += ddlFields.Items[ddlFields.SelectedIndex].Value + " ";
        switch (ddlCdt.SelectedValue)
        {
            case "大于":
                liText += "大于 ";
                liValue += "> ";
                break;
            case "小于":
                liText += "小于 ";
                liValue += "< ";
                break;
            case "等于":
                liText += "等于 ";
                liValue += "= ";
                break;
            case "大于或等于":
                liText += "大于或等于 ";
                liValue += ">= ";
                break;
            case "小于或等于":
                liText += "小于或等于 ";
                liValue += "<= ";
                break;
            case "包含":
                liText += "包含 ";
                liValue += "like ";
                break;
            case "不包含":
                liText += "不包含 ";
                liValue += "not like ";
                break;
        }
        liText += txtValue.Text;
        DataTable dt = (DataTable)ViewState["filedDt"];
        DataRow[] drs = dt.Select("CNname='" + ddlFields.Items[ddlFields.SelectedIndex].Text + "'");
        string type = drs[0]["type"].ToString();
        if (type == "小数型" || type == "整数型" || type == "整数" || type == "币种" || type == "数字" || type == "复选框")
            liValue += txtValue.Text;
        else
            liValue += "'" + txtValue.Text + "'";
        li.Text = liText;
        li.Value = liValue;
        lbCdt.Items.Add(li);
    }
    protected void btnRemovecdt_Click(object sender, EventArgs e)
    {
        int i = lbCdt.SelectedIndex;
        if (i != -1)
        {
            if (i == 0)//选择的是第一项
            {
                if (lbCdt.Items.Count > 1)
                {
                    lbCdt.Items[1].Text = lbCdt.Items[1].Text.Substring(3, lbCdt.Items[1].Text.Length - 3);
                    lbCdt.Items[1].Value = lbCdt.Items[1].Value.Substring(3, lbCdt.Items[1].Value.Length - 3);
                }
            }
            lbCdt.Items.RemoveAt(i);
            lbCdt.SelectedIndex = i - 1;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //验证是否重复录入
            if (!this.validateModel())
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "scriptVal", "alert('此短信模板已被注册过,请更换内容后再试.')", true);
                return;
            }
            string insertSql = "insert into autoMessage values(@type,@mn,@mt,@cdt,@mf,@sd,@da,@lsd,@pc,@cmID,@ced,@ps,@sv,@us)";
            string cdt = "";

            DataTable dt = new DataTable("table1");
            dt.Columns.Add("data", typeof(string));
            dt.Columns.Add("value", typeof(object));
            dt.Columns.Add("type", typeof(string));

            //拼写发送会员条件
            if (lbCdt.Items.Count == 0)
                cdt = "1=1";
            else
            {
                for (int i = 0; i < lbCdt.Items.Count; i++)
                    cdt += lbCdt.Items[i].Value;
            }

            //发送日期模式
            if (rbNormal.Checked)
            {
                dt.Rows.Add(new object[] { "@type", "固定日期", "varchar" });
                dt.Rows.Add(new object[] { "@mn", txtTitle.Text, "varchar" });//概述
                dt.Rows.Add(new object[] { "@mt", txtText.Text.Trim(), "varchar" });//短信模版内容
                dt.Rows.Add(new object[] { "@cdt", cdt, "varchar" });//条件
                dt.Rows.Add(new object[] { "@mf", null, "varchar" });
                dt.Rows.Add(new object[] { "@sd", txtSendDate.Text, "datetime" });//固定的发送时间
                dt.Rows.Add(new object[] { "@da", txtDateAdd.Text, "int" });
                dt.Rows.Add(new object[] { "@lsd", null, "datetime" });
            }
            else if (rbDynamics.Checked)
            {
                dt.Rows.Add(new object[] { "@type", "动态日期", "varchar" });
                dt.Rows.Add(new object[] { "@mn", txtTitle.Text, "varchar" });
                dt.Rows.Add(new object[] { "@mt", txtText.Text.Trim(), "varchar" });
                dt.Rows.Add(new object[] { "@cdt", cdt, "varchar" });
                dt.Rows.Add(new object[] { "@mf", ddlSendDate.SelectedValue, "varchar" });
                dt.Rows.Add(new object[] { "@sd", null, "datetime" });
                dt.Rows.Add(new object[] { "@da", txtDateAdd.Text, "int" });
                dt.Rows.Add(new object[] { "@lsd", null, "datetime" });
            }
            else
            {
                dt.Rows.Add(new object[] { "@type", "节日", "varchar" });
                dt.Rows.Add(new object[] { "@mn", txtTitle.Text, "varchar" });
                dt.Rows.Add(new object[] { "@mt", txtText.Text.Trim(), "varchar" });
                dt.Rows.Add(new object[] { "@cdt", cdt, "varchar" });
                dt.Rows.Add(new object[] { "@mf", ddlHoliday.SelectedValue, "varchar" });
                dt.Rows.Add(new object[] { "@sd", null, "datetime" });
                dt.Rows.Add(new object[] { "@da", txtDateAdd.Text, "int" });
                dt.Rows.Add(new object[] { "@lsd", null, "datetime" });
            }

            //是否附赠优惠券
            if (rbCoupon1.Checked)
            {
                dt.Rows.Add(new object[] { "@pc", 1, "bit" });
                dt.Rows.Add(new object[] { "@cmID", ddlCouponList.SelectedValue, "int" });
                dt.Rows.Add(new object[] { "@ced", txtCouponDate.Text, "int" });
            }
            else
            {
                dt.Rows.Add(new object[] { "@pc", 0, "bit" });
                dt.Rows.Add(new object[] { "@cmID", null, "int" });
                dt.Rows.Add(new object[] { "@ced", null, "int" });
            }

            //是否附赠积分
            if (rbScore1.Checked)
            {
                dt.Rows.Add(new object[] { "@ps", 1, "bit" });
                dt.Rows.Add(new object[] { "@sv", txtScore.Text.Trim(), "int" });
            }
            else
            {
                dt.Rows.Add(new object[] { "@ps", 0, "bit" });
                dt.Rows.Add(new object[] { "@sv", null, "int" });
            }

            //附加使用状态 默认启用
            dt.Rows.Add(new object[] { "@us", 1, "bit" });

            so.sqlExcuteNonQuery(insertSql, dt);

            //清理输入数据
            this.resetPage();
            //重载管理页
            initGVmsg();
            ClientScript.RegisterClientScriptBlock(GetType(), "script1", "alert('新模板类型注册成功,已自动启用.')", true);
        }
        catch (Exception ex)
        { ClientScript.RegisterClientScriptBlock(GetType(), "script2", "alert('" + ex.Message.Replace("\r\n", "").Replace("'", "") + "')", true); }
    }

    private void resetPage()
    {
        rbNormal.Checked = true;
        txtSendDate.Text = "";
        txtDateAdd.Text = "0";
        txtTitle.Text = "";
        ddlFields.SelectedIndex = 0;
        txtValue.Text = "";
        lbCdt.Items.Clear();

    }

    private bool validateModel()
    {
        string sql = "select count(id) from autoMessage where modelText='" + txtText.Text.Trim() + "'";
        int i = (int)so.sqlExecuteScalar(sql);
        if (i == 0)
            return true;
        else
            return false;
    }

    private void initGVmsg()
    {
        string sql = "SELECT [id],[type],[modelName],[mainField],[sendDate],[dateAdd],[lastSendDate],[presendCoupon],[presendScore],[usingState] FROM autoMessage";
        DataTable dt = so.sqlExcuteQueryTable(sql);
        GVmessage.DataSource = dt;
        ViewState["dataSource"] = dt;
        GVmessage.DataBind();
    }

    private void bind()
    {
        GVmessage.DataSource = (DataTable)ViewState["dataSource"];
        GVmessage.DataBind();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            ArrayList al = new ArrayList();
            string sqlstr = "";
            for (int i = 0; i < GVmessage.Rows.Count; i++)
            {
                sqlstr = "update autoMessage set usingState='"
                    + (((CheckBox)(GVmessage.Rows[i].Cells[9].Controls[1])).Checked ? "1" : "0").ToString() + "' where id='" +
                    GVmessage.Rows[i].Cells[0].Text + "'";
                al.Add(sqlstr);
            }
            so.sqlExcuteNonQuery(al, true);
            GVmessage.EditIndex = -1;
            initGVmsg();
            ScriptManager.RegisterClientScriptBlock(upc, GetType(), "suc", "alert('修改成功!')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(upc, GetType(), "err", "alert('" + ex.Message.Replace("\r\n", "").Replace("'", "") + "')", true);
        }
    }
    #region//旧版
    protected void GVmessage_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //string sql = "delete from autoMessage where id='" + GVmessage.Rows[e.RowIndex].Cells[0].Text + "'";
        //so.sqlExcuteNonQuery(sql, false);
        //initGVmsg();
    }
    #endregion
    protected void GVmessage_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GVmessage.EditIndex = -1;
        initGVmsg();
    }
    #region//新版
    protected void GVmessage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ImageButton img = (ImageButton)e.Row.FindControl("delete");
                if (img != null)
                {
                    img.Attributes.Add("onclick", "return confirm('您确定要删除此项吗？')");
                    img.CommandArgument = e.Row.RowIndex.ToString();
                }
            }
        }
    }
    protected void GVmessage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            int index = Int32.Parse(e.CommandArgument.ToString());
            if (index <= -1 || index == GVmessage.Rows.Count) return;
            string sql = "delete from autoMessage where id='" + GVmessage.DataKeys[index]["id"].ToString() + "'";
            so.sqlExcuteNonQuery(sql, false);
            initGVmsg();
        }
    }
    #endregion
}