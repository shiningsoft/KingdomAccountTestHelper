using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yushen.WebService.KessClient
{
    class RiskLevel
    {
        private string _name;
        private string _value;
        private string _survey_cols;
        private string _survey_cells;

        public RiskLevel(string name,string value,string survey_cols,string survey_cells)
        {
            this.name = name;
            this.value = value;
            this.survey_cols = survey_cols;
            this.survey_cells = survey_cells;
        }

        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                this._name = value;
            }
        }

        public string value
        {

            get
            {
                return _value;
            }
            set
            {
                this._value = value;
            }
        }

        public string survey_cols
        {

            get
            {
                return _survey_cols;
            }
            set
            {
                this._survey_cols = value;
            }
        }

        public string survey_cells
        {

            get
            {
                return _survey_cells;
            }
            set
            {
                this._survey_cells = value;
            }
        }
    }
}
