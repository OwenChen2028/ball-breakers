using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float lifeSpan;
    public bool dead;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        StartCoroutine(DoLifeSpan());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator DoLifeSpan()
    {
        yield return new WaitForSeconds(lifeSpan);
        StartCoroutine(DoDeath());
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall")) {
            StartCoroutine(DoDeath());
        }
    }

    public IEnumerator DoDeath()
    {
        if (!dead)
        {
            dead = true;
            GetComponent<Collider2D>().enabled = false;
            anim.SetTrigger("Death");
            yield return new WaitForSeconds(0.6f);
            Destroy(gameObject);
        }
    }
}
