using System.Linq;
using Constants;
using Data.Settings;
using Presenters;
using Presenters.Window;
using Services.Input;
using Services.Log;
using Services.Pool;
using Services.RayCast;
using UnityEngine;
using View;
using Zenject;
using Signals;
using Services.Essence;
using Services.SFX;
using Services.VFX;
using UniRx;
using View.Camera;

namespace Services.Shooting
{
    public class ShootingService 
    { 
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;

        private readonly RayCastService _rayCastService;

        private readonly PoolService _poolService;

        private readonly SFXService _sFXService;

         private readonly VFXService _vFXService;

        private readonly CameraPresenter _cameraPresenter;

        private readonly MainHUDPresenter _mainHUDPresenter;

        private readonly ShootingServiceSettings[] _shootingServiceSettings;
        private readonly CompositeDisposable _disposables;
        private TransmitterHolder _transmitterCamera;
        private ReceiverHolder _receiverAnchorArea;
        private bool _isComplete = true;

        public ShootingService(SignalBus signalBus, 
                                LogService logService,
                                RayCastService rayCastService,
                                PoolService poolService,
                                SFXService sFXService,
                                VFXService vFXService,
                                CameraPresenter cameraPresenter,
                                MainHUDPresenter mainHUDPresenter,
                                ShootingServiceSettings[] shootingServiceSettings)
        {
            _signalBus = signalBus;

            _logService = logService;

            _rayCastService = rayCastService;

            _poolService = poolService;

            _sFXService = sFXService;
            _vFXService = vFXService;

            _cameraPresenter = cameraPresenter;
            _mainHUDPresenter = mainHUDPresenter;

            _shootingServiceSettings = shootingServiceSettings;
            
             _disposables = new CompositeDisposable();

             _poolService.InitPool(PoolServiceConstants.SingleBulletItemViewPool);
             _poolService.InitPool(PoolServiceConstants.MDBulletItemViewPool);
             _poolService.InitPool(PoolServiceConstants.RPGBulletItemViewPool);

             _signalBus.Subscribe<ShootingServiceSignals.Hit>(signal => OnHit(signal.BaseEssence, signal.Collision));
        }

        private void OnHit(BaseEssence baseEssence, Collision collision)
        {
            _logService.ShowLog(GetType().Name,
                                    Services.Log.LogType.Message, "Hit object: " + baseEssence.name + " | " +
                                    collision.gameObject.name,
                                    LogOutputLocationType.Console);

          //  if (_disposables != null) _disposables.Dispose();
          
         baseEssence.gameObject.SetActive(false);
         baseEssence.gameObject.transform.localPosition = Vector3.zero;
         baseEssence.gameObject.transform.localRotation  = Quaternion.Euler(Vector3.zero);
         baseEssence.gameObject.transform.localScale = Vector3.one;

         baseEssence.gameObject.GetComponent<Rigidbody>().Sleep();

         _poolService.Despawn(baseEssence);
           
        }

        public void Shoot(ActionModifier actionModifier, string settingId)
        {
              switch(actionModifier)
              {
                case ActionModifier.SingleFire:
                {
                    Proccesing<RifleBulletItemView>(settingId, _mainHUDPresenter._sniperRifleItemView.FirePivot, PoolServiceConstants.SingleBulletItemViewPool);

                    break;
                }

                case ActionModifier.BurstFire:
                {
                    Proccesing<RifleBulletItemView>(settingId, _mainHUDPresenter._mDItemView.FirePivot, PoolServiceConstants.MDBulletItemViewPool);

                    break;
                }

                case ActionModifier.UltaFire:
                {
                    Proccesing<RPGBulletItemView>(settingId, _mainHUDPresenter._rPGItemView.FirePivot, PoolServiceConstants.RPGBulletItemViewPool);

                    break;
                }
            }
        }

        private void Proccesing<TParam>(string id, Transform pivot, string poolName, bool isPTYR = false) where TParam : BaseEssence
        {
            Vector3 targetPoint;

            _transmitterCamera = _cameraPresenter.GetView().GetGameObject().GetComponent<TransmitterHolder>();

            var ray = (_cameraPresenter.GetView() as FPSCameraView).GetMainCamera().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            if (_transmitterCamera)
            {
                RaycastHit hitReceiver = _rayCastService.Emit(ray);

                targetPoint = hitReceiver.point;

                _receiverAnchorArea = hitReceiver.collider?.gameObject.GetComponent<ReceiverHolder>();

                if (_receiverAnchorArea)
                {
                    _logService.ShowLog(GetType().Name,
                                    Services.Log.LogType.Message, "Receiver Id: " + _receiverAnchorArea.GetId() + " | " +
                                    "Receiver Name: " + _receiverAnchorArea.GetObjectName(),
                                    LogOutputLocationType.Console);

                    var settings = _shootingServiceSettings.FirstOrDefault(item => item.Id == id);

                    if(!_isComplete) return;

                    TParam rifleBulletItemView = null;
                    rifleBulletItemView  = _poolService.Spawn<TParam>(pivot, poolName);

                    if(!isPTYR) rifleBulletItemView.gameObject.transform.parent = null;   

                    Vector3 dirWithoutSpread  = targetPoint - rifleBulletItemView.GetGameObject().transform.position;

                    float x = Random.Range(-settings.ShootingElement.Spread, settings.ShootingElement.Spread);
                    float y = Random.Range(-settings.ShootingElement.Spread, settings.ShootingElement.Spread);

                    Vector3 dirWithSpread = dirWithoutSpread + new Vector3(x, y, 0);

                    rifleBulletItemView.transform.forward = dirWithSpread.normalized;

                    rifleBulletItemView.GetComponent<Rigidbody>().WakeUp();
                    rifleBulletItemView.GetComponent<Rigidbody>().AddForce(dirWithSpread.normalized * settings.ShootingElement.Speed);
                       
                    Timer(settings.ShootingElement.Rate);
                }
                else
                {
                        // TODO:
                }

            }
            else 
                targetPoint = Vector3.zero;
        }

        private void Timer(float rate)
        {
            _isComplete = false;
                Observable.Timer (System.TimeSpan.FromSeconds(rate))
                .Subscribe (_ => { 
                        _logService.ShowLog(GetType().Name,
                                    Services.Log.LogType.Message,"TimerComplete!",
                                    LogOutputLocationType.Console);
                   _isComplete =  true;
                }).AddTo(_disposables);
        }
    }
}
