using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDemo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        var ray = Physics2D.OverlapCircle(transform.position, 3f);
        Debug.Log(ray.name);
    }
    
}
