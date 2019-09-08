using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] groups;
    // Start is called before the first frame update
    void Start()
    {
        spawnerNext();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnerNext()
    {
        int rand = Random.Range(0, groups.Length);

        Instantiate(groups[rand], transform.position, Quaternion.identity);
    }
}
