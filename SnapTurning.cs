using HarmonyLib;
using UnityEngine;
using UnityEngine.XR;

namespace VRTweaks.SnapTurn
{
    public static class SnapTurning
    {
        private static float SnapAngle => SnapTurningOptions.SnapAngles[SnapTurningOptions.SnapAngleChoiceIndex];
        private static bool _didLookRight;
        private static bool _didLookLeft;
        private static bool _isLookingLeft;
        private static bool _isLookingRight;
        private static bool _isLooking;
        private static bool _isLookingUpOrDown;
        private static bool _shouldSnapTurn;
        private static bool _shouldIgnoreLookRightOrLeft;

        public static bool Prefix(MainCameraControl __instance)
        {
            if (!SnapTurningOptions.EnableSnapTurning || Player.main.isPiloting)
            {
                return true; //Enter vanilla method
            }

            UpdateFields();

            if (_isLookingUpOrDown)
            {
                return false; //Disable looking up or down with the joystick
            }

            if (_shouldSnapTurn)
            {
                UpdatePlayerRotation();
                return false; //Don't enter vanilla method if we snap turn
            }

            if (_shouldIgnoreLookRightOrLeft || SnapTurningOptions.DisableMouseLook)
            {
                return false;
            }

            return true;
        }

        private static void UpdateFields()
        {
            _didLookRight = GameInput.GetButtonDown(GameInput.Button.LookRight);
            _didLookLeft = GameInput.GetButtonDown(GameInput.Button.LookLeft); 
            _isLookingLeft = GameInput.GetButtonHeld(GameInput.Button.LookLeft);
            _isLookingRight = GameInput.GetButtonHeld(GameInput.Button.LookRight);
            _isLooking = _didLookLeft || _didLookRight || _isLookingLeft || _isLookingRight;

            _shouldSnapTurn = XRSettings.enabled && _isLooking;
        }

        private static void UpdatePlayerRotation()
        {
            Player.main.transform.localRotation = Quaternion.Euler(GetNewEulerAngles());
        }

        private static Vector3 GetNewEulerAngles()
        {
            var newEulerAngles = Player.main.transform.localRotation.eulerAngles;

            if (_didLookRight)
            {
                newEulerAngles.y += SnapAngle;
            }
            else if (_didLookLeft)
            {
                newEulerAngles.y -= SnapAngle;
            }

            return newEulerAngles;
        }
    }
}