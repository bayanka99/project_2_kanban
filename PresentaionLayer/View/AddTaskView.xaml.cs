using Presentation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PresentaionLayer;
using Presentation.Model;

namespace Presentation.View
{
    /// <summary>
    /// Interaction logic for AddTaskView.xaml
    /// </summary>
    public partial class AddTaskView : Window
    {

        private AddTaskViewModel viewModel;
        //field
        public AddTaskView(BoardModel b, UserModel u)
        {
            InitializeComponent();
            this.DataContext = new AddTaskViewModel(b,u);
            this.viewModel = (AddTaskViewModel)DataContext;

        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.AddTask()) { 
                BoardView boardView = new BoardView(viewModel.board, viewModel.user);
                boardView.Show();
                this.Close();
            }

        }

    }
}
