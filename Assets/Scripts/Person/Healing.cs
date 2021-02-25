using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    bool isSpawning;
    public float time2spawn;
    public GameObject kit;
    GameObject actualKit;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        isSpawning = false;
        actualKit = null;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if((!isSpawning) && (actualKit == null))
        {
            StartCoroutine(SpawnKit());
        }
    }
    IEnumerator SpawnKit()
    {
        isSpawning = true;
        yield return new WaitForSeconds(Random.Range(time2spawn - 1f, time2spawn + 1f));
        actualKit = Instantiate(kit, new Vector3(transform.position.x, transform.position.y + 4f, 0), Quaternion.Euler(0, 0, 0), transform);
        isSpawning = false;
    }
}
