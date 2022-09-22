using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;
    public GameObject soundObj;
    public AudioClip hitSound;

    [SerializeField] private bool invulnerable = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (health <= 0) EndGame();
    }

    public void IsHit()
    {
        if (invulnerable) return;
        health--;
        Debug.Log("HIT!");
        Instantiate(soundObj, transform.position, Quaternion.identity)
            .GetComponent<SingleTimeSound>().LoadClipAndPlay(hitSound);
        StartCoroutine(BecomeInvulnerable());
    }

    IEnumerator BecomeInvulnerable()
    {
        invulnerable = true;
        yield return new WaitForSeconds(1.5f);
        invulnerable = false;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            IsHit();
        }
    }

    private void EndGame()
    {
        throw new System.NotImplementedException();
    }
}
