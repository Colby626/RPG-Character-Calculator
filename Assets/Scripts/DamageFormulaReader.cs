using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DamageFormulaReader : MonoBehaviour
{
    public TMP_InputField inputField;
    public CharacterCard attackingCard;
    public int damage;
    public CharacterCard receivingCard;

    private GameObject warningSign;
    private List<string> possibleKeys = new();

    private void Start()
    {
        warningSign = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        float newValue;
        //tryParse the inputfield
        //Compare strings with cardStatVariables
        if (attackingCard.characterStats.TryGetValue(inputField.text, out newValue))
        {
            warningSign.SetActive(false);
        }
        else
        {
            warningSign.SetActive(true);
        }
        //If it matches, grab the value of that variable
        Debug.Log("This is the value of the inputField: " + newValue);
        //If it doesn't, create a warning signal on the inputField
        damage = (int)newValue;
    }
}
