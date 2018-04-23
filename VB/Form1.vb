Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

Namespace CustomDateFilterPopup
	Partial Public Class MainForm
		Inherits DevExpress.XtraEditors.XtraForm
		Private orderList As OrderList
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			orderList = New OrderList(10)
			gridControl1.DataSource = orderList
		End Sub
	End Class
End Namespace
