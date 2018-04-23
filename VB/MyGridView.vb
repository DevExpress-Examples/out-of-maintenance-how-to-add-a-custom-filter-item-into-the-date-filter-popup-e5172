Imports Microsoft.VisualBasic
Imports System
Imports System.Linq
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Collections.Generic
Imports System.ComponentModel
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Localization
Imports System.Windows.Forms
Imports DevExpress.Data.Filtering

Namespace CustomDateFilterPopup
	<System.ComponentModel.DesignerCategory("")> _
	Public Class MyGridView
		Inherits GridView
		Private ReadOnly customName As String = GridLocalizer.Active.GetLocalizedString(GridStringId.PopupFilterCustom)

		Public Sub New()
			Me.New(Nothing)
		End Sub
		Public Sub New(ByVal grid As GridControl)
			MyBase.New(grid)
			DateFilterInfo = Nothing
		End Sub

		Friend DateFilterInfo As ColumnFilterInfo

		Protected Overrides Function CreateDateFilterPopup(ByVal column As GridColumn, ByVal ownerControl As Control, ByVal creator As Object) As DateFilterPopup
			Return New MyDateFilterPopup(Me, column, ownerControl, creator)
		End Function

		Protected Overrides Sub RaiseFilterPopupDate(ByVal filterPopup As DateFilterPopup, ByVal list As List(Of FilterDateElement))
			Dim filterString As String = If(DateFilterInfo IsNot Nothing, DateFilterInfo.FilterString, "")
			Dim filterCriteria As CriteriaOperator = If(DateFilterInfo IsNot Nothing, DateFilterInfo.FilterCriteria, Nothing)
			list.Add(New FilterDateElement(customName, filterString, filterCriteria))
			MyBase.RaiseFilterPopupDate(filterPopup, list)
		End Sub
	End Class
End Namespace