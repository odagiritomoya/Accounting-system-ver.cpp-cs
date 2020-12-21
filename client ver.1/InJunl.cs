﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static kaikei0_c.Space;

namespace kaikei0_c
{
    public partial class InJunl : Form
    {
        private const string m_meg1 = NetMeg.INSORT;
        private const string m_meg2 = NetMeg.REQ_KEIHI_KOU;
        private bool m_IsClosedByUeser;

        public InJunl()
        {
            InitializeComponent();
        }

        private bool IsntDouble(string str)
        {
            double i;
            return !double.TryParse(str, out i);
        }

        private bool IsntDate(string str)
        {
            DateTime dt;
            return !DateTime.TryParse(str, out dt);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //未入力の項目がある
            if (textBox1.TextLength == 0 || IsntDouble(textBox2.Text))
            {
                MessageBox.Show("必要な項目に入力してください！", "失敗",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                return;
            }
            //登録
            Cnet.Connect(Space.Conf.HostName);
            Cnet.Send("INSERT_JUNL(" + textBox1.Text + "," + textBox2.Text + ")");
            if (Cnet.Get() != NetMeg.ANS_SUCCESS)
                goto ERROR;
            Cnet.CutEnd();
            if (!checkBox1.Checked)
            {
                this.OnClosed(e);
                m_IsClosedByUeser = false;
            }
            return;

        ERROR:
            {
                Cnet.CutEnd();
                MessageBox.Show("登録に失敗しました！", "失敗",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                return;
            }
        }

        private void InJunl_Load(object sender, EventArgs e)
        {
            m_IsClosedByUeser = true;
        }

        private new void Closed(EventArgs e)
        {
            Space.IsInsert = false;
            this.Close();
        }
    }
}
