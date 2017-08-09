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

        public static string cells_A = "5|5|4|1|4|1|1|1|1|5|1|1|1|1|1|5|5|3|1|5";
        public static string cells_B = "5|5|1|1|4|1|1|1|1|5|1|1|1|1|1|1|1|3|3|5";
        public static string cells_C = "2|2|2|2|2|2|2|2|2|2|2|2|2|2|2|2|3|2|2|2";
        public static string cells_D = "2|2|1|2|1|3|3|4|2|4|3|5|4|4|4|1|1|1|3|4";
        public static string cells_E = "1|5|1|2|3|4|3|2|5|3|3|4|4|4|3|1|1|1|3|1";
        
        public RiskTest()
        {
            riskLevelList.Add(new RiskLevel("保守型", "A", RiskTest.cols, RiskTest.cells_A));
            riskLevelList.Add(new RiskLevel("谨慎型", "B", RiskTest.cols, RiskTest.cells_B));
            riskLevelList.Add(new RiskLevel("稳健型", "C", RiskTest.cols, RiskTest.cells_C));
            riskLevelList.Add(new RiskLevel("积极型", "D", RiskTest.cols, RiskTest.cells_D));
            riskLevelList.Add(new RiskLevel("激进型", "E", RiskTest.cols, RiskTest.cells_E));
        }
    }
}
