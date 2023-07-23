using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject target;
    
    private Vector2 moveDirection;
    public float moveForce;

    public GameObject drop;

    public bool ranged;
    public GameObject projectile;
    public float throwForce;
    private bool throwing;

    public bool dead;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();

        if (ranged)
        {
            InvokeRepeating("StartThrowing", 2.0f, 2.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && !target.GetComponent<PlayerController>().dead && !throwing)
        {
            moveDirection = target.transform.position - transform.position;
            anim.SetBool("Moving", true);
        }
        else
        {
            moveDirection = Vector2.zero;
            anim.SetBool("Moving", false);
        }

        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector2(1, 1);
        }

        transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
    }
    void FixedUpdate()
    {
        if (!dead)
        {
            rb.AddForce(moveDirection.normalized * moveForce);
        }
    }
    void StartThrowing() {
        if (!dead && target != null && !target.GetComponent<PlayerController>().dead)
        {
            StartCoroutine(DoThrow());
        }
    }

    public IEnumerator DoThrow()
    {
        throwing = true;
        anim.SetTrigger("Kick");
        Vector2 throwDirection = target.transform.position - transform.position;
        GameObject instance = Instantiate(projectile, transform.position, transform.rotation);
        instance.transform.up = throwDirection;
        instance.GetComponent<Rigidbody2D>().AddForce(throwDirection.normalized * throwForce);
        yield return new WaitForSeconds(0.6f);
        throwing = false;

    }

    public IEnumerator DoDeath()
    {
        dead = true;
        GetComponent<Collider2D>().enabled = false;
        anim.SetTrigger("Death");
        yield return new WaitForSeconds(0.6f);
        GameObject instance = Instantiate(drop, transform.position, transform.rotation);
        instance.GetComponent<CoinMagnet>().target = target;
        Destroy(gameObject);
    }
}