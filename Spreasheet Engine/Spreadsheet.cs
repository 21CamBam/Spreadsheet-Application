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
        public class Spreadsheet
        {
            private Cell[,] workbook;
            private ACell changed;
            private int rowCount;
            private int columnCount;
            public event PropertyChangedEventHandler CellPropertyChanged = delegate { };

            private class Cell : ACell
            {
                public Cell(int rows, int cols) : base(rows, cols) { }

                public override string EditValue // Now only Spreadsheet class can set Cell value
                {
                    get { return cellValue; }
                    set { cellValue = value; }
                }
            }

            public Spreadsheet(int rows, int cols)
            {
                workbook = new Cell[rows, cols]; // initialize new 2D array of Cells
                rowCount = rows;
                columnCount = cols;

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        workbook[i, j] = new Cell(i, j);
                        workbook[i, j].PropertyChanged += Cell_PropertyChanged; // Subscribe to all Cell PropertyChanged events
                    }
                }
            }

            public int RowCount // Returns row count
            {
                get { return rowCount; }
            }

            public int ColumnCount // Returns column count
            {
                get { return columnCount; }
            }

            public string ChangedCellVal  // For UI to read current changed Cell value
            {
                get { return changed.Value; }
            }

            public int ChangedCellRow // For UI to read current changed Cell row index
            {
                get { return changed.RowIndex; }
            }

            public int ChangedCellCol // For UI to read current changed Cell column index
            {
                get { return changed.ColumnIndex; }
            }

            private void Cell_PropertyChanged(object sender, PropertyChangedEventArgs e) // when ACell property is changed, this even is triggered
            {
                if (e.PropertyName == "Text") // If property changed was Text
                {
                    (sender as Cell).EditValue = evaluateText((sender as Cell).Text); // Evaluate new Text value to new Value value
                    changed = (sender as Cell); // Update current cell changed
                    CellPropertyChanged(this, new PropertyChangedEventArgs("Text")); // Trigger event for UI
                }
            }

            private string evaluateText(string str) // evaluates text string
            {
                if (str == "") // if cell is empty
                    return "";
                else if (str[0] != '=') // if cell is string
                    return str;
                else // if cell contains formula
                {
                    int col = Convert.ToInt32(str[1]) - (int)'A'; // Converts given char to int corresponding to column number
                    int row = Convert.ToInt32(str.Substring(2)) - 1; // Converts rest of string to int corresponding to row number

                    return workbook[row, col].EditValue; // Returns value from specified indeces
                }

                // More to be added in later HW
            }

            public ACell GetCell(int row, int col) // returns cell at specifid indeces
            {
                return workbook[row, col];
            }

            public void CellChanged(int row, int col, string text) // Changes cells based on specified indeces to specified string
            {
                workbook[row, col].Text = text;
            }
        }
    }
}