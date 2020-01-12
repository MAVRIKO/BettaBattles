using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stress : MonoBehaviour
{

    public float stress = 100f;
    public float finHealth = 100f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stress <= 0)
        {
            Debug.Log(gameObject.name + " Died!");
            Destroy(gameObject);
        }
    }
}
