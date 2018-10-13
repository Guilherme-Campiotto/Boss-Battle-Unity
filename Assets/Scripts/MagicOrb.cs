using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicOrb : MonoBehaviour {

    private bool canMove = true;
    public int hitPoint = 10;
    public float speed = 5.0f;
    public AudioClip audioHit = null;
    public AudioClip audioShoot = null;
    public ParticleSystem particle = null;
    private void Awake()
    {
        this.GetComponent<AudioSource>().PlayOneShot(audioShoot);
    }

    void Update()
    {
        MoveObject();
    }

    void MoveObject()
    {
        if (canMove)
        {
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Turret"))
        {
            this.GetComponent<AudioSource>().PlayOneShot(audioHit);
            // Faz o objeto parar, desaparecer e o sistema de particulas suspender ao colidir.
            this.GetComponent<Renderer>().enabled = false;
            this.GetComponent<Collider>().enabled = false;
            canMove = false;
            ParticleSystem.EmissionModule emission = particle.emission;
            emission.enabled = false;
            // Destroi o objeto somente quando o audio terminar de tocar.
            Destroy(this.gameObject, audioHit.length);
        }
    }

}

internal class ParticleSystemEmissionModule
{
}