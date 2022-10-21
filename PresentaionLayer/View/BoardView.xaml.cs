using Frontend.ViewModel;
using PresentaionLayer;
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
using Presentation.Model;

namespace Presentation.View
{
    /// <summary>
    /// Interaction logic for BoardView.xaml
    /// </summary>
    /// boa

    public partial class BoardView : Window
    {
        private BoardViewModel boardviewmodel;

        public BoardView(BoardModel b,UserModel u)
        {
            InitializeComponent();
            this.DataContext = new BoardViewModel(b,u);
            this.boardviewmodel = (BoardViewModel)DataContext;
        }

        private void AddColumn_Click(object sender, RoutedEventArgs e)
        {
            boardviewmodel.AddColumn();
        }

        private void Goback_Click(object sender, RoutedEventArgs e)
        {

            HomeView homewindow = new HomeView(boardviewmodel.user); //todo
            homewindow.Show();
            this.Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

            boardviewmodel.DeleteCol();

        }

        private void RenameColumn_Click(object sender, RoutedEventArgs e)
        {
            boardviewmodel.RenameColumn();
        }

        private void MoveCol_Click(object sender, RoutedEventArgs e)
        {
            boardviewmodel.MoveCol();
        }

        private void Limitcol_Click(object sender, RoutedEventArgs e)
        {
            //boardviewmodel.Limit
        }

        private void GotoColView_Click(object sender, RoutedEventArgs e)
        {
            ColumnView colView = new ColumnView(boardviewmodel.SelectedColumn,boardviewmodel.board.usermail,boardviewmodel.board.BoardCreator,boardviewmodel.board.BoardName);

            colView.Show();
            this.Close();
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {

            AddTaskView addTaskView = new AddTaskView(boardviewmodel.board, boardviewmodel.user);
            addTaskView.Show();
            this.Close();
        }
        private void AdvanceTask_Click(object sender, RoutedEventArgs e)
        {

            boardviewmodel.AdvenceTask();
        }
        
    }
}
