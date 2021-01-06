using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Master.Models
{
    public class LimitsItem
    {
        public string LimitsName { get; set; }
        public List<string> FuelTypesList { get; set; }
        public List<string> LimitTypes { get; set; }
        public List<string> Country { get; set; }
        public List<string> RefuilimgStations { get; set; }

        public float FuelAmount { get; set; }
        public string CurrentFuelType { get; set; }
        public int CurrentFuelTypeIndex {
            get {
                return  FuelTypesList.IndexOf(CurrentFuelType);
            }
        }
        public string CurrentlimitType { get; set; }
        public int CurrentlimitTypeIndex {
            get {
                return LimitTypes.IndexOf(CurrentlimitType);
            }
        }
    }
}
