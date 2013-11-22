Imports System.Text.RegularExpressions
Imports System.IO


Public Class Form1

    '' global constants for base paths
    Private Const COPYBASEDRIVEPATH As String = "g:\"
    Private Const CBBASEPATH As String = "cb"
    Private Const MCDBASEPATH As String = "McDaniel"
    Private Const UNBASEBATH As String = "United"
    Private Const FCBASEBATH As String = "Full Color Sheets"

    '' global variable to hold the current fileinfo list - IO.FileInfo to preserve the name and path of files
    Dim fileList As New List(Of IO.FileInfo)

    '' global variable to hold the full list of paths to search
    Dim pathList As New List(Of String)

    '' Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    ''End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged

        '' always clear the lists when the input changes
        lstbxResults.Items.Clear()
        fileList.Clear()
        pathList.Clear()

        '' if the box isn't empty or less than two characters, search as the user types
        If (txtSearch.Text <> "" And txtSearch.Text.Length > 1) Then

            '' get the search path and search string
            Dim topLevelPath As String = getPath(txtSearch.Text)
            Dim search As String = getSearchString(txtSearch.Text)

            '' add the topLevelPath to the search
            pathList.Add(topLevelPath)

            '' recursively add all subfolders at the top level path
            getAllSubfolders(topLevelPath)

            '' inserts the FCBASEBATH into the topLevelPath string after the COPYBASEDRIVEPATH string
            '' so the new path is of the form "g:\FCBASEPATH\cbA"
            topLevelPath = topLevelPath.Insert(COPYBASEDRIVEPATH.Length, FCBASEBATH + "\")

            '' add this new topLevelPath to the search
            pathList.Add(topLevelPath)

            '' now with the full color folder in the path, add subfolders again
            getAllSubfolders(topLevelPath)

            '' debug - add paths to path list box
            'lstboxPathList.Items.Clear()
            'For Each path In pathList
            '    lstboxPathList.Items.Add(path)
            'Next

            '' only get a list if there's a search string (there's enough characters typed in)
            If (search.Length > 0) Then
                getFileList(search)

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

    Private Sub lstbxResults_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lstbxResults.MouseDown
        '' determine if this is a right mouse click
        If e.Button = Windows.Forms.MouseButtons.Right Then
            '' did we actually right-click on an item in the box
            Dim selInd As Integer = lstbxResults.IndexFromPoint(e.X, e.Y)

            '' if the returned selected index from cursor coordinates is not -1, then we clicked on something
            If selInd <> -1 Then

                '' get the full path of the item selected
                Dim selectedPath As String = fileList(selInd).FullName

                '' open explorer and select the file clicked
                Call Shell("explorer.exe /select," & selectedPath, AppWinStyle.NormalFocus)

            End If
        End If
    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '' Helper functions
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Sub getAllSubfolders(ByVal topLevelPath As String)
        Dim subfolder As String

        Try
            '' iterate through folders recursively, adding all subfolders to the list
            '' this will recursively call itself, adding items to the global
            '' pathList List(Of String) each time
            For Each subfolder In Directory.GetDirectories(topLevelPath)
                pathList.Add(subfolder)

                '' now for each subfolder, run this again recursively
                getAllSubfolders(subfolder)

            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Function getPath(ByVal input As String) As String

        '' if this is Church Budget, the files are located in g:\cbX where X is first char
        '' if this is United, the files are located in g:\United\XX\ where XX is first 2 chars
        '' if this is McDaniel, the files are at g:\McDaniel\XX\ where XX is first 2 chars
        If (isChurchBudget(input)) Then
            Return COPYBASEDRIVEPATH + CBBASEPATH + input.Substring(0, 1) + "\"
        ElseIf (isUnited(input)) Then
            Return COPYBASEDRIVEPATH + UNBASEBATH + "\" + input.Substring(0, 2) + "\"
        Else
            Return COPYBASEDRIVEPATH + MCDBASEPATH + "\" + input.Substring(0, 2) + "\"
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

    Private Sub getFileList(ByVal search As String)

        '' loop through list of paths
        For Each path In pathList

            '' if path exists, get a list of files from search and add it to the global fileList List(Of IO.FileInfo)
            If (Directory.Exists(path)) Then
                Dim folderInfo As New IO.DirectoryInfo(path)
                Dim arrFilesInFolder() As IO.FileInfo

                '' old way, getting the api to run the wilcard match
                'arrFilesInFolder = folderInfo.GetFiles("*" + search + "*.*")
                '' copy the array of files to a list and append to the global fileList
                'fileList.AddRange(arrFilesInFolder.ToList())

                '' because of weird results searching for, e.g. *89*.* (files came back without "89" in the name,
                '' maybe due to something with long filenames and 8.3 equivalents?), we now get *.* and filter
                '' the results here
                For Each fileName As IO.FileInfo In folderInfo.GetFiles("*.*")
                    If fileName.Name.Contains(search) Then
                        fileList.Add(fileName)
                    End If
                Next

                '' debug
                lstboxPathList.Items.Add(path)

            Else
                '' debug
                lstboxPathList.Items.Add("====" + path)

            End If

        Next

    End Sub

End Class
