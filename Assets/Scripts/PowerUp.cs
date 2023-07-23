using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject target;

    public float magnetRange;
    public float magnetForce;

    public float lifeSpan;
    public GameObject poofEffect;

    public string type;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DoLifeSpan());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (target != null && !target.GetComponent<PlayerController>().dead && target.GetComponent<PlayerController>().money >= 10)
        {
            if (Vector2.Distance(target.transform.position, transform.position) <= magnetRange)
            {
                Vector2 magnetDirection = target.transform.position - transform.position;
                rb.AddForce(magnetDirection.normalized * magnetForce);
            }
        }
    }

    IEnumerator DoLifeSpan()
    {
        yield return new WaitForSeconds(lifeSpan);
        Destroy(gameObject);
        Instantiate(poofEffect, transform.position, transform.rotation);
    }
}
