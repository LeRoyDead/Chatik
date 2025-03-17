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
using ClientChat.ServiceChatik;

namespace ClientChat
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IServiceChatikCallback
    {
        bool isConnected = false;

        ServiceChatikClient client;

        int ID;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        void ConnectUser() 
        {
            if (!isConnected)
            {
                client = new ServiceChatikClient(new System.ServiceModel.InstanceContext(this));  // создание объекта у клиента

                TbUserName.IsEnabled = false; // блокировка поля имени юзера

                ID = client.Connect(TbUserName.Text);

                bConDiscon.Content = "Disconnect";
                
                isConnected = true;
            }
        }

        void DisconnectUser() 
        {
            if (isConnected)
            {
                TbUserName.IsEnabled = true; // разблокировка поля имени юзера

                client.Disconnect(ID);
                client = null;
                
                bConDiscon.Content = "Connect";

                isConnected = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                DisconnectUser();
            }
            else
            {
                ConnectUser();
            }
        }

        public void MsgCallback(string msg)
        {
            LbChat.Items.Add(msg); // добавляет в общий листбокс сообщения
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        private void TbMsg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (client!=null)
                {
                    client.SentMessage(TbMsg.Text, ID);
                    TbMsg.Text = string.Empty;
                }
                            
            }
        }
    }
}
