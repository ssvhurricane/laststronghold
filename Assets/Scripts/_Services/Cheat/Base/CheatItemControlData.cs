using System;

namespace Services.Cheat
{
    public class CheatItemControlData 
    {
       public string Id { get; set; }
       public string Name { get; set; }

       public Type CheatItemType { get; set; }
       public object CheatAction {get ; set; }

       public CheatItemControlData(){}

       public CheatItemControlData(string id, string name, Type cheatItemType, object cheatAction)
       {
            Id = id;

            Name = name;

            CheatItemType = cheatItemType;

            CheatAction = cheatAction;
       }
    }
}
