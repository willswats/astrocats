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

    public void IncreaseFireRate(int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            if (this.fireRate > 0.1f)
            {
                this.fireRate -= 0.05f;
                StopCoroutine(WaitHandleFire());
                this.coroutineRunning = false;
            }
        }
    }
}
