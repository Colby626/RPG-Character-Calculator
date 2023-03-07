using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterCard : MonoBehaviour
{
    public Dictionary<string, float> characterStats;
    public TMP_InputField characterStatInput;

    private void Start()
    {
        characterStats = new Dictionary<string, float>();
        characterStats.Add("Dexterity", 2); //Placeholder for parsing from what the user input
    }
}
