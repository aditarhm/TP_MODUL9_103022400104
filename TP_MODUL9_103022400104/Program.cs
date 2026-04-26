using System;
using System.IO;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        CovidConfig config = new CovidConfig();

       
        config.UbahSatuan();

        
        Console.Write("Berapa suhu badan anda saat ini? ");
        double suhu = Convert.ToDouble(Console.ReadLine());

        
        Console.Write("Berapa hari yang lalu (perkiraan) anda terakhir memiliki gejala demam? ");
        int hari = Convert.ToInt32(Console.ReadLine());

        bool suhuValid = false;
        bool hariValid = hari < config.batas_hari_deman;

        
        if (config.satuan_suhu == "celcius")
        {
            suhuValid = suhu >= 36.5 && suhu <= 37.5;
        }
        else if (config.satuan_suhu == "fahrenheit")
        {
            suhuValid = suhu >= 97.7 && suhu <= 99.5;
        }

        
        if (suhuValid && hariValid)
        {
            Console.WriteLine(config.pesan_diterima);
        }
        else
        {
            Console.WriteLine(config.pesan_ditolak);
        }
    }
}

class CovidConfig
{
    public string satuan_suhu { get; set; }
    public int batas_hari_deman { get; set; }
    public string pesan_ditolak { get; set; }
    public string pesan_diterima { get; set; }

    private string path = "covid_config.json";

    public CovidConfig()
    {
        LoadConfig();
    }

    private void LoadConfig()
    {
        try
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                var data = JsonSerializer.Deserialize<JsonElement>(json);

                satuan_suhu = data.GetProperty("satuan_suhu").GetString();
                batas_hari_deman = int.Parse(data.GetProperty("batas_hari_deman").GetString());
                pesan_ditolak = data.GetProperty("pesan_ditolak").GetString();
                pesan_diterima = data.GetProperty("pesan_diterima").GetString();
            }
            else
            {
                SetDefault();
                SaveConfig();
            }
        }
        catch
        {
            SetDefault();
        }
    }

    private void SetDefault()
    {
        satuan_suhu = "celcius";
        batas_hari_deman = 14;
        pesan_ditolak = "Anda tidak diperbolehkan masuk ke dalam gedung ini";
        pesan_diterima = "Anda dipersilahkan untuk masuk ke dalam gedung ini";
    }

    private void SaveConfig()
    {
        var obj = new
        {
            satuan_suhu,
            batas_hari_deman = batas_hari_deman.ToString(),
            pesan_ditolak,
            pesan_diterima
        };

        string json = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(path, json);
    }

    
    public void UbahSatuan()
    {
        if (satuan_suhu == "celcius")
        {
            satuan_suhu = "fahrenheit";
        }
        else
        {
            satuan_suhu = "celcius";
        }

        SaveConfig(); 
    }
}