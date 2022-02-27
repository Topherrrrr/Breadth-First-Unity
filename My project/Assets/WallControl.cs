using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerMover.visited.Add(gameObject.transform.position+ new Vector3(0,.25f,0));
        PlayerMover.blockList.Add(gameObject);
        foreach (Vector3 i in PlayerMover.visited)
        {
            print(i + " is in visited");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
