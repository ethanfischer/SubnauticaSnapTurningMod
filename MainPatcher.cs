using System.Reflection;
using SMLHelper.V2.Handlers;
using BepInEx;
using HarmonyLib;

namespace SubnauticaSnapTurningMod
{
    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class MainPatcher : BaseUnityPlugin
    {
        public const string
            MODNAME = "SnapTurning",
            AUTHOR = "ethanfischer",
            GUID = "com.ethanfischer.subnautica.snapturning.mod",
            VERSION = "1.3.1";

        private static Harmony harmony = new Harmony(GUID);

        public void Awake()
        {
            SnapTurningConfig.Load();
            OptionsPanelHandler.RegisterModOptions(new Options());

            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
