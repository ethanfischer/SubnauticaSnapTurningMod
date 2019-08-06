using SMLHelper.V2.Utility;
using UnityEngine;

public static class Config
{
    public static bool ToggleValue;
    public static int ChoiceIndex;

    public static void Load()
    {
        ToggleValue = PlayerPrefsExtra.GetBool(Options.PLAYER_PREF_KEY_TOGGLE_SNAP_TURNING, true);
        ChoiceIndex = PlayerPrefs.GetInt(Options.PLAYER_PREF_KEY_SNAP_ANGLE, 0);
    }
}