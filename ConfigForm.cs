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
        private Plugin parent;
        private string[] playlists;
        private string[] selectedGenres;
        public ConfigForm(string[] playlists, string[] genres, string[] selectedPlaylists, string[] selectedGenres, Plugin parent)
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

            this.playlists = playlists; this.selectedGenres = selectedGenres;
            // give all playlists and genres
            // apply selected playlists and genres
            for (int i = 0; i < 10;  i++)
            {
                playlistBoxes[i].Items.AddRange(playlists);
                playlistBoxes[i].SelectedIndex = numInPlaylists(selectedPlaylists[i]);
                playlistBoxes[i].SelectedIndexChanged += new EventHandler((sender, e) => 
                                                          PlaylistBox_SelectedIndexChanged(sender, e, i));

                genreBoxes[i].Text = selectedGenres[i];
                //genreBoxes[i].SelectedIndex = numInGenres(selectedGenres[i]);
                genreBoxes[i].TextChanged += new EventHandler((sender, e) =>
                                              GenreBox_TextChanged(sender, e, i));

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
        private void GenreBox_TextChanged(object sender, EventArgs e, int num)
        {
            // update config in main when button is pressed
            
            //parent.SetSingleGenre(num, genreBoxes[num].Text);
            //selectedGenres[num] = genreBoxes[num].Text;
            
        }

        private void UpdateSelectedGenres()
        {
            // sets array from text boxes
            for (int i = 0; i < 10; i++)
            {
                selectedGenres[i] = genreBoxes[i].Text;
            }
        }

        private void genreUpdateButton_Click(object sender, EventArgs e)
        {
            // update all genres
            UpdateSelectedGenres();
            parent.SetCurrentGenres(selectedGenres);
        }
    }
}
