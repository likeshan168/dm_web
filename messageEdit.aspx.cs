using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class messageEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtMessage.Attributes.Add("onkeyup", "wordsCount()");
            try
            {
                DataTable structTable;
                messageDo md = new messageDo();
                //
                //TODO:代码需要修改
                //
                #region//新版
                structTable = (DataTable)Cache["table"];
                if (structTable == null)
                {
                    structTable = md.msgStruct();
                    Cache["table"] = structTable;
                }
                #endregion
                #region//旧版
                //structTable = md.msgStruct();
                //ViewState["table"] = structTable;
                #endregion

                RB_CheckedChanged(type_1, null);
            }
            catch (Exception ex)
            { ClientScript.RegisterClientScriptBlock(GetType(), "errorScript1", ex.Message, true); }
        }
        ClientScript.RegisterStartupScript(GetType(), "top", "wordsCount()", true);
    }

    protected void RB_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton rb = (RadioButton)sender;
        if (rb.Checked)
        {
            string condition = "";
            switch (rb.Text)
            {
                case "开卡短信": condition = "开卡";
                    break;
                case "销售短信": condition = "销售";
                    break;
                case "积分兑换(成功)": condition = "积分换礼(成功)";
                    break;
                case "积分兑换(失败)": condition = "积分换礼(失败)";
                    break;
            }
            DataTable structTable = (DataTable)Cache["table"];
            DataRow[] dr = structTable.Select("messageType='" + condition + "'");
            ddlMsgField.Items.Clear();
            foreach (DataRow mdr in dr)
            {
                ListItem li = new ListItem("[" + mdr[1].ToString() + "]", mdr[2].ToString());
                ddlMsgField.Items.Add(li);
            }
        }
    }
    protected void lbSave_Click(object sender, EventArgs e)
    {
        if (txtMessage.Text.Length > 0)
        {
            int filedLength = 0;
            string clearText = txtMessage.Text;
            DataTable structTable = (DataTable)Cache["table"];
            foreach (DataRow dr in structTable.Rows)
            {
                if (clearText.IndexOf("[" + dr[1].ToString() + "]") >= 0)
                {
                    filedLength += int.Parse(dr[2].ToString());
                    clearText = clearText.Replace("[" + dr[1].ToString() + "]", "");
                }
            }
            filedLength += clearText.Length;
            if (filedLength <= 130)
            {
                messageDo md = new messageDo();
                string type = "";
                if (type_1.Checked)
                    type = "开卡";
                else if (type_2.Checked)
                    type = "销售";
                else if (type_3.Checked)
                    type = "积分换礼(成功)";
                else
                    type = "积分换礼(失败)";
                string r = md.msgUpdate(txtMessage.Text, type);
                if (r != "success")
                    ClientScript.RegisterClientScriptBlock(GetType(), "errorScript2", "alert('" + r.Replace("'", "").Replace("\"", "") + "')", true);
                else
                    ClientScript.RegisterClientScriptBlock(GetType(), "complete", "alert('模板保存完成.')", true);
            }
            else
                ClientScript.RegisterClientScriptBlock(GetType(), "alertScript1", "alert('经计算,实际短信发送的最长内容已超过130个字符,请重新编辑.')", true);
        }
    }
    protected void lbRead_Click(object sender, EventArgs e)
    {
        string type = "";
        if (type_1.Checked)
            type = "开卡";
        else if (type_2.Checked)
            type = "销售";
        else if (type_3.Checked)
            type = "积分换礼(成功)";
        else
            type = "积分换礼(失败)";
        messageDo md = new messageDo();
        string s = md.msgQuery(type);
        if (s != "")
            txtMessage.Text = s;
        else
            ClientScript.RegisterClientScriptBlock(GetType(), "complete_un", "alert('目前还没有该类模板.')", true);
    }
}