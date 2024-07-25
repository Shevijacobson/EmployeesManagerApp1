using BLL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
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
    /// Interaction logic for FindCandidate.xaml
    /// </summary>
    public partial class FindCandidate : Window
    {
        public EmployeeManagerBLL EmployeeManagerBLL { get; set; }
        public List<Candidate> ListOfCandidate { get; set; }
        public Dictionary<int, List<InformationInterviewer>> DicIdCandidateToAllIntreview { get; set; }
        public List<Interview> ListOfInterview { get; set; }
        public List<Employee> ListOfEmployee { get; set; }

        public FindCandidate()
        {


            EmployeeManagerBLL = new EmployeeManagerBLL();
            InitializeComponent();

            DataContext = this;
            ListOfCandidate = EmployeeManagerBLL.GetAllCandidates();
            ListOfInterview = EmployeeManagerBLL.GetAllInterview();
            ListOfEmployee = EmployeeManagerBLL.GetAllEmployees();
            DicIdCandidateToAllIntreview = new Dictionary<int, List<InformationInterviewer>>();

            BuildDicIdCandidateToIntreview();

        }
        public void BuildDicIdCandidateToIntreview()
        {

            var lstInterviews = new List<InformationInterviewer>();
            Dictionary<int, Employee> DicEmployeeById = new Dictionary<int, Employee>();

            foreach (var employee in ListOfEmployee)
            {
                DicEmployeeById.Add(employee.Id, employee);
            }

            foreach (Candidate candidate in ListOfCandidate)
            {

                lstInterviews = ListOfInterview.Where(interview => interview.CandidateId == candidate.Id)
                .OrderBy(iterview => iterview.InterviewDate)

               .Select(interview => new InformationInterviewer
               {
                  
                   InterviewNumber = interview.InterviewNumber,
                   InterviewDate = interview.InterviewDate,
                   RoleInCompany = interview.RoleInCompany,
                   NameInterviewer = DicEmployeeById.ContainsKey(interview.InterviewerId) ?
                   DicEmployeeById[interview.InterviewerId].FirstName + " " + DicEmployeeById[interview.InterviewerId].LastName :
                  "Interviewer Not Found",

                   PhoneInterviewer = DicEmployeeById.ContainsKey(interview.InterviewerId) ?
                   DicEmployeeById[interview.InterviewerId].PhoneNumber : "Phone Not Found",
               }).ToList();

                DicIdCandidateToAllIntreview.Add(candidate.Id, lstInterviews);

            }

        }

        public void FilterByselectedInterviewer(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (sender as ComboBox).SelectedItem;

            if (selectedItem != null && DicIdCandidateToAllIntreview.ContainsKey(((Candidate)selectedItem).Id))
            {
                DataGridInterviews.ItemsSource = DicIdCandidateToAllIntreview[((Candidate)selectedItem).Id];
            }

        }


    }
}
