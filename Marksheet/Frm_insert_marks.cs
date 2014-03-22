﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marksheet
{
    public partial class Frm_Insert_marks : Form
    {
        database db = new database();
        class_insert_marks InsertMarks = new class_insert_marks();
        public Frm_Insert_marks()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            InsertMarks.Roll = Convert.ToInt32(cbRollNo.Text);
            InsertMarks.SubjectName = cbName.Text;
            try
            {
                InsertMarks.Assignment = Convert.ToInt16(txtAssignment.Text);
                if (InsertMarks.Assignment > 10)
                    errorProvider1.SetError(txtAssignment, "value must be between 0-10");
                else
                {
                    errorProvider1.Dispose();
                    InsertMarks.Attendance = Convert.ToInt16(txtAttendence.Text);
                    if (InsertMarks.Attendance > 10)
                        errorProvider1.SetError(txtAttendence, "value must be  between 0-10");
                    else
                    {
                        errorProvider1.Dispose();
                        InsertMarks.TermTest = Convert.ToInt16(txtTermTest.Text);
                        if (InsertMarks.TermTest > 70)
                            errorProvider1.SetError(txtTermTest, "value must be  between 0-70");
                        else
                        {
                            InsertMarks.Practical = Convert.ToInt16(txtPractical.Text);
                            if (InsertMarks.Practical > 10)
                                errorProvider1.SetError(txtPractical, "value Must be between 0-10");
                            else
                            {
                                errorProvider1.Dispose();
                                InsertMarks.Calculate_Total();

                                if (InsertMarks.Add_Student_Marks() == "inserted")
                                    MessageBox.Show("Success!");
                                else
                                    if (MessageBox.Show("The marks of roll:'" + InsertMarks.Roll+ "' and subject:'"+InsertMarks.SubjectName +"' already exists\n" + "Do you want to update the information? ", "The Data already exists.", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                    MessageBox.Show(InsertMarks.Update_Student_Marks());

                                txtAssignment.Text  = "0";
                                txtAttendence .Text = "0";
                                txtPractical.Text = "0";
                                txtTermTest.Text = "0";
                            }

                        }
                    }
                }
            }
            catch
            { MessageBox.Show("Please enter numeric character"); }
        }

        private void Frm_insert_marks_Load(object sender, EventArgs e)
        {
            
            
            DataTable dt = new DataTable();
            dt = InsertMarks.Get_All_Student_Roll();
            cbRollNo.DataSource=dt;
            cbRollNo.DisplayMember = "Student_Roll";
            

            dt = InsertMarks.Get_All_Subjects();
            cbName.DataSource = dt;
            cbName.DisplayMember = "Subject_Name";



        }

        private void cbName_SelectedIndexChanged(object sender, EventArgs e)
        {
            InsertMarks.SubjectName = cbName.Text;
            if (InsertMarks.Subject_Has_practical() == "True")
            {
                gbPractical.Enabled = true;
            }
            else
            {
                gbPractical.Enabled = false;
                txtPractical.Text = "0";
            }



        }

      

    
    }
}
