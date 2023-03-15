using UnityEngine;
using TMPro;
using System.Collections.Generic;
// Expression Evaluator converted from (c) Peter Kankowski, 2007. 

public class DamageFormulaReader : MonoBehaviour
{
    public TMP_InputField inputField;
    public CharacterCard attackingCard;
    public int damage;
    public CharacterCard receivingCard;
    public CharacterCard characterCardTemplate;
    public Vector2 instantiateSpot;
	public enum ERROR_CODES 
    {
        NO_ERROR = 0,
        PARENTHESIS = 1,
        WRONG_CHAR = 2,
        DIVIDE_BY_ZERO = 3
    };

	private ERROR_CODES errorCode;
	private int errorPosition;
	private int parenthesisCount = 0;
    private float newValue;
    private GameObject warningSign;
    private List<string> possibleKeys = new();

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

	// Parse a number or an expression in parenthesis
	private double ParseAtom(string expression, int index)
	{
		// Skip spaces
		while (expression[index] == ' ')
			index++;

		// Handle the sign before parenthesis (or before number) 
		bool negative = false;
		if (expression[index] == '-')
		{
			negative = true;
			index++;
		}
		if (expression[index] == '+')
		{
			index++;
		}

		// Check if there is parenthesis
		if (expression[index] == '(')
		{
			index++;
			parenthesisCount++;
			double result = ParseSummands(expression, index);
			if (expression[index] != ')')
			{
				// Unmatched opening parenthesis
				errorCode = ERROR_CODES.PARENTHESIS;
				errorPosition = index;
				return 0;
			}
			index++;
			parenthesisCount--;
            return negative ? -result : result;
		}
        else
        {
            // It should be a number; convert it to double
            double? result = expression[index];
            if (result == null)
            {
                // Report error
                errorCode = ERROR_CODES.WRONG_CHAR;
                errorPosition = index;
                return 0;
            }
            else
            {
                // Advance the pointer and return the result
                index++;
		        return negative ? (double)-result : (double)result;
            }
        }
    }

    // Parse multiplication and division
    private double ParseFactors(string expression, int index)
    {
        double num1 = ParseAtom(expression, index);
        for (; ; )
        {
            // Skip spaces
            while (expression[index] == ' ')
                index++;
            // Save the operation and position
            char operation = expression[index];
            int position = index;
            if (operation != '/' && operation != '*')
                return num1;
            index++;
            double num2 = ParseAtom(expression, index);
            // Perform the saved operation
            if (operation == '/')
            {
                // Handle division by zero
                if (num2 == 0)
                {
                    errorCode = ERROR_CODES.DIVIDE_BY_ZERO;
                    errorPosition = index;
                    return 0;
                }
                num1 /= num2;
            }
            else
                num1 *= num2;
        }
    }

    // Parse addition and subtraction
    private double ParseSummands(string expression, int index)
    {
        double num1 = ParseFactors(expression, index);
        for (; ; )
        {
            // Skip spaces
            while (expression[index] == ' ')
                index++;
            char operation = expression[index];
            if (operation != '-' && operation != '+')
                return num1;
            index++;
            double num2 = ParseFactors(expression, index);
            if (operation == '-')
                num1 -= num2;
            else
                num1 += num2;
        }
    }

    public void Evaluate(string expression)
    {
        parenthesisCount = 0;
        errorCode = ERROR_CODES.NO_ERROR;
        int index = 0;
        double result = ParseSummands(expression, index);
        // Now, expr should point to '\0', and _paren_count should be zero
        if (parenthesisCount != 0 || expression[index] == ')')
        {
            errorCode = ERROR_CODES.PARENTHESIS;
            errorPosition = index;
            //return 0;
            Debug.Log(errorCode);
        }
        if (expression[index] != '\0')
        {
            errorCode = ERROR_CODES.WRONG_CHAR;
            errorPosition = index;
            //return 0;
            Debug.Log(errorCode);
        }
        //return result;
        Debug.Log(result);
    }

    public ERROR_CODES GetError()
    {
        return errorCode;
    }

    public int GetErrPosition()
    {
        return errorPosition;
    }

    public void AddNewCard()
    {
        CharacterCard instance = Instantiate(characterCardTemplate, instantiateSpot, Quaternion.identity, transform.parent);
        instantiateSpot = new Vector2(instantiateSpot.x + 130f, instantiateSpot.y); //+130f will be removed when dragging the cards works
    } //Called from button
}
