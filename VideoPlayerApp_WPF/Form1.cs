using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// Added IO , to access external files for video player.
using System.IO;

namespace VideoPlayerApp_WPF
{
    public partial class Form1 : Form
    {
        //To store the video file.
        List<string> filteredFiles = new List<string>();
        // To show the browsable files. 
        FolderBrowserDialog browser = new FolderBrowserDialog();
        // To show which file is currently playing.
        int currentFile = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void loadFolderEvent(object sender, EventArgs e)
        {
            // Stop any playing file and clearing the list.
            VideoPlayer.Ctlcontrols.stop();
            if(filteredFiles.Count > 1)
            {
                filteredFiles.Clear();
                filteredFiles = null;

                Playlist.Items.Clear();

                currentFile = 0;
            }

            // Any file with the correct extension will be added to the playlist.
            DialogResult result = browser.ShowDialog();
            
            if (result == DialogResult.OK)
            {
                filteredFiles = Directory.GetFiles(browser.SelectedPath, "*.*").Where(file => file.ToLower().EndsWith("webm") || file.ToLower().EndsWith("mp4") || file.ToLower().EndsWith("wmv") || file.ToLower().EndsWith("mkv") || file.ToLower().EndsWith("avi")).ToList();

                LoadPlayList();
            }
        }
        //Shows a messagebox for the about item.
        private void ShowAboutEvent(object sender, EventArgs e)
        {
            MessageBox.Show("Made by RonaldC" + Environment.NewLine + "Done as practice for C# and WPF" + Environment.NewLine + "Click on Load Folder to load and play videos from your system" + Environment.NewLine + "","About:");

        }

        private void MediaPlayerStateChangeEvent(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {

        }

        private void PlayListChanged(object sender, EventArgs e)
        {

        }

        private void TimerEvent(object sender, EventArgs e)
        {

        }
        private void LoadPlayList()
        {
            //Creates a new playlist and displaying them in the Playlist box.
            VideoPlayer.currentPlaylist = VideoPlayer.newPlaylist("Playlist", "");
            foreach (string videos in filteredFiles)
            {
                VideoPlayer.currentPlaylist.appendItem(VideoPlayer.newMedia(videos));
                Playlist.Items.Add(videos);
            }
            //See if there are playable files in the folder if yes ,sends them to the Playfile method.
            if (filteredFiles.Count > 0)
            {
                FileName.Text = "Files present" + filteredFiles.Count;
                Playlist.SelectedIndex = currentFile;
                PlayFile(Playlist.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show("Could not find any video files in this folder.");
            }



        }
        
        private void PlayFile(string url)
        {   //Takes the video and plays it. 
            VideoPlayer.URL = url;
        }
        private void ShowFileName(Label name)
        {   //Takes the filename from the current video and shows it.
            string file = Path.GetFileName(Playlist.SelectedItem.ToString());
            name.Text = "Currently playing : " + file;

        }
    }
}
