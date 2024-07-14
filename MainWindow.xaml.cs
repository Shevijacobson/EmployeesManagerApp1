using BLL;
using DAL.Models;
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
using static UI.AddEmployee;


namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Employee> ListOfDisplayEmployees { get; set; }
        public EmployeeManagerBLL employeeManagerBll { get; set; }
        public List<string> AllRolesInCompany { get; set; }
      

        public MainWindow()
        {
            employeeManagerBll = new EmployeeManagerBLL();
            
            InitializeComponent();

            ListOfDisplayEmployees = new ObservableCollection<Employee>(employeeManagerBll.GetAllEmployees());

            AllRolesInCompany = employeeManagerBll.GetAllRoleInCompany();
            ComboBoxFilterOptions.ItemsSource = AllRolesInCompany;
            CandidateDataGrid.ItemsSource = ListOfDisplayEmployees;


            

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selectedItem = (sender as ComboBox).SelectedItem.ToString();
            ListOfDisplayEmployees.Clear();

            List<Employee> listOfEmployeeByRole = employeeManagerBll.GetFilterEmployeeByRole(selectedItem);

            foreach (var employee in listOfEmployeeByRole)
            {
                ListOfDisplayEmployees.Add(employee);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddEmployee addEmployeeWin = new AddEmployee();
            addEmployeeWin.OnAddEmployee += employeeManagerBll.AddEmployee;
            addEmployeeWin.ShowDialog();
        }





    }
}
