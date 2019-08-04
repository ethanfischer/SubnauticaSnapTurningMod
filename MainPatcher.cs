using System.Reflection;
using Harmony;

namespace SubnauticaSnapTurningMod
{
    public class MainPatcher
    {
        public static void Patch()
        {
            var harmony = HarmonyInstance.Create("com.oldark.subnautica.acceleratedstart.mod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

    }
}
