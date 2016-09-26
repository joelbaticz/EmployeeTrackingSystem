using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ETS.domain;
using ETS.logic;

namespace ETS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Binds the event handler DrawOnTab to the DrawItem event 
            // through the DrawItemEventHandler delegate.
            tabControl1.DrawItem += new DrawItemEventHandler(DrawOnTab);
        }

        // Declares the event handler DrawOnTab which is a method that
        // draws a string and Rectangle on the tabPage1 tab.
        private void DrawOnTab(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Black);
            Font font = new Font("Arial", 10.0f);
            SolidBrush brush = new SolidBrush(Color.DarkGray);

            Rectangle tabArea = tabControl1.GetTabRect(0);
            Rectangle tabTextArea = tabArea;//(RectangleF)tabControl1.GetTabRect(0);
            tabTextArea.X+= 5;
            tabTextArea.Y += 5;

            //g.DrawRectangle(p, tabArea);
            g.DrawString("tabPage1", font, brush, tabTextArea);
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            //input
            Employee emp = new Employee();
            emp.FirstName = txtFirstName.Text;
            emp.LastName = txtLastName.Text;
            emp.Email = txtEmail.Text;
            emp.DOB = DateTime.Parse(dtpDOB.Text);
            emp.Phone = txtPhone.Text;

            //process - add employee
            EmployeeManager manager = new EmployeeManager();
            ResultEnum result = manager.InsertEmployee(emp);

            //output
            if (result == ResultEnum.Success)
            {
                MessageBox.Show("Employee added successfully");
            }
            else
            {
                MessageBox.Show("Error occured!");
            }

        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            
            //input

            //process - get employees
            EmployeeManager manager = new EmployeeManager();
            Result<List<Employee>> result = manager.SelectAllEmployee();

            //output
            if (result.ResultStatus == ResultEnum.Success)
            {
                lstEmployees.Items.Clear();



                lstEmployees.DataSource = result.ResultData;
                lstEmployees.DisplayMember = "FullName";

                /*
                foreach (Employee emp in result.ResultData)
                {
                    lstEmployees.Items.Add(emp);
                }
                */
            }
            else
            {
                MessageBox.Show("Error selecting all employees");
            }
            

        }

        private void lstEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFirstName.Text = ((Employee)lstEmployees.SelectedItem).FirstName;
            txtLastName.Text = ((Employee)lstEmployees.SelectedItem).LastName;
            txtEmail.Text = ((Employee)lstEmployees.SelectedItem).Email;
            dtpDOB.Text = ((Employee)lstEmployees.SelectedItem).DOB.ToString();
            txtPhone.Text = ((Employee)lstEmployees.SelectedItem).Phone;

            //cmbEmployeeId.SelectedText= ((Employee)lstEmployees.SelectedItem).FullName;
            cmbEmployeeId.SelectedIndex = lstEmployees.SelectedIndex;
        }

        private void btnAddHours_Click(object sender, EventArgs e)
        {
            
            //input
            EmpHour hrs = new EmpHour();
            hrs.EmpID= ((Employee)lstEmployees.SelectedItem).EmpID;
            hrs.WorkDate= DateTime.Parse(dtpWorkDate.Text);
            hrs.Hours = int.Parse(txtHours.Text);

            //process - add employee
            EmployeeHourManager manager = new EmployeeHourManager();
            ResultEnum result = manager.InsertEmployeeHour(hrs);

            //output
            if (result == ResultEnum.Success)
            {
                MessageBox.Show("Hours added successfully");
            }
            else
            {
                MessageBox.Show("Error occured!");
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            EmployeeManager manager = new EmployeeManager();

            Result<List<Employee>> result = manager.SelectAllEmployee();

            if (result.ResultStatus==ResultEnum.Success)
            {
                cmbEmployeeId.DataSource = result.ResultData;
                cmbEmployeeId.DisplayMember = "FullName";
                cmbEmployeeId.ValueMember = "EmpId";


            }


        }

        private void btnShowHours_Click(object sender, EventArgs e)
        {
            EmployeeHourManager manager = new EmployeeHourManager();

            Result<List<EmpHour>> result = manager.SelectAllEmpHour();

            if (result.ResultStatus==ResultEnum.Success)
            {
                dataGridView1.DataSource = result.ResultData;
            }
            else
            {
                MessageBox.Show("Error occured accessing EmpHours");
            }
        }


        private void stuff()
        {
            EmployeeHourManager manager = new EmployeeHourManager();

            Result<List<EmpHour>> result=null;
            try
            {
                result = manager.SelectEmpHourByEmpId(Convert.ToInt32(cmbEmployeeId.SelectedValue));
            }
            catch (Exception ex)
            {

            }

            if (result != null)
            {
                if (cmbEmployeeId.SelectedValue != null)
                {


                    if (result.ResultStatus == ResultEnum.Success)
                    {
                        dataGridView1.DataSource = result.ResultData;
                    }
                    else
                    {
                        MessageBox.Show("Error occured accessing EmpHours");
                    }
                }
            }
        }
        private void btnShowHoursById_Click(object sender, EventArgs e)
        {
            stuff();

        }

        private void cmbEmployeeId_SelectedIndexChanged(object sender, EventArgs e)
        {
            stuff();
        }
    }
}
