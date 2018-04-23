using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CustomDateFilterPopup {
    public partial class MainForm : DevExpress.XtraEditors.XtraForm {
        OrderList orderList;
        public MainForm() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            orderList = new OrderList(10);
            gridControl1.DataSource = orderList;
        }
    }
}
