using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WAV_Player
{
    public partial class frmWAVPlayer : Form
    {

        SoundPlayer player;

        public frmWAVPlayer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 當使用者按下「瀏覽」按鈕時，開啟檔案對話框讓使用者選擇 WAV 檔案，並將選擇的檔案路徑顯示在 txtFilePath 文字框中。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            // 過濾條件設定為WAV檔案
            this.ofdWAVFile.Filter = "WAV Files(*.wav)|*.wav";
            // 打開檔案對話方塊
            if (this.ofdWAVFile.ShowDialog() == DialogResult.OK)
            {
                this.txtPath.Text = this.ofdWAVFile.FileName;
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            try
            {
                player = new SoundPlayer();
                player.SoundLocation = txtPath.Text;
                player.Load();
                player.Play();
                //player.PlaySync();
                //MessageBox.Show("播放完成！", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("無法播放音效檔，請確認檔案路徑是否正確。\n" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoop_Click(object sender, EventArgs e)
        {
            // 使用完整檔名建立物件
            player = new SoundPlayer(txtPath.Text);
            player.PlayLooping(); // 重複播放


        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            player.Stop(); // 停止播放
            // fsWAV.Close(); // 關閉串流
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            Application.Exit();
            //this.Close();
        }

        private void frmWAVPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("確定要關閉應用程式嗎？", "關閉確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true; // 取消關閉
            }
        }
    }
}
