using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


    public class Serialize
    {
        /// <summary>
        /// 序列化DataTable
        /// </summary>
        /// <param name="pDt">数据表</param>
        /// <returns>序列化字符串</returns>
        public string SerializeDataTableXml(DataTable pDt)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sb);
            XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
            serializer.Serialize(writer, pDt);
            writer.Close();
            return sb.ToString();
        }

        /// <summary>
        /// 反序列化DataTable
        /// </summary>
        /// <param name="pXml">字符串</param>
        /// <returns>数据表</returns>
        public DataTable DeserializeDataTable(string pXml)
        {
            StringReader strReader = new StringReader(pXml);
            XmlReader xmlReader = XmlReader.Create(strReader);
            XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
            DataTable dt = serializer.Deserialize(xmlReader) as DataTable;
            return dt;
        }

        /// <summary>
        /// 反序列化ArrayList
        /// </summary>
        /// <param name="pXml">字符串</param>
        /// <returns>数据集</returns>
        public ArrayList DeserializeDataTable(string pXml, string o)
        {
            StringReader strReader = new StringReader(pXml);
            XmlReader xmlReader = XmlReader.Create(strReader);
            XmlSerializer serializer = new XmlSerializer(typeof(ArrayList));
            ArrayList al = serializer.Deserialize(xmlReader) as ArrayList;
            return al;
        }
    }