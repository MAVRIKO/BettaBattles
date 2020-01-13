using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stress : MonoBehaviour
{

    public float stress = 100f;
    public float finHealth = 100f;
    public float fSMultiplier = 1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (finHealth >= 100f)
        {
            fSMultiplier = 1f;
        }
        else if(finHealth >= 75f && finHealth < 100f)
        {
            fSMultiplier = 0.9f;
        }
        else if(finHealth >=50f && finHealth < 75f)
        {
            fSMultiplier = 0.8f;
        }
        else if(finHealth >= 25f && finHealth < 50f)
        {
            fSMultiplier = 0.7f;
        }
        else if(finHealth >= 0f && finHealth < 25f)
        {
            fSMultiplier = 0.6f;
        }
        else if(finHealth < 0f)
        {
            finHealth = 0f;
            fSMultiplier = 0.6f;
        }

        if (stress <= 0)
        {
            Debug.Log(gameObject.name + " Died!");
            Destroy(gameObject);
        }
    }
}
