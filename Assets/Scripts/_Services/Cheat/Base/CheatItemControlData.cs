using System;

namespace Services.Cheat
{
    public class CheatItemControlData 
    {
       public string Id { get; set; }
       public string Name { get; set; }

       public Type CheatItemType { get; set; }
       public Action<Type> CheatAction {get ; set; }

       public CheatItemControlData(){}

       public CheatItemControlData(string id, string name, Type cheatItemType, Action<Type> cheatAction)
       {
            Id = id;

            Name = name;

            CheatItemType = cheatItemType;

            CheatAction = cheatAction;
       }
    }
}
