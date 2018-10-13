using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {

    public int hitPoint = 20;
    public float minForce = 250.0f;
    public float maxForce = 650.0f;
    public float delayTime = 2.0f;

    public AudioClip audioHit;
    public AudioClip audioShoot;

    public ParticleSystem particle;
    private bool isActive = true;

    void Awake () {
        this.GetComponent<Rigidbody>().AddRelativeForce(0, GetRandomValue(), GetRandomValue());
        this.GetComponent<AudioSource>().PlayOneShot(audioShoot);
	}
	
    float GetRandomValue()
    {
        return Random.Range(minForce, maxForce);
    }

    void OnTriggerEnter(Collider other)
    {
        if (isActive) {
            isActive = false;
            this.GetComponent<AudioSource>().PlayOneShot(audioHit);
            StartCoroutine(DisableParticle());   
        }
        
    }

    /**
     * Elimina o dano se ele cair no chão e retira o efeito de fumaça;
     */
    IEnumerator DisableParticle()
    {
        yield return new WaitForSeconds(delayTime);
        ParticleSystem.EmissionModule emission = particle.emission;
        emission.enabled = false;
        hitPoint = 0;
    }

}
