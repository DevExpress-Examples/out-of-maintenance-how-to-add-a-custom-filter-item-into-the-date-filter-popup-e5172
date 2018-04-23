Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Helpers
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Localization
Imports DevExpress.XtraGrid.Views.Base

Namespace CustomDateFilterPopup
	Public Class MyDateFilterPopup
		Inherits DateFilterPopup
		Public Sub New(ByVal view As ColumnView, ByVal column As GridColumn, ByVal ownerControl As Control, ByVal creator As Object)
			MyBase.New(view, column, ownerControl, creator)
		End Sub

		Private ReadOnly customName As String = GridLocalizer.Active.GetLocalizedString(GridStringId.PopupFilterCustom)
		Private customCheck As CheckEdit
		Private dateFilterControl_Renamed As PopupOutlookDateFilterControl
		Private ReadOnly Property DateFilterControl() As PopupOutlookDateFilterControl
			Get
				If dateFilterControl_Renamed Is Nothing Then
					SetDateFilterControl(item)
				End If
				Return dateFilterControl_Renamed
			End Get
		End Property

		Protected Shadows ReadOnly Property View() As MyGridView
			Get
				Return TryCast(MyBase.View, MyGridView)
			End Get
		End Property
		Private item As RepositoryItemPopupContainerEdit
		Protected Overrides Function CreateRepositoryItem() As RepositoryItemPopupBase
			item = TryCast(MyBase.CreateRepositoryItem(), RepositoryItemPopupContainerEdit)
			If DateFilterControl.Controls.Count > 0 Then
				customCheck = GetCheckEdit()
				AddHandler customCheck.CheckedChanged, AddressOf CheckedChanged
				For Each ctrl As Control In DateFilterControl.Controls
					Dim ce As CheckEdit = (TryCast(ctrl, CheckEdit))
					If ce IsNot Nothing Then
						If ce.Text <> customName Then
							AddHandler ce.CheckedChanged, AddressOf OriginalDateFilterPopup_CheckedChanged
						End If
					End If
				Next ctrl
			End If
			Return item
		End Function

		Private Sub CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
			Dim ctrl As CheckEdit = TryCast(sender, CheckEdit)
			If ctrl.Checked Then
				UpdateOurControlCheckedState()
				If ReferenceEquals(View.DateFilterInfo, Nothing) AndAlso ctrl.Text = customName Then
					PopupActivator.ClosePopup()
				End If
			End If
		End Sub

		Protected Overridable Sub UpdateOurControlCheckedState()
			For Each ctrl As Control In DateFilterControl.Controls
				Dim ce As CheckEdit = (TryCast(ctrl, CheckEdit))
				If ce IsNot Nothing Then
					If ce.Text <> customName Then
						ce.Checked = False
					End If
				End If
			Next ctrl
		End Sub

		Private Sub OriginalDateFilterPopup_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
			Dim ctrl As CheckEdit = TryCast(sender, CheckEdit)
			If ctrl.Checked Then
				If ctrl.Text <> customName Then
					customCheck.Checked = False
				End If
			End If
		End Sub

		Public Function GetCheckEdit() As CheckEdit
			For Each ctrl As Control In DateFilterControl.Controls
				If ctrl.Text = customName Then
					Return TryCast(ctrl, CheckEdit)
				End If
			Next ctrl
			Return Nothing
		End Function

		Private Sub SetDateFilterControl(ByVal item As RepositoryItemPopupContainerEdit)
			For Each ctrl As Control In item.PopupControl.Controls
				If TypeOf ctrl Is PopupOutlookDateFilterControl Then
					dateFilterControl_Renamed = TryCast(ctrl, PopupOutlookDateFilterControl)
					Exit For
				End If
			Next ctrl
		End Sub

		Private Sub ApplyCustomFilter(ByVal e As CloseUpEventArgs)
			If customCheck.Checked Then
				Dim cfi As ColumnFilterInfo = Column.FilterInfo
				View.ShowCustomFilterDialog(Column)
				If (Not ReferenceEquals(cfi.FilterCriteria, Column.FilterInfo.FilterCriteria)) Then
					View.DateFilterInfo = Column.FilterInfo
					For Each ctrl As Control In DateFilterControl.Controls
						Dim ce As CheckEdit = (TryCast(ctrl, CheckEdit))
						If ce IsNot Nothing Then
							If ce.Text = customName Then
								ce.Tag = New FilterDateElement(Column.FieldName, Column.FilterInfo.FilterString, View.DateFilterInfo.FilterCriteria)
								DateFilterControl.ApplyFilter()
							End If
						End If
					Next ctrl
				Else
					customCheck.Checked = False
				End If
			Else
				View.DateFilterInfo = Nothing
			End If
		End Sub

		Protected Overrides Sub OnActivator_CloseUp(ByVal sender As Object, ByVal e As CloseUpEventArgs)
			If Not(e.CloseMode = PopupCloseMode.Immediate AndAlso customCheck.Checked) Then
				ApplyCustomFilter(e)
			End If
			MyBase.OnActivator_CloseUp(sender, e)
		End Sub

		Public Overrides Sub Dispose()
			RemoveHandler customCheck.CheckedChanged, AddressOf CheckedChanged
			For Each ctrl As Control In DateFilterControl.Controls
				Dim ce As CheckEdit = (TryCast(ctrl, CheckEdit))
				If ce IsNot Nothing Then
					If ctrl.Text <> customName Then
						RemoveHandler ce.CheckedChanged, AddressOf OriginalDateFilterPopup_CheckedChanged
					End If
				End If
			Next ctrl
			MyBase.Dispose()
		End Sub
	End Class
End Namespace