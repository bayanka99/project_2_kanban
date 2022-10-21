using Presentation.Model;
using Presentation.ViewModel;
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

namespace Presentation.View
{
    /// <summary>
    /// Interaction logic for TaskView.xaml
    /// </summary>
    /// 


    public partial class TaskView : Window
    {


        private TaskViewModel taskviewmodel;
        public TaskView(TaskModel taskmod,string usermail, string boardcreator, string nameofboard,int colord)
        {
            InitializeComponent();
            this.DataContext = new TaskViewModel(taskmod,usermail,boardcreator,nameofboard,colord);
            this.taskviewmodel = (TaskViewModel)DataContext;
        }






        private void Goback_Click(object sender, RoutedEventArgs e)
        {
            //go back to colmunview
            //maybe add column as an argument in taskview contructor
        }

        private void UpdateTitle_Click(object sender, RoutedEventArgs e)
        {
            taskviewmodel.UpdateTitle();
        }

        private void UpdateDescreption_Click(object sender, RoutedEventArgs e)
        {
            taskviewmodel.UpdateDesc();
        }

        private void UpdateDueDate_Click(object sender, RoutedEventArgs e)
        {
            taskviewmodel.UpdateDuedate();
        }
    }
}
