using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DroneRepairApp
{
    public class Drone
    {
        private int? serviceTag;
        private string clientName;
        private string modelName;
        private string serviceProblem;
        private double serviceCost;

        public int? GetTag()
        {
            return serviceTag;
        }

        public void SetTag(int? serviceTag)
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
            double twoDecimal = Math.Round(serviceCost, 2);
            this.serviceCost = twoDecimal;
        }


        public string GetFinishedDisplay()
        {
            string formatted = $"[{serviceTag}] Client: {clientName} ____________ Cost: ${serviceCost}";
            return formatted;
        }

        private string TitleFormat(string title)
        {
            var ti = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo;
            string formatted = ti.ToTitleCase(title.ToLower());
            return formatted;
        }

        private string SentenceFormat(string title)
        {
            title = title.Trim();

            if (title.Length == 1)
                return title.ToUpper();

            string rest = title.Substring(1).ToLower();
            string formatted = char.ToUpper(title[0]) + rest;
            return formatted;
        }
    }
}
