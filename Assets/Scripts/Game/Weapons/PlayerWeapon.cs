using UnityEngine;
using System.Collections;


public class PlayerWeapon : Weapon
{
    public float waitFireAmount = 0.6f;
    public bool coroutineRunning = false;

    private void Update()
    {
        bool fireOnePressed = Input.GetButton("Fire1");

        if (coroutineRunning == false && fireOnePressed && !GameManager.Instance.GetGamePaused())
        {
            StartCoroutine(WaitHandleFire());
        }
    }

    public IEnumerator WaitHandleFire()
    {
        coroutineRunning = true;
        this.HandleFire();
        yield return new WaitForSeconds(waitFireAmount);
        coroutineRunning = false;
    }

    public void DecreaseWaitAmount(float decreaseAmount, int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            this.waitFireAmount -= decreaseAmount;
            StopCoroutine(WaitHandleFire());
            this.coroutineRunning = false;
        }
    }
}
