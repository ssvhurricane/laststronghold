using System;

namespace Services.Quest
{
    public abstract class QuestBase 
    {
        public QuestData Data { get; protected set; }

        public QuestState QuestState { get; set; }
        public string QuestItemId => _value;

        protected string _value;

        public string _objectType;

        public virtual void Configurate(Data.Settings.Quest quest)
        {
           Data = new QuestData();
           // TODO::
        }

        public abstract void Activate(Action<QuestBase> action = null);

        public abstract void Deactivate();

        public abstract string GetDescription(string hexColor = "", string localizationId = "");

        public string GetProgressString()
        {
            return "";
        }

        public abstract QuestSaveDataBase GetSaveData();

        public abstract void Load(QuestSaveDataBase questSaveData);

        public void Dispose()
        {
           // TODO:
        }
    }
}
