using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SevenMouths.Helpers;
using SevenMouths.Models;
using System.Configuration;
using System.Text.RegularExpressions;

namespace SevenMouths.Helpers
{
    public class CommomHelper
    {
        //生成指定范围内的随机数，作为Num号
        //暂时不能保证不重复
        public static int GetRandomNum()
        {
            int numMin = int.Parse(ConfigurationManager.AppSettings["NumMin"].ToString());
            int numMax = int.Parse(ConfigurationManager.AppSettings["NumMax"].ToString());

            Random random = new Random();
            return random.Next(numMin, numMax);
        }

        //正则表达示验证邮箱格式
        public static bool CheckEmail(string email)
        {
            Regex emailRegex = new Regex("^\\w+((-\\w+)|(\\.\\w+))*\\@[A-Za-z0-9]+((\\.|-)[A-Za-z0-9]+)*\\.[A-Za-z0-9]+$");
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            else
            {
                if (emailRegex.IsMatch(email))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }

}