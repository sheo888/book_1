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
using MySql.Data.MySqlClient;

namespace book
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int captchaCount = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        Bas bas = new Bas();
        private void Button_Input_Click(object sender, RoutedEventArgs e)
        {

            if (Login_Input.Text != "" && Password_Input.Password != "")
            {
                string pass = "", role = "";
                if (captchaCount <= 3)
                {
                    MySqlCommand Cmd = new MySqlCommand(bas.log + "'" + Login_Input.Text.ToLower() + "';", Bas.Connect);
                    try
                    {
                        Bas.Connect.Open();
                        MySqlDataReader Data = Cmd.ExecuteReader();
                        if (Data.Read())
                        {
                            role = Data[3].ToString();
                            pass = Data[2].ToString();
                            Bas.Connect.Close();
                            Bas.Connect.Open();
                            MySqlCommand cmd = new MySqlCommand(bas.rol + "'" + role + "';", Bas.Connect);
                            MySqlDataReader Date = cmd.ExecuteReader();
                            Date.Read();
                            role = Date[9].ToString();

                        }
                        else
                        {
                            MessageBox.Show("Пользователя с таким логином не существует!");
                        }
                    }
                    catch (Exception ex)
                    {
                        //throw;
                        MessageBox.Show("Ошибка подключения к Базе Данных!" + ex.ToString());
                    }
                    finally
                    {
                        Bas.Connect.Close();
                    }

                    if (Password_Input.Password.Equals(pass))
                    {
                        switch (role)
                        {
                            case "administrator":
                                {
                                    storekeeper manager_A = new storekeeper();
                                    this.Close();
                                    manager_A.Show();
                                    break;
                                }
                            //case "Менеджер С":
                            //    {
                            //        Manager_C manager_C = new Manager_C();
                            //        this.Close();
                            //        manager_C.Show();
                            //        break;
                            //    }
                            //case "Администратор":
                            //    {

                            //        Window4 window4 = new Window4();
                            //        this.Close();
                            //        window4.Show();
                            //        break;
                            //    }
                            //case "Менеджер по аренде":
                            //    {
                            //        Window5 window5 = new Window5();
                            //        this.Close();
                            //        window5.Show();
                            //        break;
                            //    }
                            case "Удален":
                                {
                                    MessageBox.Show("Этот пользователь удален!");
                                    break;
                                }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль!");
                    }
                }
                else
                {
                    MessageBox.Show("тут будет капча");

                    //    Window6 window6 = new Window6();
                    //    this.Close();
                    //    window6.Show();
                }
                captchaCount++;
            }
            else
            {
                MessageBox.Show("Заполните, пожалуйста, данные!");
            }
        }
    }
}
