using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Neumaticos_del_Cibao.Apps.Common
{
    //Idea gotten from:  http://stackoverflow.com/questions/19374471/wpf-datagrid-create-a-datagridnumericcolumn-in-wpf

    public class DataGridNumericColumn : DataGridTextColumn
    {
        protected override object PrepareCellForEdit(FrameworkElement editingElement, RoutedEventArgs editingEventArgs)
        {
            var textbox = editingElement as TextBox;
            textbox.PreviewTextInput += OnPreviewTextInput;
            return base.PrepareCellForEdit(editingElement, editingEventArgs);
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
                MessageBox.Show("Este campo solamente permite datos numericos. Favor revise su entrada.","Error en entrada");
                e.Handled = true;
            }
        }
        
    }
}
