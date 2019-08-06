using SMLHelper.V2.Utility;
using UnityEngine;

public static class Config
{
    public static bool ToggleValue;
    public static int ChoiceIndex;

    public static void Load()
    {
        ToggleValue = PlayerPrefsExtra.GetBool(Options.PLAYERPREFKEY_TOGGLE_SNAP_TURNING, true);
        ChoiceIndex = PlayerPrefs.GetInt(Options.PLAYERPREFKEY_SNAP_ANGLE, 0);
    }
}