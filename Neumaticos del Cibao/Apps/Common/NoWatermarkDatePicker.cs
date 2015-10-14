using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Neumaticos_del_Cibao.Apps.Common
{
    /// <summary>
    /// By default, Datepicker's watermark cannot be changed. The idea of creating this class
    /// is to be able to handle Datepicker watermark easly. Just setting the variable PlaceholderText
    /// in your Page/Window will suffice to add the watermark you want. Remember to use this class when adding
    /// a Datepicker.
    /// 
    /// E.g:
    /// <common:NoWatermarkDatePicker PlaceholderText="Inicio de Trabajo" x:Name="startDate" Margin="0,326,10,0" 
    /// VerticalAlignment="Top" Height="25" HorizontalAlignment="Right" 
    /// Width="100" Foreground="Black"/>
    /// 
    /// Note that PlaceholderText is set to "Inicio de Trabajo", which will appear as watermark for
    /// this datepicker.
    /// </summary>
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
