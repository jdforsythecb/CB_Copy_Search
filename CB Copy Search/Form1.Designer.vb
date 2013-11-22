<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lstbxResults = New System.Windows.Forms.ListBox()
        Me.lstboxPathList = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(13, 13)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(100, 20)
        Me.txtSearch.TabIndex = 0
        '
        'lstbxResults
        '
        Me.lstbxResults.FormattingEnabled = True
        Me.lstbxResults.Location = New System.Drawing.Point(16, 56)
        Me.lstbxResults.Name = "lstbxResults"
        Me.lstbxResults.Size = New System.Drawing.Size(186, 368)
        Me.lstbxResults.TabIndex = 1
        '
        'lstboxPathList
        '
        Me.lstboxPathList.FormattingEnabled = True
        Me.lstboxPathList.Location = New System.Drawing.Point(222, 56)
        Me.lstboxPathList.Name = "lstboxPathList"
        Me.lstboxPathList.Size = New System.Drawing.Size(274, 368)
        Me.lstboxPathList.TabIndex = 2
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(505, 438)
        Me.Controls.Add(Me.lstboxPathList)
        Me.Controls.Add(Me.lstbxResults)
        Me.Controls.Add(Me.txtSearch)
        Me.Name = "Form1"
        Me.Text = "CB Copy Search"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lstbxResults As System.Windows.Forms.ListBox
    Friend WithEvents lstboxPathList As System.Windows.Forms.ListBox

End Class
