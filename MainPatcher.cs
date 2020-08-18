using System.Reflection;
using SMLHelper.V2.Handlers;
using QModManager.API.ModLoading;
using HarmonyLib;

namespace SubnauticaSnapTurningMod
{
    [QModCore]
    public static class MainPatcher
    {
        [QModPatch]
        public static void Patch()
        {
            Config.Load();
            OptionsPanelHandler.RegisterModOptions(new Options());

            var harmony = new Harmony("com.ethanfischer.subnautica.snapturning.mod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

    }
}
