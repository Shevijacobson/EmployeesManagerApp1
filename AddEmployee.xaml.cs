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
        public IsAddEmployee isAddEmployeeDelegate;
        public AddEmployee()
        {
            employeeManagerBll = new EmployeeManagerBLL();
            OnAddEmployee += check;
            InitializeComponent();
            AllRolesInCompany = employeeManagerBll.GetAllRoleInCompany();

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            
            Regex emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}");
            Regex idRegex = new Regex("^[0-9]{9}$");
            Regex nameRegex = new Regex(@"^[a-zA-Z]{2,}$");
            Regex ageRegex = new Regex(@"^([2-5][0-9]|6[0-7]|1[8-9])$");
            Regex phoneRegex = new Regex("^[0-9]{10}$");
       
            List<string> invalidFields = new List<string>();

            if (!idRegex.IsMatch(IdTextBox.Text))
            {
                invalidFields.Add("ID");
            }

            if (!nameRegex.IsMatch(FirstNameTextBox.Text))
            {
                invalidFields.Add("First Name");
            }
            if(!nameRegex.IsMatch(LastNameTextBox.Text))
            {
                invalidFields.Add("Last Name");
            }
            if (!ageRegex.IsMatch(AgeTextBox.Text))
            {
                invalidFields.Add("Age");
            }

            if (!phoneRegex.IsMatch(PhoneNumberTextBox.Text))
            {
                invalidFields.Add("Phone Number");
            }

            if (!emailRegex.IsMatch(MailAddressTextBox.Text))
            {
                invalidFields.Add("Email Address");
            }

            if (!AllRolesInCompany.Contains(JobTitleTextBox.Text))
            {
                invalidFields.Add("Job Title");
            }

            if (invalidFields.Count > 0)
            {
                string errorMessage = "The following fields have invalid data: " + string.Join(", ", invalidFields);
                MessageBox.Show(errorMessage);
            }
            else
            {
              
                //פונקציה שמוסיפה עובד למערך העובדים בדטה
                Employee employee = new Employee();
                employee.Id = int.Parse(IdTextBox.Text) ;
                employee.FirstName=FirstNameTextBox.Text ;
                employee.LastName=LastNameTextBox.Text ;
                employee.PhoneNumber=PhoneNumberTextBox.Text ;
                employee.Age = int.Parse(AgeTextBox.Text);
                employee.StartOfWorkYear = int.Parse(StartOfWorkingYearTextBox.Text);
                employee.City = CityAddressTextBox.Text;
                employee.Street = StreetAddressTextBox.Text;
                employee.RoleInCompany = JobTitleTextBox.Text;
                employee.PhoneNumber = PhoneNumberTextBox.Text;
                employee.Email = MailAddressTextBox.Text;

                OnAddEmployee(employee);

                MessageBox.Show("Employee added Successfully");
            }
            this.Close();
        }

        public bool check(Employee employee)
        {
            return true;
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
