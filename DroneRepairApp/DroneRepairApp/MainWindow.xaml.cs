using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace DroneRepairApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public List<Drone> finishedOrders = new List<Drone>();
        public Queue<Drone> regularOrders = new Queue<Drone>();
        public Queue<Drone> expressOrders = new Queue<Drone>();


        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Adds a new item to regular or express queue, if any sub-methods throw exceptions, it shows the mesaage and returns.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> The click </param>
        private void AddNewItem(object sender, RoutedEventArgs e)
        {
            Drone drone = new Drone();
            int orderFlag;
            try
            {
                drone = CreateDrone();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            try
            {
                orderFlag = GetServicePriority(); // 1 is Regular, 2 is Express
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            if (orderFlag == 1)
            {
                IncrementTag();

                regularOrders.Enqueue(drone);
                DisplayRegularQueue();
                ClearInputs();
            }
            else if (orderFlag == 2)
            {
                double regCost = drone.GetCost();
                double expCost = regCost * 1.15;
                drone.SetCost(expCost);

                IncrementTag();

                expressOrders.Enqueue(drone);
                DisplayExpressQueue();
                ClearInputs();
            }

        }

        /// <summary>
        /// This method ensures that the Cost Input only accepts/shows doubles with 2 decimal places
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CostInput_TextChanged(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(CostInput.Text, out double cost))
            {
                CostInput.Text = cost.ToString("F2");
            }
            else if (!string.IsNullOrWhiteSpace(CostInput.Text))
            {
                CostInput.Text = "0.00";

            }
        }

        /// <summary>
        /// Button event that dequeues the next order and adds it to the finished list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinishRegularOrder(object sender, RoutedEventArgs e)
        {
            if (regularOrders.Count == 0) 
            {
                return;
            }
            Drone nextItem = regularOrders.Dequeue();

            finishedOrders.Add(nextItem);
            FinishedListBox.Items.Add(nextItem.GetFinishedDisplay());

            DisplayRegularQueue();
        }

        /// <summary>
        /// Loads the relevant client information into TextBoxes upon click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegularListView_Click(object sender, MouseButtonEventArgs e)
        {
            if (RegularListView.SelectedItem is DroneView selected)
            {
                ClientInput.Text = selected.Client;
                DescriptionInput.Text = selected.Description;
            }
        }

        /// <summary>
        /// Button event that dequeues the next order and adds it to the finished list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinishExpressOrder(object sender, RoutedEventArgs e)
        {
            if (expressOrders.Count == 0)
            {
                return;
            }
            Drone nextItem = expressOrders.Dequeue();

            finishedOrders.Add(nextItem);
            FinishedListBox.Items.Add(nextItem.GetFinishedDisplay());

            DisplayExpressQueue();
        }

        /// <summary>
        /// Loads the relevant client information into TextBoxes upon click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExpressListView_Click(object sender, MouseButtonEventArgs e)
        {
            if (ExpressListView.SelectedItem is DroneView selected)
            {
                ClientInput.Text = selected.Client;
                DescriptionInput.Text = selected.Description;
            }
        }

        /// <summary>
        /// Button event that removes order from Finished List and ListBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearFinishedOrder(object sender, RoutedEventArgs e)
        {
            int index = FinishedListBox.SelectedIndex;

            if (index < 0)
            {
                MessageBox.Show("Please select a finished order to clear.");
                return;
            }

            if (index >= 0 && index < finishedOrders.Count)
            {
                finishedOrders.RemoveAt(index);
            }

            FinishedListBox.Items.RemoveAt(index);

        }

        private void FinishedListBox_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = FinishedListBox.SelectedIndex;

            if (index < 0)
                return;

            finishedOrders.RemoveAt(index);

            FinishedListBox.Items.RemoveAt(index);
        }



        /// <summary>
        /// Returns a flag depending on which radio button is clicked in the Add New Item Area
        /// </summary>
        /// <returns> an int flag (1 = regular, 2 = express) </returns>
        /// <exception cref="Exception"></exception>
        private int GetServicePriority()
        {
            if (RegOrderRadio.IsChecked == true)
            {
                return 1;
            }
            else if (ExpOrderRadio.IsChecked == true)
            {
                return 2;
            }
            else
            {
                throw new Exception("Please select an order style\n(Regular or Express)");
            }
        }

        /// <summary>
        /// Attempts to create a full Drone object and throws exceptions if an input isn't filled.
        /// </summary>
        /// <returns> A Drone object with values from the input boxes </returns>
        /// <exception cref="Exception"> If an input is not/incorrectly filled </exception>
        private Drone CreateDrone()
        {
            Drone drone = new Drone();

            if (TagInput.Value.HasValue)
            {
                drone.SetTag(TagInput.Value);
            }
            else
            {
                throw new Exception("Input valid tag");
            }

            if (string.IsNullOrEmpty(ClientInput.Text))
            {
                throw new Exception("Input a client name");
            }
            else
            {
                drone.SetClient(ClientInput.Text);
            }

            if (string.IsNullOrEmpty(ModelInput.Text))
            {
                throw new Exception("Input a drone model");
            }
            else
            {
                drone.SetModel(ModelInput.Text);
            }

            if (string.IsNullOrEmpty(DescriptionInput.Text))
            {
                throw new Exception("Input a repair description");
            }
            else
            {
                drone.SetDescription(DescriptionInput.Text);
            }
            double cost = double.Parse(CostInput.Text); // input will always be passable due to other methods
            drone.SetCost(cost);

            return drone;
        }

        /// <summary>
        /// Fills the Regular Queue ListView with the Drones within the Regular Queue
        /// </summary>
        private void DisplayRegularQueue()
        {
            RegularListView.Items.Clear();

            foreach (var drone in regularOrders)
            {
                RegularListView.Items.Add(new DroneView(drone));
            }
        }

        /// <summary>
        /// Fills the Express Queue ListView with the Drones within the Express Queue
        /// </summary>
        private void DisplayExpressQueue()
        {
            ExpressListView.Items.Clear();

            foreach (var drone in expressOrders)
            {
                ExpressListView.Items.Add(new DroneView(drone));
            }
        }

        /// <summary>
        /// Clears all Text Inputs
        /// </summary>
        private void ClearInputs()
        {
            ClientInput.Text = string.Empty;
            ModelInput.Text = string.Empty;
            CostInput.Text = "0.00";
            DescriptionInput.Text = string.Empty;
        }

        /// <summary>
        /// Increases or resets the tag number
        /// </summary>
        private void IncrementTag()
        {
            string tagString = TagInput.Text;
            int tag = int.Parse(tagString);
            tag = tag + 10;
            if (tag == 910)
            {
                tag = 100;
            }
            TagInput.Text = tag.ToString();
        }
    }
}