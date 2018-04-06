using RazorwingGL.Framework.Platform;

namespace DustModule
{
    public class DustConfig : RazorwingGL.Framework.Configuration.ConfigManager<DustConfigEnum>
    {
        protected override string Filename => "Dust.cfg";

        public DustConfig(Storage s) : base(s)
        {

        }

        protected override void InitialiseDefaults()
        {
            Set(DustConfigEnum.GainTime, 600, 20, 99999);
            Set(DustConfigEnum.SubMul, 2, 1, 100);
        }

    }

    public enum DustConfigEnum
    {
        GainTime,
        SubMul,
    }
}
