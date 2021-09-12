using Exiled.API.Features;
using Server = Exiled.Events.Handlers.Server;
using Player = Exiled.Events.Handlers.Player;
using Exiled.Loader;
using System;
using YamlDotNet.Serialization;
using System.Collections.Generic;

namespace LeftHandedPlayers
{
    public class LeftHandedPlayers : Plugin<Config>
    {
        private static LeftHandedPlayers Singleton;
        public static LeftHandedPlayers Instance => Singleton;
        public override string Author => "TemmieGamerGuy";
        public override string Name => "LeftHandedPlayers";
        public override Version Version => new Version(1, 0, 1);
        public override Version RequiredExiledVersion => new Version(3, 0, 0);

        internal static string DataPath;
        internal IDeserializer DataLoader;
        internal ISerializer DataSaver;
        internal List<string> LeftHandList;
        internal List<string> ToRemoveList;

        private Handlers.Server server;
        private Handlers.Player player;

        public void RegisterEvents()
        {
            server = new Handlers.Server();
            player = new Handlers.Player();
            Server.RoundEnded += server.OnRoundEnded;
            Server.WaitingForPlayers += server.OnWaitingForPlayers;
            Player.Verified += player.OnVerified;
        }

        public void UnregisterEvents()
        {
            Server.RoundEnded -= server.OnRoundEnded;
            Server.WaitingForPlayers -= server.OnWaitingForPlayers;
            Player.Verified -= player.OnVerified;
            server = null;
            player = null;
        }

        public override void OnEnabled()
        {
            Singleton = this;
            DataPath = Paths.Configs + "/" + "LeftHandedData.yml";
            DataLoader = Loader.Deserializer;
            DataSaver = Loader.Serializer;
            ToRemoveList = new List<string>();
            RegisterEvents();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnregisterEvents();
            DataPath = null;
            DataLoader = null;
            DataSaver = null;
            ToRemoveList = null;
            LeftHandList = null;
            Singleton = null;
            base.OnDisabled();
        }
    }
}
