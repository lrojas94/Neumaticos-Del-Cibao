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
        public string PlaceHolderText

        {
            get
            {
                return placeHolderText;
            }
            set
            {
                placeHolderText = value;
            }
        }

        public Placeholder(string placeHolderText,TextBox textBox)
        {
            this.placeHolderText = placeHolderText;
            textBox.GotFocus += gotFocus;
            textBox.LostFocus += lostFocus;
            textBox.Loaded += initPlaceholder;
        }

        private void initPlaceholder(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == "")
            //Text may be initialized before calling placeHolderText, so, better this way.
            {
                textBox.Text = placeHolderText;
                textBox.Foreground = new SolidColorBrush(Colors.Black) { Opacity = 0.5 };
            }
            
        }

        private void gotFocus(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if(textBox.Text == placeHolderText)
            {
                textBox.Text = "";
            }
            textBox.Foreground.Opacity = 1;
        }

        private void lostFocus(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if(textBox.Text == "")
            {
                textBox.Text = placeHolderText;
                textBox.Foreground.Opacity = 0.5;
            }
        }
    }
}
