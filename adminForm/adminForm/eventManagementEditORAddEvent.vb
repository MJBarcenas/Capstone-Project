﻿Imports System.Runtime.InteropServices
Imports MySql.Data.MySqlClient

Public Class eventManagementEditORAddEvent
    Dim editEventGuestsID As String
    Dim selectedBookerID As String
    Dim loadDone As Boolean = False

    Private Sub eventManagementEditORAddGuest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        home.Timer1.Enabled = False
        DateTimePicker1.MinDate = Now.ToString("d")
        'Default event is not paid
        CheckBox2.Checked = True
        'Adding values on form_load to the textboxes if the user is editing
        If eventManagement.editOrAddEvent = "edit" Then
            Label1.Text = "EDIT EVENT"

            Dim query As String = $"SELECT name, date, type, booker, guests_id, time, ispaid FROM events WHERE name='{eventManagement.editEvent}'"
            Dim ds As DataSet = getData(query)

            Dim eventGuestsID = ds.Tables(0).Rows(0)(4)

            Dim query1 As String = $"SELECT rfid, name, address, email, number, id FROM guest WHERE name='{ds.Tables(0).Rows(0)(3)}' AND guest_id={eventGuestsID}"
            Dim ds1 As DataSet = getData(query1)

            'MessageBox.Show(Convert.ToDateTime(ds.Tables(0).Rows(0)(1) + " 08:00 AM").ToString("MM/dd/yyyy h:mm tt"))

            TextBox1.Text = ds.Tables(0).Rows(0)(0)
            DateTimePicker1.Value = Convert.ToDateTime(ds.Tables(0).Rows(0)(1)).ToString("M/d/yyyy")
            ComboBox1.Text = ds.Tables(0).Rows(0)(2)

            If ds.Tables(0).Rows(0)(6) = True Then
                CheckBox1.Checked = True
            Else
                CheckBox2.Checked = True
            End If

            TextBox3.Text = ds1.Tables(0).Rows(0)(1)
            TextBox2.Text = ds1.Tables(0).Rows(0)(2)
            TextBox4.Text = ds1.Tables(0).Rows(0)(4)
            TextBox5.Text = ds1.Tables(0).Rows(0)(3)
            TextBox6.Text = ds1.Tables(0).Rows(0)(0)
            TextBox7.Text = ds.Tables(0).Rows(0)(5)

            editEventGuestsID = ds.Tables(0).Rows(0)(4)
            selectedBookerID = ds1.Tables(0).Rows(0)(5)

            Button1.BackColor = Color.DodgerBlue
            Button1.Enabled = True
        End If
        loadDone = True
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        'Check if selected date is available

        Dim selectedDate As String = DateTimePicker1.Value.ToString("MM/dd/yyyy")

        Dim dateDT As DataSet = getData($"SELECT * FROM events WHERE date='{selectedDate}'")

        If dateDT.Tables(0).Rows.Count > 0 Then
            If loadDone Then
                MessageBox.Show("There is already an event booked for that date! Please pick another date...")
                Button1.BackColor = Color.Red
                Button1.Enabled = False
            End If
        Else
            Button1.BackColor = Color.DodgerBlue
            Button1.Enabled = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Check for empty textboxes excluding RFID
        Dim emptyTextBoxes =
            From txt In Me.Panel1.Controls.OfType(Of TextBox)()
            Where txt.Text.Length = 0 And txt.Name <> TextBox6.Name
            Select txt.Name
        If emptyTextBoxes.Any Then
            MessageBox.Show("Please fill up all the fields")
            Return
        End If

        If TextBox6.Text.Length > 0 And TextBox6.Text.Length <> 10 Then
            MessageBox.Show("Invalid RFID")
            Return
        End If

        loadDone = False

        'UPDATE Database if the user is only editing
        If eventManagement.editOrAddEvent = "edit" Then
            Dim query As String = $"UPDATE events SET name='{TextBox1.Text.Replace("'", "\'")}', date='{DateTimePicker1.Value.ToString("MM/dd/yyyy")}', time='{TextBox7.Text}', type='{ComboBox1.Text}', booker='{TextBox3.Text}', ispaid={CheckBox1.Checked}, edited=1 WHERE guests_id={editEventGuestsID}"
            Dim eventSuccess As Boolean = executeNonQuery(query, localConnection)

            query = $"UPDATE guest SET rfid='{TextBox6.Text}', name='{TextBox3.Text}', address='{TextBox2.Text}', email='{TextBox5.Text}', number='{TextBox4.Text}', edited=1 WHERE id={selectedBookerID}"
            Dim guestSuccess As Boolean = executeNonQuery(query, localConnection)

            If eventSuccess And guestSuccess Then
                eventManagement.editOrAddEvent = ""
                Me.Close()
            End If
            home.Timer1.Enabled = True
            Return
        End If

        'Get the highest available value for event guests_id
        Dim query2 As String = $"SELECT `AUTO_INCREMENT` FROM  INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'local_copy' AND TABLE_NAME = 'events';"
        Dim ds As DataSet = getData(query2)


        'INSERT values into Database if the user is adding
        Dim query1 = $"INSERT INTO events(name, date, time, type, booker, ispaid, edited) VALUES('{TextBox1.Text.Replace("'", "\'")}', '{DateTimePicker1.Value.ToString("MM/dd/yyyy")}', '{TextBox7.Text}', '{ComboBox1.Text}', '{TextBox3.Text}', {CheckBox1.Checked}, 2)"
        Dim eventSuccess1 As Boolean = executeNonQuery(query1, localConnection)

        Dim query3 = $"INSERT INTO guest(guest_id, rfid, name, address, email, number, type, edited) VALUES({ds.Tables(0).Rows(0)(0)}, '{TextBox6.Text}', '{TextBox3.Text}', '{TextBox2.Text}', '{TextBox5.Text}', '{TextBox4.Text}', 'BOOKER', 2)"
        Dim guestSuccess1 As Boolean = executeNonQuery(query3, localConnection)

        If eventSuccess1 And guestSuccess1 Then
            clearAll()
        End If
    End Sub

    'Clear the editOrAddEvent property of eventManagement.
    'Refreshes eventManagement and guesManagement forms.
    'Close the form.
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        eventManagement.editOrAddEvent = ""
        eventManagement.eventManagement_Load(Nothing, Nothing)
        guestManagement.guestManagement_Load(Nothing, Nothing)
        home.refreshAllForms()
        home.Timer1.Enabled = True
        Me.Close()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            CheckBox2.Checked = False
        Else
            CheckBox2.Checked = True
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            CheckBox1.Checked = False
        Else
            CheckBox1.Checked = True
        End If
    End Sub

    'Casting Shadow to the Form
    Private Const CS_DROPSHADOW As Integer = 131072
    Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ClassStyle = cp.ClassStyle Or CS_DROPSHADOW
            Return cp
        End Get
    End Property

    'Enables the user to drag the form
    <DllImport("user32.DLL", EntryPoint:="ReleaseCapture")>
    Private Shared Sub ReleaseCapture()
    End Sub
    <DllImport("user32.DLL", EntryPoint:="SendMessage")>
    Private Shared Sub SendMessage(ByVal hWnd As System.IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer)
    End Sub

    Private Sub Form1_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        ReleaseCapture()
        ReleaseCapture()
        SendMessage(Me.Handle, &H112&, &HF012&, 0)
    End Sub
End Class