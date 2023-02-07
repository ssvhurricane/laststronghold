using Model;
using Services.NPC;
using UnityEngine;
using View;
using Zenject;

namespace Presenters
{
    public class NPCPresenter : IPresenter
    {
        private readonly SignalBus _signalBus;

        private readonly NPCService _nPCService;

        public NPCPresenter(SignalBus signalBus, NPCService nPCService)
        {
            _signalBus = signalBus;
            _nPCService = nPCService;
        }

        public IModel GetModel()
        {
            throw new System.NotImplementedException();
        }

        public IView GetView()
        {
            throw new System.NotImplementedException();
        }

        public void HideView()
        {
            throw new System.NotImplementedException();
        }

        public void ShowView(GameObject prefab = null, Transform hTransform = null)
        {
            throw new System.NotImplementedException();
        }
    }
}
