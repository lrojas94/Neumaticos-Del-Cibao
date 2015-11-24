using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Neumaticos_del_Cibao.Apps.Common
{
    class FormattedNumericTextBox : FormattedTextBox
    {
        public override string RealValue
        {
            get
            {
                return base.RealValue;
            }

            set
            {
                var tmp = 0D;
                if (double.TryParse(value, out tmp))
                {
                    base.RealValue = value;
                    NumericValue = tmp;
                }
                else
                    base.RealValue = "0.00";
            }
        }

        public double NumericValue { get; private set;}

        public FormattedNumericTextBox() : base()
        {
            NumericValue = 0D;
            PreviewTextInput += OnPreviewTextInput;
        }

        void OnPreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            try
            {
                //Using + 0 allows adding decimal numbers.
                Convert.ToDouble(e.Text + "0");
            }
            catch
            {
                MessageBox.Show("Este campo solamente permite datos numericos. Favor revise su entrada.", "Error en entrada");
                e.Handled = true;
            }
        }
    }
}
