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
using System.Data.SqlClient;
using System.Data;
using System.Windows;

namespace Dem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection dtb = new SqlConnection();
        SqlCommand command = new SqlCommand();

        bool loggedin = false;

        /// <summary>
        /// Логика для инициализации окна авторизации
        /// </summary>
        
        public MainWindow()
        {
            InitializeComponent();

            this.Width = SystemParameters.MaximizedPrimaryScreenWidth;
            this.MinWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;

            this.Height = SystemParameters.PrimaryScreenHeight;
            this.MinHeight = SystemParameters.PrimaryScreenHeight;
            this.MaxHeight = SystemParameters.PrimaryScreenHeight;
        }

        // Обработчик нажатия на кнопку выхода из программы с одноимённой функцией 
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e, Menu menu)
        {
            DataTable ds_accs = new DataTable();
            dtb = new SqlConnection("server = localhost; Trusted_Connection = Yes; DataBase = Session1_XX;");
            dtb.Open();
            DataTable dt = new DataTable("database");
            command = dtb.CreateCommand();
            string dt_accs = "select Email, Password from Users";
            SqlDataAdapter adapter = new SqlDataAdapter(dt_accs, dtb);

            adapter.Fill(ds_accs);
            dtb.Close();

            foreach (DataRow row in ds_accs.Rows)
            {
                string login = row["Email"].ToString();
                string psswd = row["Password"].ToString();

                if (tb_username.Text.ToString() == login)
                {
                    if (tb_password.ToString() == psswd)
                    {
                        this.Close();
                        loggedin = true;
                        dtb.Close();
                        menu.Show();
                    }
                }
            }

            if (!loggedin)
            {
                dtb.Close();
                MessageBox.Show("Неверный логин и/или пароль!");
            }
        }
    }
}
