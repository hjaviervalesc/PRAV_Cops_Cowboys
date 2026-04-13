using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Player.instance.GetComponent<PlayerHealth>().isAlive == false && Player.instance.GetComponent<Transform>() == null)
            Application.Quit();
    }
}
