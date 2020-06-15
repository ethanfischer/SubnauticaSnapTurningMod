using SMLHelper.V2.Utility;
using UnityEngine;

public static class Config
{
    public static bool EnableSnapTurning = true;
    public static int SnapAngleChoiceIndex = 0;
    public static float[] SnapAngles = { 45, 90, 22.5f };
    public static KeyCode KeybindKeyLeft;
    public static KeyCode KeybindKeyRight;

    public static void Load()
    {
        EnableSnapTurning = PlayerPrefsExtra.GetBool(Options.PLAYER_PREF_KEY_TOGGLE_SNAP_TURNING, true);
        SnapAngleChoiceIndex = GetSnapAngleChoiceIndex();
        KeybindKeyLeft = PlayerPrefsExtra.GetKeyCode("SMLHelperExampleModKeybindLeft", KeyCode.LeftArrow);
        KeybindKeyRight = PlayerPrefsExtra.GetKeyCode("SMLHelperExampleModKeybindRight", KeyCode.RightArrow);
    }

    private static int GetSnapAngleChoiceIndex()
    {
        var result = PlayerPrefs.GetInt(Options.PLAYER_PREF_KEY_SNAP_ANGLE, 0);
        if (result > SnapAngles.Length)
        {
            result = 0;
        }

        return result;
    }
}