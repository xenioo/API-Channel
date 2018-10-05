namespace ChatInspector {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing ) {
			if( disposing && (components != null) ) {
				components.Dispose( );
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent( ) {
			this._mainContainer = new System.Windows.Forms.SplitContainer();
			this._chatText = new System.Windows.Forms.TextBox();
			this._chatLog = new System.Windows.Forms.TextBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this._chatState = new System.Windows.Forms.PropertyGrid();
			this._configData = new System.Windows.Forms.PropertyGrid();
			((System.ComponentModel.ISupportInitialize)(this._mainContainer)).BeginInit();
			this._mainContainer.Panel1.SuspendLayout();
			this._mainContainer.Panel2.SuspendLayout();
			this._mainContainer.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// _mainContainer
			// 
			this._mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this._mainContainer.Location = new System.Drawing.Point(0, 0);
			this._mainContainer.Name = "_mainContainer";
			// 
			// _mainContainer.Panel1
			// 
			this._mainContainer.Panel1.Controls.Add(this._chatLog);
			this._mainContainer.Panel1.Controls.Add(this._chatText);
			// 
			// _mainContainer.Panel2
			// 
			this._mainContainer.Panel2.Controls.Add(this.tabControl1);
			this._mainContainer.Size = new System.Drawing.Size(1033, 451);
			this._mainContainer.SplitterDistance = 716;
			this._mainContainer.TabIndex = 2;
			// 
			// _chatText
			// 
			this._chatText.Dock = System.Windows.Forms.DockStyle.Bottom;
			this._chatText.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._chatText.Location = new System.Drawing.Point(0, 430);
			this._chatText.Name = "_chatText";
			this._chatText.Size = new System.Drawing.Size(716, 21);
			this._chatText.TabIndex = 4;
			this._chatText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnChatTextKeyDown);
			// 
			// _chatLog
			// 
			this._chatLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this._chatLog.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._chatLog.Location = new System.Drawing.Point(0, 0);
			this._chatLog.Multiline = true;
			this._chatLog.Name = "_chatLog";
			this._chatLog.ReadOnly = true;
			this._chatLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this._chatLog.ShortcutsEnabled = false;
			this._chatLog.Size = new System.Drawing.Size(716, 430);
			this._chatLog.TabIndex = 5;
			this._chatLog.WordWrap = false;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(313, 451);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this._chatState);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(305, 425);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "State";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this._configData);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(305, 425);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Configuration";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// _chatState
			// 
			this._chatState.Dock = System.Windows.Forms.DockStyle.Fill;
			this._chatState.Location = new System.Drawing.Point(3, 3);
			this._chatState.Name = "_chatState";
			this._chatState.Size = new System.Drawing.Size(299, 419);
			this._chatState.TabIndex = 1;
			// 
			// _configData
			// 
			this._configData.Dock = System.Windows.Forms.DockStyle.Fill;
			this._configData.Location = new System.Drawing.Point(3, 3);
			this._configData.Name = "_configData";
			this._configData.Size = new System.Drawing.Size(299, 419);
			this._configData.TabIndex = 2;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1033, 451);
			this.Controls.Add(this._mainContainer);
			this.Name = "MainForm";
			this.Text = "Chat Inspector";
			this.Shown += new System.EventHandler(this.OnFormShown);
			this._mainContainer.Panel1.ResumeLayout(false);
			this._mainContainer.Panel1.PerformLayout();
			this._mainContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._mainContainer)).EndInit();
			this._mainContainer.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.SplitContainer _mainContainer;
		private System.Windows.Forms.TextBox _chatLog;
		private System.Windows.Forms.TextBox _chatText;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.PropertyGrid _chatState;
		private System.Windows.Forms.PropertyGrid _configData;
	}
}

