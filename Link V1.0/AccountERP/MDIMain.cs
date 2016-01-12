using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayer.UserHandling;

namespace AccountERP
{
    public partial class MDIMain : Form
    {
        UserHandling Myuser = null; 
        public MDIMain()
        {
            InitializeComponent();
        }

        private void MDIMain_Load(object sender, EventArgs e)
        {
            frmLogin fm = new frmLogin();
            fm.ShowDialog();
             toolStripStatusLabel2.Text = Program.AccountStatic.UserName;
            if (Program.AccountStatic.LoggingAsLocal)
            {
                toolStripStatusLabel1.Text = "localhost";
            }
            else
                toolStripStatusLabel1.Text = "3sfab";
                if (Program.AccountStatic.IsAuthenticated)
                {
                    TreeMenu.Enabled = true;
                    LoadMenu();
                }
                else
                    TreeMenu.Enabled = false;
        }
        private void LoadMenu()
        {
        Myuser = new UserHandling(Program.AccountStatic.LoggingAsLocal);
            DataTable tb = Myuser.GetMenuObjectList(0,0);
            if (tb != null)
            {
                int mcy=0;
                foreach (DataRow r in tb.Rows)
                {

                    TreeNode trn = new TreeNode();
                    trn.Name = r["ObgfileName"].ToString();
                    trn.Text = r["ObgDisplayName"].ToString();
                    int MainobgID = int.Parse (r["SysID"].ToString ());
                    TreeMenu.Nodes.Add(trn);
                    TreeMenu.Nodes[mcy].ImageIndex = int.Parse(r["IconID"].ToString());
                    mcy++;
                    DataTable tb1 = Myuser.GetMenuObjectList(MainobgID, 1);
                   // tb1 = Myuser.GetMenuObjectList(MainobgID, 1);
                    if (tb1 != null)
                    {
                        int Nount1 = TreeMenu.Nodes.Count - 1;
                        int Secline = 0;
                        foreach (DataRow r1 in tb1.Rows )
                        {
                            TreeNode   trn2 = new TreeNode();
                            trn2.Name = r1["ObgfileName"].ToString();
                            trn2.Text = r1["ObgDisplayName"].ToString();
                            MainobgID = int.Parse (r1["SysID"].ToString ());
                            TreeMenu.Nodes[Nount1].Nodes.Add(trn2);
                            TreeMenu.Nodes[Nount1].Nodes[Secline].ImageIndex  = int.Parse(r1["IconID"].ToString());
                            int MainobgID1 = int.Parse(r["SysID"].ToString());
                            Secline++;
                            DataTable tb2; // Myuser.GetMenuObjectList(0, 0);
                            tb2 = Myuser.GetMenuObjectList(MainobgID, 2);
                            int Nount2 = TreeMenu.Nodes[Nount1].Nodes.Count  - 1;
                            if (tb2 != null)
                                {
                                    int imx1 = 0;
                                foreach (DataRow r2 in tb2.Rows)
                                {
                                    TreeNode trn3 = new TreeNode();
                                    trn3.Name = r2["ObgfileName"].ToString();
                                    trn3.Text = r2["ObgDisplayName"].ToString();
                                    MainobgID = int.Parse(r2["SysID"].ToString());

                                    TreeMenu.Nodes[Nount1].Nodes[Nount2].Nodes.Add(trn3);
                                   TreeMenu.Nodes[Nount1].Nodes[Nount2].Nodes[imx1].ImageIndex =  int.Parse(r2["IconID"].ToString());
                                   imx1++;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void TreeMenu_DoubleClick(object sender, EventArgs e)
            {
                try
                {
                    string menusect = TreeMenu.SelectedNode.Name;
                    string Mcs = menusect;
                    Type CAType = Type.GetType("AccountERP." + Mcs);
                    Form fm = (Form)Activator.CreateInstance(CAType);
                    fm.TopLevel = false;
                   fm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                   this.panel2.Controls.Add(fm);
                   fm.Show ();
                }
                catch (Exception ex)
                {
                    
                   
                }
            }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }

}
