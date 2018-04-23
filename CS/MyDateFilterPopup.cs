using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Helpers;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Views.Base;

namespace CustomDateFilterPopup {
    public class MyDateFilterPopup : DateFilterPopup {
        public MyDateFilterPopup(ColumnView view, GridColumn column, Control ownerControl, object creator)
            : base(view, column, ownerControl, creator) {
        }

        readonly string customName = GridLocalizer.Active.GetLocalizedString(GridStringId.PopupFilterCustom);
        CheckEdit customCheck;
        PopupOutlookDateFilterControl dateFilterControl;
        PopupOutlookDateFilterControl DateFilterControl {
            get {
                if(dateFilterControl == null) SetDateFilterControl(item);
                return dateFilterControl;
            }
        }

        protected new MyGridView View { get { return base.View as MyGridView; } }
        RepositoryItemPopupContainerEdit item;
        protected override RepositoryItemPopupBase CreateRepositoryItem() {
            item = base.CreateRepositoryItem() as RepositoryItemPopupContainerEdit;
            if(DateFilterControl.Controls.Count > 0) {
                customCheck = GetCheckEdit();
                customCheck.CheckedChanged += CheckedChanged;
                foreach(Control ctrl in DateFilterControl.Controls) {
                    CheckEdit ce = (ctrl as CheckEdit);
                    if(ce != null)
                        if(ce.Text != customName)
                            ce.CheckedChanged += OriginalDateFilterPopup_CheckedChanged;
                }
            }
            return item;
        }

        void CheckedChanged(object sender, EventArgs e) {
            CheckEdit ctrl = sender as CheckEdit;
            if(ctrl.Checked) {
                UpdateOurControlCheckedState();
                if(ReferenceEquals(View.DateFilterInfo, null) && ctrl.Text == customName)
                    PopupActivator.ClosePopup();
            }
        }

        protected virtual void UpdateOurControlCheckedState() {
            foreach(Control ctrl in DateFilterControl.Controls) {
                CheckEdit ce = (ctrl as CheckEdit);
                if(ce != null)
                    if(ce.Text != customName)
                        ce.Checked = false;
            }
        }

        void OriginalDateFilterPopup_CheckedChanged(object sender, EventArgs e) {
            CheckEdit ctrl = sender as CheckEdit;
            if(ctrl.Checked)
                if(ctrl.Text != customName)
                    customCheck.Checked = false;
        }

        public CheckEdit GetCheckEdit() {
            foreach(Control ctrl in DateFilterControl.Controls) {
                if(ctrl.Text == customName)
                    return ctrl as CheckEdit;
            }
            return null;
        }

        private void SetDateFilterControl(RepositoryItemPopupContainerEdit item) {
            foreach(Control ctrl in item.PopupControl.Controls)
                if(ctrl is PopupOutlookDateFilterControl) {
                    dateFilterControl = ctrl as PopupOutlookDateFilterControl;
                    break;
                }
        }

        void ApplyCustomFilter(CloseUpEventArgs e) {
            if(customCheck.Checked) {
                ColumnFilterInfo cfi = Column.FilterInfo;
                View.ShowCustomFilterDialog(Column);
                if(!ReferenceEquals(cfi.FilterCriteria, Column.FilterInfo.FilterCriteria)) {
                    View.DateFilterInfo = Column.FilterInfo;
                    foreach(Control ctrl in DateFilterControl.Controls) {
                        CheckEdit ce = (ctrl as CheckEdit);
                        if(ce != null)
                            if(ce.Text == customName) {
                                ce.Tag = new FilterDateElement(Column.FieldName, Column.FilterInfo.FilterString, View.DateFilterInfo.FilterCriteria);
                                DateFilterControl.ApplyFilter();
                            }
                    }
                } else customCheck.Checked = false;
            } else View.DateFilterInfo = null;
        }

        protected override void OnActivator_CloseUp(object sender, CloseUpEventArgs e) {
            if(!(e.CloseMode == PopupCloseMode.Immediate && customCheck.Checked))
                ApplyCustomFilter(e);
            base.OnActivator_CloseUp(sender, e);
        }

        public override void Dispose() {
            customCheck.CheckedChanged -= CheckedChanged;
            foreach(Control ctrl in DateFilterControl.Controls) {
                CheckEdit ce = (ctrl as CheckEdit);
                if(ce != null)
                    if(ctrl.Text != customName)
                        ce.CheckedChanged -= OriginalDateFilterPopup_CheckedChanged;
            }
            base.Dispose();
        }
    }
}