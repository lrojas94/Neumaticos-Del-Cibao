using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Neumaticos_del_Cibao.Apps.Common
{
    class Placeholder
    {
        private string placeHolderText;
        public Placeholder(string placeHolderText,TextBox textBox)
        {
            this.placeHolderText = placeHolderText;
            textBox.Text = placeHolderText;
            textBox.Foreground = new SolidColorBrush(Colors.Black) { Opacity = 0.5 };
            textBox.GotFocus += GotFocus;
            textBox.LostFocus += LostFocus;
        }

        public void GotFocus(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if(textBox.Text == placeHolderText)
            {
                textBox.Text = "";
            }
            textBox.Foreground.Opacity = 1;
        }

        public void LostFocus(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if(textBox.Text == "")
            {
                textBox.Text = placeHolderText;
                textBox.Foreground.Opacity = 0.5;
            }
        }
    }
}
