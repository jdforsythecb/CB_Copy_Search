Imports System.Text.RegularExpressions
Imports System.IO


Public Class Form1

    '' global constants for base paths
    Private Const COPYBASEDRIVEPATH As String = "g:\"
    Private Const CBBASEPATH As String = "cb"
    Private Const MCDBASEPATH As String = "McDaniel"
    Private Const MCDBWPATHDIFFERENCE As String = "MC"
    Private Const UNBASEBATH As String = "United"
    Private Const UNBWPATHDIFFERENCE As String = "Un"
    Private Const FCBASEBATH As String = "Full Color Sheets"
    Private Const MMBASEDRIVEPATH As String = "h:\"
    Private Const MMBASEPATH As String = "MM"
    Private Const MMSUBPATH As String = "Mm"

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

            Dim topLevelPaths As New List(Of String)
            Dim search As String = txtSearch.Text

            '' get the search path and search string
            If (My.Settings.isCB = True) Then
                topLevelPaths = CBgetTopLevelPaths(txtSearch.Text)
                search = CBgetSearchString(txtSearch.Text)
            Else
                topLevelPaths = MMgetTopLevelPaths(txtSearch.Text)
            End If

            '' always add additional paths from settings
            My.Settings.Reload()
            For Each line In My.Settings.additionalPaths.Split(CChar(vbCrLf))
                '' only add to the list if the line is not empty
                If line.Trim.Length > 0 Then
                    topLevelPaths.Add(line)
                End If
            Next

            '' loop through the top level paths returned, adding them to the list and getting all
            '' their subfolders to add to the list
            For Each topLevelPath In topLevelPaths

                '' add the topLevelPath to the search
                pathList.Add(topLevelPath)

                '' recursively add all subfolders at the top level path
                getAllSubfolders(topLevelPath)
            Next

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

    '' CB ONLY
    Private Function CBgetTopLevelPaths(ByVal input As String) As List(Of String)
        Dim paths As New List(Of String)

        If (CBisChurchBudget(input)) Then
            '' if this is Church Budget, the files are located in g:\cbX where X is first char
            '' and g:\Full Color Sheets\cbX
            paths.Add(COPYBASEDRIVEPATH + CBBASEPATH + input.Substring(0, 1) + "\")
            paths.Add(COPYBASEDRIVEPATH + FCBASEBATH + "\" + input.Substring(0, 1) + "\")

        ElseIf (CBisUnited(input)) Then
            '' if this is United, the files are located in g:\United\UnXX\ where XX is first 2 chars
            '' and g:\Full Color Sheets\United\XX
            paths.Add(COPYBASEDRIVEPATH + UNBASEBATH + "\" + UNBWPATHDIFFERENCE + input.Substring(0, 2) + "\")
            paths.Add(COPYBASEDRIVEPATH + FCBASEBATH + "\" + UNBASEBATH + "\" + input.Substring(0, 2) + "\")
        Else
            '' if this is McDaniel, the files are at g:\McDaniel\MCXX\ where XX is first 2 chars
            '' and g:\Full Color Sheets\McDaniel\XX
            paths.Add(COPYBASEDRIVEPATH + MCDBASEPATH + "\" + MCDBWPATHDIFFERENCE + input.Substring(0, 2) + "\")
            paths.Add(COPYBASEDRIVEPATH + FCBASEBATH + "\" + MCDBASEPATH + "\" + input.Substring(0, 2) + "\")

        End If

        Return paths

    End Function

    Private Function CBisChurchBudget(ByVal input As String) As Boolean
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

    Private Function CBisUnited(ByVal input As String) As Boolean
        Dim firstChar As Char = input.Substring(0, 1)

        '' if the first character is the number 6, this is United
        If (firstChar = "6") Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Function CBgetSearchString(ByVal input As String) As String

        '' if Church Budget, remove the letter (first character) and search by the number
        '' if United or McDaniel, remove the first two numbers and search by the rest
        If (CBisChurchBudget(input)) Then
            '' return the string minus the first character
            Return input.Substring(1)
        Else
            '' return the string minus the first two characters
            Return input.Substring(2)
        End If

    End Function

    '' MM ONLY

    Private Function MMgetTopLevelPaths(ByVal input As String) As List(Of String)
        Dim paths As New List(Of String)

        '' for MM the files are located at h:\MM#\Mm#-#
        '' where ## are the first two numbers of the input
        paths.Add(MMBASEDRIVEPATH + MMBASEPATH + input.Substring(0, 1) + "\" + MMSUBPATH + input.Substring(0, 1) + "-" + input.Substring(1, 1) + "\")

        Return paths

    End Function

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

    Private Sub getFileList(ByVal search As String)
        '' debug
        lstboxPathList.Items.Clear()

        '' loop through list of paths
        For Each path In pathList

            '' if path exists, get a list of files from search and add it to the global fileList List(Of IO.FileInfo)
            If (Directory.Exists(path)) Then
                Dim folderInfo As New IO.DirectoryInfo(path)

                '' old way, getting the api to run the wilcard match

                '' Dim arrFilesInFolder() As IO.FileInfo
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
                lstboxPathList.Items.Add("e00 " + path)

            End If

        Next

    End Sub

    Private Sub OptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptionsToolStripMenuItem.Click
        'MessageBox.Show("clicked")
        'SettingsForm.Show()
        Dim stngFrm As New Settings()
        stngFrm.Show()

    End Sub
End Class
