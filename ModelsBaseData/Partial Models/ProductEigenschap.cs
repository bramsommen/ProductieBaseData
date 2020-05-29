using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelsBaseData
{
    public partial class ProductEigenschap
    {

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

        public override string ToString()
        {
            if (this.Eigenschap != null)
            {
                return $"{this.Eigenschap.Sort} - { this.Eigenschap.Naam} ({this.Eigenschap.DataType})";
            }

            return base.ToString();

        }
    }
}
