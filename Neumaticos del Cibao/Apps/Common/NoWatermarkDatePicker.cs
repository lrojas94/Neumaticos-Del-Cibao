using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Neumaticos_del_Cibao.Apps.Common
{
    public class NoWatermarkDatePicker : DatePicker
    {
        Placeholder placeholder;
        string placeholderText = "";
        public string PlaceholderText
        {
            get
            {
                return placeholderText;
            }
            set
            {
                placeholderText = value;
                if(placeholder != null)
                {
                    placeholder.PlaceHolderText = placeholderText;
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            DatePickerTextBox box = base.GetTemplateChild("PART_TextBox") as DatePickerTextBox;
            box.ApplyTemplate();
            ContentControl control = box.Template.FindName("PART_Watermark",box) as ContentControl;
            control.Content = "";
            placeholder = new Placeholder(placeholderText, box);
        }
    }
}
