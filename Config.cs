using SMLHelper.V2.Utility;
using UnityEngine;

public static class Config
{
    public static bool EnableSnapTurning = true;
    public static int SnapAngleChoiceIndex = 0;
    public static float[] SnapAngles = { 45, 90, 22.5f };

    public static void Load()
    {
        EnableSnapTurning = PlayerPrefsExtra.GetBool(Options.PLAYER_PREF_KEY_TOGGLE_SNAP_TURNING, true);
        SnapAngleChoiceIndex = PlayerPrefs.GetInt(Options.PLAYER_PREF_KEY_SNAP_ANGLE, 0);
    }
}