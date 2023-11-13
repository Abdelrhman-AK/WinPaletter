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

            switch (Program.TM.Windows81.Start)
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
                Program.TM.Windows81.Start = 1;
            if (img2.Checked)
                Program.TM.Windows81.Start = 2;
            if (img3.Checked)
                Program.TM.Windows81.Start = 3;
            if (img4.Checked)
                Program.TM.Windows81.Start = 4;
            if (img5.Checked)
                Program.TM.Windows81.Start = 5;
            if (img6.Checked)
                Program.TM.Windows81.Start = 6;
            if (img7.Checked)
                Program.TM.Windows81.Start = 7;
            if (img8.Checked)
                Program.TM.Windows81.Start = 8;
            if (img9.Checked)
                Program.TM.Windows81.Start = 9;
            if (img10.Checked)
                Program.TM.Windows81.Start = 10;
            if (img11.Checked)
                Program.TM.Windows81.Start = 11;
            if (img12.Checked)
                Program.TM.Windows81.Start = 12;
            if (img13.Checked)
                Program.TM.Windows81.Start = 13;
            if (img14.Checked)
                Program.TM.Windows81.Start = 14;
            if (img15.Checked)
                Program.TM.Windows81.Start = 15;
            if (img16.Checked)
                Program.TM.Windows81.Start = 16;
            if (img17.Checked)
                Program.TM.Windows81.Start = 17;
            if (img18.Checked)
                Program.TM.Windows81.Start = 18;
            if (img19.Checked)
                Program.TM.Windows81.Start = 19;
            if (img20.Checked)
                Program.TM.Windows81.Start = 20;
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}