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
            about.VersionMinor = 3;
            about.Revision = 1;
            about.MinInterfaceVersion = MinInterfaceVersion;
            about.MinApiRevision = MinApiRevision;
            about.ReceiveNotifications = (ReceiveNotificationFlags.PlayerEvents | ReceiveNotificationFlags.TagEvents);
            about.ConfigurationPanelHeight = 25;   // height in pixels that musicbee should reserve in a panel for config settings. When set, a handle to an empty panel will be passed to the Configure function
            this.pluginForm = new Form1(this.mbApiInterface);
            GetSavedSettings();
            GetAllPlaylists();
            createMenuItem();
            createCommands();
            return about;
        }

        private void OpenConfigForm()
        {
            this.configForm = new ConfigForm(playlistList, playlistNames, currPlaylists, this);
            configForm.Show();
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
                Button button = new Button
                {
                    AutoSize = true,
                    Location = new Point(0, 0),
                    Text = "Configure"
                };
                // TODO: Check opening multiple
                button.Click += new EventHandler((sender, e) => OpenConfigForm());
                configPanel.Controls.Add(button);

                //Label prompt = new Label();
                //TextBox textBox = new TextBox();
                //textBox.Bounds = new Rectangle(60, 0, 100, textBox.Height);
                //configPanel.Controls.AddRange(new Control[] { prompt, textBox });
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
            if (!Directory.Exists(dataPath + "/hotkeyOrg/"))
            {
                Directory.CreateDirectory(dataPath + "/hotkeyOrg/");
            }

            SavePlaylists();
        }

        private void SavePlaylists()
        {
            string dataPath = mbApiInterface.Setting_GetPersistentStoragePath();
            using (var stream = File.Create(dataPath + "/hotkeyOrg/currPlaylists.xml"))
            {
                var serializer = new XmlSerializer(typeof(string[]));
                serializer.Serialize(stream, currPlaylists);
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
                    string filename = mbApiInterface.NowPlaying_GetFileUrl();
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

        private const int numCommands = 10;
        private string np = "";
        private void createCommands()
        {
            for (int i = 1; i <= numCommands; i++)
            {
                int a = i;
                mbApiInterface.MB_RegisterCommand("HotkeyOrganiser: Command " + a.ToString(), 
                                                  new EventHandler((sender, e) => AddToPlaylist(a-1)));
            }
        }

        private string[] playlistList;
        private string[] playlistNames;
        private string[] currTags = new string[numCommands];
        private string[] currPlaylists = new string[numCommands];
        private string[] currGenres = new string[numCommands];
        private void GetSavedSettings()
        {
            string dataPath = mbApiInterface.Setting_GetPersistentStoragePath();
            Console.WriteLine(dataPath);

            // open file into currPlaylists and currGenre
            // check if file exists

            if (File.Exists(dataPath + "/hotkeyOrg/currPlaylists.xml"))
            {
                using (var stream = new FileStream(dataPath + "/hotkeyOrg/currPlaylists.xml", FileMode.Open))
                {
                    var serializer = new XmlSerializer(typeof(string[]));
                    currPlaylists = serializer.Deserialize(stream) as string[]; // will this work?
                    //serializer.Serialize(stream, currPlaylists);
                }
            }
            if (File.Exists(dataPath + "/hotkeyOrg/currGenres.xml"))
            {
                using (var stream = new FileStream(dataPath + "/hotkeyOrg/currGenres.xml", FileMode.Open))
                {
                    var serializer = new XmlSerializer(typeof(string[]));
                    currGenres = serializer.Deserialize(stream) as string[]; // TODO: really will it?
                }
            }
            if (File.Exists(dataPath + "/hotkeyOrg/currTags.xml"))
            {
                using (var stream = new FileStream(dataPath + "/hotkeyOrg/currTags.xml", FileMode.Open))
                {
                    var serializer = new XmlSerializer(typeof(string[]));
                    currTags = serializer.Deserialize(stream) as string[]; // TODO: really will it?
                }
            }

        }

        private void GetAllPlaylists()
        {
            // list all playlists
            mbApiInterface.Playlist_QueryPlaylists();
            string file = "";
            List<string> filesList = new List<string>();
            List<string> namesList = new List<string>();

            //file = mbApiInterface.Playlist_QueryGetNextPlaylist();
            while ((file = mbApiInterface.Playlist_QueryGetNextPlaylist()) != null)
            {
                if (mbApiInterface.Playlist_GetType(file) == PlaylistFormat.Auto) { continue; }
                filesList.Add(file);
                namesList.Add(mbApiInterface.Playlist_GetName(file));
            }
            playlistList = filesList.ToArray();
            playlistNames = namesList.ToArray();
            // list all genres???

        }

        public void SetSinglePlaylist(int commandNum, int i)
        {
            currPlaylists[commandNum] = playlistList[i];
        }

        private void AddToPlaylist(int commandNum)
        {
            np = mbApiInterface.NowPlaying_GetFileUrl();
            if (np == null || np == "") return;
            if (currPlaylists[commandNum] == "") return;
            if (!mbApiInterface.Playlist_IsInList(currPlaylists[commandNum], np))
            {
                mbApiInterface.Playlist_AppendFiles(currPlaylists[commandNum], new string[] { np });
            }
            // TODO: figure out something for removing from playlist maybe??

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
        // TODO: Test this maybe
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

        // TODO: idk what this is
        //private void panel_Paint(object sender, PaintEventArgs e)
        //{
        //    e.Graphics.Clear(Color.Red);
        //    TextRenderer.DrawText(e.Graphics, "hello", SystemFonts.CaptionFont, new Point(10, 10), Color.Blue);
        //}

        private Form1 pluginForm;
        private ConfigForm configForm;
    }
}