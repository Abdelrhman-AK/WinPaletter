using System;
using System.Drawing;

namespace WinPaletter
{
    public partial class Start8Selector
    {
        public Start8Selector()
        {
            InitializeComponent();
        }
        private void Start8Selector_Load(object sender, EventArgs e)
        {
            Icon = Forms.MainFrm.Icon;

            this.LoadLanguage();
            ApplyStyle(this);
            LoadImagesFromDLL();

            switch (Forms.Win81Colors.StartBackground_ID)
            {
                case 1:
                    {
                        img1.Checked = true;
                        break;
                    }
                case 2:
                    {
                        img2.Checked = true;
                        break;
                    }
                case 3:
                    {
                        img3.Checked = true;
                        break;
                    }
                case 4:
                    {
                        img4.Checked = true;
                        break;
                    }
                case 5:
                    {
                        img5.Checked = true;
                        break;
                    }
                case 6:
                    {
                        img6.Checked = true;
                        break;
                    }
                case 7:
                    {
                        img7.Checked = true;
                        break;
                    }
                case 8:
                    {
                        img8.Checked = true;
                        break;
                    }
                case 9:
                    {
                        img9.Checked = true;
                        break;
                    }
                case 10:
                    {
                        img10.Checked = true;
                        break;
                    }
                case 11:
                    {
                        img11.Checked = true;
                        break;
                    }
                case 12:
                    {
                        img12.Checked = true;
                        break;
                    }
                case 13:
                    {
                        img13.Checked = true;
                        break;
                    }
                case 14:
                    {
                        img14.Checked = true;
                        break;
                    }
                case 15:
                    {
                        img15.Checked = true;
                        break;
                    }
                case 16:
                    {
                        img16.Checked = true;
                        break;
                    }
                case 17:
                    {
                        img17.Checked = true;
                        break;
                    }
                case 18:
                    {
                        img18.Checked = true;
                        break;
                    }
                case 19:
                    {
                        img19.Checked = true;
                        break;
                    }
                case 20:
                    {
                        img20.Checked = true;
                        break;
                    }

                default:
                    {
                        img1.Checked = true;
                        break;
                    }
            }
        }

        public void LoadImagesFromDLL()
        {
            img19.Image = Program.TM.Windows81.PersonalColors_Background.ToBitmap(new Size(64, 64));
            img20.Image = Program.Wallpaper.Resize(64, 64);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (img1.Checked)
                Forms.Win81Colors.StartBackground_ID = 1;
            else if (img2.Checked)
                Forms.Win81Colors.StartBackground_ID = 2;
            else if (img3.Checked)
                Forms.Win81Colors.StartBackground_ID = 3;
            else if (img4.Checked)
                Forms.Win81Colors.StartBackground_ID = 4;
            else if (img5.Checked)
                Forms.Win81Colors.StartBackground_ID = 5;
            else if (img6.Checked)
                Forms.Win81Colors.StartBackground_ID = 6;
            else if (img7.Checked)
                Forms.Win81Colors.StartBackground_ID = 7;
            else if (img8.Checked)
                Forms.Win81Colors.StartBackground_ID = 8;
            else if (img9.Checked)
                Forms.Win81Colors.StartBackground_ID = 9;
            else if (img10.Checked)
                Forms.Win81Colors.StartBackground_ID = 10;
            else if (img11.Checked)
                Forms.Win81Colors.StartBackground_ID = 11;
            else if (img12.Checked)
                Forms.Win81Colors.StartBackground_ID = 12;
            else if (img13.Checked)
                Forms.Win81Colors.StartBackground_ID = 13;
            else if (img14.Checked)
                Forms.Win81Colors.StartBackground_ID = 14;
            else if (img15.Checked)
                Forms.Win81Colors.StartBackground_ID = 15;
            else if (img16.Checked)
                Forms.Win81Colors.StartBackground_ID = 16;
            else if (img17.Checked)
                Forms.Win81Colors.StartBackground_ID = 17;
            else if (img18.Checked)
                Forms.Win81Colors.StartBackground_ID = 18;
            else if (img19.Checked)
                Forms.Win81Colors.StartBackground_ID = 19;
            else if (img20.Checked)
                Forms.Win81Colors.StartBackground_ID = 20;

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }
    }
}