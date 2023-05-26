using TMPro;
using UnityEngine;

public class TextSetter : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI txt;
    [SerializeField]private string textToSet;

    public void UpdateText(int newValue)
    {
        txt.text = textToSet + newValue;
    }

    public void NewText(string newText)
    {
        textToSet = newText;
    }
}
