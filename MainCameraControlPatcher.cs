﻿using Harmony;
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
            var didLookRight = GameInput.GetButtonDown(GameInput.Button.LookRight);
            var didLookLeft = GameInput.GetButtonDown(GameInput.Button.LookLeft);
            var didLook = didLookLeft || didLookRight;

            var shouldSnapTurn = Player.main.motorMode != Player.MotorMode.Vehicle && VRSettings.enabled && didLook;
            if (!shouldSnapTurn)
            {
                return true; //Enter vanilla method as usual
            }

            var newEulerAngles = Player.main.transform.localRotation.eulerAngles;

            if (didLookRight)
            {
                newEulerAngles.y += SNAP_AMOUNT;
            }
            else if (didLookLeft)
            {
                newEulerAngles.y -= SNAP_AMOUNT;
            }

            Player.main.transform.localRotation = Quaternion.Euler(newEulerAngles);

            return false; //Don't enter vanilla method
        }
    }
}
