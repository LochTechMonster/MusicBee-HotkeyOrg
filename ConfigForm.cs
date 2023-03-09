using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicBeePlugin
{
    public partial class ConfigForm : Form
    {
        private ComboBox[] playlistBoxes;
        private TextBox[] genreBoxes;
        private TextBox[] tagBoxes;

        private Plugin parent;
        private string[] playlists;
        private string[] selectedGenres;
        private string[] selectedTags;
        public ConfigForm(string[] playlists, string[] playlistNames, string[] selectedTags, string[] selectedPlaylists, string[] selectedGenres, Plugin parent)
        {
            InitializeComponent();
            playlistBoxes = new ComboBox[]
            {
                playlistBox1, playlistBox2, playlistBox3,
                playlistBox4, playlistBox5, playlistBox6,
                playlistBox7, playlistBox8, playlistBox9,
                playlistBox10
            };

            genreBoxes = new TextBox[]
            {
                genreBox1, genreBox2, genreBox3,
                genreBox4, genreBox5, genreBox6,
                genreBox7, genreBox8, genreBox9,
                genreBox10
            };

            tagBoxes = new TextBox[] 
            { 
                tagBox1, tagBox2, tagBox3, 
                tagBox4, tagBox5, tagBox6, 
                tagBox7, tagBox8, tagBox9, 
                tagBox10 
            };

            this.playlists = playlists;
            this.selectedGenres = selectedGenres;
            this.selectedTags = selectedTags;

            // give all playlists and genres
            // apply selected playlists and genres
            for (int i = 0; i < 10;  i++)
            {
                // TODO: Sort out playlist names and playlist file
                int a = i;
                playlistBoxes[i].Items.AddRange(playlistNames);
                playlistBoxes[i].SelectedIndex = numInPlaylists(selectedPlaylists[i]);
                playlistBoxes[i].SelectedIndexChanged += new EventHandler((sender, e) => 
                                                          PlaylistBox_SelectedIndexChanged(sender, e, a));

                genreBoxes[i].Text = selectedGenres[i];
                //genreBoxes[i].SelectedIndex = numInGenres(selectedGenres[i]);
                //genreBoxes[i].TextChanged += new EventHandler((sender, e) =>
                //                              GenreBox_TextChanged(sender, e, i));

                tagBoxes[i].Text = selectedTags[i];

            }
            this.parent = parent;

        }

        private int numInList(string s, string[] strings)
        {
            return Array.IndexOf(strings, s);
        }

        private int numInPlaylists(string s) { return numInList(s, playlists);}

        //private int numInGenres(string s) { return numInList(s, genres);}

        private void PlaylistBox_SelectedIndexChanged(object sender, EventArgs e, int num)
        {
            // update config in main
            parent.SetSinglePlaylist(num, playlistBoxes[num].SelectedIndex);
        }
        //private void GenreBox_TextChanged(object sender, EventArgs e, int num)
        //{
        //    // update config in main when button is pressed

        //    parent.SetSingleGenre(num, genreBoxes[num].Text);
        //    selectedGenres[num] = genreBoxes[num].Text;

        //}

        private void UpdateSelectedGenres()
        {
            // sets array from text boxes
            for (int i = 0; i < 10; i++)
            {
                selectedGenres[i] = genreBoxes[i].Text;
            }
        }

        private void UpdateSelectedTags()
        {
            // sets array from text boxes
            for (int i = 0; i < 10; i++)
            {
                selectedTags[i] = tagBoxes[i].Text;
            }
        }

        private void genreUpdateButton_Click(object sender, EventArgs e)
        {
            // update all genres
            UpdateSelectedGenres();
            parent.SetCurrentGenres(selectedGenres);
        }

        private void tagUpdateButton_Click(object sender, EventArgs e)
        {
            UpdateSelectedTags();
            parent.SetCurrentTags(selectedTags);
        }
    }
}
