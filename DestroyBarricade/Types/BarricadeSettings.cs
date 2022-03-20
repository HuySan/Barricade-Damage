using System.Collections.Generic;
using System.Xml.Serialization;

namespace DestroyBarricade.Types
{
    public class BarricadeSettings
    {
        [XmlAttribute]
        public bool enabled;
        [XmlAttribute]
        public int minutesForDamage;
        [XmlAttribute]
        public int percentDamage;
        [XmlArray]
        public List<int> WhiteList;

        public BarricadeSettings(bool enabled, int minutesForDamage, int percentDamage, List<int> WhiteList)
        {
            this.enabled = enabled;
            this.minutesForDamage = minutesForDamage;
            this.WhiteList = WhiteList;
            this.percentDamage = percentDamage;
        }

        public BarricadeSettings() { }

        public bool IsInWhiteList(uint idBarricade)
        {
            foreach(int id in WhiteList)
            {
                if(idBarricade == id) return true; 
            }
            return false;
        }
    }
}
