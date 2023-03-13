using System; //For Char
using System.Collections.Generic; //For Lists
using System.Linq;
using TMPro;
using UnityEngine;

public class CharacterCard : MonoBehaviour
{
    public DamageFormulaReader damageFormulaReader;
    public Dictionary<string, float> characterStats;
    public TMP_InputField characterStatInput;
    public TMP_Text healthText;
    public int maxHealth;

    private RectTransform rect;
    private GameObject addVariableButton;
    private bool error = false;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        addVariableButton = transform.GetChild(0).gameObject;
        characterStats = new Dictionary<string, float>();
        characterStats.Add("MaxHealth", maxHealth);
        characterStats.Add("Health", characterStats["MaxHealth"]);
        healthText.text = "Health: " + characterStats["Health"] + "/" + characterStats["MaxHealth"];
    }

    public void UpdateDictonary() //instead of the biggest for and a giant input field, do a button that adds more input fields in a layout group and then for the number of input fields in the layout group do the check
    {
        //Check characterStatInput and take anything starting with the new line character up until a colon as a key and after a space in front of a colon a number is the value
        List<string> splitStrings = characterStatInput.text.Split("\n").ToList(); //splitStrings has an element for every line of input
        for (int j = 0; j < splitStrings.Count; j++)
        {
            List<string> splitString = splitStrings[j].Split(": ").ToList(); //splitString has an element for before the colon and an element for after the colon
            for (int i = 0; i < splitString[0].Length - 1; i++) //If the element before the colon isn't only letters, don't update the dictonary
            {
                if (!char.IsLetter(splitString[0][i]))
                {
                    error = true;
                    Debug.LogWarning("input variable not letters " + splitString[0][i]);
                    break;
                }
            }
            for (int i = 0; i < splitString[1].Length; i++) //If the element after the colon isn't a number, don't update the dictonary
            {
                if (!char.IsDigit(splitString[1][i]))
                {
                    error = true;
                    Debug.LogWarning("input number not a number " + splitString[1][i]);
                    break;
                }
            }
            if (!error)
            {
                if (!characterStats.ContainsKey(splitString[0])) //If the dictonary doesn't already contain that key value 
                {
                    characterStats.Add(splitString[0], float.Parse(splitString[1]));
                    Debug.Log("Key: '" + splitString[0] + "' Value '" + splitString[1] + "'");
                }
            }
            error = false;
        }
    } //Called from OnValueChanged on the input field

    public void AddNewVariable()
    {
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y + 25);
        TMP_InputField instance = Instantiate(characterStatInput);
        instance.transform.SetParent(transform.GetChild(1)); //Makes the scale .87 something
        instance.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1); //Sets the scale back to 1
    } //Called from addVariableButton

    public void RemoveVariable(GameObject caller)
    {
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y - 25);
        Destroy(caller.transform.parent.gameObject);
    }

    /*
    public void TakeDamage()
    {
        //take health text, turn it into an int, subtract damage, turn it back into text and display it 
        List<string> health = healthText.Split("/").ToList();
        Debug.Log("damageFormulaReader.damage = " + damageFormulaReader.damage);
        float newHealth = int.Parse(health[0]) - damageFormulaReader.damage;
        healthText = newHealth.ToString() + "/" + health[1];
    }
    */
    public void TakeDamage()
    {
        characterStats["Health"] -= damageFormulaReader.damage;
        healthText.text = "Health: " + characterStats["Health"] + "/" + characterStats["MaxHealth"];
    } //Called from attack button 
}