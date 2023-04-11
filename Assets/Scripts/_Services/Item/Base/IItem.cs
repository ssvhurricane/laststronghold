using UnityEngine;

namespace Services.Item
{
    public interface IItem 
    {
        public string Name { get; set; }

        public bool IsActive { get; set;}

        public string Description { get; set; }

        public GameObject Prefab { get; set; }

        public ItemType ItemType { get; set; }

        public GameObject Owner { get; set; }
    }
}
