namespace MethodInvoker {
   partial class Form1 {
      /// <summary>
      /// 필수 디자이너 변수입니다.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// 사용 중인 모든 리소스를 정리합니다.
      /// </summary>
      /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
      protected override void Dispose(bool disposing) {
         if (disposing && (components != null)) {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form 디자이너에서 생성한 코드

      /// <summary>
      /// 디자이너 지원에 필요한 메서드입니다. 
      /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
      /// </summary>
      private void InitializeComponent() {
         this.grdParameter = new System.Windows.Forms.PropertyGrid();
         this.cbxFunction = new System.Windows.Forms.ComboBox();
         this.btnRun = new System.Windows.Forms.Button();
         this.lbxLog = new System.Windows.Forms.ListBox();
         this.SuspendLayout();
         // 
         // grdParameter
         // 
         this.grdParameter.Location = new System.Drawing.Point(12, 38);
         this.grdParameter.Name = "grdParameter";
         this.grdParameter.Size = new System.Drawing.Size(311, 232);
         this.grdParameter.TabIndex = 0;
         // 
         // cbxFunction
         // 
         this.cbxFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cbxFunction.FormattingEnabled = true;
         this.cbxFunction.Location = new System.Drawing.Point(12, 12);
         this.cbxFunction.Name = "cbxFunction";
         this.cbxFunction.Size = new System.Drawing.Size(311, 20);
         this.cbxFunction.TabIndex = 1;
         this.cbxFunction.SelectedIndexChanged += new System.EventHandler(this.cbxFunction_SelectedIndexChanged);
         // 
         // btnRun
         // 
         this.btnRun.Location = new System.Drawing.Point(12, 276);
         this.btnRun.Name = "btnRun";
         this.btnRun.Size = new System.Drawing.Size(75, 23);
         this.btnRun.TabIndex = 2;
         this.btnRun.Text = "Run";
         this.btnRun.UseVisualStyleBackColor = true;
         this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
         // 
         // lbxLog
         // 
         this.lbxLog.FormattingEnabled = true;
         this.lbxLog.ItemHeight = 12;
         this.lbxLog.Location = new System.Drawing.Point(329, 12);
         this.lbxLog.Name = "lbxLog";
         this.lbxLog.Size = new System.Drawing.Size(372, 256);
         this.lbxLog.TabIndex = 3;
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(713, 318);
         this.Controls.Add(this.lbxLog);
         this.Controls.Add(this.btnRun);
         this.Controls.Add(this.cbxFunction);
         this.Controls.Add(this.grdParameter);
         this.Name = "Form1";
         this.Text = "Form1";
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.PropertyGrid grdParameter;
      private System.Windows.Forms.ComboBox cbxFunction;
      private System.Windows.Forms.Button btnRun;
      private System.Windows.Forms.ListBox lbxLog;
   }
}

