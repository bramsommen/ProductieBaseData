using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsBaseData
{
    public partial class MaakInstelling
    {
        public override string ToString()
        {
            if (this.MachineOnderdeel != null)
            {
                return $"{Naam}: {DataType}  - Machine Onderdeel: {this.MachineOnderdeel.Naam}";
            }

            return $"{Naam}: {DataType}";        
        }
    }
}
