using Harmony;
using UnityEngine;
using UnityEngine.VR;

namespace SubnauticaSnapTurningMod
{
    [HarmonyPatch(typeof(MainCameraControl))]
    [HarmonyPatch("Update")]
    internal class MainCameraControlPatcher
    {
        const float SNAP_AMOUNT = 45f;

        [HarmonyPrefix]
        public static bool Prefix()
        {
            bool shouldSnapTurn = Player.main.motorMode != Player.MotorMode.Vehicle && VRSettings.enabled;
            if (!shouldSnapTurn)
            {
                return true; //Enter vanilla method as usual
            }

            var newEulerAngles = Player.main.transform.localRotation.eulerAngles;

            if (GameInput.GetButtonDown(GameInput.Button.LookRight))
            {
                newEulerAngles.y += SNAP_AMOUNT;
            }
            else if (GameInput.GetButtonDown(GameInput.Button.LookLeft))
            {
                newEulerAngles.y -= SNAP_AMOUNT;
            }

            Player.main.transform.localRotation = Quaternion.Euler(newEulerAngles);

            return false; //Don't enter vanilla method
        }
    }
}
