using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Services.Cheat
{
    public class CheatButtonDropdownControl : CheatButtonControl
    {
         [SerializeField] private Dropdown _dropdown;

         public int CurIndex => _dropdown.value;

         public string CurItemText => _dropdown.options[_dropdown.value].text;
       

         public CheatButtonDropdownControl SetDropdownOptions(IEnumerable<string> options)
         {
            _dropdown.options = new List<Dropdown.OptionData>();

            foreach(var option in options)
            {
                _dropdown.options.Add(new Dropdown.OptionData(option));
            }

            return this;
         }
    }
}