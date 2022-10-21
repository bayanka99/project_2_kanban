using PresentaionLayer;
using Presentation.Model;
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
    /// Interaction logic for JoinBoard.xaml
    /// </summary>
    public partial class JoinBoard : Window
    {
        private JoinBoardViewModel viewModel;
        private Model.UserModel userM;
        //field
        public JoinBoard(Model.UserModel userM)
        {
            InitializeComponent();
            this.userM = userM;
            this.DataContext = new JoinBoardViewModel(userM);
            this.viewModel = (JoinBoardViewModel)DataContext;

        }


        private void JoinBoard_Click(object sender, RoutedEventArgs e)
        {
            BoardModel board = viewModel.JoinBoard();
            if(board != null)
            {
                BoardView boardWindow = new BoardView(board, userM);
                boardWindow.Show();
                this.Close();
            }
        }

    }
}
