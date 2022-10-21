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
    /// Interaction logic for ColumnView.xaml
    /// </summary>
    public partial class ColumnView : Window
    {
        private ColumnViewModel columnViewModel;
        private string usermail;
        private string CreatorofBoard;
        private string boardname;


        public ColumnView(Model.ColumnModel col,string usermail,string boardcreator,string nameofboard)
        {
            InitializeComponent();
            this.DataContext = new ColumnViewModel(col);
            this.columnViewModel = (ColumnViewModel)DataContext;
            this.usermail = usermail;
            this.CreatorofBoard = boardcreator;
            this.boardname = nameofboard;
        }

        private void GotoTaskView_Click(object sender, RoutedEventArgs e)
        {
            //taskviewmodel
        }

       

        private void AdvanceTask_Click(object sender, RoutedEventArgs e)
        {
            columnViewModel.AdvanceTask(this.usermail, this.CreatorofBoard, this.boardname);
        }

        private void AssignTask_Click(object sender, RoutedEventArgs e)
        {
            columnViewModel.AssignTask(usermail, CreatorofBoard, boardname);
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            columnViewModel.AddTask(usermail, CreatorofBoard, boardname);
        }
    }
}
