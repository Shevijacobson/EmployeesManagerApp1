using BLL;
using DAL.Models;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Interaction logic for FindCandidate.xaml
    /// </summary>
    public partial class FindCandidate : Window
    {
        public EmployeeManagerBLL EmployeeManagerBLL { get; set; }
        public List<Candidate>  ListOfCandidate { get; set; }
        public FindCandidate()
        {
            DataContext=this;
            EmployeeManagerBLL = new EmployeeManagerBLL();
            ListOfCandidate= EmployeeManagerBLL.GetAllCandidates();  
            InitializeComponent();
        }
    }
}
