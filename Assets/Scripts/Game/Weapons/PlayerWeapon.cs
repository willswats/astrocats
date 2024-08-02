using UnityEngine;
using System.Collections;


public class PlayerWeapon : Weapon
{
    public float waitFireAmount = 0.6f;
    private bool coroutineRunning = false;
    private bool changedWeapon = false;

    private void Update()
    {
        bool fireOnePressed = Input.GetButton("Fire1");

        if (this.changedWeapon)
        {
            StartCoroutine(WaitChangedWeapon());
        }
        else if (this.coroutineRunning == false && fireOnePressed && !GameManager.Instance.GamePaused)
        {
            StartCoroutine(WaitHandleFire());
        }
    }

    // Prevent an extra projectile from firing upon changing weapon (waiting stops two coroutines from running at the same time)
    public void TriggerChangedWeapon()
    {
        this.changedWeapon = true;
        this.coroutineRunning = false;
        this.StopAllCoroutines();
    }

    private IEnumerator WaitChangedWeapon()
    {
        yield return new WaitForSeconds(this.waitFireAmount);
        this.changedWeapon = false;
    }

    private IEnumerator WaitHandleFire()
    {
        this.coroutineRunning = true;
        this.HandleFire();
        yield return new WaitForSeconds(this.waitFireAmount);
        this.coroutineRunning = false;
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
