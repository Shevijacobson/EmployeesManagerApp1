using BLL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using static UI.AddEmployee;


namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Employee> ListOfDisplayEmployees { get; set; }
        public EmployeeManagerBLL employeeManagerBll { get; set; }
        public List<string> AllRolesInCompany { get; set; }
        public List<string> FilterByList { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public MainWindow()
        {
            employeeManagerBll = new EmployeeManagerBLL();

            InitializeComponent();
            DataContext = this;
            ListOfDisplayEmployees = new ObservableCollection<Employee>(employeeManagerBll.GetAllEmployees());
            FilterByList = new List<string> { "RoleInCompany", "City", "YearStartWorking", "Age" };

            ComboBoxFilterCategory.ItemsSource = FilterByList;
            AllRolesInCompany = employeeManagerBll.GetAllRoleInCompany();

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Employee> displayListByFilter = new List<Employee>();
            var selectedItem = "";
            if ((sender as ComboBox).SelectedItem != null)
                selectedItem = (sender as ComboBox).SelectedItem.ToString();
            ListOfDisplayEmployees.Clear();

            //switch()
            string filterBy = ComboBoxFilterCategory.Text;
            switch (filterBy)
            {
                case "RoleInCompany":
                    displayListByFilter = employeeManagerBll.GetFilterEmployeeByRole(selectedItem);
                    break;
                case "City":
                    displayListByFilter = employeeManagerBll.FilterByCity(selectedItem);
                    break;
                case "YearStartWorking":
                    displayListByFilter = employeeManagerBll.FilterByStartYears(selectedItem);
                    break;
                case "Age":
                    if (selectedItem.Length >= 5)
                    {
                        string age1 = selectedItem.Substring(0, 2), age2 = selectedItem.Substring(3);
                        displayListByFilter = employeeManagerBll.FilterByRangeAge(age1, age2);
                    }
                    break;
                default:
                    displayListByFilter = null; break;

            }


            foreach (var employee in displayListByFilter)
            {
                ListOfDisplayEmployees.Add(employee);
            }

        }
        public void FilterBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //אתגר!!
            var selectedItem = (sender as ComboBox).SelectedItem.ToString();
            switch (selectedItem)
            {
                case "RoleInCompany":
                    AllRolesInCompany = employeeManagerBll.GetAllRoleInCompany();
                    ComboBoxFilterOptions.ItemsSource = AllRolesInCompany;
                    break;
                case "City":
                    ComboBoxFilterOptions.ItemsSource = employeeManagerBll.GetAllCitiesOfEmployees();
                    break;

                case "YearStartWorking":
                    ComboBoxFilterOptions.ItemsSource = employeeManagerBll.GetAllStartOfWorkYear();
                    break;

                case "Age":
                    ComboBoxFilterOptions.ItemsSource = new List<string> { "20-30", "30-40", "40-50" };
                    break;

            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddEmployee addEmployeeWin = new AddEmployee();
            addEmployeeWin.OnAddEmployee += AddEmployeeWin_OnAddEmployee;
            addEmployeeWin.ShowDialog();
        }


        private bool AddEmployeeWin_OnAddEmployee(Employee newEmployee)
        {
            string error;
            bool res = employeeManagerBll.AddEmployee(newEmployee, out error);
            if (!res)
            {
                MessageBox.Show(error);
            }
            else
            {
                MessageBox.Show("Employee added Successfully");
            }
            ListOfDisplayEmployees = new ObservableCollection<Employee>(employeeManagerBll.GetAllEmployees());
            OnPropertyChanged("ListOfDisplayEmployees");
            return res;
        }

        private void DisplayButton_Click(object sender, RoutedEventArgs e)
        {
            FindCandidate findCandidateWin = new FindCandidate();
            findCandidateWin.ShowDialog();
        }
    }
}
