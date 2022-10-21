using Presentation.ViewModel;
using System.Windows;
using Presentation.Model;

namespace Presentation.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : Window
    {
        private HomeViewModel viewModel;

        public HomeView(UserModel u)
        {
            InitializeComponent();
            this.viewModel = new HomeViewModel(u);
            this.DataContext = (HomeViewModel)viewModel;
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Logout();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }


        private void Search_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreateBoard_Click(object sender, RoutedEventArgs e)
        {
            CreateBoardView createBoardView = new CreateBoardView(viewModel.User);
            createBoardView.Show();
            this.Close();
        }

        private void InProgressTasks_Click(object sender, RoutedEventArgs e)
        {
            //ToDo
        }

        private void JoinBoard_Click(object sender, RoutedEventArgs e)
        {
            UserModel user = viewModel.JoinBoard();
            JoinBoard joinBoard = new JoinBoard(user);
            joinBoard.Show();
            this.Close();
        }

        private void ChooseBoard_Click(object sender, RoutedEventArgs e)
        {
            BoardView boardView = new BoardView(viewModel.SelectedBoard, viewModel.User);
            boardView.Show();
            this.Close();
        }

        private void RemoveBoard_Click(object sender, RoutedEventArgs e)
        {
            viewModel.RemoveBoard();
        }
    }
}