using UnityEngine;
using UnityEngine.UI;

namespace ui
{
    public class TogglerButton : MonoBehaviour
    {
        [SerializeField] GameObject target;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(ToggleActivity);
        }

        private void ToggleActivity()
        {
            target.SetActive(!target.activeSelf);
        }
    }
}