using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Contact : Page
{
    static int total = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "InProgress.txt")).Count() + 3;
    public string[,] needs = new string[total, 30];
    public string[,] display = new string[total, 30];

    /// <summary>
    /// Main method
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadTable();
        }
    }

    /// <summary>
    /// Displays parts in table
    /// </summary>
    protected void LoadTable()
    {
        display = new string[total, 30];
        display = ReadFile();

        if (display[0, 0] != null)
        {
            NoneLable.Visible = false;
            DataTable dt2 = new DataTable("test");

            // DataColumn you can use constructor DataColumn(name,type);
            CheckBox dc0 = new CheckBox();
            DataColumn dc1 = new DataColumn("Date Entered");
            DataColumn dc2 = new DataColumn("Function");
            DataColumn dc3 = new DataColumn("Engineer's Initials");
            DataColumn dc4 = new DataColumn("Part Description");
            DataColumn dc5 = new DataColumn("Part Number");
            DataColumn dc6 = new DataColumn("Quantity");
            DataColumn dc7 = new DataColumn("Revision");
            DataColumn dc8 = new DataColumn("Cut by Date");
            DataColumn dc9 = new DataColumn("Form by Date");
            DataColumn dc10 = new DataColumn("Type of Part");
            DataColumn dc11 = new DataColumn("Material");
            DataColumn dc12 = new DataColumn("Gas");
            DataColumn dc13 = new DataColumn("Priority");
            DataColumn dc14 = new DataColumn("Grain Restrictions");
            DataColumn dc15 = new DataColumn("Etch Lines");
            DataColumn dc16 = new DataColumn("Seam of Tube Location Critical");
            DataColumn dc17 = new DataColumn("Nest in Pairs");
            DataColumn dc20 = new DataColumn("Restirctions on Pierces");
            DataColumn dc21 = new DataColumn("Circle Correction");
            DataColumn dc22 = new DataColumn("After Laser Cut");
            DataColumn dc23 = new DataColumn("After Press Brake");
            DataColumn dc24 = new DataColumn("DXF");
            DataColumn dc25 = new DataColumn("PDF");
            DataColumn dc26 = new DataColumn("Notes for Programmer");
            DataColumn dc27 = new DataColumn("Programmer's Initials");
            



            dt2.Columns.Add(dc1);
            dt2.Columns.Add(dc2);
            dt2.Columns.Add(dc3);
            dt2.Columns.Add(dc27);
            dt2.Columns.Add(dc4);
            dt2.Columns.Add(dc5);
            dt2.Columns.Add(dc6);
            dt2.Columns.Add(dc7);
            dt2.Columns.Add(dc8);
            dt2.Columns.Add(dc9);
            dt2.Columns.Add(dc10);
            dt2.Columns.Add(dc11);
            dt2.Columns.Add(dc12);
            dt2.Columns.Add(dc13);
            dt2.Columns.Add(dc14);
            dt2.Columns.Add(dc15);
            dt2.Columns.Add(dc16);
            dt2.Columns.Add(dc17);
            dt2.Columns.Add(dc20);
            dt2.Columns.Add(dc21);
            dt2.Columns.Add(dc22);
            dt2.Columns.Add(dc23);
            dt2.Columns.Add(dc24);
            dt2.Columns.Add(dc25);
            dt2.Columns.Add(dc26);


            for (int i = 0; i < total; i++)
            {
                if (display[i, 0] != null)
                {
                    //DataGridViewCheckBoxColumn checkBox = new DataGridViewCheckBoxColumn();
                    DataRow dr = dt2.NewRow();
                    dr["Date Entered"] = display[i, 0];
                    dr["Function"] = display[i, 1];
                    dr["Engineer's Initials"] = display[i, 2];
                    dr["Part Description"] = display[i, 3];
                    dr["Part Number"] = display[i, 4];
                    dr["Quantity"] = display[i, 5];
                    dr["Revision"] = display[i, 6];
                    dr["Cut by Date"] = display[i, 7];
                    dr["Form by Date"] = display[i, 8];
                    dr["Type of Part"] = display[i, 9];
                    dr["Material"] = display[i, 10];
                    dr["Gas"] = display[i, 11];
                    dr["Priority"] = display[i, 12];
                    dr["Grain Restrictions"] = display[i, 13];
                    dr["Etch Lines"] = display[i, 14];
                    dr["Seam of Tube Location Critical"] = display[i, 15];
                    dr["Nest in Pairs"] = display[i, 16];
                    dr["Restirctions on Pierces"] = display[i, 19];
                    dr["Circle Correction"] = display[i, 20];
                    dr["After Laser Cut"] = display[i, 21];
                    dr["After Press Brake"] = display[i, 22];
                    dr["DXF"] = display[i, 23];
                    dr["PDF"] = display[i, 24];
                    dr["Notes for Programmer"] = display[i, 25];
                    if (display[i, 27] != null)
                    {
                        dr["Programmer's Initials"] = display[i, 27];
                    }
                    dt2.Rows.Add(dr);

                }
            }


            Grid.DataSource = dt2;
            Grid.DataBind();
        }
        else
        {
            NoneLable.Visible = true;
        }
    }
    public class ArrayComparer : System.Collections.IComparer
    {
        int ix;
        public ArrayComparer(int SortFieldIndex)
        {
            ix = SortFieldIndex;
        }
        public int Compare(object x, object y)
        {
            IComparable cx = (IComparable)((Array)x).GetValue(ix);
            IComparable cy = (IComparable)((Array)y).GetValue(ix);
            return cx.CompareTo(cy);
        }
    }

  /// <summary>
  /// Reads file to get all parts
  /// </summary>
  /// <returns></returns>
    protected string[,] ReadFile()
    {
        needs = new string[total, 30];
        string[] split = new string[1300];
        using (StreamReader SR = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "InProgress.txt")))
        {
            string line;
            int m = 0;
            for (int i = 0; i < total; i++)
            {
                line = SR.ReadLine();
                if (line != null)
                {
                    m = 0;
                    split = line.Split('|');

                    for (int j = 0; j < 28; j++)
                    {
                        if ((split.Length - 2) >= m)
                        {
                            needs[i, j] = split[m];
                            m++;
                        }
                    }
                }
            }
            SR.Close();
        }


        string[] open = new string[total];
        for (int i = 0; i < total; i++)
        {
            if (needs[i, 10] != null)
            {
                open[i] = needs[i, 10];
            }
        }
        System.Array.Sort(open);

        int p = 0;
        int n = 0;
        while (n < open.Length)
        {
            if (open[n] != null)
            {
                for (int m = total-1 ; m > -1; m--)
                {
                    if (p < total)
                    {
                        if (open[n] == needs[m, 10])
                        {
                            for (int q = 0; q < 28; q++)
                            {
                                if (needs[m, q] != null)
                                {
                                    display[p, q] = needs[m, q];
                                }
                            }
                            needs[m, 10] = "";

                            p++;
                        }

                    }
                }
            }
            n++;
        }

        return display;
    }

}

//Response.Write("  <script language='javascript'> window.open('HomePage.aspx','','width=1020,Height=720,fullscreen=1,location=0,scrollbars=1,menubar=1,toolbar=1'); </script>");




