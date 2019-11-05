using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace FLEX.API.Common.Utils
{
    public class XmlUtil
    {
        //Create by Mr.C
        public static string Serialize<T>(List<T> list) where T : class
        {
            Type type = typeof(T);
            PropertyInfo[] fields = type.GetProperties().ToArray();

            XElement root = new XElement("Root");
            for (int n = 0; n < list.Count; n++)
            {
                T t = list[n];
                XElement table = new XElement(type.Name);

                for (int i = 0; i < fields.Length; i++)
                {
                    object value = fields[i].GetValue(t, BindingFlags.Instance | BindingFlags.Public, null, null, null);
                    table.Add(new XElement(fields[i].Name, value));
                }

                root.Add(table);
            }

            XDocument doc = new XDocument(root);
            return doc.ToString();
        }

        public static string ConvertToXml_Store<T>(List<T> lst, params string[] fields) where T : class
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml("<rows></rows>");

                if (lst != null)
                {
                    Type cType = typeof(T);
                    PropertyInfo[] props = cType.GetProperties();

                    if (fields != null)
                    {
                        if (fields.Length > 0)
                        {
                            List<PropertyInfo> pLst = new List<PropertyInfo>();
                            foreach (string f in fields)
                            {
                                PropertyInfo prop = cType.GetProperty(f);
                                if (prop != null)
                                    pLst.Add(prop);
                            }
                            props = pLst.ToArray();
                        }
                    }

                    foreach (T obj in lst)
                    {
                        XmlNode node = doc.CreateNode(XmlNodeType.Element, "row", "");
                        doc.ChildNodes[0].AppendChild(node);

                        if (props == null)
                            continue;

                        foreach (PropertyInfo prop in props)
                        {
                            XmlNode iNode = doc.CreateNode(XmlNodeType.Element, prop.Name, "");

                            object value = prop.GetValue(obj, null);
                            if (value != null)
                            {
                                string val = string.Empty;
                                if (prop.PropertyType == typeof(bool))
                                {
                                    bool b = (bool)value;
                                    if (b)
                                        val = "1";
                                    else
                                        val = "0";
                                }
                                else if (prop.PropertyType == typeof(bool?))
                                {
                                    bool? b = (bool?)value;
                                    if (b.HasValue)
                                    {
                                        if (b.Value)
                                            val = "1";
                                        else
                                            val = "0";
                                    }
                                }
                                else if (prop.PropertyType == typeof(DateTime))
                                {
                                    DateTime dt = (DateTime)value;
                                    if (dt.Ticks == 0)
                                        dt = DateTime.Now;

                                    CultureInfo c = new CultureInfo("en-US");
                                    val = dt.ToString("yyyy/MM/dd HH:mm:ss.fff", c);
                                }
                                else if (prop.PropertyType == typeof(DateTime?))
                                {
                                    DateTime? dt = (DateTime?)value;
                                    if (dt.HasValue)
                                    {
                                        CultureInfo c = new CultureInfo("en-US");
                                        val = dt.Value.ToString("yyyy/MM/dd HH:mm:ss.fff", c);
                                    }
                                }
                                else
                                    val = value.ToString();

                                iNode.InnerText = val;
                            }
                            else
                                iNode.InnerText = string.Empty;

                            node.AppendChild(iNode);
                        }
                    }
                }

                StringWriter sw = new StringWriter();
                XmlTextWriter tx = new XmlTextWriter(sw);
                doc.WriteTo(tx);

                string xml = sw.ToString();
                xml = xml.Replace("'", "''");

                return xml;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
