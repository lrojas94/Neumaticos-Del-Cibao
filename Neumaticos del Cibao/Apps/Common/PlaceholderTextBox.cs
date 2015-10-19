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
    public class PlaceholderTextBox : TextBox,IPlaceholder
    {
        private string placeholderText;
        public string PlaceholderText

        {
            get
            {
                return placeholderText;
            }
            set
            {
                placeholderText = value;
            }
        }
        
        public string RealText

        {
            get
            {
                if(Text == placeholderText)
                {
                    return "";
                }
                return Text;
            }
            set
            {
                Text = value;
                Foreground.Opacity = 1;
            }
        }

        public PlaceholderTextBox()
        {
            GotFocus += gotFocus;
            LostFocus += lostFocus;
            Loaded += initPlaceholder;
        }

        public PlaceholderTextBox(string placeHolderText)
        {
            this.placeholderText = placeHolderText;
            GotFocus += gotFocus;
            LostFocus += lostFocus;
            Loaded += initPlaceholder;
        }

        public void initPlaceholder(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == "")
            //Text may be initialized before calling placeHolderText, so, better this way.
            {
                textBox.Text = placeholderText;
                textBox.Foreground = new SolidColorBrush(Colors.Black) { Opacity = 0.5 };
            }
            
        }

        public void gotFocus(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if(textBox.Text == placeholderText)
            {
                textBox.Text = "";
            }
            textBox.Foreground.Opacity = 1;
        }

        public void lostFocus(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if(textBox.Text == "")
            {
                textBox.Text = placeholderText;
                textBox.Foreground.Opacity = 0.5;
            }
        }
    }
}
