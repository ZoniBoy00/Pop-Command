using Oxide.Core.Plugins;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Oxide.Plugins
{
    [Info("Pop Command", "ZoniBoy00", "1.0.0")]
    [Description("Displays server population with cooldown")]

    class PopCommand : RustPlugin
    {
        private Dictionary<ulong, DateTime> lastUsed = new Dictionary<ulong, DateTime>();

        #region Config
        private class PluginConfig
        {
            public int CooldownMs = 10000;
            public string TitleColor = "yellow";
            public string OnlineColor = "green";
            public string SleepingColor = "orange";
            public string MessageFormat =
            @"<color={TitleColor}>Server Pop:</color>
            <color={OnlineColor}>{online}/{maxPlayers}</color> online
            <color={SleepingColor}>{sleeping}</color> sleeping";
        }

        private PluginConfig config;

        protected override void LoadDefaultConfig()
        {
            config = new PluginConfig();
            SaveConfig(); // Write default config to disk
        }

        protected override void LoadConfig()
        {
            base.LoadConfig();
            try
            {
                config = Config.ReadObject<PluginConfig>();
                if (config == null) throw new Exception();
            }
            catch
            {
                PrintWarning("Could not load config, generating default...");
                LoadDefaultConfig();
            }
        }

        protected override void SaveConfig() => Config.WriteObject(config);
        #endregion

        [ChatCommand("pop")]
        void PopCommandHandler(BasePlayer player, string command, string[] args)
        {
            ulong id = player.userID;

            if (lastUsed.TryGetValue(id, out DateTime last) && (DateTime.UtcNow - last).TotalMilliseconds < config.CooldownMs)
            {
                float seconds = (config.CooldownMs - (float)(DateTime.UtcNow - last).TotalMilliseconds) / 1000f;
                SendReply(player, $"<color=red>Wait {seconds:F1}s before using /pop again.</color>");
                return;
            }

            lastUsed[id] = DateTime.UtcNow;

            int online = BasePlayer.activePlayerList.Count;
            int sleeping = BasePlayer.sleepingPlayerList.Count;
            int maxPlayers = ConVar.Server.maxplayers;

            string msg = config.MessageFormat
                .Replace("{TitleColor}", config.TitleColor)
                .Replace("{OnlineColor}", config.OnlineColor)
                .Replace("{SleepingColor}", config.SleepingColor)
                .Replace("{online}", online.ToString())
                .Replace("{maxPlayers}", maxPlayers.ToString())
                .Replace("{sleeping}", sleeping.ToString());

            SendReply(player, msg);
        }
    }
}

