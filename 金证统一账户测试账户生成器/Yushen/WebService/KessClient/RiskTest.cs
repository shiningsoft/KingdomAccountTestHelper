using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yushen.WebService.KessClient
{
    class RiskTest
    {
        public List<RiskLevel> riskLevelList = new List<RiskLevel>();

        public static string cols = "1|2|3|4|5|6|7|8|9|10|11|12|13|14|15|16|17|18|19|20";

        public static string cells_A = "2|1|0|0|0|1|1|1|0|0|1|2|1|0|0|1|0|1|1|0";
        public static string cells_B = "5|5|1|1|4|1|1|1|1|5|1|1|1|1|1|1|1|3|3|5";
        public static string cells_C = "2|2|2|2|2|2|2|2|2|2|2|2|2|2|2|2|3|2|2|2";
        public static string cells_D = "2|2|1|2|1|3|3|4|2|4|3|5|4|4|4|1|1|1|3|4";
        public static string cells_E = "1|5|1|2|3|4|3|2|5|3|3|4|4|4|3|1|1|1|3|1";
        
        public RiskTest()
        {
            riskLevelList.Add(new RiskLevel("保守型", "A", "1|2|3|4|5|6|7|8|9|10|11|12|13|14|15|16|17|18|19|20", "2|1|0|0|0|1|1|1|0|0|1|2|1|0|0|1|0|1|1|0"));
            riskLevelList.Add(new RiskLevel("谨慎型", "B", "1|2|3|4|5|6|7|8|9|10|11|12|13|14|15|16|17|18|19|20", "5|5|1|1|4|1|1|1|1|5|1|1|1|1|1|1|1|3|3|5"));
            riskLevelList.Add(new RiskLevel("稳健型", "C", "1|2|3|4|5|6|7|8|9|10|11|12|13|14|15|16|17|18|19|20", "2|2|2|2|2|2|2|2|2|2|2|2|2|2|2|2|3|2|2|2"));
            riskLevelList.Add(new RiskLevel("积极型", "D", "1|2|3|4|5|6|7|8|9|10|11|12|13|14|15|16|17|18|19|20", "2|2|1|2|1|3|3|4|2|4|3|5|4|4|4|1|1|1|3|4"));
            riskLevelList.Add(new RiskLevel("激进型", "E", "1|2|3|4|5|6|7|8|9|10|11|12|13|14|15|16|17|18|19|20", "1|5|1|2|3|4|3|2|5|3|3|4|4|4|3|1|1|1|3|1"));
        }
    }
}
