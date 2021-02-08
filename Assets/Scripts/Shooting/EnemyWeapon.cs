using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : BaseWeapon
{
   public override IEnumerator Reload(float time2reload)
   {
        yield return new WaitForSeconds(time2reload);
        yield return new WaitForSeconds(time2reload);
        ammoLeft = ammo;
        reloading = false;
    }
}
