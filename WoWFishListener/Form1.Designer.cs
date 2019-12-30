namespace WoWHealthMonitor
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.gbProcess = new System.Windows.Forms.GroupBox();
			this.lstProcessList = new System.Windows.Forms.ListBox();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.btnSelect = new System.Windows.Forms.Button();
			this.btnStart = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.cbUseWand = new System.Windows.Forms.CheckBox();
			this.cbAutoAttack = new System.Windows.Forms.CheckBox();
			this.cbDrinking = new System.Windows.Forms.CheckBox();
			this.cbFollowing = new System.Windows.Forms.CheckBox();
			this.cbCombat = new System.Windows.Forms.CheckBox();
			this.cbCasting = new System.Windows.Forms.CheckBox();
			this.lvAbilities = new System.Windows.Forms.ListView();
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lvPlayers = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.gbProcess.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbProcess
			// 
			this.gbProcess.Controls.Add(this.lstProcessList);
			this.gbProcess.Controls.Add(this.btnRefresh);
			this.gbProcess.Controls.Add(this.btnSelect);
			this.gbProcess.Location = new System.Drawing.Point(6, 6);
			this.gbProcess.Name = "gbProcess";
			this.gbProcess.Size = new System.Drawing.Size(209, 239);
			this.gbProcess.TabIndex = 9;
			this.gbProcess.TabStop = false;
			// 
			// lstProcessList
			// 
			this.lstProcessList.FormattingEnabled = true;
			this.lstProcessList.Location = new System.Drawing.Point(16, 19);
			this.lstProcessList.Name = "lstProcessList";
			this.lstProcessList.Size = new System.Drawing.Size(177, 173);
			this.lstProcessList.TabIndex = 0;
			// 
			// btnRefresh
			// 
			this.btnRefresh.Location = new System.Drawing.Point(16, 202);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(75, 23);
			this.btnRefresh.TabIndex = 2;
			this.btnRefresh.Text = "Refresh";
			this.btnRefresh.UseVisualStyleBackColor = true;
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// btnSelect
			// 
			this.btnSelect.Location = new System.Drawing.Point(118, 202);
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.Size = new System.Drawing.Size(75, 23);
			this.btnSelect.TabIndex = 3;
			this.btnSelect.Text = "Select";
			this.btnSelect.UseVisualStyleBackColor = true;
			this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(228, 458);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 23);
			this.btnStart.TabIndex = 8;
			this.btnStart.Text = "Start";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(367, 397);
			this.tabControl1.TabIndex = 10;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.gbProcess);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(359, 371);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "WoW Process";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.cbUseWand);
			this.tabPage2.Controls.Add(this.cbAutoAttack);
			this.tabPage2.Controls.Add(this.cbDrinking);
			this.tabPage2.Controls.Add(this.cbFollowing);
			this.tabPage2.Controls.Add(this.cbCombat);
			this.tabPage2.Controls.Add(this.cbCasting);
			this.tabPage2.Controls.Add(this.lvAbilities);
			this.tabPage2.Controls.Add(this.lvPlayers);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(359, 371);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Status";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// cbUseWand
			// 
			this.cbUseWand.AutoSize = true;
			this.cbUseWand.Location = new System.Drawing.Point(226, 348);
			this.cbUseWand.Name = "cbUseWand";
			this.cbUseWand.Size = new System.Drawing.Size(77, 17);
			this.cbUseWand.TabIndex = 1;
			this.cbUseWand.Text = "Use Wand";
			this.cbUseWand.UseVisualStyleBackColor = true;
			this.cbUseWand.CheckedChanged += new System.EventHandler(this.cbUseWand_CheckedChanged);
			// 
			// cbAutoAttack
			// 
			this.cbAutoAttack.AutoSize = true;
			this.cbAutoAttack.Enabled = false;
			this.cbAutoAttack.Location = new System.Drawing.Point(226, 235);
			this.cbAutoAttack.Name = "cbAutoAttack";
			this.cbAutoAttack.Size = new System.Drawing.Size(79, 17);
			this.cbAutoAttack.TabIndex = 1;
			this.cbAutoAttack.Text = "AutoAttack";
			this.cbAutoAttack.UseVisualStyleBackColor = true;
			// 
			// cbDrinking
			// 
			this.cbDrinking.AutoSize = true;
			this.cbDrinking.Enabled = false;
			this.cbDrinking.Location = new System.Drawing.Point(226, 212);
			this.cbDrinking.Name = "cbDrinking";
			this.cbDrinking.Size = new System.Drawing.Size(65, 17);
			this.cbDrinking.TabIndex = 1;
			this.cbDrinking.Text = "Drinking";
			this.cbDrinking.UseVisualStyleBackColor = true;
			// 
			// cbFollowing
			// 
			this.cbFollowing.AutoSize = true;
			this.cbFollowing.Enabled = false;
			this.cbFollowing.Location = new System.Drawing.Point(226, 189);
			this.cbFollowing.Name = "cbFollowing";
			this.cbFollowing.Size = new System.Drawing.Size(70, 17);
			this.cbFollowing.TabIndex = 1;
			this.cbFollowing.Text = "Following";
			this.cbFollowing.UseVisualStyleBackColor = true;
			// 
			// cbCombat
			// 
			this.cbCombat.AutoSize = true;
			this.cbCombat.Enabled = false;
			this.cbCombat.Location = new System.Drawing.Point(226, 166);
			this.cbCombat.Name = "cbCombat";
			this.cbCombat.Size = new System.Drawing.Size(62, 17);
			this.cbCombat.TabIndex = 1;
			this.cbCombat.Text = "Combat";
			this.cbCombat.UseVisualStyleBackColor = true;
			// 
			// cbCasting
			// 
			this.cbCasting.AutoSize = true;
			this.cbCasting.Enabled = false;
			this.cbCasting.Location = new System.Drawing.Point(226, 143);
			this.cbCasting.Name = "cbCasting";
			this.cbCasting.Size = new System.Drawing.Size(61, 17);
			this.cbCasting.TabIndex = 1;
			this.cbCasting.Text = "Casting";
			this.cbCasting.UseVisualStyleBackColor = true;
			// 
			// lvAbilities
			// 
			this.lvAbilities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
			this.lvAbilities.HideSelection = false;
			this.lvAbilities.Location = new System.Drawing.Point(3, 143);
			this.lvAbilities.Name = "lvAbilities";
			this.lvAbilities.Size = new System.Drawing.Size(212, 118);
			this.lvAbilities.TabIndex = 0;
			this.lvAbilities.UseCompatibleStateImageBehavior = false;
			this.lvAbilities.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Slot";
			this.columnHeader4.Width = 42;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "inRange";
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "onCooldown";
			this.columnHeader6.Width = 78;
			// 
			// lvPlayers
			// 
			this.lvPlayers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader7,
            this.columnHeader8});
			this.lvPlayers.HideSelection = false;
			this.lvPlayers.Location = new System.Drawing.Point(3, 6);
			this.lvPlayers.Name = "lvPlayers";
			this.lvPlayers.Size = new System.Drawing.Size(350, 118);
			this.lvPlayers.TabIndex = 0;
			this.lvPlayers.UseCompatibleStateImageBehavior = false;
			this.lvPlayers.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Member";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "HpCurrent";
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "HpMax";
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Mana";
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "ManaMax";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(396, 543);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.btnStart);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.gbProcess.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox gbProcess;
		private System.Windows.Forms.ListBox lstProcessList;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Button btnSelect;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.ListView lvPlayers;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.CheckBox cbDrinking;
		private System.Windows.Forms.CheckBox cbFollowing;
		private System.Windows.Forms.CheckBox cbCombat;
		private System.Windows.Forms.CheckBox cbCasting;
		private System.Windows.Forms.ListView lvAbilities;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.CheckBox cbAutoAttack;
		private System.Windows.Forms.CheckBox cbUseWand;
	}
}

