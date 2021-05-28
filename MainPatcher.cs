using BepInEx;
using HarmonyLib;
using UnityEngine;
using UnityEngine.XR;
using VRTweaks.SnapTurn;

namespace SubnauticaSnapTurningMod
{
	[BepInPlugin(GUID, MODNAME, VERSION)]
	public class MainPatcher : BaseUnityPlugin
	{
		public const string
			MODNAME = "SnapTurning",
			AUTHOR = "ethanfischer",
			GUID = "com.ethanfischer.subnautica.snapturning.mod",
			VERSION = "1.0.0.0";

		private static Harmony harmony = new Harmony(GUID);

		public void Start()
        {
			if(XRSettings.enabled)
            {
				MainPatcher.harmony.Patch(AccessTools.Method(typeof(MainCameraControl), "Update"), prefix: new HarmonyMethod(typeof(SnapTurning).GetMethod(nameof(SnapTurning.Prefix))));
				SnapTurningMenu.Patch();
            }
        }
    }
}
