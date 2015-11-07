using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Neumaticos_del_Cibao.Database;
using System.Reflection;

namespace Neumaticos_del_Cibao.Apps.Common
{
    public class OptionGrid : Grid
    {

        public Page ParentPage { get; set; }
        public int ButtonHeight { get; set; }
        public int ColumnCount { get; set; }

        public OptionGrid() : base()
        {
            ButtonHeight = 25;
            ColumnCount = 2; 
        }

        public OptionGrid(Page parentPage,int buttonHeight,int columnCount) : base()
        {
            ParentPage = parentPage;
            ButtonHeight = buttonHeight;
            ColumnCount = columnCount;
        }

        //Build should also take an user to see his/her permissions.
        public void BuildOptionGrid(List<Option> options)
        {
            //Clear data:
            Children.Clear();
            RowDefinitions.Clear();
            ColumnDefinitions.Clear();

            //Add Columns:
            for(int i = 0;i < ColumnCount; i++)
            {
                ColumnDefinition definition = new ColumnDefinition();
                //Default width will be perfectly fine.
                ColumnDefinitions.Add(definition);
            }

            //Now make rows:
            int rowCount = options.Count / 2 + 1;

            //Now add rows:
            for (int i = 0; i < rowCount; i++)
            {
                RowDefinition definition = new RowDefinition();
                //Since we use standard size of height of 25px and 10px padding:
                definition.Height = new GridLength(10 + ButtonHeight);

                //Add the Row definition to the grid:
                RowDefinitions.Add(definition);
            }

            //Now, proceed to add options:
            //Have a var to know the row to go and the column:
            int column = 0;
            int row = 0;
            int rowCounter = 0;
            //Remember, 2 options per row, column alternates ALWAYS.

            foreach (var option in options)
            {
                Button button = new Button();
                button.Height = ButtonHeight;
                //We will use top and bottom -> Margin 5 // left/Right: 10
                button.Margin = new Thickness(10, 5, 10, 5);
                button.Content = option.OptionTitle;

                //Here we should add some action handler for each button using the OptionClass
                if(option.ClassView != null)
                {
                    Type T = Type.GetType(option.ClassView);
                    button.Click += (source, e) =>
                    {
                        object target = Activator.CreateInstance(T);
                        ParentPage.NavigationService.Navigate(target as Page);
                    };
                }
                
                //Set the rows and columns.
                Grid.SetColumn(button, column);
                Grid.SetRow(button, row);

                //Add to the actual grid.
                Children.Add(button);

                //Fill until columnCount
                column++;
                if (column == ColumnCount)
                    column = 0;

                //Check rows.
                rowCounter++;
                if (rowCounter == ColumnCount)
                {
                    rowCounter = 0;
                    row++;
                }
            }
        }
    }
}
