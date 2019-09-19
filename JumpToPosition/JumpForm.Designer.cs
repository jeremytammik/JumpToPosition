namespace JumpToPosition
{
  partial class JumpForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
    {
      if( disposing && (components != null) )
      {
        components.Dispose();
      }
      base.Dispose( disposing );
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.textBoxEye = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.textBoxViewdir = new System.Windows.Forms.TextBox();
      this.buttonJump = new System.Windows.Forms.Button();
      this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
      this.SuspendLayout();
      // 
      // textBoxEye
      // 
      this.textBoxEye.Location = new System.Drawing.Point(104, 12);
      this.textBoxEye.Name = "textBoxEye";
      this.textBoxEye.Size = new System.Drawing.Size(100, 20);
      this.textBoxEye.TabIndex = 1;
      this.textBoxEye.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxEye_Validating);
      this.textBoxEye.Validated += new System.EventHandler(this.textBoxEye_Validated);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(23, 15);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(64, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Eye position";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(23, 45);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(73, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "View direction";
      // 
      // textBoxViewdir
      // 
      this.textBoxViewdir.Location = new System.Drawing.Point(104, 42);
      this.textBoxViewdir.Name = "textBoxViewdir";
      this.textBoxViewdir.Size = new System.Drawing.Size(100, 20);
      this.textBoxViewdir.TabIndex = 3;
      this.textBoxViewdir.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxViewdir_Validating);
      this.textBoxViewdir.Validated += new System.EventHandler(this.textBoxViewdir_Validated);
      // 
      // buttonJump
      // 
      this.buttonJump.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.buttonJump.Location = new System.Drawing.Point(78, 87);
      this.buttonJump.Name = "buttonJump";
      this.buttonJump.Size = new System.Drawing.Size(75, 30);
      this.buttonJump.TabIndex = 4;
      this.buttonJump.Text = "Jump";
      this.buttonJump.UseVisualStyleBackColor = true;
      // 
      // errorProvider1
      // 
      this.errorProvider1.ContainerControl = this;
      // 
      // JumpForm
      // 
      this.AcceptButton = this.buttonJump;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(223, 134);
      this.Controls.Add(this.buttonJump);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.textBoxViewdir);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.textBoxEye);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "JumpForm";
      this.Text = "Jump to Position";
      ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox textBoxEye;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox textBoxViewdir;
    private System.Windows.Forms.Button buttonJump;
    private System.Windows.Forms.ErrorProvider errorProvider1;
  }
}