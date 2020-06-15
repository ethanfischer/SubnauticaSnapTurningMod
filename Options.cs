using SMLHelper.V2.Options;
using SMLHelper.V2.Utility;
using UnityEngine;

public class Options : ModOptions
{
    public const string PLAYER_PREF_KEY_TOGGLE_SNAP_TURNING = "SnapTurningTogglePlayerPrefKey";
    public const string PLAYER_PREF_KEY_SNAP_ANGLE = "SnapAnglePlayerPrefKey";
    private const string TOGGLE_CHANGED_ID_SNAP_TURNING = "SnapTurningId";
    private const string CHOICE_CHANGED_ID_SNAP_ANGLE = "SnapAngleId";

    public Options() : base("Snap Turning")
    {
        ToggleChanged += Options_ToggleChanged;
        ChoiceChanged += Options_ChoiceChanged;
        KeybindChanged += Options_KeybindChanged;
    }

    public void Options_ToggleChanged(object sender, ToggleChangedEventArgs e)
    {
        if (e.Id != TOGGLE_CHANGED_ID_SNAP_TURNING) return;
        Config.EnableSnapTurning = e.Value;
        PlayerPrefsExtra.SetBool(PLAYER_PREF_KEY_TOGGLE_SNAP_TURNING, e.Value);
    }

    public void Options_ChoiceChanged(object sender, ChoiceChangedEventArgs e)
    {
        if (e.Id != CHOICE_CHANGED_ID_SNAP_ANGLE) return;
        Config.SnapAngleChoiceIndex = e.Index;
        PlayerPrefs.SetInt(PLAYER_PREF_KEY_SNAP_ANGLE, e.Index);
    }

    public void Options_KeybindChanged(object sender, KeybindChangedEventArgs e)
    {
        if (e.Id == "exampleKeybindLeft")
        {
            Config.KeybindKeyLeft = e.Key;
            PlayerPrefsExtra.SetKeyCode("SMLHelperExampleModKeybindLeft", e.Key);
        }
        if (e.Id == "exampleKeybindRight")
        {
            Config.KeybindKeyRight = e.Key;
            PlayerPrefsExtra.SetKeyCode("SMLHelperExampleModKeybindRight", e.Key);
        }
    }

    public override void BuildModOptions()
    {
        AddToggleOption(TOGGLE_CHANGED_ID_SNAP_TURNING, "Enabled", Config.EnableSnapTurning);
        AddChoiceOption(CHOICE_CHANGED_ID_SNAP_ANGLE, "Angle", new string[] { "45", "90", "22.5" }, Config.SnapAngleChoiceIndex);
        AddKeybindOption("exampleKeybindLeft", "Look Left", GameInput.Device.Keyboard, Config.KeybindKeyLeft);
        AddKeybindOption("exampleKeybindRight", "Look Right", GameInput.Device.Keyboard, Config.KeybindKeyRight);
    }
}