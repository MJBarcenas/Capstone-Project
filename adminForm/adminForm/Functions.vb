﻿Imports MySql.Data.MySqlClient

Module Functions
    'Declaring DB-related variables
    Public str As String = "server=191.101.230.103; uid=u608197321_van_; pwd=~C3qt^9kZ; database=u608197321_real_capstone"
    Public con As New MySqlConnection(str)

    'Assorted Variables
    'Get what is the current event
    Public practiceDate As New Date(2022, 11, 20)
    Public practiceDateString As String = practiceDate.ToString("d")

    'Assorted Functions
    Public Sub addColumns(ByVal numberOfColumns As Integer, ByVal dataTable As DataTable)
        Dim column As DataColumn
        For i As Integer = 0 To numberOfColumns - 1
            column = New DataColumn()
            column.DataType = System.Type.GetType("System.String")
            column.ColumnName = $"{i}"
            dataTable.Columns.Add(column)
        Next
    End Sub

    'Execute a query
    Public Function executeNonQuery(ByVal query As String) As Boolean
        Dim complete As Boolean = True
        Dim cmd As MySqlCommand
        Try
            con.Open()
            cmd = con.CreateCommand()
            cmd.CommandText = query
            cmd.ExecuteNonQuery()
        Catch ex As MySql.Data.MySqlClient.MySqlException
            MessageBox.Show(ex.Message())
            complete = False
        End Try
        con.Close()
        Return complete
    End Function

    Public Function getAllData() As DataSet
        Dim trackingReportQuery = "SELECT name, guests_id, date FROM events WHERE date_format(str_to_date(date, '%m/%d/%Y'), '%Y/%m/%d')<=date_format(curdate(), '%Y/%m/%d') ORDER BY date DESC;"
        Dim guestManagementQuery = "SELECT guests_id, name, date FROM events;"
        Dim eventManagementQuery = "SELECT name, type, date, guests_id FROM events;"
        Dim userManagementQuery = "SELECT fullname, role, status FROM admin;"
        Dim guestQuery = "SELECT name, type, guest_id FROM guest;"

        Dim query As String = $"{trackingReportQuery} {guestManagementQuery} {eventManagementQuery} {userManagementQuery} {guestQuery}"
        Dim newCon As MySqlConnection
        Dim ds As New DataSet()

        If con.State = ConnectionState.Closed Then
            newCon = con.Clone()
            Dim adpt As New MySqlDataAdapter(query, newCon)
            adpt.Fill(ds)
        Else
            Dim adpt As New MySqlDataAdapter(query, con)
            adpt.Fill(ds)
        End If

        Return ds
    End Function

    Public Function getData(ByVal query As String) As DataSet
        'Dim adpt As New MySqlDataAdapter(query, con)
        'Dim ds As New DataSet()
        'adpt.Fill(ds)

        Dim newCon As MySqlConnection
        Dim ds As New DataSet()

        If con.State = ConnectionState.Closed Then
            newCon = con.Clone()
            Dim adpt As New MySqlDataAdapter(query, newCon)
            adpt.Fill(ds)
        Else
            Dim adpt As New MySqlDataAdapter(query, con)
            adpt.Fill(ds)
        End If

        'Return ds

        Return ds
    End Function

    'Home Functions
    Public Sub changeColor(ByVal tochange As Button, ByVal stay1 As Button, ByVal stay2 As Button, ByVal stay3 As Button)
        tochange.BackColor = Color.White
        stay1.BackColor = Color.WhiteSmoke
        stay2.BackColor = Color.WhiteSmoke
        stay3.BackColor = Color.WhiteSmoke
    End Sub

    Public Sub showThis(ByVal sender As Object, ByVal container As Panel, ByVal toShow As Form)
        With toShow
            .TopLevel = False
            container.Controls.Add(toShow)
            .BringToFront()
            .Show()
        End With
    End Sub

    'Tracking Report Functions
    Public Function isIn(ByVal logs As String()) As Boolean
        Dim count As Integer = logs.Length

        Return count Mod 2
    End Function

    'Event Management Functions
    Public Sub clearAll()
        eventManagementEditORAddEvent.TextBox1.Clear()
        eventManagementEditORAddEvent.TextBox2.Clear()
        eventManagementEditORAddEvent.TextBox3.Clear()
        eventManagementEditORAddEvent.TextBox4.Clear()
        eventManagementEditORAddEvent.TextBox5.Clear()
        eventManagementEditORAddEvent.TextBox6.Clear()
        eventManagementEditORAddEvent.TextBox7.Clear()
        eventManagementEditORAddEvent.ComboBox1.Text = ""
    End Sub

End Module
