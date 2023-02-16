namespace Signals
{
    public class QuestServiceSignals 
    {
        public class OnQuestBaseEvent
        {
            public int Id { get; }

            public OnQuestBaseEvent(int id)
            {
                Id = id;
            }
        }

        public class OnQuestGetEvent : OnQuestBaseEvent
        {
            public OnQuestGetEvent(int id) : base(id)
            {
                // TODO:
            }
        }

        public class OnQuestActivateEvent : OnQuestBaseEvent
        {
            public OnQuestActivateEvent(int id) : base(id)
            {
                // TODO:
            }
        }

        public class OnQuestAssignEvent : OnQuestBaseEvent
        {
            public OnQuestAssignEvent(int id) : base(id)
            {
                // TODO:
            }
        }

        public class OnQuestBuildEvent : OnQuestBaseEvent
        {
            public OnQuestBuildEvent(int id) : base(id)
            {
                // TODO:
            }
        }

        public class OnQuestCollectEvent : OnQuestBaseEvent
        {
            public OnQuestCollectEvent(int id) : base(id)
            {
                // TODO:
            }
        }

        public class OnQuestDestroyEvent : OnQuestBaseEvent
        {
            public OnQuestDestroyEvent(int id) : base(id)
            {
                // TODO:
            }
        }

        public class OnQuestKillEvent : OnQuestBaseEvent
        {
            public OnQuestKillEvent(int id) : base(id)
            {
                // TODO:
            }
        }

        public class OnQuestUpgradeEvent : OnQuestBaseEvent
        {
            public OnQuestUpgradeEvent(int id) : base(id)
            {
                // TODO:
            }
        }
    }
}