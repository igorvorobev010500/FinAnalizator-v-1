using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FinAnalizator_v_1.Forms
{
    public partial class MessageForm : Window
    {
        public MessageForm()
        {
            InitializeComponent();
        }

        public MessageForm(string message, string title)
            : this()
        {
            MessageTextBlock.Text = message;
            this.Title = title;
        }

        private void CloseForm(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
