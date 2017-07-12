using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class Home : System.Web.UI.Page
{
    static int total;
    int showing;
    string[,] needs = new string[total, 32];
    string[,] display = new string[total, 32];
    string[,] found = new string[total, 32];


    /// <summary>
    /// Main method that gets parts and displays in table
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            total = CountParts();
            showing = Show();
            Start();
        }
    }

    protected void Start() { 

        if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\RDParts.txt")))
        {
            var yep = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\RDParts.txt"));
            yep.Close();
        }


        needs = new string[total, 32];
        display = new string[total, 32];

        string[] split = new string[32];
        StreamReader SR = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\RDParts.txt"));
        string line;
        for (int i = 0; i < total; i++)
        {


            line = SR.ReadLine();
            if (line != null)
            {
                split = line.Split('|');
                int m = 0;
                if (split.Length > 32)
                    m = 32;
                else
                    m = split.Length;
                for (int j = 0; j < m; j++)
                {
                    needs[i, j] = split[j];
                }
                split = new string[32];

                //needs = (string[,])Session["nest"];

            }

        }
        SR.Close();


        string[] open = new string[(total)];
        for (int i = 0; i < total; i++)
        {
            if (needs[i, 0] != null)
            {
                open[i] = needs[i, 4];
            }
        }

        System.Array.Sort(open);
        showing = Show();
        display = new string[showing, 32];
        int p = 0;
        int n = open.Length - 1;
        while (n > -1)
        {
            if (open[n] != null)
            {
                for (int h = total - 1; h > -1; h--)
                {
                    if (p < showing)
                    {
                        if (open[n] == needs[h, 4])
                        {
                            for (int q = 0; q < 32; q++)
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
        loadTable(display);
    }

        
    protected void loadTable(string[,] display) {

        GridView1.DataSource = null;
        GridView1.DataBind();
        if (display[0, 0] != null)
        {
            Needs_Box.Visible = false;
            Session["array"] = display;
            DataTable dt2 = new DataTable("test");

            // DataColumn you can use constructor DataColumn(name,type);
            //DataColumn dc0 = new DataColumn("Location");
            DataColumn dc1 = new DataColumn("Date Entered");
            DataColumn dc2 = new DataColumn("Engineer's Initials");
            DataColumn dc3 = new DataColumn("Part Number");
            DataColumn dc4 = new DataColumn("Part Description");
            DataColumn dc5 = new DataColumn("Project");
            //DataColumn dc6 = new DataColumn("Quantity");
           // DataColumn dc7 = new DataColumn("Revision");
           // DataColumn dc8 = new DataColumn("Cut by Date");
            //DataColumn dc9 = new DataColumn("Form by Date");
            //DataColumn dc10 = new DataColumn("Type of Part");
           // DataColumn dc11 = new DataColumn("Material");
           // DataColumn dc12 = new DataColumn("Gas");
            //DataColumn dc13 = new DataColumn("Priority");
            //DataColumn dc14 = new DataColumn("Grain Restrictions");
            //DataColumn dc15 = new DataColumn("Etch Lines");
            //DataColumn dc16 = new DataColumn("Seam of Tube Location Critical");
            //DataColumn dc17 = new DataColumn("Nest in Pairs");
            DataColumn dc18 = new DataColumn("Product Line");
            DataColumn dc19 = new DataColumn("Department to Charge");
            //DataColumn dc20 = new DataColumn("Restirctions on Pierces");
            //DataColumn dc21 = new DataColumn("Circle Correction");
           // DataColumn dc22 = new DataColumn("After Laser Cut");
           // DataColumn dc23 = new DataColumn("After Press Brake");
           // DataColumn dc24 = new DataColumn("DXF");
           // DataColumn dc25 = new DataColumn("PDF");
            DataColumn dc26 = new DataColumn("Notes");
            //DataColumn dc27 = new DataColumn("Programmer's Initials");
           // DataColumn dc28 = new DataColumn("Nest File Location");
            //DataColumn dc29 = new DataColumn("Date Nested");
           // DataColumn dc30 = new DataColumn("Machine Progrmaed For");


            //dt2.Columns.Add(dc0);
            dt2.Columns.Add(dc1);
            ///dt2.Columns.Add(dc29);
            dt2.Columns.Add(dc2);
            dt2.Columns.Add(dc3);
            //dt2.Columns.Add(dc27);
            dt2.Columns.Add(dc4);
            dt2.Columns.Add(dc5);
            dt2.Columns.Add(dc26);
            ///dt2.Columns.Add(dc30);
            //dt2.Columns.Add(dc6);
            //dt2.Columns.Add(dc7);
            //dt2.Columns.Add(dc8);
            //dt2.Columns.Add(dc9);
            //dt2.Columns.Add(dc10);
            //dt2.Columns.Add(dc11);
            //dt2.Columns.Add(dc12);
            //dt2.Columns.Add(dc13);
            //dt2.Columns.Add(dc14);
            //dt2.Columns.Add(dc15);
            //dt2.Columns.Add(dc16);
            //dt2.Columns.Add(dc17);
            dt2.Columns.Add(dc18);
            dt2.Columns.Add(dc19);
            //dt2.Columns.Add(dc20);
            //dt2.Columns.Add(dc21);
            //dt2.Columns.Add(dc22);
            //dt2.Columns.Add(dc23);
            //dt2.Columns.Add(dc24);
            //dt2.Columns.Add(dc25);
            //dt2.Columns.Add(dc28);

            int show;
            if (display.Length / 32 > 0)
                show = (display.Length / 32);
            else
                show = 1;
            for (int i = 0; i < show; i++)
            {
                if (display[i, 0] != null)
                {

                    DataRow dr = dt2.NewRow();
                   // dr["Location"] = display[i, 28];
                    dr["Date Entered"] = display[i, 0];
                    dr["Project"] = display[i, 31];
                    dr["Engineer's Initials"] = display[i, 2];
                    dr["Part Description"] = display[i, 3];
                    dr["Part Number"] = display[i, 4];
                    //dr["Quantity"] = display[i, 5];
                    //dr["Revision"] = display[i, 6];
                    //dr["Cut by Date"] = display[i, 7];
                    //dr["Form by Date"] = display[i, 8];
                   // dr["Type of Part"] = display[i, 9];
                   // dr["Material"] = display[i, 10];
                   // dr["Gas"] = display[i, 11];
                    //dr["Priority"] = display[i, 12];
                    //dr["Grain Restrictions"] = display[i, 13];
                   // dr["Etch Lines"] = display[i, 14];
                   // dr["Seam of Tube Location Critical"] = display[i, 15];
                    //dr["Nest in Pairs"] = display[i, 16];
                    dr["Product Line"] = display[i, 17];
                    dr["Department to Charge"] = display[i, 18];
                   // dr["Restirctions on Pierces"] = display[i, 19];
                    //dr["Circle Correction"] = display[i, 20];
                    //dr["After Laser Cut"] = display[i, 21];
                    //dr["After Press Brake"] = display[i, 22];
                    //dr["DXF"] = display[i, 23];
                   // dr["PDF"] = display[i, 24];
                    dr["Notes"] = display[i, 25];
                    if (display[i, 27] != null)
                    {
                        //dr["Programmer's Initials"] = display[i, 27];
                    }
                    if (display[i, 26] != null)
                    {
                        //dr["Nest File Location"] = display[i, 26];
                    }
                    ///dr["Date Nested"] = display[i, 29];
                    ///dr["Machine Progrmaed For"] = display[i, 30];
                    //GridView1.Columns.Insert(0, checkBox);
                    dt2.Rows.Add(dr);
                }
            }

            GridView1.DataSource = dt2;
            Session["dt3"] = dt2;
            if (!IsPostBack)
                GridView1.DataBind();

        }
        else
        {
            Needs_Box.Text = "No Parts";
        }
    }


    /// <summary>
    /// Finds How many parts exist
    /// </summary>
    /// <returns></returns>
    protected int CountParts()
    {
        int total = 0;
        total = total + File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\RDParts.txt")).Count() + 3;
        return (total);
    }


    /// <summary>
    /// Colors parts based on location
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_DataBound(object sender, GridViewRowEventArgs e)
    {
    }


    /// <summary>
    /// Gets row number and calls bind data
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
    /// Cancels row edit
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
    /// Edits part from textboxes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TaskGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //Retrieve the table from the session object.
        DataTable dt = (DataTable)Session["dt3"];

        //Update the values.
        GridViewRow row = GridView1.Rows[e.RowIndex];

        int selected = e.RowIndex;

        display = (string[,])Session["array"];
        display[selected, 2] = ((TextBox)(row.Cells[2].Controls[0])).Text;
        //display[selected, 4] = ((TextBox)(row.Cells[3].Controls[0])).Text;
        display[selected, 3] = ((TextBox)(row.Cells[4].Controls[0])).Text;
        display[selected, 31] = ((TextBox)(row.Cells[5].Controls[0])).Text;
        display[selected, 25] = ((TextBox)(row.Cells[6].Controls[0])).Text;
        display[selected, 17] = ((TextBox)(row.Cells[7].Controls[0])).Text;
        display[selected, 18] = ((TextBox)(row.Cells[8].Controls[0])).Text;
        

        //Reset the edit index.
        GridView1.EditIndex = -1;

        //Bind data to the GridView control.
        FixLists(display, selected);
        Page_Load(null, null);
        BindData();
    }

    /// <summary>
    /// Sets table source and binds it
    /// </summary>
    protected void BindData()
    {
        GridView1.DataSource = Session["dt3"];
            GridView1.DataBind();
    }



    /// <summary>
    /// Updates files to new inputs
    /// </summary>
    /// <param name="use"></param>
    protected void FixLists(string[,] use, int selected)
    {
        int all = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\RDParts.txt")).Count() + 3;
        string[] split = new string[(showing * 32)];
        string[,] updated = new string[all, 32];
        using (StreamReader SR = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\RDParts.txt")))
        {
            string line;
            for (int i = 0; i < all; i++)
            {
                line = SR.ReadLine();
                if (line != null)
                {
                    split = line.Split('|');
                    if (split[4] != use[selected, 4])
                    {
                        int q;
                        if (split.Length < 32)
                        { q = split.Length; }
                        else
                        { q = 32; }           

                        for (int j = 0; j < q; j++)
                        {
                            updated[i, j] = split[j];
                        }
                    }
                    else
                    {
                        for (int j = 0; j < 32; j++)
                        {
                            updated[i, j] = use[selected, j];
                        }
                    }
                }
            }

        }
        var fole1 = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\RDParts.txt"));
        fole1.Close();
        using (var sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\RDParts.txt"), true))
        {
            for (int i = 0; i < all; i++)
            {
                string output = "";
                if (updated[i, 0] != null && updated[i, 0] != "")
                {
                    for (int j = 0; j < 32; j++)
                    {
                        output += updated[i, j] + "|";
                    }
                    sw.WriteLine(output);
                }

            }
            sw.Close();
        }
        Start();
    }


    protected int Show()
    {
        int picked;


        if (DropDownList1.Text == "100")
        {
            picked = 100;
        }
        else if (DropDownList1.Text == "500")
        {
            picked = 500;
        }
        else
        {
            picked = CountParts();
        }

        return picked;
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        showing = Show();
        Start();
        BindData();
    }

    protected void Sbut_Click(object sender, EventArgs e)
    {
        Search();
        BindData();
    }

    protected void Search()
    {
        needs = new string[total, 32];
        display = new string[total, 32];

        string[] split = new string[32];
        StreamReader SR = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\RDParts.txt"));
        string line;
        for (int i = 0; i < total; i++)
        {


            line = SR.ReadLine();
            if (line != null)
            {
                split = line.Split('|');
                int m = 0;
                if (split.Length > 32)
                    m = 32;
                else
                    m = split.Length;
                for (int j = 0; j < m; j++)
                {
                    needs[i, j] = split[j];
                }
                split = new string[32];

                //needs = (string[,])Session["nest"];

            }

        }
        SR.Close();


        string[] open = new string[(total)];
        for (int i = 0; i < total; i++)
        {
            if (needs[i, 0] != null)
            {
                open[i] = needs[i, 4];
            }
        }

        System.Array.Sort(open);
        display = new string[total, 32];
        int p = 0;
        int n = open.Length - 1;
        while (n > -1)
        {
            if (open[n] != null)
            {
                for (int h = total - 1; h > -1; h--)
                {
                        if (open[n] == needs[h, 4])
                        {
                            for (int q = 0; q < 32; q++)
                            {
                                display[p, q] = needs[h, q];
                            }
                            needs[h, 0] = "";

                            p++;
                        }

                    
                }
            }
            n = n - 1;
        }
        

        string[] check = new string[32];
        int o = 0;
        for (int k = 0; k < (display.Length / 32); k++)
        {
            if (display[k, 0] != null)
            {
                for (int z = 0; z < 31; z++)
                {
                    check[z] = display[k, z];
                }


                for (p = 0; p < 32; p++)
                {
                    if (check[p] != null)
                    {
                        if (check[p].ToLower().Contains(sBox.Text.ToLower()))
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
                
            }

        }
        loadTable(found);
    }
}




