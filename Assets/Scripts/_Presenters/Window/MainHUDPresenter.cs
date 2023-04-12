using Services.Anchor;
using Services.Factory;
using Services.Log;
using Services.Pool;
using Services.Window;
using System.Linq;
using UnityEngine;
using View.Window;
using Zenject;
using Constants;
using Services.Item;
using View.Camera;

namespace Presenters.Window
{
    public class MainHUDPresenter 
    {
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;
        private readonly IWindowService _windowService;
        
        private readonly FactoryService _factoryService;
        private readonly HolderService _holderService;

        private readonly CameraPresenter _cameraPresenter;
      
        private readonly PlayerPresenter _playerPresenter;
        private MainHUDView _mainHUDView;

        private MDItemView _mDItemView;
        private RPGItemView _rPGItemView;
        private SniperRifleItemView _sniperRifleItemView;

        public MainHUDPresenter(SignalBus signalBus,
            LogService logService,
            IWindowService windowService,
            FactoryService factoryService, 
            HolderService holderService,
            CameraPresenter cameraPresenter,
            PlayerPresenter playerPresenter
            ) 
        {
            _signalBus = signalBus;
            _logService = logService;
            _windowService = windowService;

            _factoryService = factoryService;
            _holderService = holderService;

            _cameraPresenter = cameraPresenter;
            _playerPresenter = playerPresenter;

            _logService.ShowLog(GetType().Name,
                Services.Log.LogType.Message,
                "Call Constructor Method.", 
                LogOutputLocationType.Console);
        }

        public void ShowView()
        {
            if (_windowService.IsWindowShowing<MainHUDView>()) return; 
            
            if (_windowService.GetWindow<MainHUDView>() != null)
                _mainHUDView = (MainHUDView)_windowService.ShowWindow<MainHUDView>();
            else
            {
                Transform holderTansform = _holderService._windowTypeHolders.FirstOrDefault(holder => holder.Key == WindowType.BaseWindow).Value;

                if (holderTansform != null)
                    _mainHUDView = _factoryService.Spawn<MainHUDView>(holderTansform);
            }

            PlayerItems();
        }

        private void PlayerItems()
        {
            var mCamera = (FPSCameraView)_cameraPresenter.GetView();
            
            _sniperRifleItemView = _factoryService.Spawn<SniperRifleItemView>(mCamera.GetWeaponAnchor().transform);
            _sniperRifleItemView.gameObject.SetActive(true);

            _mDItemView = _factoryService.Spawn<MDItemView>(mCamera.GetWeaponAnchor().transform);
            _mDItemView.gameObject.SetActive(false);

            _rPGItemView = _factoryService.Spawn<RPGItemView>(mCamera.GetWeaponAnchor().transform);
            _rPGItemView.gameObject.SetActive(false);
        }

        private void PlayerParams()
        {
            // TODO:

            _playerPresenter.GetModel();
        }

        private void PlayerAbilities()
        {
            // TODO:
            _playerPresenter.GetModel();
        }

        public IWindow GetView() 
        {
           return _mainHUDView;
        }
    }
}
