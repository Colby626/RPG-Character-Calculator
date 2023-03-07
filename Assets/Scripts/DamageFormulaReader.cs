using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DamageFormulaReader : MonoBehaviour
{
    public TMP_InputField inputField;
    public CharacterCard attackingCard;
    public CharacterCard receivingCard;
    public GameObject warningSign;

    private bool error = false;
    private List<string> possibleKeys = new();

    private void Update()
    {
        float newValue;
        //tryParse the inputfield
        //Compare strings with cardStatVariables
        if (attackingCard.characterStats.TryGetValue(inputField.text, out newValue))
        {
            Debug.Log("It has your input");
        }
        //If it matches, grab the value of that variable
        Debug.Log("This is the value of the inputField: " + newValue);
        //If it doesn't, create a warning signal on the inputField
        if (error)
        {
            warningSign.SetActive(true);
        }
        else
        {
            warningSign.SetActive(false);
        }
    }
}
