using UnityEngine;
using System.Collections;


public class PlayerWeapon : Weapon
{
    public float fireRate = 0.6f;
    public bool coroutineRunning = false;

    private void Update()
    {
        bool fireOnePressed = Input.GetButton("Fire1");

        if (coroutineRunning == false && fireOnePressed && !GameManager.Instance.gamePaused)
        {
            StartCoroutine(WaitHandleFire());
        }
    }

    public IEnumerator WaitHandleFire()
    {
        coroutineRunning = true;
        this.HandleFire();
        yield return new WaitForSeconds(fireRate);
        coroutineRunning = false;
    }
}
