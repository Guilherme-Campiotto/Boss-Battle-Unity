using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

    public Color hitColor = Color.white;
    private Color originalColor = Color.white;
    public float moveSpeed = 10.0f;
    public float rotateSpeed = 100.0f;
    public int magicOrbAmount = 20;
    public GameObject magicOrb = null;
    public GameObject socket = null;
    public int health = 100;

    private void Awake()
    {
        originalColor = this.GetComponent<Renderer>().material.color;
    }
    void Update () {
        MoveControls();
        ShootControls();
	}

    void MoveControls()
    {
        float move = Input.GetAxis("Vertical") * moveSpeed;
        float rotation = Input.GetAxis("Horizontal") * rotateSpeed;
        this.transform.Translate(0, 0, move * Time.deltaTime);
        this.transform.Rotate(0, rotation * Time.deltaTime, 0);
    }

    void ShootControls()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (magicOrbAmount > 0)
            {
                magicOrbAmount --;
                GameObject magicOrbObj = Instantiate(magicOrb, socket.transform.position, socket.transform.rotation) as GameObject;
                magicOrbObj.name = "magicOrb";
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CannonBall"))
        {
            int damage = other.GetComponent<CannonBall>().hitPoint;
            ApplyDamage(damage);
        }
    }

    void ApplyDamage(int damage)
    {
        if(health > 0)
        {
            health -= damage;
            Debug.Log("Vida: " + health);
            StartCoroutine(ApplyDamageIndicator());
        }
        else
        {
            Debug.Log("Game Over");
        }
    }

    /**
     * Aplica a cor vermelha no objeto para indicar dano e após isso retorna a cor original
     */
    IEnumerator ApplyDamageIndicator()
    {
        this.GetComponent<Renderer>().material.color = hitColor;
        yield return new WaitForSeconds(0.4f);
        this.GetComponent<Renderer>().material.color = originalColor;
    }
}
