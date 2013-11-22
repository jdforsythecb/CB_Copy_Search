<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Settings
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
        Me.lblSettings1 = New System.Windows.Forms.Label()
        Me.btnSaveSettings = New System.Windows.Forms.Button()
        Me.btnCancelSettings = New System.Windows.Forms.Button()
        Me.txtExtraPaths = New System.Windows.Forms.TextBox()
        Me.rdioMM = New System.Windows.Forms.RadioButton()
        Me.rdioCB = New System.Windows.Forms.RadioButton()
        Me.SuspendLayout()
        '
        'lblSettings1
        '
        Me.lblSettings1.AutoSize = True
        Me.lblSettings1.Location = New System.Drawing.Point(30, 76)
        Me.lblSettings1.Name = "lblSettings1"
        Me.lblSettings1.Size = New System.Drawing.Size(562, 13)
        Me.lblSettings1.TabIndex = 2
        Me.lblSettings1.Text = "Enter any extra top-level directories you wish to always search, in addition to t" & _
    "he typical search directories, one per line"
        '
        'btnSaveSettings
        '
        Me.btnSaveSettings.Location = New System.Drawing.Point(33, 379)
        Me.btnSaveSettings.Name = "btnSaveSettings"
        Me.btnSaveSettings.Size = New System.Drawing.Size(101, 23)
        Me.btnSaveSettings.TabIndex = 4
        Me.btnSaveSettings.Text = "Save Settings"
        Me.btnSaveSettings.UseVisualStyleBackColor = True
        '
        'btnCancelSettings
        '
        Me.btnCancelSettings.Location = New System.Drawing.Point(491, 379)
        Me.btnCancelSettings.Name = "btnCancelSettings"
        Me.btnCancelSettings.Size = New System.Drawing.Size(101, 23)
        Me.btnCancelSettings.TabIndex = 5
        Me.btnCancelSettings.Text = "Cancel"
        Me.btnCancelSettings.UseVisualStyleBackColor = True
        '
        'txtExtraPaths
        '
        Me.txtExtraPaths.Location = New System.Drawing.Point(30, 111)
        Me.txtExtraPaths.Multiline = True
        Me.txtExtraPaths.Name = "txtExtraPaths"
        Me.txtExtraPaths.Size = New System.Drawing.Size(562, 246)
        Me.txtExtraPaths.TabIndex = 3
        '
        'rdioMM
        '
        Me.rdioMM.AutoSize = True
        Me.rdioMM.Location = New System.Drawing.Point(30, 37)
        Me.rdioMM.Name = "rdioMM"
        Me.rdioMM.Size = New System.Drawing.Size(84, 17)
        Me.rdioMM.TabIndex = 1
        Me.rdioMM.TabStop = True
        Me.rdioMM.Text = "Monthly Mail"
        Me.rdioMM.UseVisualStyleBackColor = True
        '
        'rdioCB
        '
        Me.rdioCB.AutoSize = True
        Me.rdioCB.Location = New System.Drawing.Point(30, 13)
        Me.rdioCB.Name = "rdioCB"
        Me.rdioCB.Size = New System.Drawing.Size(96, 17)
        Me.rdioCB.TabIndex = 0
        Me.rdioCB.TabStop = True
        Me.rdioCB.Text = "Church Budget"
        Me.rdioCB.UseVisualStyleBackColor = True
        '
        'Settings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(636, 432)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnCancelSettings)
        Me.Controls.Add(Me.btnSaveSettings)
        Me.Controls.Add(Me.txtExtraPaths)
        Me.Controls.Add(Me.lblSettings1)
        Me.Controls.Add(Me.rdioMM)
        Me.Controls.Add(Me.rdioCB)
        Me.Name = "Settings"
        Me.Text = "Settings"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdioCB As System.Windows.Forms.RadioButton
    Friend WithEvents rdioMM As System.Windows.Forms.RadioButton
    Friend WithEvents lblSettings1 As System.Windows.Forms.Label
    Friend WithEvents txtExtraPaths As System.Windows.Forms.TextBox
    Friend WithEvents btnSaveSettings As System.Windows.Forms.Button
    Friend WithEvents btnCancelSettings As System.Windows.Forms.Button
End Class
