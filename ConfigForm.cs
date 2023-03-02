﻿using System;
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
        private ComboBox[] genreBoxes;
        public ConfigForm(string[] playlists, string[] genres, int[] selectedPlaylists, int[] selectedGenres)
        {
            InitializeComponent();
            playlistBoxes = new ComboBox[]
            {
                playlistBox1, playlistBox2, playlistBox3,
                playlistBox4, playlistBox5, playlistBox6,
                playlistBox7, playlistBox8, playlistBox9,
                playlistBox10
            };

            genreBoxes = new ComboBox[]
            {
                genreBox1, genreBox2, genreBox3,
                genreBox4, genreBox5, genreBox6,
                genreBox7, genreBox8, genreBox9,
                genreBox10
            };

            // give all playlists and genres
            // apply selected playlists and genres
            for (int i = 0; i < 10;  i++)
            {
                playlistBoxes[i].Items.AddRange(playlists);
                playlistBoxes[i].SelectedIndex = selectedPlaylists[i];
                playlistBoxes[i].SelectedIndexChanged += new EventHandler((sender, e) => 
                                                          PlaylistBox_SelectedIndexChanged(sender, e, i));

                genreBoxes[i].Items.AddRange(genres);
                genreBoxes[i].SelectedIndex = selectedGenres[i];

                playlistBoxes[i].SelectedIndexChanged += new EventHandler((sender, e) =>
                                                          GenreBox_SelectedIndexChanged(sender, e, i));

            }


        }

        private void PlaylistBox_SelectedIndexChanged(object sender, EventArgs e, int num)
        {
            // update config in main
        }
        private void GenreBox_SelectedIndexChanged(object sender, EventArgs e, int num)
        {
            // update config in main
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
