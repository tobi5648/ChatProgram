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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Controllers;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public LogInController login = new LogInController();
        public MainWindow()
        {
            InitializeComponent();
            txtUsernameBox.Focus();
        }

        #region KeyUp
        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogIn_Click(sender, e);
            }
        }

        private void txtUsernameBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtPasswordBox.Focus();
            }
        } 
        #endregion

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            string message;
            if (login.Attempt(out message, txtUsernameBox.Text, txtPasswordBox.Text))
            {
                MessageBoxResult mbr = MessageBox.Show(message, "Message", MessageBoxButton.OKCancel);
                if (mbr == MessageBoxResult.Cancel)
                {
                    MessageBox.Show("Did not login");
                }
                else if (mbr == MessageBoxResult.OK)
                {
                    this.Hide();
                    Chatroom cr = new Chatroom(txtUsernameBox.Text);
                    cr.ShowDialog();


                }
            }
            else if (!login.Attempt(out message, txtUsernameBox.Text, txtPasswordBox.Text))
            {
                MessageBox.Show(message, "Failed Logon", MessageBoxButton.OK);
            }
        }
    }
}
