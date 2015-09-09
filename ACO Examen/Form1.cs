using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACO_Examen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); 
            dataGridView1.DataSource = GetTable(); 
        }

        public DataTable GetTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("T1", typeof(int));
            table.Columns.Add("T2", typeof(int));
            table.Columns.Add("T3", typeof(int));
            table.Columns.Add("T4", typeof(int));
            table.Columns.Add("T5", typeof(int));
            table.Columns.Add("T6", typeof(int));
            table.Columns.Add("T7", typeof(int));
            table.Columns.Add("T8", typeof(int));
            table.Columns.Add("T9", typeof(int));
            table.Columns.Add("T10", typeof(int));
            table.Columns.Add("T11", typeof(int));
            table.Columns.Add("T12", typeof(int));
            table.Columns.Add("T13", typeof(int));
            table.Columns.Add("T14", typeof(int));
            table.Columns.Add("T15", typeof(int));
            table.Rows.Add(3, 6, 8, 4, 3, 5, 4, 8, 6, 4, 3, 5, 8, 4, 3);
            table.Rows.Add(5, 5, 3, 6, 4, 6, 5, 3, 4, 7, 5, 5, 3, 7, 7);
            table.Rows.Add(6, 7, 4, 7, 6, 8, 4, 7, 6, 6, 4, 3, 7, 6, 5);
            table.Rows.Add(4, 3, 5, 5, 4, 3, 7, 5, 3, 3, 8, 4, 6, 5, 8);
            table.Rows.Add(8, 4, 6, 5, 8, 7, 3, 6, 5, 5, 4, 7, 6, 3, 4);
            table.Rows.Add(3, 8, 7, 3, 6, 4, 8, 4, 7, 6, 5, 6, 4, 4, 3);
            return table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ACO objACO = new ACO();
            Values val = new Values();
            val.tablevisibility = new double[6, 15];
            val.tablepheromones = new double[6, 15];
            int i, j, k, flag = 0;

            int[,] table = new int[6, 15];
            string x;
            for (i = 0; i < 6; i++)
            {
                for (j = 0; j < 15; j++)
                {
                    x = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    table[i, j] = int.Parse(x);
                }
            }
            val = objACO.initializearrays(table);
            richTextBox1.Clear();
            AppendText(this.richTextBox1, Color.Black, "Table of Visibility\n".ToString());
            for (i = 0; i < 6; i++)
            {
                for (j = 0; j < 15; j++)
                {
                    AppendText(this.richTextBox1, Color.Black, String.Format("{0:0.0000}", val.tablevisibility[i, j]) + "     ".ToString());
                    if (j / 14 == 1) AppendText(this.richTextBox1, Color.Black, '\n'.ToString());
                }
            }

            int sumbesttotal = 0, sumbest = 0, sumnormal = 0, n;
            int[] psumbest = new int[6] { 0, 0, 0, 0, 0, 0 };
            int[] psumnormal = new int[6] { 0, 0, 0, 0, 0, 0 };
            int[] psumbesttotal = new int[6] { 0, 0, 0, 0, 0, 0 };
            int big, bigger, biggest, bestsolution = 0;
            for (n = 0; n < numericUpDown5.Value; n++)
            {
                for (i = 0; i < numericUpDown4.Value; i++)
                {
                    val = objACO.buildsolution((double)numericUpDown1.Value, (double)numericUpDown2.Value, val);
                    if (i == 0)
                    {
                        val.bestsolution = val.taboo;
                        if (n == 0)
                        {
                            val.bestsolutiontotal = val.bestsolution;
                        }
                    }
                    else
                    {
                        sumbesttotal = sumbest = sumnormal = big = bigger = biggest = 0;
                        for (j = 0; j < 6; j++)
                        {
                            psumnormal[j] = psumbest[j] = psumbesttotal[j] = 0;
                        }
                        for (j = 0; j < 15; j++)
                        {
                            sumbesttotal = sumbesttotal + table[val.bestsolutiontotal[j], j];
                            sumbest = sumbest + table[val.bestsolution[j], j];
                            sumnormal = sumnormal + table[val.taboo[j], j];

                            psumbesttotal[val.bestsolutiontotal[j]] = psumbesttotal[val.bestsolutiontotal[j]] + table[val.bestsolutiontotal[j], j];
                            psumbest[val.bestsolution[j]] = psumbest[val.bestsolution[j]] + table[val.bestsolution[j], j];
                            psumnormal[val.taboo[j]] = psumnormal[val.taboo[j]] + table[val.taboo[j], j];
                        }
                        for (j = 0; j < 6; j++)
                        {
                            if (j == 0)
                            {
                                biggest = psumbesttotal[j];
                                bestsolution = biggest;
                                bigger = psumbest[j];
                                big = psumnormal[j];
                            }
                            else
                            {
                                if (bigger < psumbest[j])
                                {
                                    bigger = psumbest[j];
                                }
                                if (biggest < psumbesttotal[j])
                                {
                                    biggest = psumbesttotal[j];
                                    bestsolution = biggest;
                                }
                                if (big < psumnormal[j])
                                {
                                    big = psumnormal[j];
                                }
                            }
                        }
                        if (big < bigger)
                        {
                            val.bestsolution = val.taboo;
                        }
                        if (bigger < biggest)
                        {
                            val.bestsolutiontotal = val.bestsolution;
                        }
                    }
                    if (i == numericUpDown4.Value - 1)
                    {
                        if (flag == 0)
                        {
                            AppendText(this.richTextBox1, Color.Black, "\n\nTable of Pheromones\n".ToString());
                            for (j = 0; j < 6; j++)
                            {
                                for (k = 0; k < 15; k++)
                                {
                                    AppendText(this.richTextBox1, Color.Black, String.Format("{0:00.000}", val.tablepheromones[j, k]) + "     ".ToString());
                                    if (k / 14 == 1) AppendText(this.richTextBox1, Color.Black, '\n'.ToString());
                                }
                            }
                            AppendText(this.richTextBox1, Color.Black, "\n\nBest Solution (Generation)\n".ToString());
                            for (j = 0; j < 15; j++)
                            {
                                AppendText(this.richTextBox1, Color.Black, "(T"+(j+1)+", P"+(val.bestsolution[j]+1)+") ".ToString());
                            }
                            AppendText(this.richTextBox1, Color.Black, "\n\nTotal time:" + sumbest.ToString());
                            AppendText(this.richTextBox1, Color.Black, "\n\nFinal Time:" + psumbest.Max() + '\n'); 
                            flag = 1;
                        }
                        else
                        {
                            AppendText(this.richTextBox1, Color.DarkBlue, "\n\nTable of Pheromones\n".ToString());
                            for (j = 0; j < 6; j++)
                            {
                                for (k = 0; k < 15; k++)
                                {
                                    AppendText(this.richTextBox1, Color.DarkBlue, String.Format("{0:0.000}", val.tablepheromones[j, k]) + "     ".ToString());
                                    if (k / 14 == 1) AppendText(this.richTextBox1, Color.DarkBlue, '\n'.ToString());
                                }
                            }
                            AppendText(this.richTextBox1, Color.DarkBlue, "\n\nBest Solution (Generation)\n".ToString());
                            for (j = 0; j < 15; j++)
                            {
                                AppendText(this.richTextBox1, Color.DarkBlue, "(T" + (j + 1) + ", P" + (val.bestsolution[j] + 1) + ") ".ToString());
                            }
                            AppendText(this.richTextBox1, Color.DarkBlue, "\n\nTotal time:" + sumbest.ToString());
                            AppendText(this.richTextBox1, Color.DarkBlue, "\n\nFinal Time:" + psumbest.Max() + '\n');
                            flag = 0;
                        }
                    }
                }
                objACO.updatepheromone((double)numericUpDown3.Value, val);
            }
            
            AppendText(this.richTextBox1, Color.Red, "\nBEST SOLUTION\n".ToString());
            for (i = 0; i < 6; i++)
            {
                AppendText(this.richTextBox1, Color.Black, "P" + (i+ 1) + ": ".ToString());
                for (j = 0; j < 15; j++)
                {
                    if (i == val.bestsolutiontotal[j])
                    {
                        AppendText(this.richTextBox1, Color.Black, "(T" + (j + 1) + ") ".ToString());
                    }
                }
                AppendText(this.richTextBox1, Color.Black, "\n".ToString());
                //AppendText(this.richTextBox1, Color.Black, "(T" + (j + 1) + ", P" + (val.bestsolution[j] + 1) + ") ".ToString());
            }
            AppendText(this.richTextBox1, Color.Black, "Best Time:"+bestsolution.ToString());
            MessageBox.Show("End of the Process");
        }

        void AppendText(RichTextBox box, Color color, string text)
        {
            int start = box.TextLength;
            box.AppendText(text);
            int end = box.TextLength;
            box.Select(start, end - start);
            {
                box.SelectionColor = color;
            }
            box.SelectionLength = 0;
        }

    }
}
