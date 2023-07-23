using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSpan : MonoBehaviour
{
    public float lifeSpan;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DoLifeSpan());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DoLifeSpan()
    {
        yield return new WaitForSeconds(lifeSpan);
        Destroy(gameObject);
    }
}
