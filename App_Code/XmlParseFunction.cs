using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

/// <summary>
///XmlParseFunction 的摘要说明
/// </summary>
public class XmlParseFunction
{
	public XmlParseFunction()
	{
		
	}

    public XmlNode SelectXmlNode(XmlDocument doc, string xPath)//根据xPath找到xml文档中的具体节点
    {
        XmlNode node = null;
        try
        {
            node = doc.SelectSingleNode(xPath);
        }
        catch (Exception) { }
        return node;
    }
    public string GetNodeAttribute(XmlNode node, String attrName)//获得节点的指定属性
    {
        string attrValue = null;
        try
        {
            attrValue = node.Attributes[attrName].InnerText;
        }
        catch (Exception) { }
        return attrValue;
    }
}