using System;
namespace Smart_Home_Dashboard.Data
{
    public class SmartDevice
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Room { get; set; } = "Гостиная";
        public bool IsOn { get; set; }
        public int Value { get; set; }
        public string Icon { get; set; } = "💡";

        public int Wattage { get; set; } = 50;
    }
}
