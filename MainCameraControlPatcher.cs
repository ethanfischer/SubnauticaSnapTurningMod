using Harmony;
using SMLHelper.V2.Utility;
using UnityEngine;
using UnityEngine.XR;

namespace SubnauticaSnapTurningMod
{
    [HarmonyPatch(typeof(MainCameraControl))]
    [HarmonyPatch("Update")]
    internal class MainCameraControlPatcher
    {
        private static float SnapAngle => Config.SnapAngles[Config.SnapAngleChoiceIndex];
        private static bool IsInSeamothOrPrawnSuit => Player.main.motorMode != Player.MotorMode.Vehicle;

        [HarmonyPrefix]
        public static bool Prefix()
        {
            if (!Config.EnableSnapTurning)
            {
                return true; //Enter vanilla method
            }

            var didLookRight = GameInput.GetButtonDown(GameInput.Button.LookRight) || KeyCodeUtils.GetKeyDown(Config.KeybindKeyRight);
            var didLookLeft = GameInput.GetButtonDown(GameInput.Button.LookLeft) || KeyCodeUtils.GetKeyDown(Config.KeybindKeyLeft);
            var isLookingLeft = GameInput.GetButtonHeld(GameInput.Button.LookLeft);
            var isLookingRight = GameInput.GetButtonHeld(GameInput.Button.LookRight);
            var isLooking = didLookLeft || didLookRight || isLookingLeft || isLookingRight;

            var shouldSnapTurn = /*Player.main.motorMode != Player.MotorMode.Vehicle &&*/ XRSettings.enabled && isLooking;
            if (!shouldSnapTurn)
            {
                return Config.EnableMouseLook; //Enter vanilla if mouse look enabled
            }

            UpdatePlayerOrVehicleRotation(didLookRight, didLookLeft);

            return false; //Don't enter vanilla method
        }

        private static Vector3 GetNewEulerAngles(bool didLookRight, bool didLookLeft)
        {
            var newEulerAngles = IsInSeamothOrPrawnSuit
                ? Player.main.currentMountedVehicle.transform.localRotation.eulerAngles
                : Player.main.transform.localRotation.eulerAngles;

            if (didLookRight)
            {
                newEulerAngles.y += SnapAngle;
            }
            else if (didLookLeft)
            {
                newEulerAngles.y -= SnapAngle;
            }

            return newEulerAngles;
        }

        private static void UpdatePlayerOrVehicleRotation(bool didLookRight, bool didLookLeft)
        {
            var newEulerAngles = GetNewEulerAngles(didLookRight, didLookLeft);

            if (IsInSeamothOrPrawnSuit)
            {
                Player.main.currentMountedVehicle.transform.localRotation = Quaternion.Euler(newEulerAngles);
            }
            else
            {
                Player.main.transform.localRotation = Quaternion.Euler(newEulerAngles);
            }
        }
    }
}
