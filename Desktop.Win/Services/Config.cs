﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Remotely.Desktop.Win.Services
{
    public class Config
    {
public string Host { get; set; } = "https://quicksupport.avp.de";
        private static string ConfigFile => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AvP Quicksupport", "Config.json");
        private static string ConfigFolder => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AvP Quicksupport");
        public static Config GetConfig()
        {
            if (!Directory.Exists(ConfigFolder))
            {
                return new Config();
            }

            if (File.Exists(ConfigFile))
            {
                try
                {
                    return JsonSerializer.Deserialize<Config>(File.ReadAllText(ConfigFile));
                }
                catch
                {
                    return new Config();
                }
            }
            return new Config();
        }

        public void Save()
        {
            try
            {
                Directory.CreateDirectory(ConfigFolder);
                File.WriteAllText(ConfigFile, JsonSerializer.Serialize(this));
            }
            catch
            {
            }
        }
    }
}
