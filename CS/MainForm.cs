using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;

namespace CustomDateFilterPopup {
    public partial class MainForm : XtraForm {
        OrderList orderList;
        public MainForm() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            orderList = new OrderList(10);
            myGridControl1.DataSource = orderList;
            foreach(GridColumn column in myGridView1.Columns)
                if(column.ColumnType == typeof(DateTime))
                    column.OptionsFilter.FilterPopupMode = FilterPopupMode.Date;
                else column.OptionsFilter.FilterPopupMode = FilterPopupMode.CheckedList;
            myGridView1.Columns[0].OptionsFilter.FilterPopupMode = FilterPopupMode.List;
        }
    }
}
