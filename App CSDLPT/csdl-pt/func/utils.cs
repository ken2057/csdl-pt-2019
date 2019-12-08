using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csdl_pt.func
{
    class utils
    {
        public static string convertUTF8(string text)
        {
            return Encoding.UTF8.GetString(Encoding.Default.GetBytes(text));
        }
        
        public static string outputMoney(string text)
        {
            try
            {
                return double.Parse(text).ToString("#,## vnđ;(#,##) vnđ");
            }
            catch
            {
                return text;
            }
        }
    }
}
