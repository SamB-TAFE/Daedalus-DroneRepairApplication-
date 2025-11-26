using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneRepairApp
{
    public class Drone
    {
        private int serviceTag;
        private string clientName;
        private string modelName;
        private string serviceProblem;
        private double serviceCost;

        public int GetTag()
        {
            return serviceTag;
        }

        public void SetTag(int serviceTag)
        {
            this.serviceTag = serviceTag;
        }

        public string GetClient()
        {
            return clientName;
        }

        public void SetClient(string clientName)
        {
            clientName = TitleFormat(clientName);
            this.clientName = clientName;
        }

        public string GetModel()
        {
            return modelName;
        }

        public void SetModel(string modelName)
        {
            modelName = TitleFormat(modelName);
            this.modelName = modelName;
        }

        public string GetDescription()
        {
            return serviceProblem;
        }

        public void SetDescription(string serviceProblem)
        {
            serviceProblem = SentenceFormat(serviceProblem);
            this.serviceProblem = serviceProblem;
        }

        public double GetCost()
        {
            return serviceCost;
        }

        public void SetCost(double serviceCost)
        {
            this.serviceCost = serviceCost;
        }


        public string GetFinishedDisplay()
        {
            string formatted = $"Client: {clientName} ____________ Cost: ${serviceCost}";
            return formatted;
        }

        private string TitleFormat(string title)
        {
            string formatted = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(title);
            return formatted;
        }

        private string SentenceFormat(string title)
        {
            string formatted = char.ToUpper(title[0]) + title.Substring(title[1]);
            return formatted;
        }
    }
}
