namespace Services.Area
{
    public interface IConstruction : IArea
    {
        int Level {get; set; }
        StatusType StatusType { get; set; }
        StageType StageType { get; set; }

        void SetLevel();

        int GetLevel();

        bool LevelUp();
    }
}
