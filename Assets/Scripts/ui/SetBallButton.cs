using gameplay;
using UnityEngine;
using UnityEngine.UI;

public class SetBallButton : MonoBehaviour
{
    [SerializeField] Ball ballPrefab;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            GameManager.current.ballPrefab = ballPrefab;
            GameManager.current.Restart();
        });
    }
}
