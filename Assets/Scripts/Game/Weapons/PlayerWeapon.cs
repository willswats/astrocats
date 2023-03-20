using UnityEngine;

public class PlayerWeapon : Weapon
{
    private void Update()
    {
        bool fireOnePressed = Input.GetButtonDown("Fire1");

        if (fireOnePressed && !GameManager.Instance.gamePaused)
        {
            this.HandleFire();
        }
    }
}
