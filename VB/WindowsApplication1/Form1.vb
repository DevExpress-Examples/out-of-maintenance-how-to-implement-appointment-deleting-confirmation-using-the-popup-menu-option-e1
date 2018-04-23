Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraScheduler

Namespace WindowsApplication1
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub schedulerControl1_PreparePopupMenu(ByVal sender As Object, ByVal e As DevExpress.XtraScheduler.PreparePopupMenuEventArgs) Handles schedulerControl1.PreparePopupMenu
			If e.Menu.Id = SchedulerMenuItemId.AppointmentMenu Then
				Dim oldDelete As DXMenuItem = GetMenuItem(e.Menu, "&Delete")
				oldDelete.Visible = False
				Dim newDelete As New DXMenuItem("&Delete", AddressOf Deleted_Click, oldDelete.Image)
				e.Menu.Items.Add(newDelete)
			End If
		End Sub

		Private deleteBtnClicked As Boolean = False
		Private Sub Deleted_Click(ByVal sender As Object, ByVal e As EventArgs)
			deleteBtnClicked = True
			Me.schedulerStorage1.Appointments.Remove(Me.schedulerControl1.SelectedAppointments(0))
		End Sub

		Private Sub schedulerStorage1_AppointmentDeleting(ByVal sender As Object, ByVal e As PersistentObjectCancelEventArgs) Handles schedulerStorage1.AppointmentDeleting
			If deleteBtnClicked Then
				Dim res As DialogResult = MessageBox.Show("Are you sure?", "Please confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
				If res <> System.Windows.Forms.DialogResult.Yes Then
					e.Cancel = True
				End If
			End If
			deleteBtnClicked = False
		End Sub

		Protected Function GetMenuItem(ByVal menu As SchedulerPopupMenu, ByVal menuItemName As String) As DXMenuItem
			Dim foundItem As DXMenuItem = Nothing
			For Each item As DXMenuItem In menu.Items
				If item.Caption = menuItemName Then
					foundItem = item
				End If
			Next item
			Return foundItem
		End Function
	End Class
End Namespace