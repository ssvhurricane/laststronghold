namespace Services.Area
{
    public interface IConstruction : IArea
    {
        StatusType StatusType { get; set; }
    }
}
