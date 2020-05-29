using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelsBaseData
{
    public partial class CyclusMaakInstelling
    {
        [NotMapped]
        private string _waarde;
        [NotMapped]
        public string Waarde
        {
            get
            {

                if (string.IsNullOrEmpty(_waarde) && !string.IsNullOrEmpty(this.StaticWaarde))
                {
                    return this.StaticWaarde;
                }
                else
                {
                    return _waarde;
                }

            }
            set { _waarde = value; }
        }


        [NotMapped]
        public string WaardeToString
        {
            get { return Waarde; }
        }

        [NotMapped]
        public int WaardeToInt
        {
            get
            {
                if (!string.IsNullOrEmpty(Waarde))
                {
                    int.TryParse(Waarde, out int tmpInt);

                    return tmpInt;
                }
                else
                {
                    return 0;
                }
      
            }
        }

        [NotMapped]
        public decimal WaardeToDecimal
        {
            get
            {
                if (!string.IsNullOrEmpty(Waarde))
                {
                    string tmp = this.Waarde.Replace('.', ',');

                    decimal.TryParse(tmp, out decimal tmpFloat);

                    return tmpFloat;
                }
                else
                {
                    return 0;
                }
            }
        }

        [NotMapped]
        public bool WaardeToBool
        {
            get
            {
                if (this.WaardeToInt.Equals(0))
                {
                    return false;

                }
                else if (this.WaardeToInt.Equals(1))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        [NotMapped]
        public int sortTmp { get; set; }

        public override string ToString()
        {
            if (this.MaakInstelling != null)
            {
                return $"{Stap} - {ChildStap}  -  {MaakInstelling.Naam} - {MaakInstelling.DataType}";
            }

            return "";


        }
    }
}
