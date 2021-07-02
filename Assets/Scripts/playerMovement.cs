using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class playerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float upForce;
    public float localPositionOfX;
    public ParticleSystem jetFire;
    public Animator anime;

    public bool isPlayerFlying = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        forceUp();
        transform.localPosition = new Vector3(localPositionOfX, transform.position.y,10);
    }
    void forceUp()
    {

        if (isPlayerFlying && !GameManager.manager.gameOver)
        {
            rb.AddForce(Vector2.up * upForce);
            jetFire.Play();
        }
        else if(!isPlayerFlying)
        {
            StartCoroutine(jeFire(1.5f));
        }
            

        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
        {
            isPlayerFlying = true;
        }
        else
        {
            isPlayerFlying = false;
        }
    }

    IEnumerator jeFire(float t)
    {
        yield return new WaitForSeconds(t);
        jetFire.Stop();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("obs"))
        {
            GameManager.manager.currentHealth--;
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            anime.SetBool("isGrounded", true);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        anime.SetBool("isGrounded", false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            GameManager.manager.ScorePoint++;
            Destroy(other.gameObject);
        }
    }
}
