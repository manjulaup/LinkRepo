using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayer.AccountTranactions;
using BusinessLayer.UserHandling;
namespace AccountERP
    {
    public partial class frmUserRight : Form
        {
        UserHandling Myuser = null; 
        private AccountTranaction MyAccount = null; // new AccountTranaction();
 
        public frmUserRight()
            {
            InitializeComponent();
            }

        private void frmUserRight_Load(object sender, EventArgs e)
        {
            panel1.Top = (this.Height - panel1.Height) / 2;
            panel1.Left = (this.Width - panel1.Width) / 2;
            MyAccount = new AccountTranaction(true);
            LoadMenu();
            Myuser.LoadRolls(cmbUserRoll);
        }
        private void LoadMenu()
            {
            Myuser = new UserHandling(Program.AccountStatic.LoggingAsLocal);
            DataTable tb = Myuser.GetMenuObjectList(0, 0);
            if (tb != null)
                {
                int mcy = 0;
                foreach (DataRow r in tb.Rows)
                    {

                    TreeNode trn = new TreeNode();
                    trn.Name = r["ObgfileName"].ToString();
                    trn.Text = r["ObgDisplayName"].ToString();
                    int MainobgID = int.Parse(r["SysID"].ToString());
                    TreeMenu.Nodes.Add(trn);
                    TreeMenu.Nodes[mcy].ImageIndex = int.Parse(r["IconID"].ToString());
                    mcy++;
                    DataTable tb1 = Myuser.GetMenuObjectList(MainobgID, 1);
                  
                    if (tb1 != null)
                        {
                        int Nount1 = TreeMenu.Nodes.Count - 1;
                        int Secline = 0;
                        foreach (DataRow r1 in tb1.Rows)
                            {
                            TreeNode trn2 = new TreeNode();
                            trn2.Name = r1["ObgfileName"].ToString();
                            trn2.Text = r1["ObgDisplayName"].ToString();
                            MainobgID = int.Parse(r1["SysID"].ToString());
                            TreeMenu.Nodes[Nount1].Nodes.Add(trn2);
                            TreeMenu.Nodes[Nount1].Nodes[Secline].ImageIndex = int.Parse(r1["IconID"].ToString());
                            int MainobgID1 = int.Parse(r["SysID"].ToString());
                            Secline++;
                            DataTable tb2; // Myuser.GetMenuObjectList(0, 0);
                            tb2 = Myuser.GetMenuObjectList(MainobgID, 2);
                            int Nount2 = TreeMenu.Nodes[Nount1].Nodes.Count - 1;
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
                                    TreeMenu.Nodes[Nount1].Nodes[Nount2].Nodes[imx1].ImageIndex = int.Parse(r2["IconID"].ToString());
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
                string menusect = TreeMenu.SelectedNode.Name;
                int obid=Myuser.GetObjectID(menusect);
                string[] row1 = {"0", obid.ToString() , TreeMenu.SelectedNode.Text, "0", "0", "0", "0", "0", "0", "0" };
                int i = ExistRoninList(obid);
            if (i==-1)
                dgvObject.Rows.Add(row1); 
            }
        private int ExistRoninList(int ID)
        {
        foreach (DataGridViewRow r in dgvObject.Rows)
            {
            int obid = 0;
            bool resp = int.TryParse(r.Cells[1].Value.ToString(), out   obid);
            if (ID == obid)
                return r.Index;
            }
        return -1;
        }
        private void chkall_CheckedChanged(object sender, EventArgs e)
            {
            if (chkall.Checked)
                {
                foreach (DataGridViewRow r in dgvObject.Rows)
                    {
                        r.Cells[0].Value = "1";
                    }
                }
            else
                {
                foreach (DataGridViewRow r in dgvObject.Rows)
                    {
                        r.Cells[0].Value = "0";
                    }
                }
            }

        private void chkShow_CheckedChanged(object sender, EventArgs e)
            {
            if (chkShow.Checked)
                {
                foreach (DataGridViewRow r in dgvObject.Rows)
                    {
                    r.Cells["dgvObject_Show"].Value = "1";
                    }
                }
            else
                {
                foreach (DataGridViewRow r in dgvObject.Rows)
                    {
                    r.Cells["dgvObject_Show"].Value = "0";
                    }
                }
            }

        private void chkSave_CheckedChanged(object sender, EventArgs e)
            {
            if (chkSave.Checked)
                {
                foreach (DataGridViewRow r in dgvObject.Rows)
                    {
                    r.Cells["dgvObject_Save"].Value = "1";
                    }
                }
            else
                {
                foreach (DataGridViewRow r in dgvObject.Rows)
                    {
                    r.Cells["dgvObject_Save"].Value = "0";
                    }
                }
            }

        private void chkupdate_CheckedChanged(object sender, EventArgs e)
            {
            if (chkupdate.Checked)
                {
                foreach (DataGridViewRow r in dgvObject.Rows)
                    {
                    r.Cells["dgvObject_Update"].Value = "1";
                    }
                }
            else
                {
                foreach (DataGridViewRow r in dgvObject.Rows)
                    {
                    r.Cells["dgvObject_Update"].Value = "0";
                    }
                }
            }

        private void chkDelete_CheckedChanged(object sender, EventArgs e)
            {
            if (chkDelete.Checked)
                {
                foreach (DataGridViewRow r in dgvObject.Rows)
                    {
                    r.Cells["dgvObject_Delete"].Value = "1";
                    }
                }
            else
                {
                foreach (DataGridViewRow r in dgvObject.Rows)
                    {
                    r.Cells["dgvObject_Delete"].Value = "0";
                    }
                }
            }

        private void chkPrint_CheckedChanged(object sender, EventArgs e)
            {
            if (chkPrint.Checked)
                {
                foreach (DataGridViewRow r in dgvObject.Rows)
                    {
                    r.Cells["dgvObject_Print"].Value = "1";
                    }
                }
            else
                {
                foreach (DataGridViewRow r in dgvObject.Rows)
                    {
                    r.Cells["dgvObject_Print"].Value = "0";
                    }
                }
            }

        private void chkAprova_CheckedChanged(object sender, EventArgs e)
            {
            if (chkAprova.Checked)
                {
                foreach (DataGridViewRow r in dgvObject.Rows)
                    {
                    r.Cells["dgvObject_Aproval"].Value = "1";
                    }
                }
            else
                {
                foreach (DataGridViewRow r in dgvObject.Rows)
                    {
                    r.Cells["dgvObject_Aproval"].Value = "0";
                    }
                }
            }

        private void chkPostAc_CheckedChanged(object sender, EventArgs e)
            {
            if (chkPostAc.Checked)
                {
                foreach (DataGridViewRow r in dgvObject.Rows)
                    {
                    r.Cells["dgvObject_PostToAc"].Value = "1";
                    }
                }
            else
                {
                foreach (DataGridViewRow r in dgvObject.Rows)
                    {
                    r.Cells["dgvObject_PostToAc"].Value = "0";
                    }
                }
            }

        private void txtUser_TextChanged(object sender, EventArgs e)
            {
                Myuser.UserName = txtUser.Text;
                if (Myuser.ExistUser())
                    lblCorrectuser.BackColor = Color.GreenYellow;
                else
                    lblCorrectuser.BackColor = Color.Red;
            }
        private string SetDataToClass(out List<UserHandling.ObjectUserRollDataType> _SaveData)
        {
           BusinessLayer.CommonOperation.ComboboxItem _Cmb=new BusinessLayer.CommonOperation.ComboboxItem();
   
            _SaveData = new List<UserHandling.ObjectUserRollDataType>();
            int UID = Myuser.GetUserID(txtUser.Text);
            string RollID = _Cmb.GetReleventTextFromID(cmbUserRoll, cmbUserRoll.Text, false);
            int RoalD = int.Parse(RollID);
            foreach (DataGridViewRow r in dgvObject.Rows)
                {
                if (r.Cells["dgvObject_select"].Value.ToString() == "1")
                    {

                    UserHandling.ObjectUserRollDataType _OneObject = new UserHandling.ObjectUserRollDataType();
                    int ObgID = 0, Sh = 0, S = 0, U = 0, D = 0, P = 0, A = 0, PA = 0;
                   
                    try
                        {
                        bool respond = int.TryParse(r.Cells["dgvObject_Show"].Value.ToString(), out Sh);
                        respond = int.TryParse(r.Cells["dgvObject_Save"].Value.ToString(), out S);
                        respond = int.TryParse(r.Cells["dgvObject_Update"].Value.ToString(), out U);
                        respond = int.TryParse(r.Cells["dgvObject_Delete"].Value.ToString(), out D);
                        respond = int.TryParse(r.Cells["dgvObject_Print"].Value.ToString(), out P);
                        respond = int.TryParse(r.Cells["dgvObject_Aproval"].Value.ToString(), out A);
                        respond = int.TryParse(r.Cells["dgvObject_PostToAc"].Value.ToString(), out PA);
                        respond = int.TryParse(r.Cells["dgvObject_ID"].Value.ToString(), out ObgID);
                        _OneObject.userID = UID;
                        _OneObject.RollID = RoalD;
                        _OneObject.ObgID = ObgID;
                        _OneObject.OView = Sh;
                        _OneObject.OSave = S;
                        _OneObject.OUpdate = U;
                        _OneObject.Odelete = D;
                        _OneObject.OPrint = P;
                        _OneObject.OPostToApp = A;
                        _OneObject.OPostToAcc = PA;
                        _SaveData.Add(_OneObject);
                        }
                    catch (Exception ex)
                        {
                        return ex.Message;
                        }


                    }
                else
                    return "No Selected Items";
                }
            return "True";
            
        }
        private void btnSave_Click(object sender, EventArgs e)
            {
            if (MessageBox.Show("Do you want to save ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                List<UserHandling.ObjectUserRollDataType> _SaveData;
                string respond = SetDataToClass(out _SaveData);
                if (respond != "True")
                    MessageBox.Show(respond);
                else
                {
 
                }

                }
            }
            
        }
    }
