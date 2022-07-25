using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public Transform position;

    private List<GameObject> items = new List<GameObject>();

    public void Spawn()
    {
        var item = Instantiate(prefab, position.position, position.rotation, transform);
        items.Add(item);
        item.SetActive(true);
    }
}
