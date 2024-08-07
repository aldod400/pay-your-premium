﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pay_your_premium
{
  
    public partial class Regist : Form
    {
        public Regist()
        {
            InitializeComponent();
        }
        private void User_Enter(object sender, EventArgs e)
        {
            if (User.Text == "Username")
            {
                User.Text = "";
            }
        }
        
        private void User_Leave(object sender, EventArgs e)
        {
            if (User.Text == "")
            {
                User.Text = "Username";
            }
        }

        private void Pass_Enter(object sender, EventArgs e)
        {
            if (Pass.Text == "Password")
            {
                Pass.Text = "";
            }
            Pass.UseSystemPasswordChar = true;
        }
        private void Pass_Leave(object sender, EventArgs e)
        {
            if (Pass.Text == "")
            {
                Pass.Text = "Password";
            }
            Pass.UseSystemPasswordChar = true;
        }

        private void email_Enter(object sender, EventArgs e)
        {
            if (email.Text == "Email")
            {
                email.Text = "";
            }
        }

        private void email_Leave(object sender, EventArgs e)
        {
            if (email.Text == "")
            {
                email.Text = "Email";
            }
        }

        private void confirm_Enter(object sender, EventArgs e)
        {
            if (confirm.Text == "Confirm Password")
            {
                confirm.Text = "";
            }
            confirm.UseSystemPasswordChar = true;
        }

        private void confirm_Leave(object sender, EventArgs e)
        {
            if (confirm.Text == "")
            {
                confirm.Text = "Confirm Password";
            }
            confirm.UseSystemPasswordChar = true;
        }

        private void close_Click(object sender, EventArgs e)
        {
            DialogResult m = MessageBox.Show("Are You Sure About Closing The Program ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (m == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-RGLU690;Initial Catalog=pay_your_premium;Integrated Security=True");
            //SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-C8AHDPB\PROGRAMMER;Initial Catalog=pay_your_premium;Integrated Security=True");
            cn.Open();

            if (User.Text == "Username" || email.Text == "Email" || Pass.Text == "Password" || confirm.Text == "Confirm Password")
            {
                MessageBox.Show("Check Empty Field","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email.Text);
                if (match.Success)
                {

                    SqlCommand sm = new SqlCommand("select*from users where  user_email='" + email.Text + "'", cn);
                    SqlDataReader sdr = sm.ExecuteReader();
                    sdr.Read();
                    if (sdr.HasRows == true)
                    {
                        MessageBox.Show("Repeated email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {
                        if (Pass.Text != confirm.Text)
                        {
                            MessageBox.Show("This password Dosn't Match","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            sdr.Close();
                            SqlCommand sn = new SqlCommand("INSERT into users([user_name],[user_email],[user_password])values('" + User.Text + "','" + email.Text + "' , '" + Pass.Text + "');", cn);
                            sn.ExecuteNonQuery();
                            MessageBox.Show("User Added Successfully " + User.Text, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            Login lg = new Login();
                            lg.Show();
                            this.Hide();

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please Inter The Correct Email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            cn.Close();
        }

        private void Pass_TextChanged(object sender, EventArgs e)
        {

        }
    }
}