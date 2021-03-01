using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLoadSceneAnim : MonoBehaviour
{
    public GameObject[] prefabs; //prefabs with animations

    // Start is called before the first frame update
    public void Awake()
    {
        Instantiate(prefabs[Random.Range(0, prefabs.Length)], this.gameObject.transform);
    }

    // Update is called once per frame
}
