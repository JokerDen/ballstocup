using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextFormat : MonoBehaviour
{
    private string format;
    private bool inited;
    private Text textField;

    public void Show(params object[] texts)
    {
        if (!inited)
        {
            textField = GetComponent<Text>();
            format = textField.text;
            inited = true;
        }

        textField.text = string.Format(format, texts);
    }
}
