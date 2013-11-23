Public Class Settings

    Dim additionalPaths As New List(Of String)

    Private Sub btnSaveSettings_Click(sender As Object, e As EventArgs) Handles btnSaveSettings.Click
        'MessageBox.Show("CB: " + rdioCB.Checked.ToString() + " MM: " + rdioMM.Checked.ToString() + " addtl: " + txtExtraPaths.Text)
        My.Settings.isCB = rdioCB.Checked
        My.Settings.isMM = rdioMM.Checked

        '' clear out extra paths first
        My.Settings.additionalPaths = ""

        '' remove blank lines from text box
        If (txtExtraPaths.Text <> "") Then
            For Each line In txtExtraPaths.Text.Split(Environment.NewLine)
                If line.Trim.Length > 0 Then
                    My.Settings.additionalPaths += line + Environment.NewLine
                End If
            Next
        End If

        My.Settings.Save()
        Me.Close()
    End Sub

    Private Sub Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Settings.Reload()
        'MessageBox.Show("CB: " + My.Settings.isCB.ToString + " MM: " + My.Settings.isMM.ToString + " addtl: " + My.Settings.additionalPaths)

        '' restore saved settings
        rdioCB.Checked = My.Settings.isCB
        rdioMM.Checked = My.Settings.isMM

        If (My.Settings.additionalPaths <> "") Then

            For Each line In My.Settings.additionalPaths.Split(Environment.NewLine)
                If line.Trim.Length > 0 Then
                    txtExtraPaths.Text += line + Environment.NewLine
                    additionalPaths.Add(line)
                End If
            Next
        Else
            txtExtraPaths.Text = ""
        End If


    End Sub

    Private Sub btnCancelSettings_Click(sender As Object, e As EventArgs) Handles btnCancelSettings.Click
        Me.Close()
    End Sub
End Class