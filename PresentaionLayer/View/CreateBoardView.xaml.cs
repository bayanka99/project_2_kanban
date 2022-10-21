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
    /// Interaction logic for CreateBoardView.xaml
    /// </summary>
    public partial class CreateBoardView : Window
    {
        private CreateBoardViewModel viewModel;
        public CreateBoardView(UserModel user)
        {
            InitializeComponent();
            this.viewModel = new CreateBoardViewModel(user);
            this.DataContext = (CreateBoardViewModel)viewModel;
        }

        private void CreateBoard_Click(object sender, RoutedEventArgs e)
        {
            BoardModel board = viewModel.AddBoard();
            if(board != null)
            {
                BoardView boardWindow = new BoardView(board, viewModel.User);
                boardWindow.Show();
                this.Close();
            }
            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            HomeView homewindow = new HomeView(viewModel.User);
            homewindow.Show();
            this.Close();
        }
    }
}
