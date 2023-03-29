using Services.Essence;
using Signals;
using UnityEngine;
using Zenject;
using UnityEngine.UI;
using Services.Area;

namespace View
{
    public class AreaView : NetworkEssence
    { 
        [SerializeField] protected EssenceType Layer; 
        
        [SerializeField] protected Text AreaName; 
        [SerializeField] protected Text AreaDescription; 
        [SerializeField] protected GameObject AreaPrefab;
        [SerializeField] protected Text Level;

        [SerializeField] protected Slider AreaHealthPoint;

        [SerializeField] protected Text AreaStatusType;
        [SerializeField] protected Text AreaStageType;
        [SerializeField] protected Text AreaAreaType;

        private SignalBus _signalBus;
        
        [Inject]
        public void Constrcut(SignalBus signalBus)
        {
            _signalBus = signalBus;

            _signalBus.Fire(new EssenceServiceSignals.Register(this));
        }

        public void UpdateView(AreaViewArgs areaViewArgs)
        {
            Id = areaViewArgs.Id;

            AreaName.text = ""; 

            AreaDescription.text = ""; 

            AreaPrefab = areaViewArgs.Prefab;

            Level.text = areaViewArgs.CurLevel.ToString();

            AreaHealthPoint.value = areaViewArgs.CurHealthPoint;

            AreaStatusType.text = "";

            AreaStageType.text = "";

            AreaAreaType.text = "";
        }
    }

    public class AreaViewArgs
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int CurLevel { get; set; }
        public float CurHealthPoint { get; set; }
        public float MaxHealthPoint { get; set; }
        public bool IsInteractive { get; set; }
        public StatusType StatusType { get; set; }
        public StageType StageType { get; set; }
        public AreaType AreaType { get; set; }

        public GameObject Prefab { get; set; }

        public AreaViewArgs(){}

        public AreaViewArgs(string id,
                             string name, 
                             string description, 
                             int curLevel, 
                             float curHealthPoint, 
                             float maxHealthPoint,
                             bool isInteractive,
                             StatusType statusType,
                             StageType stageType,
                             AreaType areaType,
                             GameObject prefab)
                            
        {
            Id = id;

            Name = name;

            Description = description;

            CurLevel  = curLevel;

            CurHealthPoint = curHealthPoint;

            MaxHealthPoint = maxHealthPoint;

            IsInteractive  = isInteractive;

            StatusType = statusType;

            StageType  = stageType;

            AreaType = areaType;

            Prefab = prefab;
        }
    }
}
