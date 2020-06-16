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
        private static float SeamothSnapAngle => Config.SnapAngles[Config.SeamothAngleChoiceIndex];
        private static float PrawnSnapAngle => Config.SnapAngles[Config.PrawnAngleChoiceIndex];
        private static bool IsInPrawnSuit => Player.main.inExosuit;
        private static bool IsInSeamoth => Player.main.inSeamoth; 

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

            var shouldSnapTurn = XRSettings.enabled && isLooking;
            if (shouldSnapTurn)
            {
                UpdatePlayerOrVehicleRotation(didLookRight, didLookLeft);
                return false; //Don't enter vanilla method if we snap turn
            }

            return Config.EnableMouseLook; //Enter vanilla method if mouse look is enabled
        }

        private static void UpdatePlayerOrVehicleRotation(bool didLookRight, bool didLookLeft)
        {
            var newEulerAngles = GetNewEulerAngles(didLookRight, didLookLeft);

            if (IsInSeamoth && Config.EnableSeamoth)
            {
                Player.main.currentMountedVehicle.transform.localRotation = Quaternion.Euler(newEulerAngles);
            }
            else if (IsInPrawnSuit && Config.EnablePrawn)
            {
                Player.main.currentMountedVehicle.transform.localRotation = Quaternion.Euler(newEulerAngles);
            }
            else
            {
                Player.main.transform.localRotation = Quaternion.Euler(newEulerAngles);
            }
        }

        private static Vector3 GetNewEulerAngles(bool didLookRight, bool didLookLeft)
        {
            var isInVehicle = IsInSeamoth || IsInPrawnSuit;
            var newEulerAngles = isInVehicle
                ? Player.main.currentMountedVehicle.transform.localRotation.eulerAngles
                : Player.main.transform.localRotation.eulerAngles;

            var angle = GetSnapAngle();

            if (didLookRight)
            {
                newEulerAngles.y += angle;
            }
            else if (didLookLeft)
            {
                newEulerAngles.y -= angle;
            }

            return newEulerAngles;
        }

        private static float GetSnapAngle()
        {
            var snapAngle = SnapAngle;
            if (IsInSeamoth)
            {
                snapAngle = SeamothSnapAngle;
            }
            else if (IsInPrawnSuit)
            {
                snapAngle = PrawnSnapAngle;
            }

            return snapAngle;
        }
    }
}
