using Harmony;
using UnityEngine;
using UnityEngine.VR;

namespace SubnauticaSnapTurningMod
{
    [HarmonyPatch(typeof(MainCameraControl))]
    [HarmonyPatch("Update")]
    internal class MainCameraControlPatcher
    {
        private static float SnapAngle => Config.SnapAngles[Config.SnapAngleChoiceIndex];

        [HarmonyPrefix]
        public static bool Prefix()
        {
            if(!Config.EnableSnapTurning)
            {
                return true; //Enter vanilla method
            }

            var didLookRight = GameInput.GetButtonDown(GameInput.Button.LookRight);
            var didLookLeft = GameInput.GetButtonDown(GameInput.Button.LookLeft);
            var isLookingLeft = GameInput.GetButtonHeld(GameInput.Button.LookLeft);
            var isLookingRight = GameInput.GetButtonHeld(GameInput.Button.LookRight);
            var isLooking = didLookLeft || didLookRight || isLookingLeft || isLookingRight;

            var shouldSnapTurn = Player.main.motorMode != Player.MotorMode.Vehicle && VRSettings.enabled && isLooking;
            if (!shouldSnapTurn)
            {
                return true; //Enter vanilla method as usual
            }

            var newEulerAngles = Player.main.transform.localRotation.eulerAngles;

            if (didLookRight)
            {
                newEulerAngles.y += SnapAngle;
            }
            else if (didLookLeft)
            {
                newEulerAngles.y -= SnapAngle;
            }

            Player.main.transform.localRotation = Quaternion.Euler(newEulerAngles);

            return false; //Don't enter vanilla method
        }
    }
}
