using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

/// <summary>
/// Wzorowane na:
/// https://github.com/ksopyla/MorfeuszNet
/// </summary>

namespace Application.Common.Morfeusz
{
    public enum MorfeuszOptions
    {
        Encoding = 1
    }

    public enum MorfeuszEncodings
    {
        Utf8 = 8,
        Iso88592 = 88592,
        Cp1250 = 1250,
        Cp852 = 852
    }

    /// <summary>
    /// Marcin Woliński Morfeusz reloaded. 
    /// In Nicoletta Calzolari, Khalid Choukri, Thierry Declerck, Hrafn Loftsson, Bente Maegaard, Joseph Mariani, Asuncion Moreno, Jan Odijk, and Stelios Piperidis, editors, 
    /// Proceedings of the Ninth International Conference on Language Resources and Evaluation, LREC 2014, pages 1106–1111, Reykjavík, Iceland, 2014. ELRA.
    /// http://morfeusz.sgjp.pl/
    /// </summary>    
    public static class Morfeusz2
    {
        [DllImport("morfeusz2.dll", EntryPoint = "morfeusz_set_option")]
        public static extern int MorfeuszSetOption(int option, int value);

        public static int SetEncoding(MorfeuszEncodings encoding)
        {
            return MorfeuszSetOption((int)MorfeuszOptions.Encoding, (int)encoding);
        }

        [DllImport("morfeusz2.dll", EntryPoint = "morfeusz_analyse")]
        public static extern IntPtr MorfAnalyse(byte[] tekst);

        public static InterpMorf[] Analyse(string text)
        {
            SetEncoding(MorfeuszEncodings.Utf8);
            byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(text);

            IntPtr morfStruct = MorfAnalyse(bytes);

            List<InterpMorf> morf = new List<InterpMorf>();
            int itemSize = Marshal.SizeOf(typeof(InterpMorf));
            int offset = 0;
            InterpMorf interpMorf;

            try
            {
                interpMorf = (InterpMorf)Marshal.PtrToStructure((morfStruct + (offset * itemSize)), typeof(InterpMorf));

            }
            catch (Exception ex)
            {
                Console.WriteLine("Param: {0}  Error: {1} Msg:{2}", text, ex.GetType().ToString(), ex.Message);
                throw;
            }
            //gdy p==-1 to znaczy że to jest ostatni InterpMorf na liście
            while (interpMorf.p != -1)
            {
                offset++;
                morf.Add(interpMorf);
                interpMorf = (InterpMorf)Marshal.PtrToStructure((morfStruct + (offset * itemSize)), typeof(InterpMorf));
            }

            return morf.ToArray();
        }

        public static List<string> GetAllUniqueTags(string text)
        {
            var tags = new List<string>();
            var structs = Analyse(text);

            foreach (InterpMorf s in structs)
            {
                if (!s.interp.Equals("interp") && !s.interp.Equals("dig"))
                    tags.Add(s.haslo.ToLower());
            }

            tags = tags.Distinct().ToList();
            return tags;
        }

    }
}
