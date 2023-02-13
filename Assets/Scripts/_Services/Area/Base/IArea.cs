using System.Collections.Generic;
using Services.Item;
using View;

namespace Services.Area
{
    public interface IArea 
    {
        int Id { get; set; }

        string Name { get; set; }

        string Description { get; set; }

        bool IsInteractive { get; set; }

        AreaType AreaType { get; set; }

        List<IItem> Items { get; set; }

        List<IView> ViewWorkers { get; set; }
    }
}
