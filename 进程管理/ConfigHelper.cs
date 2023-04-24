using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 进程管理
{
    public class ConfigHelper
    {
        /// <summary>
        /// 返回配置值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string ReadConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        /// <summary>
        ///写入配置文件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void WriteConfig(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value =value;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
