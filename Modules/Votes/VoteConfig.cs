using RazorwingGL.Framework.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteModule
{
    public class VoteConfig : RazorwingGL.Framework.Configuration.ConfigManager<VoteConfigs>
    {
        protected override string Filename => @"vote.cfg";

        protected override void InitialiseDefaults()
        {
            //Set(BotSetting.ThreadSleepBeforeSend, 1000, 0);
            //Set(BotSetting.CommandDelay, 2000, 0);
            ////Set(BotSetting.MailDelay, 60000, 0);
            ////Set(BotSetting.MailShowtime, 10000, 1000);
            //Set(BotSetting.Channel, "#redcrafting");
            ////Set(BotSetting.AllowMailMod, true);
            ////Set(BotSetting.AllowMailSub, true);
            ////Set(BotSetting.AllowMailFollow, false);
            ////Set(BotSetting.AllowMentionDefault, true);
            //Set(BotSetting.DustGainTime, 10f, 0.01f);
            //Set(BotSetting.DustSubMultiply, 2, 1);
            Set(VoteConfigs.FollowersCanStart, true);
            Set(VoteConfigs.FollowersCanVote, true);
            Set(VoteConfigs.MaxTimeLimit, 600, 30);
            Set(VoteConfigs.FollowerStartCost, 10, 0, 100);
        }

        public VoteConfig(Storage storage)
            : base(storage)
        {
        }

    }

    public enum VoteConfigs
    {
        FollowersCanStart,
        FollowersCanVote,
        MaxTimeLimit,
        FollowerStartCost,
    }
}
