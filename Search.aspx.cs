using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Home : System.Web.UI.Page
{
    static int total;
    public string[,] needs = new string[total, 31];
    string[,] display = new string[total, 31];
    string[,] found = new string[total, 31];


    /// <summary>
    /// Main method, that gets and displays the parts
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        total = CountParts();

        needs = new string[total, 31];
        display = new string[total, 31];
        found = new string[total,31];


        if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"needsNested.txt")))
        {
            var yep= File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"needsNested.txt"));
            yep.Close();
        }
        if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"needsFormed.txt")))
        {
            var yep = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"needsFormed.txt"));
            yep.Close();
        }
        if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"InProgress.txt")))
        {
            var yep = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"InProgress.txt"));
            yep.Close();
        }
        if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Finished.txt")))
        {
            var yep = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Finished.txt"));
            yep.Close();
        }
        string[] split = new string[total];
        StreamReader SR = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"needsNested.txt"));
        int toUse = 0;
        string line;
        int m = 0;
        for (int i = 0; i < total; i++)
        {
            if (toUse == 0)
            {
                needs[i, 28] = "Needs Nested";
            }
            else if (toUse == 1)
            {
                needs[i, 28] = "Needs Formed";
            }
            else if(toUse == 2)
            {
                needs[i, 28] = "In Progress";

            }
            else if (toUse == 3)
            {
                needs[i, 28] = "Finished";

            }


            line = SR.ReadLine();
            if (line != null)
            {
                m = 0;
                split = line.Split('|');

                for (int j = 0; j < 31; j++)
                {
                    if ((split.Length - 2) >= m)
                    {
                        if (j != 28)
                        {
                            needs[i, j] = split[m];
                        }
                        m++;
                        //needs = (string[,])Session["nest"];
                    }
                }
            }
            else
            {
                toUse++;
                switch (toUse)
                {
                    case 1:
                        SR.Close();
                        SR = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"needsFormed.txt"));
                        break;
                    case 2:
                        SR.Close();
                        SR = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"InProgress.txt"));
                        break;
                    case 3:
                        SR.Close();
                        SR = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Finished.txt"));
                        break;
                    case 4:
                        //SR.Close();
                        break;                   
                }


            }
        }
        SR.Close();
    

        string[] open = new string[total];
        for (int i = 0; i < total; i++)
        {
            if (needs[i, 0] != null)
            {
                open[i] = needs[i, 0];
            }
        }

        System.Array.Sort(open);

        display = new string[total, 31];
        int p = 0;
        int n = open.Length-1;
        while (n>-1)
        {
            if (open[n] != null)
            {
                for (int h = total-1; h> -1; h--)
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
            n=n-1;
        }


        found = find(display);
        if (found[0, 0] != null && PartNum.Text != "")
        {
            Needs_Box.Visible = false;
            GridView1.Visible = true;
            Session["array"] = found;
            DataTable dt2 = new DataTable("test");

            // DataColumn you can use constructor DataColumn(name,type);
            DataColumn dc0 = new DataColumn("Location");
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
            DataColumn dc18 = new DataColumn("Product Line");
            DataColumn dc19 = new DataColumn("Department to Charge");
            DataColumn dc20 = new DataColumn("Restirctions on Pierces");
            DataColumn dc21 = new DataColumn("Circle Correction");
            DataColumn dc22 = new DataColumn("After Laser Cut");
            DataColumn dc23 = new DataColumn("After Press Brake");
            DataColumn dc24 = new DataColumn("DXF");
            DataColumn dc25 = new DataColumn("PDF");
            DataColumn dc26 = new DataColumn("Notes for Programmer");
            DataColumn dc27 = new DataColumn("Programmer's Initials");
            DataColumn dc28 = new DataColumn("Nest File Location");
            DataColumn dc29 = new DataColumn("Date Nested");


            dt2.Columns.Add(dc0);
            dt2.Columns.Add(dc1);
            dt2.Columns.Add(dc29);
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
            dt2.Columns.Add(dc18);
            dt2.Columns.Add(dc19);
            dt2.Columns.Add(dc20);
            dt2.Columns.Add(dc21);
            dt2.Columns.Add(dc22);
            dt2.Columns.Add(dc23);
            dt2.Columns.Add(dc24);
            dt2.Columns.Add(dc25);
            dt2.Columns.Add(dc28);
            dt2.Columns.Add(dc26);


            for (int i = 0; i < total; i++)
            {
                if (found[i, 0] != null)
                {

                    DataRow dr = dt2.NewRow();
                    dr["Location"] = found[i, 28];
                    dr["Date Entered"] = found[i, 0];
                    dr["Function"] = found[i, 1];
                    dr["Engineer's Initials"] = found[i, 2];
                    dr["Part Description"] = found[i, 3];
                    dr["Part Number"] = found[i, 4];
                    dr["Quantity"] = found[i, 5];
                    dr["Revision"] = found[i, 6];
                    dr["Cut by Date"] = found[i, 7];
                    dr["Form by Date"] = found[i, 8];
                    dr["Type of Part"] = found[i, 9];
                    dr["Material"] = found[i, 10];
                    dr["Gas"] = found[i, 11];
                    dr["Priority"] = found[i, 12];
                    dr["Grain Restrictions"] = found[i, 13];
                    dr["Etch Lines"] = found[i, 14];
                    dr["Seam of Tube Location Critical"] = found[i, 15];
                    dr["Nest in Pairs"] = found[i, 16];
                    dr["Product Line"] = found[i, 17];
                    dr["Department to Charge"] = found[i, 18];
                    dr["Restirctions on Pierces"] = found[i, 19];
                    dr["Circle Correction"] = found[i, 20];
                    dr["After Laser Cut"] = found[i, 21];
                    dr["After Press Brake"] = found[i, 22];
                    dr["DXF"] = found[i, 23];
                    dr["PDF"] = found[i, 24];
                    dr["Notes for Programmer"] = found[i, 25];
                    if (found[i, 27] != null)
                    {
                        dr["Programmer's Initials"] = found[i, 27];
                    }
                    if (found[i, 26] != null)
                    {
                        dr["Nest File Location"] = found[i, 26];
                    }
                    dr["Date Nested"] = found[i, 29];
                    //GridView1.Columns.Insert(0, checkBox);
                    dt2.Rows.Add(dr);
                }
            }

            GridView1.DataSource = dt2;
            Session["dt2"] = dt2;
            GridView1.DataBind();
        }
        else
        {
            Needs_Box.Visible = true;
            GridView1.Visible = false;
        }
    }


    /// <summary>
    /// Finds How many parts exist
    /// </summary>
    /// <returns></returns>
    protected int CountParts()
    {
        int total = 0;
        total = total + File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "needsNested.txt")).Count();
        total = total + File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "needsFormed.txt")).Count();
        total = total + File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "InProgress.txt")).Count();
        total = total + File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Finished.txt")).Count() + 3;
        return (total);
    }

    /// <summary>
    /// Finds the part in the array of parts
    /// </summary>
    /// <param name="display"> Array of Parts </param>
    /// <returns></returns>
   /* protected string[,] find(string[,] display)
    {
        int o = 0;
        for (int i = 0; i < total; i++)
        {
            if (display[i, 4] == (PartNum.Text))
            {
                for (int j = 0; j < 31; j++)
                {
               
                    found[o, j] = display[i, j];
                }
                o++;
            }
        }
        return found;
    }
    */


    protected string[,] find(string [,] display)
    {
        string[] check = new string[32];
        int o = 0;
        for (int k = 0; k < (display.Length/31); k++)
        {
            if (display[k, 0] != null)
            {
                for(int z = 0; z<31; z++)
                {
                    check[z] = display[k, z];
                }


                for(int p=0; p<31; p++)
                {
                    if (check[p] != null)
                    {
                        if (check[p].ToLower().Contains(PartNum.Text.ToLower()))
                        {
                            for (int j = 0; j < 31; j++)
                            {
                                found[o, j] = display[k, j];
                            }
                            o++;
                            break;
                        }
                    }
                   
                }

                /*
                if(check.Contains(PartNum.Text)){

                    for (int j = 0; j < 31; j++)
                    {
                        found[o, j] = display[k, j];
                    }
                    o++;

                }
                
                if (Array.IndexOf(check, (PartNum.Text)) >= 0)
                {
                    for (int j = 0; j < 31; j++)
                    {
                        found[o, j] = display[k, j];
                    }
                    o++;

                }
                */
            }
            
        }
        return found;
    }




    /// <summary>
    /// Colors the Cells based on location
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_DataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string location = (e.Row.Cells[1].Text);

            foreach (TableCell cell in e.Row.Cells)
            {
                if (location.Equals("In Progress"))
                {
                    cell.BackColor = Color.FromArgb(0, 252, 252, 100);
                }
                else if (location.Equals("Needs Nested") || location.Equals("Needs Formed"))
                {
                    cell.BackColor = Color.IndianRed;
                }
                else if (location.Equals("Finished"))
                {
                    cell.BackColor = Color.LightGreen;
                }
                
            }
        }
    }


    /// <summary>
    /// Gets row index, then calls BindData()
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //GridView1.PageIndex = e.NewEditIndex;
        GridView1.EditIndex = e.NewEditIndex;
        BindData();
    }

    /// <summary>
    /// Cancels the editing
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TaskGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //Reset the edit index.
        GridView1.EditIndex = -1;
        //Bind data to the GridView control.
        BindData();
    }

    /// <summary>
    /// Updates the row to what is inputed in the textboxes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TaskGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //Retrieve the table from the session object.
        DataTable dt = (DataTable)Session["dt2"];

        //Update the values.
        GridViewRow row = GridView1.Rows[e.RowIndex];


        display = (string[,])Session["array"];
        display[e.RowIndex, 2] = ((TextBox)(row.Cells[5].Controls[0])).Text;
        display[e.RowIndex, 3] = ((TextBox)(row.Cells[7].Controls[0])).Text;
        display[e.RowIndex, 4] = ((TextBox)(row.Cells[8].Controls[0])).Text;
        display[e.RowIndex, 5] = ((TextBox)(row.Cells[10].Controls[0])).Text;
        display[e.RowIndex, 6] = ((TextBox)(row.Cells[11].Controls[0])).Text;
        display[e.RowIndex, 7] = ((TextBox)(row.Cells[12].Controls[0])).Text;
        display[e.RowIndex, 8] = ((TextBox)(row.Cells[13].Controls[0])).Text;
        display[e.RowIndex, 9] = ((TextBox)(row.Cells[14].Controls[0])).Text;
        display[e.RowIndex, 10] = ((TextBox)(row.Cells[15].Controls[0])).Text;
        display[e.RowIndex, 11] = ((TextBox)(row.Cells[16].Controls[0])).Text;
        display[e.RowIndex, 12] = ((TextBox)(row.Cells[17].Controls[0])).Text;
        display[e.RowIndex, 13] = ((TextBox)(row.Cells[18].Controls[0])).Text;
        display[e.RowIndex, 14] = ((TextBox)(row.Cells[19].Controls[0])).Text;
        display[e.RowIndex, 15] = ((TextBox)(row.Cells[20].Controls[0])).Text;
        display[e.RowIndex, 16] = ((TextBox)(row.Cells[21].Controls[0])).Text;
        display[e.RowIndex, 17] = ((TextBox)(row.Cells[22].Controls[0])).Text;
        display[e.RowIndex, 18] = ((TextBox)(row.Cells[23].Controls[0])).Text;
        display[e.RowIndex, 19] = ((TextBox)(row.Cells[24].Controls[0])).Text;
        display[e.RowIndex, 20] = ((TextBox)(row.Cells[25].Controls[0])).Text;
        display[e.RowIndex, 21] = ((TextBox)(row.Cells[26].Controls[0])).Text;
        display[e.RowIndex, 22] = ((TextBox)(row.Cells[27].Controls[0])).Text;
        display[e.RowIndex, 23] = ((TextBox)(row.Cells[28].Controls[0])).Text;
        display[e.RowIndex, 24] = ((TextBox)(row.Cells[29].Controls[0])).Text;
        display[e.RowIndex, 25] = ((TextBox)(row.Cells[30].Controls[0])).Text;
        //dt.Rows[row.DataItemIndex]["Nest File Location"] = ((TextBox)(row.Cells[29].Controls[0])).Text;
        //dt.Rows[row.DataItemIndex]["IsComplete"] = ((CheckBox)(row.Cells[3].Controls[0])).Checked;

        //Reset the edit index.
        GridView1.EditIndex = -1;

        //Bind data to the GridView control.
        FixLists(display);
        Page_Load(null, null);
        BindData();
    }

    /// <summary>
    /// Gives table a source and then Binds it
    /// </summary>
    protected void BindData()
    {
        GridView1.DataSource = Session["dt2"];
        GridView1.DataBind();
    }

    /// <summary>
    /// Fixs part in file
    /// </summary>
    /// <param name="use"></param>
    protected void FixLists(string[,] use)
    {
        string[,] NN = new string[total, 31];
        string[,] NF = new string[total, 31];
        string[,] IP = new string[total, 31];
        string[,] FI = new string[total, 31];

        int e = 0;
        int o = 0;
        int r = 0;
        int n = 0;
        for (int i = 0; i < total; i++)
        {
            if (use[i, 28] == "Needs Nested")
            {
                for (int k = 0; k < 31; k++)
                {
                    NN[e, k] = use[i, k];
                }
                e++;
            }
            else if (use[i, 28] == "Needs Formed")
            {
                for (int k = 0; k < 31; k++)
                {
                    NF[o, k] = use[i, k];
                }
                o++;
            }
            else if (use[i, 28] == "In Progress")
            {
                for (int k = 0; k < 31; k++)
                {
                    IP[r, k] = use[i, k];
                }
                r++;
            }
            else if (use[i, 28] == "Finished")
            {
                for (int k = 0; k < 31; k++)
                {
                    FI[n, k] = use[i, k];
                }
                n++;
            }
        }

        var file1 = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NeedsNested.txt"));
        file1.Close();
        using (var sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NeedsNested.txt"), true))
        {
            for (int i = 0; i < total; i++)
            {
                string output = "";
                if (NN[i, 0] != null && NN[i, 0] != "")
                {
                    for (int j = 0; j < 31; j++)
                    {
                        output += NN[i, j] + "|";
                    }
                    sw.WriteLine(output);
                }

            }
            sw.Close();
        }

        var file2 = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NeedsFormed.txt"));
        file2.Close();
        using (var sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NeedsFormed.txt"), true))
        {
            for (int i = 0; i < total; i++)
            {
                string output = "";
                if (NF[i, 0] != null && NF[i, 0] != "")
                {
                    for (int j = 0; j < 31; j++)
                    {
                        output += NF[i, j] + "|";
                    }
                    sw.WriteLine(output);
                }

            }
            sw.Close();
        }

        var file3 = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "InProgress.txt"));
        file3.Close();
        using (var sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "InProgress.txt"), true))
        {
            for (int i = 0; i < total; i++)
            {
                string output = "";
                if (IP[i, 0] != null && IP[i, 0] != "")
                {
                    for (int j = 0; j < 31; j++)
                    {
                        output += IP[i, j] + "|";
                    }
                    sw.WriteLine(output);
                }

            }
            sw.Close();
        }

        var file4 = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Finished.txt"));
        file4.Close();
        using (var sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Finished.txt"), true))
        {
            for (int i = 0; i < total; i++)
            {
                string output = "";
                if (FI[i, 0] != null && FI[i, 0] != "")
                {
                    for (int j = 0; j < 31; j++)
                    {
                        output += FI[i, j] + "|";
                    }
                    sw.WriteLine(output);
                }

            }
            sw.Close();
        }
    }


}