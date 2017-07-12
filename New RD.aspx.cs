using System;
using System.IO;
using System.Linq;
using System.Web.UI;

public partial class About : Page
{
    static int total;
    public static int i = 0;
    public static string[,] needs = new string[total,32];
    public static string[] current = new string[32];
    private int UpRD;

    //public static string[] Department = { "R & D", "PRODUCT PROTOTYPES", "ENGINEERING", "TESTING", "PRODUCTION EQUIMENT", "FIXTURES, JIGS, TOOLING", "SHOW INVENTORY", "MARKETING", "MAINTENANCE", "SAFETY", "QUALITY", "OTHER" };
    //public static string[] prodLine = { "", "FARM & RANCH", "BALLS", "BALL MOUNTS", "CAB PROTECTOR", "FLAT BED", "GOOSENECK", "JOB SHOP", "MOTORCYCLE LATCH", "RCVR HITCH", "RV", "GN COUPLER", "TOW/STOW", "BISON", "OTHER" };

    /// <summary>
    /// Main Method
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        total = CountParts();

        needs = new string[total, 32];
        //Dept_Drop.DataSource = Department;
        //Dept_Drop.DataBind();
        //Prod_Drop.DataSource = prodLine;
        //Prod_Drop.DataBind();
        if (!Page.IsPostBack)
        {
            current = new string[32];
            
        }
        int CurRD;
        using (StreamReader SR = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\RDNumber.txt")))
        {

            CurRD = Int32.Parse(SR.ReadLine());
            SR.Close();
        }
        UpRD = CurRD + 1;
        Part_Num_Box.Text = ("RD" + UpRD.ToString());
    }

    /// <summary>
    /// Finds How many parts exist
    /// </summary>
    /// <returns></returns>
    protected int CountParts()
    {
        int total = 0;
        total = total + File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\needsNested.txt")).Count();
        total = total + File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\needsFormed.txt")).Count();
        total = total + File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\InProgress.txt")).Count();
        total = total + File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\RDParts.txt")).Count();
        total = total + File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\Finished.txt")).Count() + 30;
        return (total);
    }

    /// <summary>
    /// After everything is filled out
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        makeList();
        RDUsed();
        Session["part"] = current;
        Empty();
        
    }

    protected void RDUsed()
    {

        int CurRD = 0;
        using (StreamReader SR = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\RDNumber.txt")))
        {
            string str = Part_Num_Box.Text;
            str = str.TrimStart('R', 'D');
            int on = Int32.Parse(SR.ReadLine());
            if (on >= Int32.Parse(str))
            {
                CurRD = on + 1;
            }
            else
            {
                CurRD = on;
            }
            SR.Close();
        }
        var fill = File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\RDNumber.txt"));
        fill.Close();
        UpRD = CurRD + 1;
        using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\RDNumber.txt")))
        {
            sw.WriteLine(UpRD);
            sw.Close();
        }
    }

    /// <summary>
    /// Empty's form
    /// </summary>
    protected void Empty()
    {
        Response.Redirect(Request.RawUrl);
        return;
    }

    /// <summary>
    /// Makes the list from the inputed information
    /// </summary>
    protected void makeList()
    {
        if (current[0] == null)
        {
            current[0] = DateTime.Now.ToString("MMM, dd, yyyy");
            current[1] = "";
            current[2] = Initials_Box.Text.ToUpper();
            current[3] = Desc_Box.Text;
            current[4] = Part_Num_Box.Text;
            current[5] = "";
            current[6] = "";
            current[7] = "";
            current[8] = "";
            current[9] = "";
            current[10] = "";
            current[11] = "";
            current[12] = "";
            current[13] = "";
            current[14] = "";
            current[15] = "";
            current[16] = "";
            if (Prod_Drop.Text.Equals("Other")) { current[17] = Prod_Ex_box.Text; } else { current[17] = Prod_Drop.Text; }
            if (Dept_Drop.Text.Equals("Other")) { current[18] = Dep_Ex_box.Text; } else { current[18] = Dept_Drop.Text; }
            current[19] = "";
            current[20] = "";
            current[21] = "";
            current[22] = "";
            current[25] = Notes_Box.Text;
            current[31] = Proj_Box.Text;
            
            updateList(current);
            //Session["nest"] = needs;
        }
       

    }

    /// <summary>
    /// Puts part in correct next location
    /// </summary>
    /// <param name="use"></param>
    protected void updateList(string[] use)
    {
            using (var sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\RDParts.txt"), true))
            {

                string output = "";
                if (use[0] != null && use[0] != "")
                {
                    for (int j = 0; j < 32; j++)
                    {
                        output += use[j] + "|";
                    }
                    sw.WriteLine(output);
                }
                sw.Flush();
                sw.Close();
            }
        
    }


    /// <summary>
    /// If Other it puts out the Explain Box 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Prod_Drop_Changed(object sender, EventArgs e)
    {
        if (Prod_Drop.Text.Equals("Other"))
        {
            Prod_Ex_box.Visible = true;
            Prod_Line_exp.Visible = true;
        }
        else
        {
            Prod_Ex_box.Visible = false;
            Prod_Line_exp.Visible = false;
        }
         
    }

    /// <summary>
    /// If Other it puts out the Explain Box 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Dept_Drop_Changed(object sender, EventArgs e)
    {
        if (Dept_Drop.Text.Equals("Other"))
        {
            Dep_Ex_box.Visible = true;
            Dept_exp.Visible = true;
        }
        else
        {
            Dep_Ex_box.Visible = false;
            Dept_exp.Visible = false;
        }
         
    }



    /// <summary>
    /// Gets the part number to get
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Get_but_Click(object sender, EventArgs e)
    {
        if (CNum_Box.Text != "" && CNum_Box.Text != null)
            Refill(CNum_Box.Text);
        else
            Empty();
    }
    
    /// <summary>
    /// Puts past part back into form
    /// </summary>
    /// <param name="use"></param>
    protected void Refill(string use) {
        string[] split = new string[32];
        StreamReader SR = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,@"App_Data\needsNested.txt"));
        int toUse = 0;
        string line;
        int m = 0;
        for (int i = 0; i < total; i++)
        {
            line = SR.ReadLine();
            if (line != null)
            {
                m = 0;
                split = line.Split('|');

                for (int j = 0; j < 27; j++)
                {
                    if ((split.Length) > m)
                    {
                        
                            needs[i, j] = split[m];
                        
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
                        SR = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\RDParts.txt"));
                        break;
                    case 2:
                        SR.Close();
                        SR = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,@"App_Data\needsFormed.txt"));
                        break;
                    case 3:
                        SR.Close();
                        SR = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,@"App_Data\InProgress.txt"));
                        break;
                    case 4:
                        SR.Close();
                        SR = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,@"App_Data\Finished.txt"));
                        break;
                    case 5:
                        break;
                }
            }
        }
            for (int j = 0; j < total; j++)
            {
                if (needs[j, 1] != null)
                {
                    string look = needs[8264, 4];
                    if (needs[j, 4].Equals(use.ToUpper()))
                    {
                        //Function_Drop.Text = needs[j, 1];
                        Initials_Box.Text = needs[j, 2];
                        Desc_Box.Text = needs[j, 3];
                        
                        if (Prod_Drop.Enabled == true)
                            if (needs[j, 17] != "" && needs[j, 17] != null)
                            {
                                if (needs[j, 17] != "Farm & Ranch" && needs[j, 17] != "Balls" && needs[j, 17] != "Ball Mounts" && needs[j, 17] != "Cab Protector" && needs[j, 17] != "Flat Bed" && needs[j, 17] != "Gooseneck" && needs[j, 17] != "Job Shop" && needs[j, 17] != "Motorcycle Latch" && needs[j, 17] != "RCVR Hitch" && needs[j, 17] != "RV" && needs[j, 17] != "GN Coupler" && needs[j, 17] != "Tow/Stow" && needs[j, 17] != "Bison")
                                {
                                    Prod_Drop.Text = "Other"; Prod_Line_exp.Visible = true; Prod_Ex_box.Visible = true; Prod_Ex_box.Text = needs[j, 17];
                                }
                                else
                                {

                                    Prod_Drop.Text = needs[j, 17]; Prod_Line_exp.Visible = false; Prod_Ex_box.Visible = false; Prod_Ex_box.Text = "";
                                }
                            }
                        if (Dept_Drop.Enabled == true && needs[j, 18] != null)
                            if (needs[j, 18] != "")
                            {
                                if (needs[j, 18] != "R&D Product Prototypes" && needs[j, 18] != "Engineering, Testing" && needs[j, 18] != "Production Equipment" && needs[j, 18] != "Fixtures, Jigs, Tooling" && needs[j, 18] != "Show Inventory" && needs[j, 18] != "Marketing" && needs[j, 18] != "Maintenance" && needs[j, 18] != "Safety" && needs[j, 18] != "Quality")
                                {
                                    Dept_Drop.Text = "Other"; Dept_exp.Visible = true; Dep_Ex_box.Visible = true; Dep_Ex_box.Text = needs[j, 18];
                                }
                                else
                                {

                                    Dept_Drop.Text = needs[j, 18]; Dept_exp.Visible = false; Dep_Ex_box.Visible = false; Dep_Ex_box.Text = "";
                                }
                            }
                        
                       
                        Notes_Box.Text = needs[j, 25];
                        Proj_Box.Text = needs[j, 31];

                    

                }
                }
            }
         
    }

    /// <summary>
    /// Puts back last part 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GP_But_Click(object sender, EventArgs e)
    {
        string[] last = (string[])Session["part"];

        if(last != null) { 
            ///Function_Drop.Text = last[1];
            Initials_Box.Text = last[ 2];
            Desc_Box.Text = last[ 3];
            
            if(Prod_Drop.Enabled == true)
            if (last[ 17] != "")
            {
                if (last[ 17] != "Farm & Ranch" && last[ 17] != "Balls" && last[ 17] != "Ball Mounts" && last[ 17] != "Cab Protector" && last[ 17] != "Flat Bed" && last[ 17] != "Gooseneck" && last[ 17] != "Job Shop" && last[ 17] != "Motorcycle Latch" && last[ 17] != "RCVR Hitch" && last[ 17] != "RV" && last[ 17] != "GN Coupler" && last[ 17] != "Tow/Stow" && last[ 17] != "Bison")
                {
                    Prod_Drop.Text = "Other"; Prod_Line_exp.Visible = true; Prod_Ex_box.Visible = true; Prod_Ex_box.Text = last[ 17];
                }
                else
                {

                    Prod_Drop.Text = last[ 17]; Prod_Line_exp.Visible = false; Prod_Ex_box.Visible = false; Prod_Ex_box.Text = "";
                }
            }
            if (Dept_Drop.Enabled == true)
            if (last[ 18] != "")
            {
                if (last[ 18] != "R&D Product Prototypes" && last[ 18] != "Engineering, Testing" && last[ 18] != "Production Equipment" && last[ 18] != "Fixtures, Jigs, Tooling" && last[ 18] != "Show Inventory" && last[ 18] != "Marketing" && last[ 18] != "Maintenance" && last[ 18] != "Safety" && last[ 18] != "Quality")
                {
                    Dept_Drop.Text = "Other"; Dept_exp.Visible = true; Dep_Ex_box.Visible = true; Dep_Ex_box.Text = last[ 18];
                }
                else
                {

                    Dept_Drop.Text = last[ 18]; Dept_exp.Visible = false; Dep_Ex_box.Visible = false; Dep_Ex_box.Text = "";
                }
            }
            

            Notes_Box.Text = last[ 25];
        }

         
    }

    /// <summary>
    /// Submits and leaves form filled except part # and Description 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void SC_But_Click(object sender, EventArgs e)
    {
        makeList();
        RDUsed();
        Desc_Box.Text = "";
        
    }



}
