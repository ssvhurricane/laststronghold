using Services.Anchor;
using Services.Factory;
using Services.Log;
using Services.Window;
using System.Linq;
using UnityEngine;
using View.Window;
using Zenject;
using Services.Item;
using View.Camera;
using Signals;
using Services.Essence;
using UniRx;

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

        private readonly CompositeDisposable _disposables;
        private MainHUDView _mainHUDView;

        public MDItemView _mDItemView { get; private set; }
        public RPGItemView _rPGItemView { get; private set; }
        public SniperRifleItemView _sniperRifleItemView { get; private set; }
      

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

             _signalBus.Subscribe<MainHUDViewSignals.SelectWeaponItem>(signal => OnSelectWeaponItem(signal.Essence));

             _signalBus.Subscribe<MainHUDViewSignals.StartTimer>(signal => OnStartTimer(signal.Id, signal.Rate));
        }
        private void OnStartTimer(string id, float rate)
        { 
            if(rate > 0f) _mainHUDView.UpdateTimerUI(id, rate);
        }
        
        private void OnSelectWeaponItem(IEssence essence)
        {
            if(essence as SniperRifleItemView)
            {
                _sniperRifleItemView.gameObject.SetActive(true);
                _sniperRifleItemView.IsActive = true;

                _mDItemView.gameObject.SetActive(false);
                _mDItemView.IsActive = false;

                _rPGItemView.gameObject.SetActive(false);
                _rPGItemView.IsActive = false;

                _mainHUDView.UpdateView(new MainHUDViewWeaponSelectContainerArgs
                {
                    Weapon1Color = new Color(255f, 255f,255f, 1f),
                    Weapon2Color = new Color(255f, 255f,255f, .3f),
                    Weapon3Color = new Color(255f, 255f,255f, .3f)
                });

            }
            else if(essence as MDItemView)
            {
                _sniperRifleItemView.gameObject.SetActive(false);
                _sniperRifleItemView.IsActive = false;

                _mDItemView.gameObject.SetActive(true);
                _mDItemView.IsActive = true;

                _rPGItemView.gameObject.SetActive(false);
                _rPGItemView.IsActive = false;

                 _mainHUDView.UpdateView(new MainHUDViewWeaponSelectContainerArgs
                {
                    Weapon2Color = new Color(255f, 255f,255f, 1f),
                    Weapon1Color = new Color(255f, 255f,255f, .3f),
                    Weapon3Color = new Color(255f, 255f,255f, .3f)
                });
            }
            else if(essence as RPGItemView)
            {
                _sniperRifleItemView.gameObject.SetActive(false);
                _sniperRifleItemView.IsActive = false;

                _mDItemView.gameObject.SetActive(false);
                _mDItemView.IsActive = false;

                _rPGItemView.gameObject.SetActive(true);
                _rPGItemView.IsActive = true;

                 _mainHUDView.UpdateView(new MainHUDViewWeaponSelectContainerArgs
                {
                    Weapon3Color = new Color(255f, 255f,255f, 1f),
                    Weapon2Color = new Color(255f, 255f,255f, .3f),
                    Weapon1Color = new Color(255f, 255f,255f, .3f)
                });
            }
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
                    
                _mainHUDView.UpdateView(new MainHUDViewWeaponSelectContainerArgs
                {
                    Weapon1Color = new Color(255f, 255f,255f, 1f),
                    Weapon2Color = new Color(255f, 255f,255f, .3f),
                    Weapon3Color = new Color(255f, 255f,255f, .3f)
                });
            }

            PlayerItems();
        }

        private void PlayerItems()
        {
             // TODO: Get IsActive
            _playerPresenter.GetModel();

            var mCamera = (FPSCameraView)_cameraPresenter.GetView();
            
            _sniperRifleItemView = _factoryService.Spawn<SniperRifleItemView>(mCamera.GetWeaponAnchor().transform);
            _sniperRifleItemView.gameObject.SetActive(true);
            _sniperRifleItemView.IsActive = true;

            _mDItemView = _factoryService.Spawn<MDItemView>(mCamera.GetWeaponAnchor().transform);
            _mDItemView.gameObject.SetActive(false);
            _mDItemView.IsActive = false;

            _rPGItemView = _factoryService.Spawn<RPGItemView>(mCamera.GetWeaponAnchor().transform);
            _rPGItemView.gameObject.SetActive(false);
            _rPGItemView.IsActive = false;
        }


        public IWindow GetView() 
        {
           return _mainHUDView;
        }
    }
}
