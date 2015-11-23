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
        private Brush brush;
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
                brush.Opacity = 1;
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

        private void initPlaceholder(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            brush = new SolidColorBrush(Colors.Black);
            textBox.Foreground = brush;
            brush.Opacity = 0.5;

            if (textBox.Text == "")
            //Text may be initialized before calling placeHolderText, so, better this way.
                textBox.Text = placeholderText;
            
        }

        private void gotFocus(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if(textBox.Text == placeholderText)
            {
                textBox.Text = "";
            }
            brush.Opacity = 1;
        }

        private void lostFocus(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if(textBox.Text == "")
            {
                textBox.Text = placeholderText;
                brush.Opacity = 0.5;
            }
        }
    }
}
