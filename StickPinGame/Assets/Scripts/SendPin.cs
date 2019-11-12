using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendPin : MonoBehaviour
{
    public GameObject pinFrefab;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            pin();
        }
    }

   void pin()
    {
        Instantiate(pinFrefab, transform.position, transform.rotation);
    }
}
