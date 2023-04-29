using Services.Essence;
using Signals;
using UnityEngine;
using Zenject;
using UnityEngine.UI;
using Services.Area;

namespace View
{
    public class AreaView : NetworkEssence, ITakingDamage
    { 
        [SerializeField] protected EssenceType Layer; 
        [SerializeField] protected string ObjectName;
        [SerializeField] protected Text AreaName; 
        [SerializeField] protected Text AreaDescription; 
        [SerializeField] protected GameObject AreaPrefab;
        [SerializeField] protected Text Level;

        [SerializeField] protected Slider AreaHealthPoint;

        [SerializeField] protected Text AreaStatusType;
        [SerializeField] protected Text AreaStageType;
        [SerializeField] protected Text AreaAreaType;

        private SignalBus _signalBus;

        public bool IsInteractive { get; private set; } = false;
        
        [Inject]
        public void Constrcut(SignalBus signalBus)
        {
            _signalBus = signalBus;

            _signalBus.Fire(new EssenceServiceSignals.Register(this));
        }

        public void InteractiveArea(bool isInteractive)
        {
            IsInteractive = isInteractive;
        }

        public void StartSimulate()
        {
            // TODO:
        }

        public void StopSimulate()
        {
            // TODO:
        }


        public void UpdateView(AreaViewArgs areaViewArgs)
        {
            Id = areaViewArgs.Id;

            AreaName.text = areaViewArgs.Name; 

            AreaDescription.text = areaViewArgs.Description;

           // AreaPrefab = areaViewArgs.Prefab;

            Level.text = areaViewArgs.CurLevel.ToString();

            AreaHealthPoint.value = areaViewArgs.CurHealthPoint;

            AreaStatusType.text = areaViewArgs.StatusType.ToString();

            AreaStageType.text = areaViewArgs.StageType.ToString();

            AreaAreaType.text = areaViewArgs.AreaType.ToString();
        } 
        public string GetObjectName()
        {
            return ObjectName;
        }

        public void Damage(float damageCount)
        {
            throw new System.NotImplementedException();
        }

        public bool Kill()
        {
            throw new System.NotImplementedException();
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
