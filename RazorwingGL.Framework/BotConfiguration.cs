using RazorwingGL.Framework.Platform;

namespace TwitchBot.Config
{
    public class BotConfiguration : RazorwingGL.Framework.Configuration.ConfigManager<BotSetting>
    {
        protected override string Filename => @"bot.cfg";

        protected override void InitialiseDefaults()
        {
            Set(BotSetting.ThreadSleepBeforeSend, 1000, 0);
            Set(BotSetting.CommandDelay, 2000, 0);
            Set(BotSetting.Channel, "#redcrafting");
            Set(BotSetting.DustGainTime, 10f, 0.01f);
            Set(BotSetting.DustSubMultiply, 2, 1);
            Set(BotSetting.Username, "");
            Set(BotSetting.SuperSecretSettings, "");
            Set(BotSetting.BotChatRoom, "#chatrooms:30773965:1a1bd140-55b3-4a63-815c-077b094ffba1");
        }

        public BotConfiguration(Storage storage)
            : base(storage)
        {
        }
    }



    public enum BotSetting
    {
        ThreadSleepBeforeSend,
        CommandDelay,
        Channel,

        ///Dust
        DustGainTime,
        DustSubMultiply,
        Username,
        SuperSecretSettings,
        BotChatRoom,
    }
}