using BLL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        public delegate bool IsAddEmployee(Employee newEmployee);
        public event IsAddEmployee OnAddEmployee;
        public EmployeeManagerBLL employeeManagerBll { get; set; }
        public List<string> AllRolesInCompany { get; set; }
        //public IsAddEmployee isAddEmployeeDelegate;

        public AddEmployee()
        {
            employeeManagerBll = new EmployeeManagerBLL();

            InitializeComponent();
            AllRolesInCompany = employeeManagerBll.GetAllRoleInCompany();

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = new Employee();
            int idText, age, year;

            bool idIsNumber = int.TryParse(IdTextBox.Text, out idText);
            bool ageIsNumber = int.TryParse(AgeTextBox.Text, out age);
            bool yearIsNumber = int.TryParse(StartOfWorkingYearTextBox.Text, out year);
            if (idIsNumber && ageIsNumber && yearIsNumber)
            {
                employee.Id = idText;
                employee.Age = int.Parse(AgeTextBox.Text);
                employee.StartOfWorkYear = int.Parse(StartOfWorkingYearTextBox.Text);

                employee.FirstName = FirstNameTextBox.Text;
                employee.LastName = LastNameTextBox.Text;
                employee.PhoneNumber = PhoneNumberTextBox.Text;
                employee.City = CityAddressTextBox.Text;
                employee.Street = StreetAddressTextBox.Text;
                employee.RoleInCompany = JobTitleTextBox.Text;
                employee.PhoneNumber = PhoneNumberTextBox.Text;
                employee.Email = MailAddressTextBox.Text;


                if (OnAddEmployee(employee) == true)
                {
                    IdTextBox.Text = "";
                    FirstNameTextBox.Text = "";
                    LastNameTextBox.Text = "";
                    PhoneNumberTextBox.Text = "";
                    AgeTextBox.Text = "";
                    StartOfWorkingYearTextBox.Text = "";
                    CityAddressTextBox.Text = "";
                    StreetAddressTextBox.Text = "";
                    JobTitleTextBox.Text = "";
                    PhoneNumberTextBox.Text = "";
                    MailAddressTextBox.Text = "";
                }
            }
    
        }


        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
