
namespace WinPaletter
{
    public class Converter
    {

        public static Converter_CP.WP_Format Format;
        private Converter_CP Converter_CP;

        public Converter_CP.WP_Format FetchFile(string File)
        {
            if (System.IO.File.Exists(File))
            {
                Converter_CP = new Converter_CP(File);
                return Format;
            }
            else
            {
                return Converter_CP.WP_Format.Error;
            }
        }

        public void Convert(string File, string SaveAs, bool Compress, bool OldWPTH1069)
        {
            if (System.IO.File.Exists(File))
            {
                Converter_CP = new Converter_CP(File);

                switch (Format)
                {
                    case Converter_CP.WP_Format.JSON:
                        {
                            Converter_CP.Save(Converter_CP.WP_Format.WPTH, SaveAs, Compress, OldWPTH1069);
                            break;
                        }

                    case Converter_CP.WP_Format.WPTH:
                        {
                            Converter_CP.Save(Converter_CP.WP_Format.JSON, SaveAs, Compress, OldWPTH1069);
                            break;
                        }

                }

            }
        }
    }
}