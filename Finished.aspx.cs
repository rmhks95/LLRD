using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Default3 : System.Web.UI.Page
{
    static int total = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Finished.txt")).Count() + 3;
    public string[,] needs = new string[total, 31];
    string[,] display = new string[total, 31];

    /// <summary>
    /// Main method that gets parts and displays in table
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Finished.txt")))
        {
            var yep = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Finished.txt"));
            yep.Close();
        }
        string[] split = new string[3900];
        using (StreamReader SR = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Finished.txt")))
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

                    for (int j = 0; j < 31; j++)
                    {
                        if ((split.Length - 2) >= m)
                        {
                            needs[i, j] = split[m];
                            m++;
                            //needs = (string[,])Session["nest"];
                        }
                    }
                }
            }
            SR.Close();
        }
        
        


       string[] open = new string[total];
        for (int i = 0; i< total; i++)
        {
            if (needs[i, 0] != null)
            {
                open[i] = needs[i, 0];
            }
        }


        System.Array.Sort(open);

        string[,] display = new string[total, 31];
        int p = 0;
        int n = open.Length - 1;
        while (n > -1)
        {
            if (open[n] != null)
            {
                for (int h = total -1 ; h > -1; h--)
                {
                    if (p < total)
                    {
                        if (open[n] == needs[h, 0])
                        {
                            for (int q = 0; q < 31; q++)
                            {
                                display[p, q] = needs[h, q];
                            }
                            needs[h, 0] = "";

                            p++;
                        }

                    }
                }
            }
            n = n - 1;
        }


        if (display[0, 0] != null)
        {
            Needs_Box.Visible = false;
            DataTable dt2 = new DataTable("test");

            // DataColumn you can use constructor DataColumn(name,type);
            DataColumn dc1 = new DataColumn("Date Entered");
            DataColumn dc2 = new DataColumn("Function");
            DataColumn dc3 = new DataColumn("Engineer's Initials");
            DataColumn dc4 = new DataColumn("Part Description");
            DataColumn dc5 = new DataColumn("Part Number");
            DataColumn dc6 = new DataColumn("Quantity");
            DataColumn dc7 = new DataColumn("Revision");
            //DataColumn dc8 = new DataColumn("Cut by Date");
            //DataColumn dc9 = new DataColumn("Form by Date");
            DataColumn dc10 = new DataColumn("Type of Part");
            DataColumn dc11 = new DataColumn("Material");
            DataColumn dc12 = new DataColumn("Gas");
            DataColumn dc13 = new DataColumn("Priority");
            //DataColumn dc14 = new DataColumn("Grain Restrictions");
            //DataColumn dc15 = new DataColumn("Etch Lines");
            //DataColumn dc16 = new DataColumn("Seam of Tube Location Critical");
            //DataColumn dc17 = new DataColumn("Nest in Pairs");
            //DataColumn dc20 = new DataColumn("Restirctions on Pierces");
            //DataColumn dc21 = new DataColumn("Circle Correction");
            DataColumn dc22 = new DataColumn("After Laser Cut");
            DataColumn dc23 = new DataColumn("After Press Brake");
            DataColumn dc24 = new DataColumn("DXF");
            DataColumn dc25 = new DataColumn("PDF");
            DataColumn dc26 = new DataColumn("Notes for Programmer");
            DataColumn dc27 = new DataColumn("Programmer's Initials");
            DataColumn dc28 = new DataColumn("Nest File Location");
            DataColumn dc29 = new DataColumn("Date Nested");
            DataColumn dc30 = new DataColumn("Machine Progrmaed For");


            dt2.Columns.Add(dc1);
            dt2.Columns.Add(dc29);
            dt2.Columns.Add(dc2);
            dt2.Columns.Add(dc3);
            dt2.Columns.Add(dc27);
            dt2.Columns.Add(dc4);
            dt2.Columns.Add(dc5);
            dt2.Columns.Add(dc30);
            dt2.Columns.Add(dc6);
            dt2.Columns.Add(dc7);
            //dt2.Columns.Add(dc8);
            //dt2.Columns.Add(dc9);
            dt2.Columns.Add(dc10);
            dt2.Columns.Add(dc11);
            dt2.Columns.Add(dc12);
            dt2.Columns.Add(dc13);
            //dt2.Columns.Add(dc14);
            //dt2.Columns.Add(dc15);
            //dt2.Columns.Add(dc16);
            //dt2.Columns.Add(dc17);
            //dt2.Columns.Add(dc20);
            //dt2.Columns.Add(dc21);
            dt2.Columns.Add(dc22);
            dt2.Columns.Add(dc23);
            dt2.Columns.Add(dc24);
            dt2.Columns.Add(dc25);
            dt2.Columns.Add(dc28);
            dt2.Columns.Add(dc26);


            for (int i = 0; i < total; i++)
            {
                if (display[i, 0] != null)
                {
                    DataRow dr = dt2.NewRow();
                    dr["Date Entered"] = display[i, 0];
                    dr["Function"] = display[i, 1];
                    dr["Engineer's Initials"] = display[i, 2];
                    dr["Part Description"] = display[i, 3];
                    dr["Part Number"] = display[i, 4];
                    dr["Quantity"] = display[i, 5];
                    dr["Revision"] = display[i, 6];
                    //dr["Cut by Date"] = display[i, 7];
                    //dr["Form by Date"] = display[i, 8];
                    dr["Type of Part"] = display[i, 9];
                    dr["Material"] = display[i, 10];
                    dr["Gas"] = display[i, 11];
                    dr["Priority"] = display[i, 12];
                    //dr["Grain Restrictions"] = display[i, 13];
                    //dr["Etch Lines"] = display[i, 14];
                    //dr["Seam of Tube Location Critical"] = display[i, 15];
                    //dr["Nest in Pairs"] = display[i, 16];
                    //dr["Restirctions on Pierces"] = display[i, 19];
                    //dr["Circle Correction"] = display[i, 20];
                    dr["After Laser Cut"] = display[i, 21];
                    dr["After Press Brake"] = display[i, 22];
                    dr["DXF"] = display[i, 23];
                    dr["PDF"] = display[i, 24];
                    dr["Notes for Programmer"] = display[i, 25];
                    if (display[i, 27] != null)
                    {
                        dr["Programmer's Initials"] = display[i, 27];
                    }
                    if (display[i, 26] != null)
                    {
                        dr["Nest File Location"] = display[i, 26];
                    }
                    dr["Date Nested"] = display[i, 29];
                    dr["Machine Progrmaed For"] = display[i, 30];
                    //GridView1.Columns.Insert(0, checkBox);
                    dt2.Rows.Add(dr);
                }
            }

            GridView1.DataSource = dt2;
            GridView1.DataBind();

        }
        else
        {
            Needs_Box.Text = "No Parts";
        }
    }
}