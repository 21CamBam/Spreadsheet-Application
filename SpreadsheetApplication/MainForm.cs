// Cammi Smith
// 11366085
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CptS322.Spreadsheet_Engine;

namespace SpreadsheetApplication
{
    public partial class MainForm : Form
    {
        Spreadsheet workbook;
        private int numRows = 50;
        private int numCols = 26;

        public MainForm()
        {
            InitializeComponent();

            workbook = new Spreadsheet(numRows, numCols);
            workbook.CellPropertyChanged += Cell_PropertyChanged; // Subrscribe to CellPropetyChanged event

            int i = 0, character;
            for (i = 0; i < numCols; i++) // Initialize columns
            {
                character = 'A' + i;
                DataColumn col = new DataColumn(Convert.ToString((char)character));
                dataGridView.Columns.Add("col" + Convert.ToString(i), Convert.ToString((char)character));
            }
            
            for (i = 1; i <= numRows; i++) // Initialize rows
            {
                dataGridView.Rows.Add();
                dataGridView.Rows[i-1].HeaderCell.Value = Convert.ToString(i);
            }
        } 

        private void Cell_PropertyChanged(object sender, PropertyChangedEventArgs e) // Triggered when CellPropertyChanged event happens
        {
            if (e.PropertyName == "Text")
                dataGridView.Rows[workbook.ChangedCellRow].Cells[workbook.ChangedCellCol].Value = workbook.ChangedCellVal; // Updates UI
        }

        private void buttonDemo_Click(object sender, EventArgs e) // Creates Demo for functionality of App
        {
            Random rand = new Random();

            for (int i = 0; i < 50; i++)
                workbook.CellChanged(rand.Next(numRows), rand.Next(numCols), "Swarley");

            for (int j = 0; j < numRows; j++)
            {
                workbook.CellChanged(j, 1, "This is cell B" + Convert.ToString(j+1));
                workbook.CellChanged(j, 0, "=B" + Convert.ToString(j+1));
            }
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e) // Updates Spreadsheet class workbook when change is made to UI component
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                workbook.CellChanged(e.RowIndex, e.ColumnIndex, dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
        }
    }
}
