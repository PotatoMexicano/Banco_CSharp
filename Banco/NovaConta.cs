﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Banco
{
    public partial class NovaConta : Form
    {
        public NovaConta()
        {
            InitializeComponent();
        }

        private void NovaConta_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            new Login().ShowDialog();
            
        }
    }
}