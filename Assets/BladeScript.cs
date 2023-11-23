using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate (-Vector3.forward * 1000 * Time.deltaTime);
        // move blade forward and bavkward automatically using ping pong.
        transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * 2, 2), transform.position.z);
    }
}
