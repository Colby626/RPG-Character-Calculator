using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DamageFormulaReader : MonoBehaviour
{
    public TMP_InputField inputField;
    public CharacterCard attackingCard;
    public int damage;
    public CharacterCard receivingCard;
    public CharacterCard characterCardTemplate;

    private float newValue;
    private GameObject warningSign;
    private List<string> possibleKeys = new();
    private Vector2 instantiateSpot;

    private void Start()
    {
        warningSign = transform.GetChild(0).gameObject;
        instantiateSpot = new Vector2(320f, 420f);
    }

    private void Update()
    {
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
        //If it doesn't, create a warning signal on the inputField
        damage = (int)newValue;
    }

    public void AddNewCard()
    {
        CharacterCard instance = Instantiate(characterCardTemplate, instantiateSpot, Quaternion.identity, transform.parent);
        instantiateSpot = new Vector2(instantiateSpot.x + 130f, instantiateSpot.y);
    } //Called from button
}
