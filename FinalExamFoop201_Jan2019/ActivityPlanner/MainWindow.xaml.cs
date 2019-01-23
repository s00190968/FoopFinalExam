using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ActivityPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //lists
        //List<Activity> allActivities;
        //List<Activity> selectedActivitites;
        //List<Activity> filteredActivitites;

        //observable collections
        ObservableCollection<Activity> allActivities;
        ObservableCollection<Activity> selectedActivitites;
        ObservableCollection<Activity> filteredActivitites;
        public decimal totalCost;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //allActivities = new List<Activity>();
            //selectedActivitites = new List<Activity>();
            //filteredActivitites = new List<Activity>();

            allActivities = new ObservableCollection<Activity>();
            selectedActivitites = new ObservableCollection<Activity>();
            filteredActivitites = new ObservableCollection<Activity>();

            #region making of activities
            //activity to collection
            allActivities.Add(new Activity("Kayaking", new DateTime(2019, 06, 01), 25, "Down to the rapids.", Activity.ActivityType.Water));
            allActivities.Add(new Activity("Paraschuting", new DateTime(2019, 06, 01), 40, "Back to the ground.", Activity.ActivityType.Air));
            allActivities.Add(new Activity("Mountain Biking", new DateTime(2019, 06, 02), 15, "Cycling up the hill.", Activity.ActivityType.Land));
            allActivities.Add(new Activity("Hang gliding", new DateTime(2019, 06, 02), 30, "Gone with the wind.", Activity.ActivityType.Air));
            allActivities.Add(new Activity("Abseiling", new DateTime(2019, 06, 03), 50, "Even we don't know what this is.", Activity.ActivityType.Water));
            allActivities.Add(new Activity("Sailing", new DateTime(2019, 06, 03), 45, "Leisurely time on water, but no meals included!", Activity.ActivityType.Water));
            allActivities.Add(new Activity("Helicopter tour", new DateTime(2019, 06, 03), 60, "Let's go and see what the ground looks like from high up. But you can't probably hear anything properly.", Activity.ActivityType.Air));
            allActivities.Add(new Activity("Treking", new DateTime(2019, 06, 01), 10, "Walking in the nature. Remember to wear shoes.", Activity.ActivityType.Land));
            #endregion

            //update listboxes
            updateListBoxes();
        }

        ObservableCollection<Activity> FilterProducts(int variation)// make a temp list of the objects that have "variation" 
        {
            ObservableCollection<Activity> temp = new ObservableCollection<Activity>();

            foreach (Activity a in allActivities)
            {
                if ((int)a.Type == variation)//if the given integer is the same as the enum turned to integer then add a to temp list
                {
                    temp.Add(a);
                }
            }

            return temp;
        }

        public void filterByRadioButtons(object sender, RoutedEventArgs e)
        {
            if (AirRadioBtn.IsChecked == true)//if air
            {
                filteredActivitites = FilterProducts(0);//filter list by the type of activity
                //filteredActivitites.Sort();
                allActivitiesLbx.ItemsSource = filteredActivitites;//set listbox source to filtered
            }
            else if (WaterRadioBtn.IsChecked == true)//if water
            {
                filteredActivitites = FilterProducts(1);
                //filteredActivitites.Sort();
                allActivitiesLbx.ItemsSource = filteredActivitites;
            }
            else if (LandRadioBtn.IsChecked == true)//if land
            {
                filteredActivitites = FilterProducts(2);
                //filteredActivitites.Sort();
                allActivitiesLbx.ItemsSource = filteredActivitites;
            }
            else//show all
            {
                //allActivities.Sort();
                allActivitiesLbx.ItemsSource = allActivities;
            }
        }

        private void allActivitiesLbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Activity temp = allActivitiesLbx.SelectedItem as Activity;
            if (temp != null)
            {
                descriptionTxBl.Text = temp.Description + " Cost - €" + temp.Cost;
            }
            else
            {
                descriptionTxBl.Text = "";
            }
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            errorMsgTxBl.Text = "";

            Activity temp = allActivitiesLbx.SelectedItem as Activity;//selected activity

            if (temp != null)
            {
                selectedActivitites.Add(temp);//add to selected list
                allActivities.Remove(temp);//remove from all activities list

                //update total cost
                totalCost += temp.Cost;
                totalCostTxBl.Text = "€" + totalCost.ToString();

                //go through all items in selected list
                foreach (Activity a in selectedActivitites)
                {
                    if (a.ActivityDate.ToShortDateString() == temp.ActivityDate.ToShortDateString())//if there are activities selected with the same date
                    {
                        errorMsgTxBl.Text = "You have activitites with the same date selected.";
                    }
                    else
                    {
                        errorMsgTxBl.Text = "";
                    }
                }
            }
            else
            {
                errorMsgTxBl.Text = "Nothing was selected!";
            }

            //sort
            //allActivities.Sort();
            //selectedActivitites.Sort();

            //update listboxes
            updateListBoxes();
        }

        private void removeBtn_Click(object sender, RoutedEventArgs e)
        {
            Activity temp = selectedActivitiesLbx.SelectedItem as Activity;//selected activity

            //if the selection exists
            if (temp != null)
            {
                allActivities.Add(temp);//add to selected list
                selectedActivitites.Remove(temp);//remove from all activities list

                //update total cost
                totalCost -= temp.Cost;
                totalCostTxBl.Text = "€" + totalCost.ToString();
            }
            else
            {
                errorMsgTxBl.Text = "Nothing was selected!";
            }

            //sort
            //allActivities.Sort();
            //selectedActivitites.Sort();

            //update listboxes
            updateListBoxes();
        }

        private void selectedActivitiesLbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Activity temp = selectedActivitiesLbx.SelectedItem as Activity;
            if (temp != null)
            {
                descriptionTxBl.Text = temp.Description + " Cost - €" + temp.Cost;
            }
            else
            {
                descriptionTxBl.Text = "";
            }
        }
        void updateListBoxes()
        {
            //list box items from the collections
            allActivitiesLbx.ItemsSource = allActivities;
            selectedActivitiesLbx.ItemsSource = selectedActivitites;
        }
    }

}
