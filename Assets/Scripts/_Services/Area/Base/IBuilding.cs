namespace Services.Area
{
    public interface IBuilding : IArea 
    {
        int Level {get; set; }

        int Purse { get; set; }
        StatusType StatusType { get; set; }
        StageType StageType { get; set; }

        void SetLevel();

        int GetLevel();

        bool LevelUp();
    }
}