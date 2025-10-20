using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumberPad : MonoBehaviour
{
    public string codeNumber;
    public int inputLength = 4;
    public string currentInput = "";
    public TextMeshProUGUI inputText;
    public TextMeshProUGUI screenText;
    public GameObject KeyCard;
    public Transform keyTransform;
    private GameObject instKeyCard;


    void Update()
    {
        inputText.text = currentInput;
        if (currentInput.Length == inputLength)
        {
            if (currentInput == codeNumber)
            {
                screenText.text = "Valid Code";
                Debug.Log("Valid Code");
                if (instKeyCard == null && KeyCard != null)
                {
                    instKeyCard = Instantiate(KeyCard, keyTransform.position, keyTransform.rotation);
                }
            }
            else
            {
                screenText.text = "Invalid Code";
                Debug.Log("Invalid Code");
            }
            currentInput = "";
        }
        else if (currentInput.Length > inputLength)
        {
            screenText.text = "Long Code";
            Debug.Log("Long Code");
            currentInput = "";
        }
    }
}
