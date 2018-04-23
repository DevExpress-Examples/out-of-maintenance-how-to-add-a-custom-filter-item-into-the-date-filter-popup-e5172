Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns

Namespace CustomDateFilterPopup
	Partial Public Class MainForm
		Inherits XtraForm
		Private orderList As OrderList
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			orderList = New OrderList(10)
			myGridControl1.DataSource = orderList
			For Each column As GridColumn In myGridView1.Columns
				If column.ColumnType Is GetType(DateTime) Then
					column.OptionsFilter.FilterPopupMode = FilterPopupMode.Date
				Else
					column.OptionsFilter.FilterPopupMode = FilterPopupMode.CheckedList
				End If
			Next column
			myGridView1.Columns(0).OptionsFilter.FilterPopupMode = FilterPopupMode.List
		End Sub
	End Class
End Namespace
