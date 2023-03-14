using System.Collections.Generic; //For Lists
using System.Linq;
using TMPro;
using UnityEngine;

public class CharacterCard : MonoBehaviour
{
    public DamageFormulaReader damageFormulaReader;
    public Dictionary<string, float> characterStats;
    public TMP_InputField characterStatInput;
    public TMP_InputField health;

    private float healthValue;
    private float maxHealth;
    private RectTransform rect;
    private bool error = false;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        characterStats = new Dictionary<string, float>();

        List<string> healthAmount = health.text.Split("/").ToList();
        if (healthAmount.Count != 2)
        {
            health.transform.GetChild(1).gameObject.SetActive(true);
            return;
        }
        for (int i = 0; i < healthAmount[0].Count(); i++) //If the elements aren't numbers, don't update
        {
            if (!char.IsDigit(healthAmount[0][i]))
            {
                Debug.LogWarning("input number not a number " + healthAmount[0][i]);
                health.transform.GetChild(1).gameObject.SetActive(true);
                return;
            }
        }
        for (int i = 0; i < healthAmount[1].Count(); i++) //If the elements aren't numbers, don't update
        {
            if (!char.IsDigit(healthAmount[1][i]))
            {
                Debug.LogWarning("input number not a number " + healthAmount[1][i]);
                health.transform.GetChild(1).gameObject.SetActive(true);
                return;
            }
        }
        healthValue = float.Parse(healthAmount[0]);
        maxHealth = float.Parse(healthAmount[1]);
        characterStats.Add("MaxHealth", maxHealth);
        characterStats.Add("Health", healthValue);
        health.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void UpdateHealth()
    {
        List<string> healthAmount = health.text.Split("/").ToList();
        if (healthAmount.Count != 2)
        {
            health.transform.GetChild(1).gameObject.SetActive(true);
            return;
        }
        for (int i = 0; i < healthAmount[0].Count(); i++) //If the elements aren't numbers, don't update
        {
            if (!char.IsDigit(healthAmount[0][i]))
            {
                Debug.LogWarning("input number not a number " + healthAmount[0][i]);
                health.transform.GetChild(1).gameObject.SetActive(true);
                return;
            }
        }
        for (int i = 0; i < healthAmount[1].Count(); i++) //If the elements aren't numbers, don't update
        {
            if (!char.IsDigit(healthAmount[1][i]))
            {
                Debug.LogWarning("input number not a number " + healthAmount[1][i]);
                health.transform.GetChild(1).gameObject.SetActive(true);
                return;
            }
        }
        healthValue = float.Parse(healthAmount[0]);
        maxHealth = float.Parse(healthAmount[1]);
        characterStats.Add("MaxHealth", maxHealth);
        characterStats.Add("Health", healthValue);
        health.transform.GetChild(1).gameObject.SetActive(false);
    } //Called from onValueChanged from healthInput

    public void UpdateDictonary(TMP_InputField characterStatInputInstance)
    {
        error = false;
        if (characterStatInputInstance.GetComponent<CharacterStatsInput>().key != null)
        {
            characterStats.Remove(characterStatInputInstance.GetComponent<CharacterStatsInput>().key);
            characterStatInputInstance.GetComponent<CharacterStatsInput>().key = null;
        }
        //Check characterStatInput and take anything starting with the new line character up until a colon as a key and after a space in front of a colon a number is the value
        List<string> splitString = characterStatInputInstance.text.Split(": ").ToList(); //splitString has an element for before the colon and an element for after the colon
        if (splitString.Count != 2)
        {
            characterStatInputInstance.transform.GetChild(0).gameObject.SetActive(true); //show error symbol
            return;
        }
        if (splitString[0] != null)
        {
            for (int i = 0; i < splitString[0].Length - 1; i++) //If the element before the colon isn't only letters, don't update the dictonary
            {
                if (!char.IsLetter(splitString[0][i]))
                {
                    error = true;
                    Debug.LogWarning("input variable not letters " + splitString[0][i]);
                    break;
                }
            }
        }
        else
            error = true;
        if (splitString[1].Count() > 0)
        {
            for (int i = 0; i < splitString[1].Count(); i++) //If the element after the colon isn't a number, don't update the dictonary
            {
                if (!char.IsDigit(splitString[1][i]))
                {
                    error = true;
                    Debug.LogWarning("input number not a number " + splitString[1][i]);
                    break;
                }
            }
        }
        else
            error = true;
        if (!error)
        {
            if (!characterStats.ContainsKey(splitString[0])) //If the dictonary doesn't already contain that key value 
            {
                characterStatInputInstance.transform.GetChild(0).gameObject.SetActive(false); //If no error, remove error symbol
                characterStatInputInstance.GetComponent<CharacterStatsInput>().key = splitString[0];
                characterStats.Add(splitString[0], float.Parse(splitString[1]));
            }
        }
        else
        {
            characterStatInputInstance.transform.GetChild(0).gameObject.SetActive(true); //If error, show error symbol
        }
    } //Called from OnValueChanged on the input field

    public void AddNewVariable()
    {
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y + 25);
        TMP_InputField instance = Instantiate(characterStatInput);
        instance.transform.SetParent(transform.GetChild(1)); //Makes the scale .87 something
        instance.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1); //Sets the scale back to 1
        instance.onValueChanged.AddListener(delegate { UpdateDictonary(instance); });
    } //Called from addVariableButton

    
    public void TakeDamage()
    {
        //take health text, turn it into an int, subtract damage, turn it back into text and display it 
        List<string> healthAmount = health.text.Split("/").ToList();
        healthValue = float.Parse(healthAmount[0]);
        healthValue -= damageFormulaReader.damage;
        characterStats["Health"] = healthValue;
        health.text = characterStats["Health"] + "/" + characterStats["MaxHealth"];
    }

    public void PrintDictonary()
    {
        foreach (string item in characterStats.Keys)
        {
            Debug.Log("Key: " + item + " Value: " + characterStats[item]);   
        }
    }
}