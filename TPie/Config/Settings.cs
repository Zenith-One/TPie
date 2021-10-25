﻿using Dalamud.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using TPie.Models;

namespace TPie.Config
{
    public class Settings
    {
        public List<Ring> Rings = new List<Ring>();

        public bool AppearAtCursor = true;
        public Vector2 CenterPositionOffset = Vector2.Zero;

        public bool UseCustomFont = true;
        public int FontSize = 20;

        public bool DrawRingBackground = true;

        public RingAnimationType AnimationType = RingAnimationType.Spiral;
        public float AnimationDuration = 0.2f;

        public bool AnimateIconSizes = true;

        public bool ShowCooldowns = true;
        public bool ShowRemainingItemCount = true;

        #region load / save
        private static string JsonPath = Path.Combine(Plugin.PluginInterface.GetPluginConfigDirectory(), "Settings.json");
        public static Settings Load()
        {
            string path = JsonPath;
            Settings? settings = null;

            try
            {
                if (File.Exists(path))
                {
                    string jsonString = File.ReadAllText(path);
                    settings = JsonConvert.DeserializeObject<Settings>(jsonString);
                }
            }
            catch (Exception e)
            {
                PluginLog.Error("Error reading settings file: " + e.Message);
            }

            if (settings == null)
            {
                settings = new Settings();
                Save(settings);
            }

            return settings;
        }

        public static void Save(Settings settings)
        {
            try
            {
                JsonSerializerSettings serializerSettings = new JsonSerializerSettings
                {
                    TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                    TypeNameHandling = TypeNameHandling.Objects
                };
                string jsonString = JsonConvert.SerializeObject(settings, Formatting.Indented, serializerSettings);

                File.WriteAllText(JsonPath, jsonString);
            }
            catch (Exception e)
            {
                PluginLog.Error("Error saving settings file: " + e.Message);
            }
        }
        #endregion
    }

    public enum RingAnimationType
    {
        None = 0,
        Spiral = 1,
        Sequential = 2,
        Fade = 3
    }
}
