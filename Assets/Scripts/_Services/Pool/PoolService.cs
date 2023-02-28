using Data.Settings;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using View;
using Zenject;

namespace Services.Pool
{
    public class PoolService
    {
        private SignalBus _signalBus;

        private ViewPool _viewPool;

        private PoolServiceSettings[] _poolServiceSettings;
       
       private List<PoolData> _poolDatas;

        public PoolService(SignalBus signalBus,
            ViewPool viewPool,
            PoolServiceSettings[] poolServiceSettings)
        {
            _signalBus = signalBus;

            _poolServiceSettings = poolServiceSettings;

            _viewPool = viewPool;

            _poolDatas = new List<PoolData>();
        }

        public void InitPool(string settingsId)
        {
            var settings = _poolServiceSettings.FirstOrDefault(item => item.Id == settingsId);

            if (_poolDatas.Any(item  => item.Id == settings.Id || item.Name == settings.Name)) return;

            _poolDatas.Add(new PoolData()
            {
                Id = settings.Id,
                Name = settings.Name,
                InitialSize = settings.InitialSize,
                MaxSize = settings.MaxPoolSize
             });
        }

        public void InitPool(PoolData poolData)
        {
             if (_poolDatas.Any(item  => item.Id == poolData.Id || item.Name == poolData.Name)) return;

             _poolDatas.Add(poolData);
        }

        public List<PoolData> GetPoolDatas()
        {
            return _poolDatas;
        }

        public TView Spawn<TView>(Transform parentTransform, string poolDataName) where TView : IView
        {
            if (_poolDatas != null && _poolDatas.Count() != 0)
            {
                var data =_poolDatas.FirstOrDefault(poolData => poolData.Name == poolDataName);

                if(data == null) return default(TView);

                _viewPool.InitViewPool(data.InitialSize, data.MaxSize);
                _viewPool.SetParent(parentTransform);

                return _viewPool.Spawn<TView>();
            }
            else
            {
                Debug.LogWarning("[PoolService] -> Service not initialize!");
                return default(TView);
            }
        }

        public void Despawn(IView baseView, bool isRemove = false)
        {
            _viewPool.Despawn(baseView, isRemove);
        }

        public ViewPool GetPool()
        {
            return _viewPool;
        }

        public void Despose(ViewPool viewPool)
        {
            foreach (var pool in viewPool?.GetViewPools())
            {
                pool.Value?.Clear();
                pool.Value?.Dispose();
            }
            viewPool?.GetViewPools().Clear();
        } 
        public void ClearServiceValues() 
        {
            Despose(_viewPool);
        }
    }

    public class PoolData
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int InitialSize { get; set; }

        public int MaxSize { get; set; }

        public PoolData (string id, string name, int initialSize, int maxSize)
        {
            Id = id;

            Name = name;

            InitialSize = initialSize;

            MaxSize = maxSize;
        }

        public PoolData()
        {
        }
    }
}
