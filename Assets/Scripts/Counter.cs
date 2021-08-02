using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Text CounterText;
    public Text timerText;
    public bool hasScored;

    private int Count = 0;

    private float timerTime = 15f;

    private ShootPower shootPower;

    private void Start()
    {
        shootPower = GameObject.Find("Ball").GetComponent<ShootPower>();

        Count = 0;
    }

    private void Update()
    {
        if (shootPower.isGameActive)
        {
            timerTime -= Time.deltaTime;
            timerText.text = "Timer: " + Mathf.Round(timerTime);

            if (timerTime < 0)
            {
                shootPower.GameOver();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Count += 1;
        CounterText.text = "Count: " + Count;
        hasScored = true;
    }
}
