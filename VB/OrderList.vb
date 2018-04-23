Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.ComponentModel

Namespace CustomDateFilterPopup
	Friend Class Order
		Private Const factor As Integer = 1000
		Private privateOrderNumber As Integer
		Public Property OrderNumber() As Integer
			Get
				Return privateOrderNumber
			End Get
			Set(ByVal value As Integer)
				privateOrderNumber = value
			End Set
		End Property
		Private privateOrderDate As DateTime
		Public Property OrderDate() As DateTime
			Get
				Return privateOrderDate
			End Get
			Set(ByVal value As DateTime)
				privateOrderDate = value
			End Set
		End Property
		Private privateCustomer As String
		Public Property Customer() As String
			Get
				Return privateCustomer
			End Get
			Set(ByVal value As String)
				privateCustomer = value
			End Set
		End Property

		Public Sub New(ByVal index As Integer)
			OrderNumber = factor + index
			OrderDate = DateTime.Today.AddDays(-index)
			Customer = "Customer " & index
		End Sub
	End Class

	Friend Class OrderList
		Inherits BindingList(Of Order)
		Private rnd As Random
		Private Const max As Integer = 7
		Public Sub New(ByVal count As Integer)
			rnd = New Random()
			For i As Integer = 0 To count - 1
				Items.Add(New Order(rnd.Next(max)))
			Next i
		End Sub
	End Class
End Namespace
