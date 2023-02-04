namespace MusicBeePlugin
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
            this.Button1 = new System.Windows.Forms.Button();
            this.stateLabel = new System.Windows.Forms.Label();
            this.songTitleLabel1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkedPlayListBox = new System.Windows.Forms.CheckedListBox();
            this.queryPlaylistLabel = new System.Windows.Forms.Label();
            this.playlistBox = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.loveLabel = new System.Windows.Forms.Label();
            this.noRatingLabel = new System.Windows.Forms.Label();
            this.loveCheckBox = new System.Windows.Forms.CheckBox();
            this.noRatingCheckBox = new System.Windows.Forms.CheckBox();
            this.ratingLabel = new System.Windows.Forms.Label();
            this.songTitleLabel3 = new System.Windows.Forms.Label();
            this.ratingBar1 = new System.Windows.Forms.TrackBar();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ratingBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // Button1
            // 
            this.Button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button1.Location = new System.Drawing.Point(189, 213);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(182, 108);
            this.Button1.TabIndex = 0;
            this.Button1.Text = "State";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // stateLabel
            // 
            this.stateLabel.AutoSize = true;
            this.stateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F);
            this.stateLabel.Location = new System.Drawing.Point(29, 25);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(153, 63);
            this.stateLabel.TabIndex = 1;
            this.stateLabel.Text = "State";
            this.stateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // songTitleLabel1
            // 
            this.songTitleLabel1.AutoSize = true;
            this.songTitleLabel1.Location = new System.Drawing.Point(98, 134);
            this.songTitleLabel1.Name = "songTitleLabel1";
            this.songTitleLabel1.Size = new System.Drawing.Size(35, 13);
            this.songTitleLabel1.TabIndex = 2;
            this.songTitleLabel1.Text = "label2";
            this.songTitleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(556, 399);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.stateLabel);
            this.tabPage1.Controls.Add(this.Button1);
            this.tabPage1.Controls.Add(this.songTitleLabel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(548, 373);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.checkedPlayListBox);
            this.tabPage2.Controls.Add(this.queryPlaylistLabel);
            this.tabPage2.Controls.Add(this.playlistBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(548, 373);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Playlists";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkedPlayListBox
            // 
            this.checkedPlayListBox.FormattingEnabled = true;
            this.checkedPlayListBox.Location = new System.Drawing.Point(37, 122);
            this.checkedPlayListBox.Name = "checkedPlayListBox";
            this.checkedPlayListBox.Size = new System.Drawing.Size(120, 94);
            this.checkedPlayListBox.TabIndex = 2;
            this.checkedPlayListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            // 
            // queryPlaylistLabel
            // 
            this.queryPlaylistLabel.AutoSize = true;
            this.queryPlaylistLabel.Location = new System.Drawing.Point(56, 45);
            this.queryPlaylistLabel.Name = "queryPlaylistLabel";
            this.queryPlaylistLabel.Size = new System.Drawing.Size(35, 13);
            this.queryPlaylistLabel.TabIndex = 1;
            this.queryPlaylistLabel.Text = "label1";
            // 
            // playlistBox
            // 
            this.playlistBox.FormattingEnabled = true;
            this.playlistBox.Location = new System.Drawing.Point(218, 90);
            this.playlistBox.Name = "playlistBox";
            this.playlistBox.Size = new System.Drawing.Size(324, 277);
            this.playlistBox.TabIndex = 0;
            this.playlistBox.SelectedIndexChanged += new System.EventHandler(this.playlistBox_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.loveLabel);
            this.tabPage3.Controls.Add(this.noRatingLabel);
            this.tabPage3.Controls.Add(this.loveCheckBox);
            this.tabPage3.Controls.Add(this.noRatingCheckBox);
            this.tabPage3.Controls.Add(this.ratingLabel);
            this.tabPage3.Controls.Add(this.songTitleLabel3);
            this.tabPage3.Controls.Add(this.ratingBar1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(548, 373);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Ratings";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // loveLabel
            // 
            this.loveLabel.AutoSize = true;
            this.loveLabel.Location = new System.Drawing.Point(438, 166);
            this.loveLabel.Name = "loveLabel";
            this.loveLabel.Size = new System.Drawing.Size(60, 13);
            this.loveLabel.TabIndex = 6;
            this.loveLabel.Text = "Love Label";
            // 
            // noRatingLabel
            // 
            this.noRatingLabel.AutoSize = true;
            this.noRatingLabel.Location = new System.Drawing.Point(28, 166);
            this.noRatingLabel.Name = "noRatingLabel";
            this.noRatingLabel.Size = new System.Drawing.Size(84, 13);
            this.noRatingLabel.TabIndex = 5;
            this.noRatingLabel.Text = "No Rating Label";
            // 
            // loveCheckBox
            // 
            this.loveCheckBox.AutoSize = true;
            this.loveCheckBox.Location = new System.Drawing.Point(438, 220);
            this.loveCheckBox.Name = "loveCheckBox";
            this.loveCheckBox.Size = new System.Drawing.Size(78, 17);
            this.loveCheckBox.TabIndex = 4;
            this.loveCheckBox.Text = "Love Song";
            this.loveCheckBox.UseVisualStyleBackColor = true;
            this.loveCheckBox.CheckedChanged += new System.EventHandler(this.loveCheckBox_CheckedChanged);
            // 
            // noRatingCheckBox
            // 
            this.noRatingCheckBox.AutoSize = true;
            this.noRatingCheckBox.Location = new System.Drawing.Point(14, 220);
            this.noRatingCheckBox.Name = "noRatingCheckBox";
            this.noRatingCheckBox.Size = new System.Drawing.Size(74, 17);
            this.noRatingCheckBox.TabIndex = 3;
            this.noRatingCheckBox.Text = "No Rating";
            this.noRatingCheckBox.UseVisualStyleBackColor = true;
            this.noRatingCheckBox.CheckedChanged += new System.EventHandler(this.noRatingCheckBox_CheckedChanged);
            // 
            // ratingLabel
            // 
            this.ratingLabel.AutoSize = true;
            this.ratingLabel.Location = new System.Drawing.Point(87, 295);
            this.ratingLabel.Name = "ratingLabel";
            this.ratingLabel.Size = new System.Drawing.Size(67, 13);
            this.ratingLabel.TabIndex = 2;
            this.ratingLabel.Text = "Rating Label";
            // 
            // songTitleLabel3
            // 
            this.songTitleLabel3.AutoSize = true;
            this.songTitleLabel3.Location = new System.Drawing.Point(125, 71);
            this.songTitleLabel3.Name = "songTitleLabel3";
            this.songTitleLabel3.Size = new System.Drawing.Size(61, 13);
            this.songTitleLabel3.TabIndex = 1;
            this.songTitleLabel3.Text = "Song Title3";
            // 
            // ratingBar1
            // 
            this.ratingBar1.Location = new System.Drawing.Point(111, 207);
            this.ratingBar1.Name = "ratingBar1";
            this.ratingBar1.Size = new System.Drawing.Size(306, 45);
            this.ratingBar1.TabIndex = 0;
            this.ratingBar1.Scroll += new System.EventHandler(this.ratingBar1_Scroll);
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(548, 373);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Genres";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(548, 373);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Tags";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 423);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ratingBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Button1;
        private System.Windows.Forms.Label stateLabel;
        private System.Windows.Forms.Label songTitleLabel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox playlistBox;
        private System.Windows.Forms.CheckedListBox checkedPlayListBox;
        private System.Windows.Forms.Label queryPlaylistLabel;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TrackBar ratingBar1;
        private System.Windows.Forms.Label songTitleLabel3;
        private System.Windows.Forms.Label ratingLabel;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.CheckBox loveCheckBox;
        private System.Windows.Forms.CheckBox noRatingCheckBox;
        private System.Windows.Forms.Label loveLabel;
        private System.Windows.Forms.Label noRatingLabel;
    }
}