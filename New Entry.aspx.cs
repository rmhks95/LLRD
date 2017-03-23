using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net.Mime;
using System.Diagnostics;

public partial class About : Page
{
    public static int i = 0;
    public static string[,] needs = new string[50,26];
    public static string[] current = new string[26];
    //public static string[] Department = { "R & D", "PRODUCT PROTOTYPES", "ENGINEERING", "TESTING", "PRODUCTION EQUIMENT", "FIXTURES, JIGS, TOOLING", "SHOW INVENTORY", "MARKETING", "MAINTENANCE", "SAFETY", "QUALITY", "OTHER" };
    //public static string[] prodLine = { "", "FARM & RANCH", "BALLS", "BALL MOUNTS", "CAB PROTECTOR", "FLAT BED", "GOOSENECK", "JOB SHOP", "MOTORCYCLE LATCH", "RCVR HITCH", "RV", "GN COUPLER", "TOW/STOW", "BISON", "OTHER" };

    /// <summary>
    /// Main Method
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //Dept_Drop.DataSource = Department;
        //Dept_Drop.DataBind();
        //Prod_Drop.DataSource = prodLine;
        //Prod_Drop.DataBind();
        if (!Page.IsPostBack)
        {
            Function_Drop_SelectedIndexChanged(null, null);
            current = new string[26];
        }

    }

   
    /// <summary>
    /// After everything is filled out
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        Function_Drop_SelectedIndexChanged(null, null);
        makeList();
        Empty();
        
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
            current[0] = DateTime.Now.ToString();
            current[1] = Function_Drop.Text;
            current[2] = Initials_Box.Text;
            current[3] = Desc_Box.Text;
            current[4] = Part_Num_Box.Text;
            current[5] = Quan_Box.Text;
            current[6] = Rev_Box.Text;
            current[7] = Cut_Date.Text;
            current[8] = Form_Date.Text;
            if (Type_Drop.Text.Equals("Other")){ current[9] = Type_Exp_Box.Text; }else {current[9] = Type_Drop.Text; }
            if (Mat_Drop.Text.Equals("Other")) { current[10] = Mat_Exp_Box.Text; } else { current[10] = Mat_Drop.Text; }
            if (Gas_Drop.Text.Equals("Other")) { current[11] = Gas_Exp_Box.Text; } else { current[11] = Gas_Drop.Text; }
            current[12] = Urg_drop.Text;
            current[13] = Grain_drop.Text;
            current[14] = Etch_drop.Text;
            current[15] = Seam_Drop.Text;
            current[16] = Pair_drop.Text;
            if (Prod_Drop.Text.Equals("Other")) { current[17] = Prod_Ex_box.Text; } else { current[17] = Prod_Drop.Text; }
            if (Dept_Drop.Text.Equals("Other")) { current[18] = Dep_Ex_box.Text; } else { current[18] = Dept_Drop.Text; }
            current[19] = Rescrit_drop.Text;
            current[20] = Circle_drop.Text;
            if (Las_drop.Text.Equals("Other")) { current[21] = Las_Ex_box.Text; } else { current[21] = Las_drop.Text; }
            if (Pres_drop.Text.Equals("Other")) { current[22] = Pres_Ex_box.Text; } else { current[22] = Pres_drop.Text; }
            string dxf = DXF_Up.FileName.ToString();
            current[23] = DxfSearch(dxf);
            string pdf = PDF_Up.FileName.ToString();
            current[24] = PDFSearch(pdf);
            current[25] = Notes_Box.Text;
            
            updateList(current);
            //Session["nest"] = needs;
        }
        else
        {
            i++;
            makeList();
        }
       

    }

    /// <summary>
    /// Puts part in correct next location
    /// </summary>
    /// <param name="use"></param>
    protected void updateList(string[] use)
    {
        Function_Drop_SelectedIndexChanged(null, null);
        if (Pres_drop.Text == "" && (!Function_Drop.Text.Equals("PRESS BRAKE: PROGRAM TO CHECK CLEARANCE/TOOLS")))
        {
            using (var sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "needsNested.txt"), true))
            {

                string output = "";
                if (use[0] != null && use[0] != "")
                {
                    for (int j = 0; j < 26; j++)
                    {
                        output += use[j] + "|";
                    }
                    sw.WriteLine(output);
                }
                sw.Flush();
                sw.Close();
            }
        }
        else if (Function_Drop.Text.Equals("PRESS BRAKE: PROGRAM TO CHECK CLEARANCE/TOOLS"))
        {
            using (var sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"needsFormed.txt"), true))
            {
                string output = "";
                if (use[0] != null && use[0] != "")
                {
                    for (int j = 0; j < 26; j++)
                    {
                        output += use[j] + "|";
                    }
                    sw.WriteLine(output);
                }
                sw.Flush();
                sw.Close();
            }
        }
        else { 
            using (var sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"needsNested.txt"), true))
            {

                string output = "";
                if (use[0] != null && use[0] != "")
                {
                    for (int j = 0; j < 26; j++)
                    {
                        output += use[j] + "|";
                    }
                    sw.WriteLine(output);
                }
                sw.Flush();
                sw.Close();
            }
            using (var sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"needsFormed.txt"), true))
            {
                string output = "";
                if (use[0] != null && use[0] != "")
                {
                    for (int j = 0; j < 26; j++)
                    {
                        output += use[j] + "|";
                    }
                    sw.WriteLine(output);
                }
                sw.Flush();
                sw.Close();
            }
        }
        if (needs[0, 0] == null)
        {
            for (int k = 0; k < 26; k++)
            {
                needs[0, k] = use[k];
                needs[1 + 1, 0] = "";
            }
        }
        else
        {
            for (int l = 0; l < 50; l++)
            {
                if (needs[l, 0] == null)
                {
                    for (int k = 0; k < 26; k++)
                    {
                        needs[l, k] = use[k];
                        needs[1 + 1, 0] = "";
                    }
                }
            }
        }
    }


    /// <summary>
    /// Selects method based off change in dropbox
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Function_Drop_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(Function_Drop.Text.Equals("2D LASER: ECR REV/RELEASE"))
        {
            ECR_Rev();
        }
        else if(Function_Drop.Text.Equals("2D LASER: NEST & CUT"))
        {
           NC();
        }
        else if(Function_Drop.Text.Equals("2D LASER: NEST,  CUT, & FORM"))
        {
            NCF();
        }
        else if(Function_Drop.Text.Equals("2D LASER: RE-CUT CURRENT PART"))
        {
            RECP();
        }
        else if(Function_Drop.Text.Equals("2D LASER: RE-CUT, & FORM CURRENT PART"))
        {
            RECFP();
        }
        else if(Function_Drop.Text.Equals("2D LASER: NEST FOR QUOTE & NESTABILITY STUDY"))
        {
            NQS();
        }
        else if(Function_Drop.Text.Equals("3D LASER: ECR REV/RELEASE "))
        {
            Tri_Rev();
        }
        else if(Function_Drop.Text.Equals("3D LASER: NEST & CUT"))
        {
            Tri_NC();
        }
        else if(Function_Drop.Text.Equals("3D LASER: RE-CUT CURRENT PART"))
        {
            TRI_RECP();
        }
        else if(Function_Drop.Text.Equals("3D LASER: NEST FOR QUOTE OR NESTABILITY STUDY"))
        {
            Tri_NQS();
        }
        else if(Function_Drop.Text.Equals("PRESS BRAKE: PROGRAM TO CHECK CLEARANCE/TOOLS"))
        {
            Press_Brake();
        }

        
    }

    /// <summary>
    /// ECR Revision
    /// </summary>
    protected void ECR_Rev()
    {
        Initials_Box.Enabled = true;
        Part_Num_Box.Enabled = true;
        Desc_Box.Enabled = true;
        Rev_Box.Enabled = true;
        DXF_Up.Enabled = true;
        PDF_Up.Enabled = false;
        Mat_Drop.Enabled = true;
        Type_Drop.Enabled = true;
        Quan_Box.Enabled = false;
        Quan_Box.Text = "";
        Cut_Date.Enabled = false;
        Cut_Date.Text = "";
        Form_Date.Enabled = false;
        Form_Date.Text = "";
        Urg_drop.Enabled = false;
        Urg_drop.Text = "";
        Las_drop.Enabled = false;
        Las_drop.Text = "";
        Pres_drop.Enabled = false;
        Pres_drop.Text = "";
        Prod_Drop.Enabled = false;
        Prod_Drop.Text = "";
        Dept_Drop.Enabled = false;
        Dept_Drop.Text = "";
        Grain_drop.Enabled = true;
        Etch_drop.Enabled = true;
        Gas_Drop.Enabled = true;
        Rescrit_drop.Enabled = true;
        Circle_drop.Enabled = true;
        Pair_drop.Enabled = true;
        Seam_Drop.Enabled = false;
        Seam_Drop.Text = "";
        Notes_Box.Enabled = true;
    }

    /// <summary>
    /// 2D Nest & Cut
    /// </summary>
    protected void NC()
    {
        Initials_Box.Enabled = true;
        Part_Num_Box.Enabled = true;
        Desc_Box.Enabled = true;
        Rev_Box.Enabled = false;
        Rev_Box.Text = "";
        DXF_Up.Enabled = true;
        PDF_Up.Enabled = true;
        Mat_Drop.Enabled = true;
        Type_Drop.Enabled = true;
        Quan_Box.Enabled = true;
        Cut_Date.Enabled = true;
        Form_Date.Enabled = false;
        Form_Date.Text = "";
        Urg_drop.Enabled = true;
        Las_drop.Enabled = true;
        Pres_drop.Enabled = false;
        Pres_drop.Text = "";
        Prod_Drop.Enabled = true;
        Dept_Drop.Enabled = true;
        Grain_drop.Enabled = true;
        Etch_drop.Enabled = true;
        Gas_Drop.Enabled = true;
        Rescrit_drop.Enabled = true;
        Circle_drop.Enabled = true;
        Pair_drop.Enabled = true;
        Seam_Drop.Enabled = false;
        Seam_Drop.Text = "";
        Notes_Box.Enabled = true;
        Cut_Date_Load();
    }

    /// <summary>
    /// 2D Nest, Cut, & Form
    /// </summary>
    protected void NCF()
    {
        Initials_Box.Enabled = true;
        Part_Num_Box.Enabled = true;
        Desc_Box.Enabled = true;
        Rev_Box.Enabled = false;
        Rev_Box.Text = "";
        DXF_Up.Enabled = true;
        PDF_Up.Enabled = true;
        Mat_Drop.Enabled = true;
        Type_Drop.Enabled = true;
        Quan_Box.Enabled = true;
        Cut_Date.Enabled = true;
        Form_Date.Enabled = true;
        Urg_drop.Enabled = true;
        Las_drop.Enabled = true;
        Pres_drop.Enabled = true;
        Prod_Drop.Enabled = true;
        Dept_Drop.Enabled = true;
        Grain_drop.Enabled = true;
        Etch_drop.Enabled = true;
        Gas_Drop.Enabled = true;
        Rescrit_drop.Enabled = true;
        Circle_drop.Enabled = true;
        Pair_drop.Enabled = true;
        Seam_Drop.Enabled = false;
        Seam_Drop.Text = "";
        Notes_Box.Enabled = true;
        Cut_Date_Load();
        Form_Date_Load();
    }

    /// <summary>
    /// 2D Re-Cut Part
    /// </summary>
    protected void RECP()
    {
        Initials_Box.Enabled = true;
        Part_Num_Box.Enabled = true;
        Desc_Box.Enabled = true;
        Rev_Box.Enabled = false;
        Rev_Box.Text = "";
        DXF_Up.Enabled = false;
        PDF_Up.Enabled = false;
        Mat_Drop.Enabled = true;
        Type_Drop.Enabled = true;
        Quan_Box.Enabled = true;
        Cut_Date.Enabled = true;
        Form_Date.Enabled = false;
        Form_Date.Text = "";
        Urg_drop.Enabled = true;
        Las_drop.Enabled = true;
        Pres_drop.Enabled = false;
        Pres_drop.Text = "";
        Prod_Drop.Enabled = true;
        Dept_Drop.Enabled = true;
        Grain_drop.Enabled = false;
        Grain_drop.Text = "";
        Etch_drop.Enabled = false;
        Etch_drop.Text = "";
        Gas_Drop.Enabled = false;
        Gas_Drop.Text = "";
        Rescrit_drop.Enabled = false;
        Rescrit_drop.Text = "";
        Circle_drop.Enabled = false;
        Circle_drop.Text = "";
        Pair_drop.Enabled = false;
        Pair_drop.Text = "";
        Seam_Drop.Enabled = false;
        Seam_Drop.Text = "";
        Notes_Box.Enabled = true;
        Cut_Date_Load();
    }

    /// <summary>
    /// 2D Recut and form part
    /// </summary>
    protected void RECFP()
    {
        Initials_Box.Enabled = true;
        Part_Num_Box.Enabled = true;
        Desc_Box.Enabled = true;
        Rev_Box.Enabled = false;
        Rev_Box.Text = "";
        DXF_Up.Enabled = false;
        PDF_Up.Enabled = true;
        Mat_Drop.Enabled = true;
        Type_Drop.Enabled = true;
        Quan_Box.Enabled = true;
        Cut_Date.Enabled = true;
        Form_Date.Enabled = true;
        Urg_drop.Enabled = true;
        Las_drop.Enabled = true;
        Pres_drop.Enabled = true;
        Prod_Drop.Enabled = true;
        Dept_Drop.Enabled = true;
        Grain_drop.Enabled = false;
        Grain_drop.Text = "";
        Etch_drop.Enabled = false;
        Etch_drop.Text = "";
        Gas_Drop.Enabled = false;
        Gas_Drop.Text = "";
        Rescrit_drop.Enabled = false;
        Rescrit_drop.Text = "";
        Circle_drop.Enabled = false;
        Circle_drop.Text = "";
        Pair_drop.Enabled = false;
        Pair_drop.Text = "";
        Seam_Drop.Enabled = false;
        Seam_Drop.Text = "";
        Notes_Box.Enabled = true;
        Cut_Date_Load();
        Form_Date_Load();
    }

    /// <summary>
    /// 2D Nest for Quote 
    /// </summary>
    protected void NQS()
    {
        Initials_Box.Enabled = true;
        Part_Num_Box.Enabled = true;
        Desc_Box.Enabled = true;
        Rev_Box.Enabled = false;
        Rev_Box.Text = "";
        DXF_Up.Enabled = true;
        PDF_Up.Enabled = false;
        Mat_Drop.Enabled = true;
        Type_Drop.Enabled = false;
        Type_Drop.Text = "";
        Quan_Box.Enabled = true;
        Cut_Date.Enabled = false;
        Cut_Date.Text = "";
        Form_Date.Enabled = false;
        Form_Date.Text = "";
        Urg_drop.Enabled = true;
        Las_drop.Enabled = false;
        Las_drop.Text = "";
        Pres_drop.Enabled = false;
        Pres_drop.Text = "";
        Prod_Drop.Enabled = false;
        Prod_Drop.Text = "";
        Dept_Drop.Enabled = false;
        Dept_Drop.Text = "";
        Grain_drop.Enabled = true;
        Etch_drop.Enabled = true;
        Gas_Drop.Enabled = true;
        Rescrit_drop.Enabled = true;
        Circle_drop.Enabled = true;
        Pair_drop.Enabled = true;
        Seam_Drop.Enabled = false;
        Seam_Drop.Text = "";
        Notes_Box.Enabled = true;
    }

    /// <summary>
    /// 3D ECR 
    /// </summary>
    protected void Tri_Rev()
    {
        Initials_Box.Enabled = true;
        Part_Num_Box.Enabled = true;
        Desc_Box.Enabled = true;
        Rev_Box.Enabled = true;
        DXF_Up.Enabled = true;
        PDF_Up.Enabled = false;
        Mat_Drop.Enabled = true;
        Type_Drop.Enabled = true;
        Quan_Box.Enabled = false;
        Quan_Box.Text = "";
        Cut_Date.Enabled = false;
        Cut_Date.Text = "";
        Form_Date.Enabled = false;
        Form_Date.Text = "";
        Urg_drop.Enabled = false;
        Las_drop.Enabled = false;
        Las_drop.Text = "";
        Pres_drop.Enabled = false;
        Pres_drop.Text = "";
        Prod_Drop.Enabled = false;
        Prod_Drop.Text = "";
        Dept_Drop.Enabled = false;
        Dept_Drop.Text = "";
        Grain_drop.Enabled = false;
        Grain_drop.Text = "";
        Etch_drop.Enabled = true;
        Gas_Drop.Enabled = true;
        Rescrit_drop.Enabled = true;
        Circle_drop.Enabled = true;
        Pair_drop.Enabled = false;
        Pair_drop.Text = "";
        Seam_Drop.Enabled = true;
        Notes_Box.Enabled = true;
    }

    /// <summary>
    /// 3D Nest and Cut
    /// </summary>
    protected void Tri_NC()
    {
        Initials_Box.Enabled = true;
        Part_Num_Box.Enabled = true;
        Desc_Box.Enabled = true;
        Rev_Box.Enabled = false;
        Rev_Box.Text = "";
        DXF_Up.Enabled = true;
        PDF_Up.Enabled = true;
        Mat_Drop.Enabled = true;
        Type_Drop.Enabled = true;
        Quan_Box.Enabled = true;
        Cut_Date.Enabled = true;
        Form_Date.Enabled = false;
        Form_Date.Text = "";
        Urg_drop.Enabled = true;
        Las_drop.Enabled = true;
        Pres_drop.Enabled = false;
        Pres_drop.Text = "";
        Prod_Drop.Enabled = true;
        Dept_Drop.Enabled = true;
        Grain_drop.Enabled = false;
        Grain_drop.Text = "";
        Etch_drop.Enabled = true;
        Gas_Drop.Enabled = true;
        Rescrit_drop.Enabled = true;
        Circle_drop.Enabled = true;
        Pair_drop.Enabled = false;
        Pair_drop.Text = "";
        Seam_Drop.Enabled = true;
        Notes_Box.Enabled = true;
        Cut_Date_Load();
    }

    /// <summary>
    /// 3D Recut Part
    /// </summary>
    protected void TRI_RECP()
    {
        Initials_Box.Enabled = true;
        Part_Num_Box.Enabled = true;
        Desc_Box.Enabled = true;
        Rev_Box.Enabled = false;
        Rev_Box.Text = "";
        DXF_Up.Enabled = false;
        PDF_Up.Enabled = false;
        Mat_Drop.Enabled = true;
        Type_Drop.Enabled = true;
        Quan_Box.Enabled = true;
        Cut_Date.Enabled = true;
        Form_Date.Enabled = false;
        Form_Date.Text = "";
        Urg_drop.Enabled = true;
        Las_drop.Enabled = true;
        Pres_drop.Enabled = false;
        Pres_drop.Text = "";
        Prod_Drop.Enabled = true;
        Dept_Drop.Enabled = true;
        Grain_drop.Enabled = false;
        Grain_drop.Text = "";
        Etch_drop.Enabled = false;
        Etch_drop.Text = "";
        Gas_Drop.Enabled = false;
        Gas_Drop.Text = "";
        Rescrit_drop.Enabled = false;
        Rescrit_drop.Text = "";
        Circle_drop.Enabled = false;
        Circle_drop.Text = "";
        Pair_drop.Enabled = false;
        Pair_drop.Text = "";
        Seam_Drop.Enabled = true;
        Notes_Box.Enabled = true;
        Cut_Date_Load();
    }

    /// <summary>
    /// 3D Nest for Quote
    /// </summary>
    protected void Tri_NQS()
    {
        Initials_Box.Enabled = true;
        Part_Num_Box.Enabled = true;
        Desc_Box.Enabled = true;
        Rev_Box.Enabled = false;
        Rev_Box.Text = "";
        DXF_Up.Enabled = true;
        PDF_Up.Enabled = false;
        Mat_Drop.Enabled = true;
        Type_Drop.Enabled = false;
        Type_Drop.Text = "";
        Quan_Box.Enabled = true;
        Cut_Date.Enabled = false;
        Cut_Date.Text = "";
        Form_Date.Enabled = false;
        Form_Date.Text = "";
        Urg_drop.Enabled = true;
        Las_drop.Enabled = false;
        Las_drop.Text = "";
        Pres_drop.Enabled = false;
        Pres_drop.Text = "";
        Prod_Drop.Enabled = false;
        Prod_Drop.Text = "";
        Dept_Drop.Enabled = false;
        Dept_Drop.Text = "";
        Grain_drop.Enabled = true;
        Etch_drop.Enabled = true;
        Gas_Drop.Enabled = true;
        Rescrit_drop.Enabled = true;
        Circle_drop.Enabled = true;
        Pair_drop.Enabled = false;
        Pair_drop.Text = "";
        Seam_Drop.Enabled = true;
        Notes_Box.Enabled = true;
    }

    /// <summary>
    /// Press Brake for Clearance
    /// </summary>
    protected void Press_Brake()
    {
        Initials_Box.Enabled = true;
        Part_Num_Box.Enabled = true;
        Desc_Box.Enabled = true;
        Rev_Box.Enabled = false;
        Rev_Box.Text = "";
        DXF_Up.Enabled = true;
        PDF_Up.Enabled = false;
        Mat_Drop.Enabled = true;
        Type_Drop.Enabled = false;
        Type_Drop.Text = "";
        Quan_Box.Enabled = false;
        Quan_Box.Text = "";
        Cut_Date.Enabled = false;
        Cut_Date.Text = "";
        Form_Date.Enabled = false;
        Form_Date.Text = "";
        Urg_drop.Enabled = true;
        Las_drop.Enabled = false;
        Las_drop.Text = "";
        Pres_drop.Enabled = false;
        Pres_drop.Text = "";
        Prod_Drop.Enabled = false;
        Prod_Drop.Text = "";
        Dept_Drop.Enabled = false;
        Dept_Drop.Text = "";
        Grain_drop.Enabled = false;
        Grain_drop.Text = "";
        Etch_drop.Enabled = false;
        Etch_drop.Text = "";
        Gas_Drop.Enabled = false;
        Gas_Drop.Text = "";
        Rescrit_drop.Enabled = false;
        Rescrit_drop.Text = "";
        Circle_drop.Enabled = false;
        Circle_drop.Text = "";
        Pair_drop.Enabled = false;
        Pair_drop.Text = "";
        Seam_Drop.Enabled = false;
        Seam_Drop.Text = "";
        Notes_Box.Enabled = true;
    }

    /// <summary>
    /// If Other it puts out the Explain Box 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Type_Drop_Changed(object sender, EventArgs e)
    {
        if (Type_Drop.Text.Equals("Other"))
        {
            Type_Explain.Visible = true;
            Type_Exp_Box.Visible = true;
        }
        else
        {
            Type_Explain.Visible = false;
            Type_Exp_Box.Visible = false;
        }
        Function_Drop_SelectedIndexChanged(null, null);
    }

    /// <summary>
    /// If Other it puts out the Explain Box 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Mat_Drop_Changed(object sender, EventArgs e)
    {
        if (Mat_Drop.Text.Equals("Other"))
        {
            Mat_Explain.Visible = true;
            Mat_Exp_Box.Visible = true;
        }
        else
        {
            Mat_Explain.Visible = false;
            Mat_Exp_Box.Visible = false;
        }
        Function_Drop_SelectedIndexChanged(null, null);
    }

    /// <summary>
    /// If Other it puts out the Explain Box 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Gas_Drop_Changed(object sender, EventArgs e)
    {

        if (Gas_Drop.Text.Equals("Other"))
        {
            Gas_Exp.Visible = true;
            Gas_Exp_Box.Visible = true;
        }
        else
        {
            Gas_Exp.Visible = false;
            Gas_Exp_Box.Visible = false;
        }
        Function_Drop_SelectedIndexChanged(null, null);
    }

    /// <summary>
    /// Sets date 2 weeks ahead
    /// </summary>
    protected void Cut_Date_Load()
    {
        Cut_Date.Text = DateTime.Today.AddDays(14).ToString("d");
    }

    /// <summary>
    /// Sets date 2 weeks ahead
    /// </summary>
    protected void Form_Date_Load()
    {
        Form_Date.Text = DateTime.Today.AddDays(14).ToString("d");
    }

    /// <summary>
    /// If Other it puts out the Explain Box 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Las_drop_Changed(object sender, EventArgs e)
    {
        if (Las_drop.Text.Equals("Other"))
        {
            Las_Ex_box.Visible = true;
            Aft_las_exp.Visible = true;
        }
        else
        {
            Las_Ex_box.Visible = false;
            Aft_las_exp.Visible = false;
        }
        Function_Drop_SelectedIndexChanged(null, null);
    }

    /// <summary>
    /// If Other it puts out the Explain Box 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Pres_drop_Changed(object sender, EventArgs e)
    {
        if (Pres_drop.Text.Equals("Other"))
        {
            Pres_Ex_box.Visible = true;
            Aft_press_exp.Visible = true;
        }
        else
        {
            Pres_Ex_box.Visible = false;
            Aft_press_exp.Visible = false;
        }
        Function_Drop_SelectedIndexChanged(null, null);
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
        Function_Drop_SelectedIndexChanged(null, null);
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
        Function_Drop_SelectedIndexChanged(null, null);
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
        string[] split = new string[3900];
        StreamReader SR = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"needsNested.txt"));
        int toUse = 0;
        string line;
        int m = 0;
        for (int i = 0; i < 50; i++)
        {
            line = SR.ReadLine();
            if (line != null)
            {
                m = 0;
                split = line.Split('|');

                for (int j = 0; j < 26; j++)
                {
                    if ((split.Length - 2) >= m)
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
                        break;
                }
            }
        }
            for (int j = 0; j < 50; j++)
            {
                if (needs[j, 1] != null)
                {
                    if (needs[j, 4].Equals(use))
                    {
                        Function_Drop.Text = needs[j, 1];
                        Initials_Box.Text = needs[j, 2];
                        Desc_Box.Text = needs[j, 3];
                        Part_Num_Box.Text = "";
                        Quan_Box.Text = "";
                        if (Rev_Box.Enabled == true)
                            Rev_Box.Text = needs[j, 6];
                        if (Cut_Date.Enabled == true)
                            Cut_Date.Text = needs[j, 7];
                        if (Form_Date.Enabled == true)
                            Form_Date.Text = needs[j, 8];
                        if (Type_Drop.Enabled == true)
                            if (needs[j, 9] != "")
                            {
                                if (needs[j, 9] != "Prototype" && needs[j, 9] != "Shop Use" && needs[j, 9] != "Personal Project" && needs[j, 9] != "Production Release by ECR" && needs[j, 9] != "Production Revision by ECR")
                                {
                                    Type_Drop.Text = "Other"; Type_Explain.Visible = true; Type_Exp_Box.Visible = true; Type_Exp_Box.Text = needs[j, 9];
                                }
                                else
                                {
                                    Type_Drop.Text = needs[j, 9]; Type_Explain.Visible = false; Type_Exp_Box.Visible = false; Type_Exp_Box.Text = "";
                                }
                            }
                        if (Mat_Drop.Enabled == true)
                            if (needs[j, 10] != "")
                            {
                                if (needs[j, 10] != "22GA, CR" && needs[j, 10] != "20GA, CR" && needs[j, 10] != "18GA, CR" && needs[j, 10] != "16GA, CR OR HR" && needs[j, 10] != "14GA, P&O" && needs[j, 10] != "12GA, P&O" && needs[j, 10] != "11GA, P&O" && needs[j, 10] != "10GA P&O" && needs[j, 10] != "7GA P&O" && needs[j, 10] != "1/4\" P&O" && needs[j, 10] != "1/4\" HR" && needs[j, 10] != "5/6\" HR" && needs[j, 10] != "3/8\" HR" && needs[j, 10] != "1/2\" P&O" && needs[j, 10] != "3/4\" HR" && needs[j, 10] != "1\" HR")
                                {
                                    Mat_Drop.Text = "Other"; Mat_Explain.Visible = true; Mat_Exp_Box.Visible = true; Mat_Exp_Box.Text = needs[j, 10];
                                }
                                else
                                {
                                    Mat_Drop.Text = needs[j, 10];
                                }
                            }
                        if (Gas_Drop.Enabled == true)
                            if (needs[j, 11] != "")
                            {
                                if (needs[j, 11] != "Any" && needs[j, 11] != "Oxygen" && needs[j, 11] != "Nitrogen" && needs[j, 11] != "Shop Air")
                                {
                                    Gas_Drop.Text = "Other"; Gas_Exp.Visible = true; Gas_Exp_Box.Visible = true; Gas_Exp_Box.Text = needs[j, 11];
                                }
                                else
                                {
                                    Gas_Drop.Text = needs[j, 11]; Gas_Exp.Visible = false; Gas_Exp_Box.Visible = false; Gas_Exp_Box.Text = needs[j, 11] = "";
                                }
                            }
                        if (Urg_drop.Enabled == true)
                            Urg_drop.Text = needs[j, 12];
                        if (Grain_drop.Enabled == true)
                            Grain_drop.Text = needs[j, 13];
                        if (Etch_drop.Enabled == true)
                            Etch_drop.Text = needs[j, 14];
                        if (Seam_Drop.Enabled == true)
                            Seam_Drop.Text = needs[j, 15];
                        if (Pair_drop.Enabled == true)
                            Pair_drop.Text = needs[j, 16];
                        if (Prod_Drop.Enabled == true)
                            if (needs[j, 17] != "")
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
                        if (Dept_Drop.Enabled == true)
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
                        if (Rescrit_drop.Enabled == true)
                            Rescrit_drop.Text = needs[j, 19];
                        if (Circle_drop.Enabled == true)
                            Circle_drop.Text = needs[j, 20];
                        if (Las_drop.Enabled == true)
                            if (needs[j, 21] != "")
                            {
                                if (needs[j, 21] != "Place in R&D Rack" && needs[j, 21] != "Page Engineer, Then Place in R&D Rack" && needs[j, 21] != "To Press Brake" && needs[j, 21] != "To R&D Shop")
                                {
                                    Las_drop.Text = "Other"; Aft_las_exp.Visible = true; Las_Ex_box.Visible = true; Las_Ex_box.Text = needs[j, 21];
                                }
                                else
                                {
                                    Las_drop.Text = needs[j, 21]; Aft_las_exp.Visible = false; Las_Ex_box.Visible = false; Las_Ex_box.Text = needs[j, 21] = "";
                                }
                            }
                        if (Pres_drop.Enabled == true)
                            if (needs[j, 22] != "")
                            {
                                if (needs[j, 22] != "Page Engineer" && needs[j, 22] != "To R&D Rack" && needs[j, 22] != "To R&D Shop")
                                {
                                    Pres_drop.Text = "Other"; Aft_press_exp.Visible = true; Pres_Ex_box.Visible = true; Pres_Ex_box.Text = needs[j, 22];
                                }
                                else
                                {
                                    Las_drop.Text = needs[j, 22]; Aft_press_exp.Visible = false; Pres_Ex_box.Visible = false; Pres_Ex_box.Text = needs[j, 22] = "";
                                }
                            }
                        Notes_Box.Text = needs[j, 25];

                    

                }
                }
            }
        Function_Drop_SelectedIndexChanged(null, null);
    }

    /// <summary>
    /// Puts back last part 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GP_But_Click(object sender, EventArgs e)
    {
        if(needs[0,0] != null)
        {
            int k = 0;
            for(int m= 49; m>-1; m--)
            {
                if (needs[m, 0] != "" && needs[m,0] != null)
                {
                    k = m;
                    break;
                }

            }
        
            Function_Drop.Text = needs[k,1];
            Initials_Box.Text = needs[k, 2];
            Desc_Box.Text = needs[k, 3];
            Part_Num_Box.Text = "";
            Quan_Box.Text = "";
            if(Rev_Box.Enabled ==true)
            Rev_Box.Text = needs[k, 6];
            if(Cut_Date.Enabled ==true)
            Cut_Date.Text = needs[k, 7];
            if(Form_Date.Enabled ==true)
            Form_Date.Text = needs[k, 8];
            if(Type_Drop.Enabled ==true)
            if (needs[k, 9] != "")
            {
                if (needs[k, 9] != "Prototype" && needs[k, 9] != "Shop Use" && needs[k, 9] != "Personal Project" && needs[k, 9] != "Production Release by ECR" && needs[k, 9] != "Production Revision by ECR")
                {
                    Type_Drop.Text = "Other"; Type_Explain.Visible = true; Type_Exp_Box.Visible = true; Type_Exp_Box.Text = needs[k, 9];
                }
                else
                {
                    Type_Drop.Text = needs[k, 9]; Type_Explain.Visible = false; Type_Exp_Box.Visible = false; Type_Exp_Box.Text = "";
                }
            }
            if(Mat_Drop.Enabled == true)
            if (needs[k, 10] != "")
            {
                if (needs[k, 10] != "22GA, CR" && needs[k, 10] != "20GA, CR" && needs[k, 10] != "18GA, CR" && needs[k, 10] != "16GA, CR OR HR" && needs[k, 10] != "14GA, P&O" && needs[k, 10] != "12GA, P&O" && needs[k, 10] != "11GA, P&O" && needs[k, 10] != "10GA P&O" && needs[k, 10] != "7GA P&O" && needs[k, 10] != "1/4\" P&O" && needs[k, 10] != "1/4\" HR" && needs[k, 10] != "5/6\" HR" && needs[k, 10] != "3/8\" HR" && needs[k, 10] != "1/2\" P&O" && needs[k, 10] != "3/4\" HR" && needs[k, 10] != "1\" HR")
                {
                    Mat_Drop.Text = "Other"; Mat_Explain.Visible = true; Mat_Exp_Box.Visible = true; Mat_Exp_Box.Text = needs[k, 10];
                }
                else
                {
                    Mat_Drop.Text = needs[k, 10];
                }
            }
            if(Gas_Drop.Enabled == true)
            if (needs[k, 11] != "")
            {
                if (needs[k, 11] != "Any" && needs[k, 11] != "Oxygen" && needs[k, 11] != "Nitrogen" && needs[k, 11] != "Shop Air")
                {
                    Gas_Drop.Text = "Other"; Gas_Exp.Visible = true; Gas_Exp_Box.Visible = true; Gas_Exp_Box.Text = needs[k, 11];
                }
                else
                {
                    Gas_Drop.Text = needs[k, 11]; Gas_Exp.Visible = false; Gas_Exp_Box.Visible = false; Gas_Exp_Box.Text = needs[k, 11] = "";
                }
            }
            if(Urg_drop.Enabled == true)
            Urg_drop.Text = needs[k, 12];
            if(Grain_drop.Enabled == true)
            Grain_drop.Text = needs[k, 13];
            if(Etch_drop.Enabled == true)
            Etch_drop.Text = needs[k, 14];
            if(Seam_Drop.Enabled == true)
            Seam_Drop.Text = needs[k, 15];
            if(Pair_drop.Enabled == true)
            Pair_drop.Text = needs[k, 16];
            if(Prod_Drop.Enabled == true)
            if (needs[k, 17] != "")
            {
                if (needs[k, 17] != "Farm & Ranch" && needs[k, 17] != "Balls" && needs[k, 17] != "Ball Mounts" && needs[k, 17] != "Cab Protector" && needs[k, 17] != "Flat Bed" && needs[k, 17] != "Gooseneck" && needs[k, 17] != "Job Shop" && needs[k, 17] != "Motorcycle Latch" && needs[k, 17] != "RCVR Hitch" && needs[k, 17] != "RV" && needs[k, 17] != "GN Coupler" && needs[k, 17] != "Tow/Stow" && needs[k, 17] != "Bison")
                {
                    Prod_Drop.Text = "Other"; Prod_Line_exp.Visible = true; Prod_Ex_box.Visible = true; Prod_Ex_box.Text = needs[k, 17];
                }
                else
                {

                    Prod_Drop.Text = needs[k, 17]; Prod_Line_exp.Visible = false; Prod_Ex_box.Visible = false; Prod_Ex_box.Text = "";
                }
            }
            if (Dept_Drop.Enabled == true)
            if (needs[k, 18] != "")
            {
                if (needs[k, 18] != "R&D Product Prototypes" && needs[k, 18] != "Engineering, Testing" && needs[k, 18] != "Production Equipment" && needs[k, 18] != "Fixtures, Jigs, Tooling" && needs[k, 18] != "Show Inventory" && needs[k, 18] != "Marketing" && needs[k, 18] != "Maintenance" && needs[k, 18] != "Safety" && needs[k, 18] != "Quality")
                {
                    Dept_Drop.Text = "Other"; Dept_exp.Visible = true; Dep_Ex_box.Visible = true; Dep_Ex_box.Text = needs[k, 18];
                }
                else
                {

                    Dept_Drop.Text = needs[k, 18]; Dept_exp.Visible = false; Dep_Ex_box.Visible = false; Dep_Ex_box.Text = "";
                }
            }
            if(Rescrit_drop.Enabled == true)
            Rescrit_drop.Text = needs[k, 19];
            if(Circle_drop.Enabled == true)
            Circle_drop.Text = needs[k, 20];
            if(Las_drop.Enabled == true)
            if (needs[k, 21] != "")
            {
                if (needs[k, 21] != "Place in R&D Rack" && needs[k, 21] != "Page Engineer, Then Place in R&D Rack" && needs[k, 21] != "To Press Brake" && needs[k, 21] != "To R&D Shop")
                {
                    Las_drop.Text = "Other"; Aft_las_exp.Visible = true; Las_Ex_box.Visible = true; Las_Ex_box.Text = needs[k, 21];
                }
                else
                {
                    Las_drop.Text = needs[k, 21]; Aft_las_exp.Visible = false; Las_Ex_box.Visible = false; Las_Ex_box.Text = needs[k, 21] = "";
                }
            }
            if(Pres_drop.Enabled == true)
            if (needs[k, 22] != "")
            {
                if (needs[k, 22] != "Page Engineer" && needs[k, 22] != "To R&D Rack" && needs[k, 22] != "To R&D Shop")
                {
                    Pres_drop.Text = "Other"; Aft_press_exp.Visible = true; Pres_Ex_box.Visible = true; Pres_Ex_box.Text = needs[k, 22];
                }
                else
                {
                    Las_drop.Text = needs[k, 22]; Aft_press_exp.Visible = false; Pres_Ex_box.Visible = false; Pres_Ex_box.Text = needs[k, 22] = "";
                }
            }
            Notes_Box.Text = needs[k, 25];

            

        }
        Function_Drop_SelectedIndexChanged(null, null);
    }

    /// <summary>
    /// Submits and leaves form filled except part # and Description 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void SC_But_Click(object sender, EventArgs e)
    {
        Function_Drop_SelectedIndexChanged(null, null);
        MailMessage m = new MailMessage();
        SmtpClient sc = new SmtpClient();

        m.From = new MailAddress("ryanhuse@gmail.com", "Engineering");
        m.To.Add(new MailAddress("ryanhuse@turnoverball.com", "Lasers"));
        m.Subject = "Test";
        m.Body = "Part " + Desc_Box.Text + " is ready";

        sc.Host = "smtp.gmail.com";
        sc.Port = 587;
        sc.Credentials = new System.Net.NetworkCredential("ryanhuse@gmail.com", "Chetdad#1");
        sc.EnableSsl = true;
        //sc.Send(m);
        makeList();
        Part_Num_Box.Text = "";
        Quan_Box.Text = "";

    }


    /// <summary>
    /// Finds DXF in folder
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    protected string DxfSearch(string file)
    {
        string location = "";

        foreach (string open in  Directory.EnumerateFiles(@"\\fileserver\shared$\Documents\Engineering Department Folder\Secured Departmental Documents\Dxf", file, SearchOption.AllDirectories))
        {
            location = open;
            break;
        }
        
        return location;
    }



    /// <summary>
    /// Finds PDF in folder
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    protected string PDFSearch(string file)
    {

        string location = "";

        foreach (string open in Directory.EnumerateFiles(@"S:\Workforce Share", file, SearchOption.AllDirectories))
        {
            location = open;
            break;
        }

       
        return location;
    }

}
