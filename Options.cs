using SMLHelper.V2.Options;
using SMLHelper.V2.Utility;
using UnityEngine;

public class Options : ModOptions
{
    public Options() : base("Snap Turning")
    {
        ToggleChanged += Options_ToggleChanged;
        ChoiceChanged += Options_ChoiceChanged;
    }

    public void Options_ToggleChanged(object sender, ToggleChangedEventArgs e)
    {
        if (e.Id != "exampleToggle") return;
        Config.ToggleValue = e.Value;
        PlayerPrefsExtra.SetBool("SMLHelperExampleModToggle", e.Value);
    }

    public void Options_ChoiceChanged(object sender, ChoiceChangedEventArgs e)
    {
        if (e.Id != "exampleChoice") return;
        Config.ChoiceIndex = e.Index;
        PlayerPrefs.SetInt("SMLHelperExampleModChoice", e.Index);
    }


    public override void BuildModOptions()
    {
        AddToggleOption("exampleToggle", "Enabled", Config.ToggleValue);
        AddChoiceOption("exampleChoice", "Angle", new string[] { "45", "90", "22.5" }, Config.ChoiceIndex);
    }
}