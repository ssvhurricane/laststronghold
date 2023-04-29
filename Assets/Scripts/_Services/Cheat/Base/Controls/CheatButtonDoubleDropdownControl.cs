using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Services.Cheat
{
    public class CheatButtonDoubleDropdownControl : CheatButtonControl
    {
        [SerializeField] private Dropdown _dropdownFirst;

        [SerializeField] private Dropdown _dropdownSecond;

         public int CurFirstIndex => _dropdownFirst.value;

         public int CurSecondIndex => _dropdownSecond.value;

         public string CurFirstItemText => _dropdownFirst.options[_dropdownFirst.value].text;

        public string CurSecondItemText => _dropdownSecond.options[_dropdownSecond.value].text;
       

         public CheatButtonDoubleDropdownControl SetFirstDropdownOptions(IEnumerable<string> options)
         {
            _dropdownFirst.options = new List<Dropdown.OptionData>();

            foreach(var option in options)
            {
                _dropdownFirst.options.Add(new Dropdown.OptionData(option));
            }

            return this;
         }

        public CheatButtonDoubleDropdownControl SetSecondDropdownOptions(IEnumerable<string> options)
         {
            _dropdownSecond.options = new List<Dropdown.OptionData>();

            foreach(var option in options)
            {
                _dropdownSecond.options.Add(new Dropdown.OptionData(option));
            }

            return this;
         }
    }
}