using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Xml.Serialization;

namespace MusicBeePlugin
{
    public partial class Plugin
    {
        private MusicBeeApiInterface mbApiInterface;
        private PluginInfo about = new PluginInfo();

        public PluginInfo Initialise(IntPtr apiInterfacePtr)
        {
            mbApiInterface = new MusicBeeApiInterface();
            mbApiInterface.Initialise(apiInterfacePtr);
            about.PluginInfoVersion = PluginInfoVersion;
            about.Name = "Hotkey Organiser";
            about.Description = "Quick organiser for songs";
            about.Author = "LochTech";
            about.TargetApplication = "";   //  the name of a Plugin Storage device or panel header for a dockable panel
            about.Type = PluginType.General;
            about.VersionMajor = 0;  // your plugin version
            about.VersionMinor = 2;
            about.Revision = 1;
            about.MinInterfaceVersion = MinInterfaceVersion;
            about.MinApiRevision = MinApiRevision;
            about.ReceiveNotifications = (ReceiveNotificationFlags.PlayerEvents | ReceiveNotificationFlags.TagEvents);
            about.ConfigurationPanelHeight = 500;   // height in pixels that musicbee should reserve in a panel for config settings. When set, a handle to an empty panel will be passed to the Configure function
            this.pluginForm = new Form1(this.mbApiInterface);
            GetSavedSettings();
            GetAllPlaylists();
            this.configForm = new ConfigForm(playlistList, genreList, currPlaylists, currGenres, this);
            createMenuItem();
            createCommands();
            return about;
        }

        public bool Configure(IntPtr panelHandle)
        {
            // save any persistent settings in a sub-folder of this path
            string dataPath = mbApiInterface.Setting_GetPersistentStoragePath();
            // panelHandle will only be set if you set about.ConfigurationPanelHeight to a non-zero value
            // keep in mind the panel width is scaled according to the font the user has selected
            // if about.ConfigurationPanelHeight is set to 0, you can display your own popup window
            if (panelHandle != IntPtr.Zero)
            {
                Panel configPanel = (Panel)Panel.FromHandle(panelHandle);
                Label prompt = new Label();
                prompt.AutoSize = true;
                prompt.Location = new Point(0, 0);
                prompt.Text = "prompt:";
                TextBox textBox = new TextBox();
                textBox.Bounds = new Rectangle(60, 0, 100, textBox.Height);
                configPanel.Controls.AddRange(new Control[] { prompt, textBox });
            }
            return false;
        }
       
        // called by MusicBee when the user clicks Apply or Save in the MusicBee Preferences screen.
        // its up to you to figure out whether anything has changed and needs updating
        public void SaveSettings()
        {
            // save any persistent settings in a sub-folder of this path
            string dataPath = mbApiInterface.Setting_GetPersistentStoragePath();
            Console.WriteLine(dataPath);
            using (var stream = File.Create(dataPath + "/hotkeyOrg/currPlaylists.xml"))
            {
                var serializer = new XmlSerializer(typeof(string[]));
                serializer.Serialize(stream, currPlaylists);
            }
            using (var stream = File.Create(dataPath + "/hotkeyOrg/currGenres.xml"))
            {
                var serializer = new XmlSerializer(typeof(string[]));
                serializer.Serialize(stream, currGenres);
            }
        }

        // MusicBee is closing the plugin (plugin is being disabled by user or MusicBee is shutting down)
        public void Close(PluginCloseReason reason)
        {
        }

        // uninstall this plugin - clean up any persisted files
        public void Uninstall()
        {
        }

        // receive event notifications from MusicBee
        // you need to set about.ReceiveNotificationFlags = PlayerEvents to receive all notifications, and not just the startup event
        public void ReceiveNotification(string sourceFileUrl, NotificationType type)
        {
            // perform some action depending on the notification type
            switch (type)
            {
                case NotificationType.PluginStartup:
                    // perform startup initialisation
                    


                    switch (mbApiInterface.Player_GetPlayState())
                    {
                        case PlayState.Playing:
                        case PlayState.Paused:
                            // ...
                            break;
                    }
                    break;
                case NotificationType.TrackChanged:
                    //string artist = mbApiInterface.NowPlaying_GetFileTag(MetaDataType.Artist);
                    string filename = mbApiInterface.NowPlaying_GetFileUrl();
                    // ...
                    pluginForm.TrackChanged(filename);
                    break;
            }
        }

        

        private void createMenuItem() 
        {
            mbApiInterface.MB_AddMenuItem("mnuTools/Start My Plugin", "HotKey For Start My Plugin", menuClicked);
            

        }

        

        private void menuClicked(object sender, EventArgs e)
        {
            this.pluginForm.Show();
        }

        public PlayState getPlayerState()
        {
            return mbApiInterface.Player_GetPlayState();
        }


        private int CommandLayer = 0;
        private const int numLayers = 4;
        private const int numCommands = 10;
        private string np = "";
        private void createCommands()
        {
            //mbApiInterface.MB_RegisterCommand("HotkeyOrganiser: Command 1", this.pluginForm.Command1);
            mbApiInterface.MB_RegisterCommand("HotkeyOrganiser: Change Layer Up", this.ChangeCommandLayerUp);
            mbApiInterface.MB_RegisterCommand("HotkeyOrganiser: Change Layer Down", this.ChangeCommandLayerDown);

            //mbApiInterface.MB_RegisterCommand("HotkeyOrganiser: Command 2", new EventHandler((sender, e) => DoCommand(2)));
            //mbApiInterface.MB_RegisterCommand("HotkeyOrganiser: Command 3", new EventHandler((sender, e) => DoCommand(3)));
            //mbApiInterface.MB_RegisterCommand("HotkeyOrganiser: Command 4", new EventHandler((sender, e) => DoCommand(4)));
            //mbApiInterface.MB_RegisterCommand("HotkeyOrganiser: Command 5", new EventHandler((sender, e) => DoCommand(5)));
            //mbApiInterface.MB_RegisterCommand("HotkeyOrganiser: Command 6", new EventHandler((sender, e) => DoCommand(6)));
            //mbApiInterface.MB_RegisterCommand("HotkeyOrganiser: Command 7", new EventHandler((sender, e) => DoCommand(7)));

            for (int i = 1; i <= numCommands; i++)
            {
                mbApiInterface.MB_RegisterCommand("HotkeyOrganiser: Command " + i.ToString(), 
                                                  new EventHandler((sender, e) => DoCommand(i-1)));
            }
        }

        private void ChangeCommandLayerUp(object sender, EventArgs e)
        {
            CommandLayer++;
            CommandLayer %= numLayers;
        }

        private void ChangeCommandLayerDown(object sender, EventArgs e)
        {
            CommandLayer--;
            CommandLayer += numLayers; // to make the mod positive
            CommandLayer %= numLayers;
        }

        private void DoCommand(int commandNum)
        {
            // Do something to make sure np is something
            np = mbApiInterface.NowPlaying_GetFileUrl();
            if (np == null || np == "") return; 

            switch (CommandLayer)
            {
                case 0: // playlists
                    AddToPlaylist(commandNum);
                    break;
                case 1: // genres
                    AddGenre(commandNum);
                    break; 
                case 2: // tags
                    AddTag(commandNum);
                    break; 
                case 3: // rating
                    SetRating(commandNum);
                    break;
            }
        }

        private string[] playlistList;
        private string[] genreList;
        private string[] currTags = new string[numCommands];
        private string[] currPlaylists = new string[numCommands];
        private string[] currGenres = new string[numCommands];
        private void GetSavedSettings()
        {
            string dataPath = mbApiInterface.Setting_GetPersistentStoragePath();
            Console.WriteLine(dataPath);

            // open file into currPlaylists and currGenre
            // check if file exists
            using (var stream = File.Create(dataPath + "/hotkeyOrg/currPlaylists.xml"))
            {
                var serializer = new XmlSerializer(typeof(string[]));
                currPlaylists = serializer.Deserialize(stream) as string[]; // will this work?
                //serializer.Serialize(stream, currPlaylists);
            }
            using (var stream = File.Create(dataPath + "/hotkeyOrg/currGenres.xml"))
            {
                var serializer = new XmlSerializer(typeof(string[]));
                currGenres = serializer.Deserialize(stream) as string[]; // TODO: really will it?
            }
        }

        private void GetAllPlaylists()
        {
            // list all playlists
            mbApiInterface.Playlist_QueryPlaylists();
            string file = "";
            List<string> list = new List<string>();
            file = mbApiInterface.Playlist_QueryGetNextPlaylist();
            while (file != null)
            {
                list.Add(file);
            }
            playlistList = list.ToArray();

            // list all genres???

        }

        public void SetCurrentPlaylist(string[] list)
        {
            currPlaylists = list;
        }

        public void SetCurrentGenres(string[] list)
        {
            currGenres = list;
        }

        public void SetCurrentTags(string[] list)
        {
            currTags = list;
        }

        public void SetSingleGenre(int commandNum, int i)
        {
            currPlaylists[commandNum] = playlistList[i];
        }

        public void SetSingleGenre(int commandNum, string s)
        {
            currGenres[commandNum] = s;
        }

        public void SetSinglePlaylist(int commandNum, int i)
        {
            currGenres[commandNum] = genreList[i];
        }

        private void AddToPlaylist(int commandNum)
        {
            //playlistUrl = playlistList[commandNum]
            if (currPlaylists[commandNum] == "") return;
            if (!mbApiInterface.Playlist_IsInList(currPlaylists[commandNum], np))
            {
                mbApiInterface.Playlist_AppendFiles(currPlaylists[commandNum], new string[] { np });
            }
            // figure out something for removing from playlist maybe??

        }

        private void AddGenre(int commandNum)
        {
            if (currGenres[commandNum] == "") return;
            mbApiInterface.Library_SetFileTag(np, Plugin.MetaDataType.Genre, currGenres[commandNum]);
        }

        private void AddTag(int commandNum)
        {
            // What is this meant to 
            // Append to comment if it isn't already there
            if (currTags[commandNum] == "") return;

            string cmt = mbApiInterface.Library_GetFileTag(np, Plugin.MetaDataType.Comment);
            string tag = currTags[commandNum];

            // check if comment is empty
            if (cmt == "")
            {
                mbApiInterface.Library_SetFileTag(np, Plugin.MetaDataType.Comment, tag);
                return;
            }

            // check if tag is in comment
            if (cmt.Contains(tag)) return;
            // TODO: figure out when to remove tags

            // check if last character is a space
            // if not, add one (unless start of comment)
            if (!cmt.EndsWith(" ")) cmt += " ";

            // append tag
            cmt += tag;
            mbApiInterface.Library_SetFileTag(np, Plugin.MetaDataType.Comment, cmt);

        }

        private void SetRating(int commandNum)
        {
            mbApiInterface.Library_SetFileTag(np, Plugin.MetaDataType.Rating, (commandNum / 2).ToString());
        }

        // return an array of lyric or artwork provider names this plugin supports
        // the providers will be iterated through one by one and passed to the RetrieveLyrics/ RetrieveArtwork function in order set by the user in the MusicBee Tags(2) preferences screen until a match is found
        //public string[] GetProviders()
        //{
        //    return null;
        //}

        // return lyrics for the requested artist/title from the requested provider
        // only required if PluginType = LyricsRetrieval
        // return null if no lyrics are found
        //public string RetrieveLyrics(string sourceFileUrl, string artist, string trackTitle, string album, bool synchronisedPreferred, string provider)
        //{
        //    return null;
        //}

        // return Base64 string representation of the artwork binary data from the requested provider
        // only required if PluginType = ArtworkRetrieval
        // return null if no artwork is found
        //public string RetrieveArtwork(string sourceFileUrl, string albumArtist, string album, string provider)
        //{
        //    //Return Convert.ToBase64String(artworkBinaryData)
        //    return null;
        //}

        //  presence of this function indicates to MusicBee that this plugin has a dockable panel. MusicBee will create the control and pass it as the panel parameter
        //  you can add your own controls to the panel if needed
        //  you can control the scrollable area of the panel using the mbApiInterface.MB_SetPanelScrollableArea function
        //  to set a MusicBee header for the panel, set about.TargetApplication in the Initialise function above to the panel header text
        //public int OnDockablePanelCreated(Control panel)
        //{
        //  //    return the height of the panel and perform any initialisation here
        //  //    MusicBee will call panel.Dispose() when the user removes this panel from the layout configuration
        //  //    < 0 indicates to MusicBee this control is resizable and should be sized to fill the panel it is docked to in MusicBee
        //  //    = 0 indicates to MusicBee this control resizeable
        //  //    > 0 indicates to MusicBee the fixed height for the control.Note it is recommended you scale the height for high DPI screens(create a graphics object and get the DpiY value)
        //    float dpiScaling = 0;
        //    using (Graphics g = panel.CreateGraphics())
        //    {
        //        dpiScaling = g.DpiY / 96f;
        //    }
        //    panel.Paint += panel_Paint;
        //    return Convert.ToInt32(100 * dpiScaling);
        //}

        // presence of this function indicates to MusicBee that the dockable panel created above will show menu items when the panel header is clicked
        // return the list of ToolStripMenuItems that will be displayed
        //public List<ToolStripItem> GetHeaderMenuItems()
        //{
        //    List<ToolStripItem> list = new List<ToolStripItem>();
        //    list.Add(new ToolStripMenuItem("A menu item"));
        //    return list;
        //}

        //private void panel_Paint(object sender, PaintEventArgs e)
        //{
        //    e.Graphics.Clear(Color.Red);
        //    TextRenderer.DrawText(e.Graphics, "hello", SystemFonts.CaptionFont, new Point(10, 10), Color.Blue);
        //}

        private Form1 pluginForm;
        private ConfigForm configForm;
    }
}