using SMLHelper.V2.Utility;
using UnityEngine;

public static class Config
{
    public static bool ToggleValue;
    public static int ChoiceIndex;

    public static void Load()
    {
        ToggleValue = PlayerPrefsExtra.GetBool("SMLHelperExampleModToggle", true);
        ChoiceIndex = PlayerPrefs.GetInt("SMLHelperExampleModChoice", 0);
    }
}