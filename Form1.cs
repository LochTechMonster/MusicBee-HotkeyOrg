using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicBeePlugin
{
    public partial class Form1 : Form
    {
        //private Plugin.MusicBeeApiInterface mbApi;
        public Form1(Plugin.MusicBeeApiInterface mbApiInterface)
        {
            this.mbApi = mbApiInterface;
            this.pl = GetPlaylists();
            InitializeComponent();
            foreach (var item in pl)
            {
                playlistBox.Items.Add(item.Key);
                checkedPlayListBox.Items.Add(item.Key);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Dictionary<String, String> playlists = GetPlaylists();
            checkedPlayListBox.CheckOnClick = true;
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            sum += 1;
            Button1.Text = "State" + sum;
            string stateText = "Undefined";
            Plugin.PlayState ps = GetPlayerState();
            switch (ps)
            {
                case Plugin.PlayState.Playing:
                    stateText = "Playing";
                    break;
                case Plugin.PlayState.Paused:
                    stateText = "Paused";
                    break;
                case Plugin.PlayState.Stopped:
                    stateText = "Stopped";
                    break;

            }
            stateLabel.Text = stateText;
            string[] res;
            mbApi.NowPlaying_GetFileTags(new Plugin.MetaDataType[] {Plugin.MetaDataType.TrackTitle, Plugin.MetaDataType.Rating} , out res);
            songTitleLabel1.Text = res[sum % 2];
            songTitleLabel3.Text = res[0];
            int rating = 0;
            if (float.TryParse(res[1], out float result))
            {
                rating = (int)result * 2;
            }

            ratingBar1.Value = rating;
            np = mbApi.NowPlaying_GetFileUrl();
            //queryPlaylistLabel.Text = mbApi.Playlist_QueryGetNextPlaylist();
            //playlistBox.Items.Add(Plugin.Playlist_QueryPlaylists());
        }

        private void playlistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string curItem = playlistBox.SelectedItem.ToString();
            queryPlaylistLabel.Text = curItem;
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string playlistUrl = pl.Values.ElementAt<String>(e.Index);
            if (e.NewValue == CheckState.Checked)
            {
                if (!mbApi.Playlist_IsInList(playlistUrl,np))
                {
                    mbApi.Playlist_AppendFiles(playlistUrl, new string[] {np});
                }
            } else if (e.NewValue == CheckState.Unchecked)
            {
                // Remove song from playlist
                // Can get all files and use removeAt
                // Or get all files and use setFiles
                // Remove at is probably better
                mbApi.Playlist_QueryFilesEx(playlistUrl, out string[] plFiles);
                int index = Array.IndexOf(plFiles, np);
                if (index != -1)
                {
                    // Remove song at index
                    mbApi.Playlist_RemoveAt(playlistUrl, index);
                } // else song already not in playlist
            }
        }

        private Dictionary<String,String> GetPlaylists()
        {
            Dictionary<String, String> dict = new Dictionary<string, string>();
            Plugin.PlaylistFormat pf;
            if (mbApi.Playlist_QueryPlaylists())
            {
                string file = "";
                string name = String.Empty;
                while (file != null)
                {
                    file = mbApi.Playlist_QueryGetNextPlaylist();
                    if (file != null)
                    {
                        pf = mbApi.Playlist_GetType(file);
                        if (pf == Plugin.PlaylistFormat.Auto || pf == Plugin.PlaylistFormat.Radio) { continue; }
                        name = mbApi.Playlist_GetName(file);
                        //if (name == null) { continue; }
                        if (!dict.ContainsKey(name)) dict.Add(name, file);
                    }
                }
            }

            return dict;
        }

        private Dictionary<String, String> GetPlaylistsContainingSong(string filename)
        {
            Dictionary<String, String> dict = new Dictionary<string, string>();
            foreach (var item in pl)
            {
                if (!mbApi.Playlist_IsInList(item.Value,filename)) continue;
                dict.Add(item.Key, item.Value);
            }
            
            return dict;
        }

        private int sum = 0;
        private Plugin.MusicBeeApiInterface mbApi;
        private Dictionary<String, String> pl;
        private string np;
        private string npName;
        private bool suppressCheckedChanged = false;

        private Plugin.PlayState GetPlayerState()
        {
            return mbApi.Player_GetPlayState();
        }

        public void TrackChanged(string filename)
        {
            np = filename;
            npName = mbApi.NowPlaying_GetFileTag(Plugin.MetaDataType.TrackTitle);
            ChangeStuff();
        }

        delegate void ChangeStuffCallback();
        private void ChangeStuff()
        {
            if (this.songTitleLabel1.InvokeRequired)
            {
                ChangeStuffCallback d = new ChangeStuffCallback(ChangeStuff);
                this.Invoke(d);
                return;
            }
            songTitleLabel1.Text = npName;
            songTitleLabel3.Text = npName;
            SetSelectedPlaylists();
            SetRatingPage();
        }

        delegate void SetSelectedPlaylistsCallback();
        private void SetSelectedPlaylists()
        {
            if (this.checkedPlayListBox.InvokeRequired)
            {
                SetSelectedPlaylistsCallback d = new SetSelectedPlaylistsCallback(SetSelectedPlaylists);
                this.Invoke(d);
            }
            else
            {
                for (int i = 0; i < pl.Count; i++)
                {
                    if (mbApi.Playlist_IsInList(pl.ElementAt(i).Value, np))
                    {
                        checkedPlayListBox.SetItemChecked(i, true);

                    }
                    else
                    {
                        checkedPlayListBox.SetItemChecked(i, false);
                    }
                }
            }
        }

        delegate void SetRatingPageCallback();
        private void SetRatingPage()
        {
            if (this.ratingBar1.InvokeRequired)
            {
                SetRatingPageCallback d = new SetRatingPageCallback(SetRatingPage);
                this.Invoke(d);
                return;
            }

            mbApi.Library_GetFileTags(np,
                new Plugin.MetaDataType[] { Plugin.MetaDataType.Rating, Plugin.MetaDataType.RatingLove },
                out string[] tags);
            this.noRatingLabel.Text = tags[0];
            this.loveLabel.Text = tags[1];
            //SetRatingLabel(tags[0]);
            //SetLoveLabel(tags[1]);
            suppressCheckedChanged = true;
            // now setting the actual controls
            if (tags[0] == "")
            {
                // Set no rating
                // Set scroll bar to 0
                noRatingCheckBox.Checked = true;
                ratingBar1.Value = 0;
            } else
            {
                // parse value
                // Set no rating to unchecked
                // Set scroll bar to value
                int rating = (int)float.Parse(tags[0]) * 2;
                ratingBar1.Value = rating;
                noRatingCheckBox.Checked = false;
            }
            if (tags[1] == "L")
            {
                // Set love check
                loveCheckBox.Checked = true;
            } else
            {
                // Set love uncheck
                loveCheckBox.Checked = false;
            }
            suppressCheckedChanged = false;
        }

        private void ratingBar1_Scroll(object sender, EventArgs e)
        {
            // only when manually changed
            mbApi.Library_SetFileTag(np, Plugin.MetaDataType.Rating, (ratingBar1.Value / 2).ToString());

        }

        private void noRatingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (suppressCheckedChanged) return;
            // set ratingBar to 0
            // set rating to ""
            if (noRatingCheckBox.Checked)
            {
                ratingBar1.Value = 0;
                mbApi.Library_SetFileTag(np, Plugin.MetaDataType.Rating, "");
            }
            else
            {
                // TODO: Check this
                mbApi.Library_SetFileTag(np, Plugin.MetaDataType.Rating, "0");
            }
        }

        private void loveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (suppressCheckedChanged) return;
            // just change love state
            if (loveCheckBox.Checked)
            {
                mbApi.Library_SetFileTag(np, Plugin.MetaDataType.RatingLove, "L");
            }
            else
            {
                mbApi.Library_SetFileTag(np, Plugin.MetaDataType.RatingLove, "");
            }
        }
    }
}
