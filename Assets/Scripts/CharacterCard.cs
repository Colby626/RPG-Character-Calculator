using System.Collections.Generic;
using System.Linq;
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

    public void TryAddInputToDictonary()
    {
        //Check characterStatInput and take anything starting with the new line character up until a colon as a key and after a space in front of a colon a number is the value
        List<string> splitString = characterStatInput.text.Split(": ").ToList();
        if (!characterStats.ContainsKey(splitString[0]))
        {
            characterStats.Add(splitString[0], float.Parse(splitString[1]));
            Debug.Log("Key: '" + splitString[0] + "' Value '" + splitString[1] + "'");
        }
    } //Called from OnValueChanged on the input field
}
