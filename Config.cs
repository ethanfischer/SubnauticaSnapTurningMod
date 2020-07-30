using SMLHelper.V2.Utility;
using UnityEngine;

public static class Config
{
    public static bool EnableSnapTurning = true;
    public static bool EnableSeamoth = false;
    public static bool EnablePrawn = false;
    public static int SnapAngleChoiceIndex = 0;
    public static int SeamothAngleChoiceIndex = 0;
    public static int PrawnAngleChoiceIndex = 0;
    public static float[] SnapAngles = { 45, 90, 22.5f };
    public static KeyCode KeybindKeyLeft;
    public static KeyCode KeybindKeyRight;

    public static void Load()
    {
        EnableSnapTurning = PlayerPrefsExtra.GetBool(Options.PLAYER_PREF_KEY_TOGGLE_SNAP_TURNING, true);
        EnableSeamoth = PlayerPrefsExtra.GetBool(Options.PLAYER_PREF_KEY_TOGGLE_SEAMOTH, false);
        EnablePrawn = PlayerPrefsExtra.GetBool(Options.PLAYER_PREF_KEY_TOGGLE_PRAWN, false);
        SnapAngleChoiceIndex = GetSnapAngleChoiceIndex(SnapType.Default);
        SeamothAngleChoiceIndex = GetSnapAngleChoiceIndex(SnapType.Seamoth);
        PrawnAngleChoiceIndex = GetSnapAngleChoiceIndex(SnapType.Prawn);
        KeybindKeyLeft = PlayerPrefsExtra.GetKeyCode("SMLHelperExampleModKeybindLeft", KeyCode.LeftArrow);
        KeybindKeyRight = PlayerPrefsExtra.GetKeyCode("SMLHelperExampleModKeybindRight", KeyCode.RightArrow);
    }

    private static int GetSnapAngleChoiceIndex(SnapType snapType)
    {
        int result = GetChoiceIndexForSnapType(snapType);
        if (result > SnapAngles.Length)
        {
            result = 0;
        }

        return result;
    }

    private static int GetChoiceIndexForSnapType(SnapType snapType)
    {
        int result = 0;
        if (snapType == SnapType.Default)
        {
            result = PlayerPrefs.GetInt(Options.PLAYER_PREF_KEY_SNAP_ANGLE, 0);
        }
        else if (snapType == SnapType.Seamoth)
        {
            result = PlayerPrefs.GetInt(Options.PLAYER_PREF_KEY_TOGGLE_SEAMOTH, 0);
        }
        else if (snapType == SnapType.Prawn)
        {
            result = PlayerPrefs.GetInt(Options.PLAYER_PREF_KEY_TOGGLE_PRAWN, 0);
        }

        return result;
    }
}

public enum SnapType
{
    Default,
    Seamoth,
    Prawn
}