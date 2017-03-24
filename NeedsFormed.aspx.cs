using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Drawing;
using System.Web.UI.WebControls;

public partial class Contact : Page
{
    static int total = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "needsFormed.txt")).Count() + 3;
    public string[,] needs = new string[total, 30];
    public string[,] display = new string[total, 30];

    /// <summary>
    /// Main Method
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
    /// Puts parts into table
    /// </summary>
    protected void LoadTable()
    {
        display = new string[total, 30];
        display = ReadFile();

        if (display[0, 0] != null)
        {
            PrInIT.Enabled = true;  
            Needs_Box.Visible = false;
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



            dt2.Columns.Add(dc1);
            dt2.Columns.Add(dc2);
            dt2.Columns.Add(dc3);
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
            //dt2.Columns.Add(dc27);


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
                    dt2.Rows.Add(dr);

                }
            }


            GridView1.DataSource = dt2;
            GridView1.DataBind();
        }
        else
        {
            Form_But.Visible = false;
            Form_But0.Visible = false;
            PrInIT.Enabled = false;
            Needs_Box.Text = "No More Parts Need Formed";
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
    /// Opens new window of given url
    /// </summary>
    /// <param name="url"></param>
    private void OpenNewWindown(string url)
    {

        ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>document.location.href = ('{0}');</script>", url));
    }

    /// <summary>
    /// Reads file into for parts
    /// </summary>
    /// <returns></returns>
    protected string[,] ReadFile()
    {
        needs = new string[total, 30];
        string[] split = new string[1300];
        using (StreamReader SR = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "needsFormed.txt")))
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

                    for (int j = 0; j < 30; j++)
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
                            for (int q = 0; q < 30; q++)
                            {
                                display[p, q] = needs[m, q];
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

    /// <summary>
    /// Updates files
    /// </summary>
    /// <param name="edited"></param>
    protected void RemakeFile(string[,] edited)
    {
        var make = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"needsFormed.txt"));
        make.Close();
        using (var sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"needsFormed.txt"), true))
        {
            for (int i = 0; i < total; i++)
            {
                string output = "";
                if (edited[i, 0] != null && edited[i, 0] != "")
                {
                    for (int j = 0; j < 30; j++)
                    {
                        output += edited[i, j] + "|";
                    }
                    sw.WriteLine(output);
                }

            }
            sw.Close();
        }

        LoadTable();
    }


    /// <summary>
    /// Sends parts to the programmer and in progress
    /// </summary>
    /// <param name="edited"></param>
    protected void inProgress(string[,] edited)
    {


        using (var sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"InProgress.txt"), true))
        {
            for (int i = 0; i < total; i++)
            {
                string output = "";
                if (edited[i, 0] != null && edited[i, 0] != "")
                {
                    edited[i, 27] = PrInIT.Text;
                    for (int j = 0; j < 30; j++)
                    {
                        output += edited[i, j] + "|";
                    }
                    sw.WriteLine(output);
                }

            }
            sw.Close();
        }

        int programmer = 0;

        if (!(File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"P1.txt"))))
        {
            programmer = 1;
        }
        else if (!(File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"P2.txt"))))
        {
            programmer = 2;
        }
        else if (!(File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"P3.txt"))))
        {
            programmer = 3;
        }

        switch (programmer)
        {
            case 1:
                var file1 = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"PB1.txt"));
                file1.Close();
                using (var sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"PB1.txt"), true))
                {
                    for (int i = 0; i < total; i++)
                    {
                        string output = "";
                        if (edited[i, 0] != null && edited[i, 0] != "")
                        {
                            edited[i, 27] = PrInIT.Text;
                            for (int j = 0; j < 30; j++)
                            {
                                output += edited[i, j] + "|";
                            }
                            sw.WriteLine(output);
                        }

                    }
                    sw.Close();
                }
                OpenNewWindown(@"PB1");
                break;
            case 2:
                var file2 = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"PB2.txt"));
                file2.Close();
                using (var sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"PB2.txt"), true))
                {
                    for (int i = 0; i < total; i++)
                    {
                        string output = "";
                        if (edited[i, 0] != null && edited[i, 0] != "")
                        {
                            edited[i, 27] = PrInIT.Text;
                            for (int j = 0; j < 30; j++)
                            {
                                output += edited[i, j] + "|";
                            }
                            sw.WriteLine(output);
                        }

                    }
                    sw.Close();
                }
                OpenNewWindown(@"PB2");
                break;
            case 3:
                var file3 = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"PB3.txt"));
                file3.Close();
                using (var sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"PB3.txt"), true))
                {
                    for (int i = 0; i < total; i++)
                    {
                        string output = "";
                        if (edited[i, 0] != null && edited[i, 0] != "")
                        {
                            edited[i, 27] = PrInIT.Text;
                            for (int j = 0; j < 30; j++)
                            {
                                output += edited[i, j] + "|";
                            }
                            sw.WriteLine(output);
                        }

                    }
                    sw.Close();
                }
                OpenNewWindown(@"PB3");
                break;
        }

    }

    /// <summary>
    /// Finds the rows that are selected
    /// </summary>
    protected void FormBut_Click(object sender, EventArgs e)
    {

        display = ReadFile();

        string[] selected = new string[total];
        int counter = 0;
        string[,] InProgresss = new string[total, 30];
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkSelect") as CheckBox);
                if (chkRow.Checked)
                {
                    selected[counter] = row.RowIndex.ToString();
                    counter++;
                }
            }
        }

        if (selected != null)
        {
            for (int i = 0; i < counter; i++)
            {
                int row = Int32.Parse(selected[i]);
                //int count = 0;
                for (int m = 0; m < 30; m++)
                {
                    InProgresss[i, m] = display[row, m];
                }
                display[row, 0] = "";

            }
        }
        inProgress(InProgresss);
        RemakeFile(display);

    }

    /// <summary>
    /// If priority is high it changes to yellow
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_DataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string location = (e.Row.Cells[13].Text);

            foreach (TableCell cell in e.Row.Cells)
            {
                if (location.Equals("High"))
                {
                    cell.BackColor = Color.FromArgb(0, 252, 252, 100);
                }

            }
        }
    }

}





