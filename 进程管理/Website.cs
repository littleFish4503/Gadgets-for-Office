using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace 进程管理
{
    public class Website
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Browser { get; set; }

    }


    public class WebsiteService
    {

        public static bool AddInfo(Website website)
        {
            List<Website> websites = new List<Website>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Website>), new XmlRootAttribute("Websites"));
            try
            {
                //将website对象序列化及反序列化保存
                using (StreamReader reader = new StreamReader("websites.xml"))
                {
                    websites = (List<Website>)serializer.Deserialize(reader);
                }
                websites.Add(website);
                using (StreamWriter writer = new StreamWriter("websites.xml"))
                {
                    serializer.Serialize(writer, websites);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public static void DeleteInfo(string name)
        {
            List<Website> websites = new List<Website>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Website>), new XmlRootAttribute("Websites"));
            try
            {
                //将website对象序列化及反序列化保存
                using (StreamReader reader = new StreamReader("websites.xml"))
                {
                    websites = (List<Website>)serializer.Deserialize(reader);
                }
                // 新建一个集合记录要删除的元素
                List<Website> websitesToDelete = new List<Website>();
                foreach (var website in websites)
                {
                    if (website.Name == name)
                    {
                        websitesToDelete.Add(website);
                    }
                }
                // 删除要删除的元素
                foreach (var website in websitesToDelete)
                {
                    websites.Remove(website);
                }
                using (FileStream stream = new FileStream("websites.xml", FileMode.Create))
                {
                    // 使用 XmlSerializer 对象将 websites 列表序列化为 XML，并写入到 FileStream 中
                    serializer.Serialize(stream, websites);
                }
                //using (StreamWriter writer = new StreamWriter("websites.xml"))
                //{
                //    serializer.Serialize(writer, websites);
                //    return true;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
              
            }
        }
        public static void UpdateInfo(string name,Website website)
        {
            
            DeleteInfo(name);
            AddInfo(website);
        }
        public static List<Website> GetWebsites()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Website>), new XmlRootAttribute("Websites"));
            using (StreamReader reader = new StreamReader("websites.xml"))
            {
                return (List<Website>)serializer.Deserialize(reader);
            }
        }
    }
}
