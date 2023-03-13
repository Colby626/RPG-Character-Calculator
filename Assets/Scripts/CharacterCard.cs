using System.Collections.Generic;
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

    private void Start()
    {
        characterStats = new Dictionary<string, float>();
        characterStats.Add("MaxHealth", maxHealth);
        characterStats.Add("Health", characterStats["MaxHealth"]);
        healthText.text = "Health: " + characterStats["Health"] + "/" + characterStats["MaxHealth"];
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
    }
}