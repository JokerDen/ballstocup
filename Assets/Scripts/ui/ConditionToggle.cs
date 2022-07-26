using levels;
using UnityEngine;
using UnityEngine.UI;

public class ConditionToggle : MonoBehaviour
{
    void Start()
    {
        var toggle = GetComponent<Toggle>();
        toggle.isOn = GameLevel.finishOnWin;
        toggle.onValueChanged.AddListener(value => GameLevel.finishOnWin = value);
    }
}
