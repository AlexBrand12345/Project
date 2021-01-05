using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns 
{
    public int damage; //урон пули
    public int bspeed; //скорость пули
    public float shotTime;
    public static Guns pistol = new Guns { damage = 1, bspeed = 10, shotTime = 1f};
    public static Guns tommy = new Guns { damage = 2, bspeed = 25, shotTime = 0.5f };
    public static Guns autogun = new Guns { damage = 3, bspeed = 35, shotTime = 1.5f };
    public static Guns rifle = new Guns{ damage = 10, bspeed = 50, shotTime = 4f };

    //public Guns()
    //{
    //    damage = new int();
    //    bspeed = new int();
    //}
}
