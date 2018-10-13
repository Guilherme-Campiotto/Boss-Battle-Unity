using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Text playerHealth;
    public Text magicOrbs;
    public Text turretHealth;

    public Hero hero = null;
    public Turret turret = null;


    // Use this for initialization
    void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update () {
        DisplayProperties();
    }

    void DisplayProperties()
    {
        playerHealth.text = "Player Health: " + hero.health.ToString();
        magicOrbs.text = "Player Magic Orbs: " + hero.magicOrbAmount.ToString();
        turretHealth.text = "Turret Health: " + turret.health.ToString();
    }
}
