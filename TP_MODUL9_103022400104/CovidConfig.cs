using System;
using System.Collections.Generic;
using System.Text;

namespace TP_MODUL9_103022400104
{
    public class CovidConfig 
    {
        private Dictionary<string, string> config = new Dictionary<string, string>()
        {
            { "satuan_suhu", "CONFIG1" },
            { "batas_hari_demam", "CONFIG2" },
            { "pesan_ditolak", "CONFIG3" },
            { "pesan_diterima", "CONFIG4" }
        };
    } 
}