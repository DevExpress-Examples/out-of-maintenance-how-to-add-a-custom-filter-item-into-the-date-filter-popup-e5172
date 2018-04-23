using System;
using System.Linq;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Localization;
using System.Windows.Forms;
using DevExpress.Data.Filtering;

namespace CustomDateFilterPopup
{
    [System.ComponentModel.DesignerCategory("")]
    public class MyGridView : GridView
    {
        readonly string customName = GridLocalizer.Active.GetLocalizedString(GridStringId.PopupFilterCustom);

        public MyGridView() : this(null) { }
        public MyGridView(GridControl grid) : base(grid)
        {
            DateFilterInfo = null;
        }

        internal ColumnFilterInfo DateFilterInfo;

        protected override DateFilterPopup CreateDateFilterPopup(GridColumn column, Control ownerControl, object creator) {
            return new MyDateFilterPopup(this, column, ownerControl, creator);
        }

        protected override void RaiseFilterPopupDate(DateFilterPopup filterPopup, List<FilterDateElement> list)
        {
            string filterString = DateFilterInfo != null ? DateFilterInfo.FilterString : "";
            CriteriaOperator filterCriteria = DateFilterInfo != null ? DateFilterInfo.FilterCriteria : null;
            list.Add(new FilterDateElement(customName, filterString, filterCriteria));
            base.RaiseFilterPopupDate(filterPopup, list);
        }
    }
}