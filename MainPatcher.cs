using System.Reflection;
using Harmony;
using SMLHelper.V2.Handlers;

namespace SubnauticaSnapTurningMod
{
    public class MainPatcher
    {
        public static void Patch()
        {
            Config.Load();
            OptionsPanelHandler.RegisterModOptions(new Options());

            var harmony = HarmonyInstance.Create("com.ethanfischer.subnautica.snapturning.mod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

    }
}
