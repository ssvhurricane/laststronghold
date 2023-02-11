namespace Services.Area
{
    public interface IBuilding : IArea 
    {
        StatusType StatusType { get; set; }
    }
}