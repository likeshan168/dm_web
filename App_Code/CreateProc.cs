using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CreateProc
{
    public class CreateProc
    {
        SQL_Operate so = new SQL_Operate();
        public CreateProc()
        {
        }
        public string CreateProcUPdataInsert()
        {
            try
            {
                string sql = so.sqlExecuteScalar("select procText_update from clientInfo").ToString();
                ArrayList al = new ArrayList();
                al.Add(" ");
                al.Add("empoxCancel" + sql);
                so.sqlExcuteNonQuery(al, true);
                //so.sqlExcuteNonQuery("empoxCancel" + sql, false);//BUG 待处理 需要先切换数据库再执行此语句
                return "success";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string CreateProcInsert()
        {
            try
            {
                string sql = so.sqlExecuteScalar("select procText_insert from clientInfo").ToString();
                ArrayList al = new ArrayList();
                al.Add(" ");
                al.Add("empoxCancel" + sql);
                so.sqlExcuteNonQuery(al, true);
                //so.sqlExcuteNonQuery("empoxCancel" + sql, false);
                return "success";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 复制vip卡号的语句
        /// </summary>
        /// <returns></returns>
        #region 新增的，是为添加vip卡号信息的
        public string CreateVipCardInsertProc()
        {
            try
            {
                string sql = string.Format("create proc BatchVipCardInsert    as    insert into VipSet select * from TempVipSet");

                ArrayList al = new ArrayList();
                al.Add(" ");
                al.Add("empoxCancel" + sql);
                so.sqlExcuteNonQuery(al, true);
                return "success";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion
        /// <summary>
        /// 将新增过来的卡号插入到表VipSet中
        /// </summary>
        /// <returns></returns>
        #region 新增的是用来更新vip卡号信息的
        public string CreateVipCardUpdateInsertProc()
        {
            try
            {
                string sql = string.Format("CREATE proc [dbo].[BatchVipCardUpdate]  as  insert into VipSet  select * from TempVipSet as vipcard  where not exists(select * from VipSet where Card_ID=vipcard.Card_ID) ");//将新增的卡号信息插入到表VipSet中，如果已经存在的话，就不需要管了

                ArrayList al = new ArrayList();
                al.Add(" ");
                al.Add("empoxCancel" + sql);
                so.sqlExcuteNonQuery(al, true);
                return "success";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion
    }
}
