using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.IRCClient
{
    //{"_total":1,"rooms":[{"_id":"1a1bd140-55b3-4a63-815c-077b094ffba1","owner_id":"30773965","name":"bot","topic":"Все операции с ботом тут","is_previewable":true,"minimum_allowed_role":"EVERYONE"}]}
    [Serializable]
    public class RoomModels
    {
        [JsonProperty("_total")]
        public int TotalRooms = 0;
        public List<Room> Rooms = new List<Room> { };
    }

    [Serializable]
    public class Room
    {
        [JsonProperty("_id")]
        public string Id = "";
        [JsonProperty("owner_id")]
        public string ChannelId = "";
        [JsonProperty("name")]
        public string ChannelName = "";
        [JsonProperty("topic")]
        public string Description = "";
        [JsonProperty("is_previewable")]
        public bool IsPreviewable = true;
        [JsonProperty("minimum_allowed_role")]
        public string MinimumRole = "EVERYONE";

        public override string ToString()
        {
            return $"{ChannelId}:{Id}:#{ChannelName}";
        }
    }
}
