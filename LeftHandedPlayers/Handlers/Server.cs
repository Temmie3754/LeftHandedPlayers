using System.Collections.Generic;
using System.Linq;
using Exiled.Events.EventArgs;
using System.IO;

namespace LeftHandedPlayers.Handlers
{
    class Server
    {
        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            // Reads saved list of left handed people in case of servers on other ports having updated the list
            FileStream DataFile = new FileStream(LeftHandedPlayers.DataPath, FileMode.Open);
            using (var reader = new StreamReader(DataFile))
            {
                List<string> ReadList;
                if (DataFile.Length == 0) ReadList = new List<string>();
                else ReadList = LeftHandedPlayers.Instance.DataLoader.Deserialize<List<string>>(reader);

                // Combines the two lists, removing duplicates and players on the removal list
                LeftHandedPlayers.Instance.LeftHandList = LeftHandedPlayers.Instance.LeftHandList.Union(ReadList).Where(id => !LeftHandedPlayers.Instance.ToRemoveList.Contains(id)).ToList();
            }

            // Serializes the data and writes it to the file
            using (var writer = new StreamWriter(LeftHandedPlayers.DataPath))
            {
                LeftHandedPlayers.Instance.DataSaver.Serialize(writer, LeftHandedPlayers.Instance.LeftHandList);
            }
        }

        public void OnWaitingForPlayers()
        {
            // Loads the list of left-handed players
            FileStream DataFile = new FileStream(LeftHandedPlayers.DataPath, FileMode.OpenOrCreate);
            using (var reader = new StreamReader(DataFile))
            {
                // Checks if file is empty to ensure data is not deserialized incorrectly
                if (DataFile.Length == 0) LeftHandedPlayers.Instance.LeftHandList = new List<string>();
                else LeftHandedPlayers.Instance.LeftHandList = LeftHandedPlayers.Instance.DataLoader.Deserialize<List<string>>(reader);
            }
        }
    }
}
