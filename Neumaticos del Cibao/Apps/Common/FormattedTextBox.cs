using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;


namespace Neumaticos_del_Cibao.Apps.Common
{
    public class FormattedTextBox : TextBox
    {
        public string Formatter{ get; set; }
        public virtual string RealValue { get; set; }

        public FormattedTextBox() {
            GotFocus += onGotFocus;
            LostFocus += onLostFocus;
            Formatter = "{0}"; //Set a default formatter just in case.
        }
        
        protected void onGotFocus (object sender, EventArgs e)
        {
            Text = RealValue;
        }

        protected void onLostFocus(object sender, EventArgs e)
        {
            RealValue = Text;
            var number = 0D;
            if (double.TryParse(Text, out number))
                Text = string.Format(Formatter, number);
            else
                Text = string.Format(Formatter, RealValue);
        }
    }
}
