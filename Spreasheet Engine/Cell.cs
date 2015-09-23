// Cammi Smith
// 11366085

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CptS322
{
    namespace Spreadsheet_Engine
    {
        public abstract class ACell : INotifyPropertyChanged
        {
            private int rowIndex;
            private int columnIndex;
            protected string text;
            protected string cellValue;
            public event PropertyChangedEventHandler PropertyChanged = delegate { };

            public ACell(int row, int col)
            {
                rowIndex = row;
                columnIndex = col;
            }

            public int RowIndex
            {
                get { return rowIndex; }
            }

            public int ColumnIndex
            {
                get { return columnIndex; }
            }

            public string Text
            {
                get { return text; }
                set
                {
                    if (value == text) { return; } // If no change to cell, don't notify subscribers

                    text = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Text")); // Triggers event
                }
            }

            public string Value // Property for non-inheriting classes to read Cell value
            {
                get { return cellValue; }
            }

            public abstract string EditValue // Property to be overwritten
            {
                get;
                set;
            }
        }

    }
}