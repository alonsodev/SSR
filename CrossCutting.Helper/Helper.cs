using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CrossCutting.Helper
{
    public class Helper
    {
        public static List<SelectListItem> ConstruirDropDownList<T1>(List<T1> pLista, string pStrCampoValor, string pStrCampoDescripcion, string pStrSeleccionarValor, Boolean pBlnInsertarOpcBlanco = false, string pStrValorOpcBlanco = "", string pStrTextoOpcBlanco = "")
        {
            List<SelectListItem> oLista = new List<SelectListItem>();

            if (pBlnInsertarOpcBlanco)
            {
                oLista.Add(new SelectListItem
                {
                    Text = pStrTextoOpcBlanco,
                    Value = pStrValorOpcBlanco
                });
            }

            foreach (T1 oItem in pLista)
            {
                oLista.Add(new SelectListItem
                {
                    Text = oItem.GetType().GetProperty(pStrCampoDescripcion).GetValue(oItem).ToString(),
                    Value = oItem.GetType().GetProperty(pStrCampoValor).GetValue(oItem).ToString()
                });

            }

            if (pStrSeleccionarValor != "" && pStrSeleccionarValor != "0")
            {
                oLista.Find(a => a.Value == pStrSeleccionarValor).Selected = true;
            }

            return oLista;
        }

        public static string Encripta(string Par_Word)
        {
            int Par_ID = 1;
           // string ID, StringCode, StringDecode;
            string resultadoEncriptado, OriginalString;

            string[] Encode = new string[5];
            string[] Decode = new string[5];
            int PosStringCode, PosStringDecode = 1;

            Decode[0] = @"|#$9874%&/()°><12356{ë}+*-áÿCÜéö£ôí¥kÆ¼ªóºñò¬Öú½«»û¦_?¡!¿][0.@aêåd\ìæ¶";
            Encode[0] = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_ .:-,;\";
            Decode[1] = @"|#$9874%&/()°><12356{ë}+*-áÿCÜéö£ôí¥kÆ¼ªóºñò¬Öú½«»û¦_?¡!¿][0.@aêåd\ìæ¶";
            Encode[1] = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_ .:-,;\";
            Decode[2] = @"7ê56{}+*-_?¡!¿][0.@|#è$%b/í¥kÆ¼ªóºñò¬Öú½«áÿCÜéö£ô»û¦()°><12398&4çb\ìæ¶";
            Encode[2] = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_ .:-,;\";
            Decode[3] = @")°><126{@}+*-_?3ê8745ïXc!¿ö£ôí¥kÆ¼ªóºáÿCÜéñò¬Öú½«»û¦]|#$%&/([0.¡9ëc\ìæ¶";
            Encode[3] = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_ .:-,;\";
            Decode[4] = @"+*-_?¡87456{!¿][0.@|#ç$%&dkÆ¼ªóºñò¬Öú½«»û¦áÿCÜéö£ôí¥()ê><1239}/°êh\ìæ¶";
            Encode[4] = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_ .:-,;\";

            resultadoEncriptado = string.Empty;

            for (PosStringDecode = 0; PosStringDecode < Par_Word.Length; PosStringDecode++)
            {
                OriginalString = Par_Word.Substring(PosStringDecode, 1);

                for (PosStringCode = 0; PosStringCode < Encode[Par_ID].Length; PosStringCode++)
                {
                    if (OriginalString == Encode[Par_ID].Substring(PosStringCode, 1))
                        {
                        resultadoEncriptado = resultadoEncriptado + Decode[Par_ID].Substring(PosStringCode, 1);                            
                        }
                }
            }

            return resultadoEncriptado;
        }
        public static List<SelectListItem> MarkSelectedAll(List<SelectListItem> list) {
            foreach (SelectListItem item in list) {
                item.Selected = true;
            }
            return list;
        }

        public static byte[] ImageBackgroundWhiteColor(byte[] file)
        {
           
            var imageConverter = new ImageConverter();
            var image = (Image)imageConverter.ConvertFrom(file);
            Bitmap bmp1 = new Bitmap(image);
            Bitmap bmp2 = Transparent2Color(bmp1, Color.White);
            MemoryStream ms = new MemoryStream();
            bmp2.Save(ms, ImageFormat.Jpeg);
            return  ms.ToArray();
        }
        private static Bitmap Transparent2Color(Bitmap bmp1, Color target)
        {
            Bitmap bmp2 = new Bitmap(bmp1.Width, bmp1.Height);
            Rectangle rect = new Rectangle(Point.Empty, bmp1.Size);
            using (Graphics G = Graphics.FromImage(bmp2))
            {
                G.Clear(target);
                G.DrawImageUnscaledAndClipped(bmp1, rect);
            }
            return bmp2;
        }


    }
}
