using UnityEngine;

namespace Services.Item.Weapon
{
    public interface IWeapon : IItem
    {
       public WeaponType WeaponType { get; set; }

       public Transform FirePivot { get; set; }
    }
}
