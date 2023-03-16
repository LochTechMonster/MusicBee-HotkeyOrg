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
        public ConfigForm(string[] playlists, string[] playlistNames, string[] selectedPlaylists, 
                          string[] selectedGenres, Plugin parent)
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

            this.playlists = playlists;

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
            }
            this.parent = parent;

        }

        private int numInList(string s, string[] strings)
        {
            return Array.IndexOf(strings, s);
        }

        private int numInPlaylists(string s) { return numInList(s, playlists);}

        private void PlaylistBox_SelectedIndexChanged(object sender, EventArgs e, int num)
        {
            // update config in main
            parent.SetSinglePlaylist(num, playlistBoxes[num].SelectedIndex);
        }

        private void updateGenreButton_Click(object sender, EventArgs e)
        {
            // update config in main
            List<string> genreNames = new List<string>();
            for (int i = 0; i < genreBoxes.Length; i++)
            {
                genreNames.Add(genreBoxes[i].Text.Trim());
            }

            parent.SetSelectedGenres(genreNames.ToArray());
        }
    }
}
