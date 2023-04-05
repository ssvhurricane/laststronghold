namespace Services.Input
{
    public enum ActionModifier 
    {
        None,

        // Move section.
        LeftMove,
        RightMove,
        FocusMove,
        FocusRotate,

        // Fire section.
        SingleFire,
        BurstFire,
        UltaFire,

        // Interact section.
        ExploreInteract,
        RestoreInteract,
        PickUpInteract
    }
}