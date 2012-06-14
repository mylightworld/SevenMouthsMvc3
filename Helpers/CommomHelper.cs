using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SevenMouths.Helpers;
using SevenMouths.Models;
using System.Configuration;

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

    }

}