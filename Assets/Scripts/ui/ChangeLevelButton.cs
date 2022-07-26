using UnityEngine;
using UnityEngine.UI;

public class ChangeLevelButton : MonoBehaviour
{
    [SerializeField] int offset;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => GameManager.current.ChangeLevel(offset));
    }
}
