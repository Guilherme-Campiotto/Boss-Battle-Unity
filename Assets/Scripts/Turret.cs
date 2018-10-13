using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public Color hitColor = Color.white;
    private Color originalColor = Color.white;
    public GameObject cannonBall = null;
    public Transform player = null;

    public float minDelay = 1.0f;
    public float maxDelay = 4.0f;

    public float lastTime = 0f;
    public float delayTime = 0f;
    public int health = 100;

    private void Awake()
    {
        originalColor = this.GetComponent<Renderer>().material.color;
    }

    void Update () {
        FollowPlayer();
        Shoot();
	}

    void FollowPlayer()
    {
        this.transform.LookAt(player);
    }

    /**
     * Atira se o tempo do ultimo tiro somado com o delay foi atingido
     */
    void Shoot()
    {
        if(Time.time > lastTime + delayTime)
        {
            lastTime = Time.time;
            delayTime = GetRandomValue();
            GameObject cannonBallObject = Instantiate(cannonBall, this.transform.position, this.transform.rotation);
            cannonBallObject.name = "cannonBall";
        }
    }

    float GetRandomValue()
    {
        return Random.Range(minDelay, maxDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MagicOrb")) {
            int damage = other.GetComponent<MagicOrb>().hitPoint;
            ApplyDamage(damage);
        }
    }

    void ApplyDamage(int damage)
    {
        if(health > 0)
        {
            health -= damage;
            Debug.Log("Vida torre: " + health);
            StartCoroutine( ApplyDamageIndicator());
        }
        else
        {
            Destroy(this.gameObject);
            Debug.Log("Torre morreu");
        }
    }

    /**
     * Aplica a cor amarela no objeto para indicar dano e após isso retorna a cor original
     */
    IEnumerator ApplyDamageIndicator()
    {
        this.GetComponent<Renderer>().material.color = hitColor;
        yield return new WaitForSeconds(0.4f);
        this.GetComponent<Renderer>().material.color = originalColor;

    }
}
