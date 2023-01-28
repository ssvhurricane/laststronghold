using Data.Settings;
using Services.Item.Weapon;
using System.Linq;
using UnityEngine;

namespace Services.Item
{
    public class SniperRifleItem : IWeapon
    {
        private ItemServiceSettings _itemSettings;
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ItemType ItemType { get; set; }
        public GameObject Owner { get; set; }
        public WeaponType WeaponType { get; set; }
        public GameObject Prefab { get; set; }

        public SniperRifleItem(ItemServiceSettings[] itemServiceSettings)
        {
            InitItem(itemServiceSettings);
        }

        public void InitItem(ItemServiceSettings[] itemServiceSettings)
        {
            Id = this.GetType().Name;

            _itemSettings = itemServiceSettings.FirstOrDefault(s => s.Id == Id);

            Id = _itemSettings.Id;
            Name = _itemSettings.Name;
            Description = _itemSettings.Description;
            ItemType = _itemSettings.ItemType;
            Prefab = _itemSettings.Prefab;
            WeaponType = WeaponType.Axe;
        }
    }
}