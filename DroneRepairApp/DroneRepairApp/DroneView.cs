using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneRepairApp
{
    public class DroneView
    {
        public int? Tag { get; set; }
        public string Client { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }

        public DroneView(Drone drone)
        {
            this.Tag = drone.GetTag();
            this.Client = drone.GetClient();
            this.Model = drone.GetModel();
            this.Description = drone.GetDescription();
            this.Cost = drone.GetCost();
        }
    }
}
