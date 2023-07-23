using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector2 mouseInput;
    public float moveForce;

    public float dashForce;
    public float dashCooldown;
    private bool canDash = true;

    public float immuneVelocity;

    public float knockbackForce;
    public float knockedbackForce;

    public float money;
    public float streak;
    public float multiplier;

    public bool dead;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        GetComponent<Collider2D>().sharedMaterial.bounciness = 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rb.velocity.magnitude);

        mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        transform.GetChild(1).up = mouseInput;
        if (mouseInput.x >= 0)
        {
            transform.GetChild(1).localScale = new Vector2 (1, 1);
        }
        else
        {
            transform.GetChild(1).localScale = new Vector2(-1, 1);
        }

        transform.GetChild(0).right = rb.velocity.normalized;

        if (rb.velocity.magnitude <= immuneVelocity)
        {
            transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
        else if (!dead)
        {
            transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }

        if (!dead && canDash && Input.GetButtonDown("Fire1"))
        {
            rb.AddForce(mouseInput.normalized * dashForce);
            StartCoroutine(DashCooldown());
            SoundManager.PlaySound("dash");
        }
    }

    void FixedUpdate()
    {
        if (!dead)
        {
            rb.AddForce(mouseInput.normalized * moveForce);
        }
    }

    void OnMouseOver()
    {
        transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnMouseExit()
    {
        if (!dead)
        {
            transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (rb.velocity.magnitude <= immuneVelocity)
            {
                Vector2 knockedbackDirection = transform.position - other.transform.position;
                rb.AddForce(knockedbackDirection.normalized * knockedbackForce);
                other.gameObject.transform.GetComponent<EnemyController>().anim.SetTrigger("Kick");
                StartCoroutine(DoDeath());
            }
            else
            {
                Vector2 knockbackDirection = other.transform.position - transform.position;
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackDirection.normalized * knockbackForce);
                StartCoroutine(other.gameObject.GetComponent<EnemyController>().DoDeath());
                streak += 1;
                SoundManager.PlaySound("pop");
            }
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            if (rb.velocity.magnitude <= immuneVelocity)
            {
                Vector2 knockedbackDirection = transform.position - other.transform.position;
                rb.AddForce(knockedbackDirection.normalized * knockedbackForce);
                StartCoroutine(DoDeath());
                StartCoroutine(other.gameObject.GetComponent<BulletScript>().DoDeath());
            }
            else
            {
                Vector2 knockbackDirection = other.transform.position - transform.position;
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackDirection.normalized * knockbackForce);
                StartCoroutine(other.gameObject.GetComponent<BulletScript>().DoDeath());
            }
        }
        else if (other.gameObject.CompareTag("Coin"))
        {
            money += 1 * multiplier;
            Destroy(other.gameObject);
            SoundManager.PlaySound("money");
        }
        else if (other.gameObject.CompareTag("Drop"))
        {
            if (money < 10)
            {
                return;
            } 
            money -= 10;
            if (other.gameObject.GetComponent<PowerUp>().type == "cleats")
            {
                dashCooldown *= 0.9f;
            }
            else if (other.gameObject.GetComponent<PowerUp>().type == "guards")
            {
                dashForce *= 1.2f;
            }
            else if (other.gameObject.GetComponent<PowerUp>().type == "gloves")
            {
                transform.localScale *= new Vector2(1.25f, 1.25f);
            }
            else if (other.gameObject.GetComponent<PowerUp>().type == "beer")
            {
                GetComponent<Collider2D>().sharedMaterial.bounciness *= 1.05f;
            }
            else if (other.gameObject.GetComponent<PowerUp>().type == "whistle")
            {
                multiplier *= 1.1f;
            }
            Destroy(other.gameObject);
            SoundManager.PlaySound("money");
        }
    }

    public IEnumerator DashCooldown()
    {
        canDash = false;
        transform.GetChild(1).GetChild(0).GetComponent<Animator>().SetBool("Charged", false);
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
        transform.GetChild(1).GetChild(0).GetComponent<Animator>().SetBool("Charged", true);
    }

    public IEnumerator DoDeath()
    {
        dead = true;
        foreach (Collider2D col in GetComponents<Collider2D>())
        {
            col.enabled = false;
        }
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        anim.SetTrigger("Death");
        SoundManager.PlaySound("hit");
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }
}
