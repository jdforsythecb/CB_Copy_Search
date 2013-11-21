Imports System.Text.RegularExpressions
Imports System.IO


Public Class Form1

    '' global constants for base paths
    Private Const CBBASEPATH As String = "g:\cb"
    Private Const MCDBASEPATH As String = "g:\McDaniel"
    Private Const UNBASEBATH As String = "g:\United"

    '' global variable to hold the current fileinfo list
    Dim fileList As New List(Of IO.FileInfo)

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged

        '' always clear the list when the input changes
        lstbxResults.Items.Clear()

        '' if the box isn't empty or less than two characters, search as the user types
        If (txtSearch.Text <> "" And txtSearch.Text.Length > 1) Then

            '' get the search path and search string
            Dim path As String = getPath(txtSearch.Text)
            Dim search As String = getSearchString(txtSearch.Text)

            '' only get a list if there's a search string (there's enough characters typed in)
            If (search.Length > 0) Then
                fileList = getFileList(path, search)

                '' add the new results to the list
                For Each file In fileList
                    lstbxResults.Items.Add(file.Name)
                Next
            End If
        End If

    End Sub

    Private Sub lstbxResults_DoubleClick(sender As Object, e As EventArgs) Handles lstbxResults.DoubleClick

        '' debug - to show the proper path/file is being selected
        'MessageBox.Show(fileList(lstbxResults.SelectedIndex).FullName)

        '' open the selected file in its default program
        Dim openFile As New ProcessStartInfo()
        With openFile
            .FileName = fileList(lstbxResults.SelectedIndex).FullName
            .UseShellExecute = True
        End With
        Process.Start(openFile)

    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' Helper functions
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Function getPath(ByVal input As String) As String

        '' if this is Church Budget, the files are located in g:\cbX where X is first char
        '' if this is United, the files are located in g:\United\XX\ where XX is first 2 chars
        '' if this is McDaniel, the files are at g:\McDaniel\XX\ where XX is first 2 chars
        If (isChurchBudget(input)) Then
            Return CBBASEPATH + input.Substring(0, 1) + "\"
        ElseIf (isUnited(input)) Then
            Return UNBASEBATH + "\" + input.Substring(0, 2) + "\"
        Else
            Return MCDBASEPATH + "\" + input.Substring(0, 2) + "\"
        End If

    End Function

    Private Function isChurchBudget(ByVal input As String) As Boolean
        Dim firstChar As Char = input.Substring(0, 1)
        '' this code does the same as above
        ' Dim firstChar As Char = input(0)

        '' if the first character is a letter, this is church budget
        If (Regex.IsMatch(firstChar, "[A-Za-z]")) Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Function isUnited(ByVal input As String) As Boolean
        Dim firstChar As Char = input.Substring(0, 1)

        '' if the first character is the number 6, this is United
        If (firstChar = "6") Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Function getSearchString(ByVal input As String) As String

        '' if Church Budget, remove the letter (first character) and search by the number
        '' if United or McDaniel, remove the first two numbers and search by the rest
        If (isChurchBudget(input)) Then
            '' return the string minus the first character
            Return input.Substring(1)
        Else
            '' return the string minus the first two characters
            Return input.Substring(2)
        End If

    End Function

    Private Function getFileList(ByVal path As String, ByVal search As String) As List(Of IO.FileInfo)
        Dim fileList As New List(Of IO.FileInfo)

        '' if path exists, get a list of files from search
        If (Directory.Exists(path)) Then
            Dim folderInfo As New IO.DirectoryInfo(path)
            Dim arrFilesInFolder() As IO.FileInfo

            arrFilesInFolder = folderInfo.GetFiles("*" + search + "*.*")

            For Each fileInFolder In arrFilesInFolder
                fileList.Add(fileInFolder)
            Next

        End If

        Return fileList
    End Function

End Class
