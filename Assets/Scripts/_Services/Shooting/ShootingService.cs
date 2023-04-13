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

namespace Services.Shooting
{
    public class ShootingService 
    { 
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;

        private readonly RayCastService _rayCastService;

        private readonly PoolService _poolService;

        private readonly CameraPresenter _cameraPresenter;

        private readonly MainHUDPresenter _mainHUDPresenter;

        private readonly ShootingServiceSettings[] _shootingServiceSettings;
        private TransmitterHolder _transmitterCamera;
        private ReceiverHolder _receiverAnchorArea;

        public ShootingService(SignalBus signalBus, 
                                LogService logService,
                                RayCastService rayCastService,
                                PoolService poolService,
                                CameraPresenter cameraPresenter,
                                MainHUDPresenter mainHUDPresenter,
                                ShootingServiceSettings[] shootingServiceSettings)
        {
            _signalBus = signalBus;

            _logService = logService;

            _rayCastService = rayCastService;

            _poolService = poolService;

            _cameraPresenter = cameraPresenter;
            _mainHUDPresenter = mainHUDPresenter;

            _shootingServiceSettings = shootingServiceSettings;
        }

        public void Shoot(ActionModifier actionModifier, string settingId)
        {
              switch(actionModifier)
              {
                case ActionModifier.SingleFire:
                {
                    SingleFire(settingId);

                    break;
                }

                case ActionModifier.BurstFire:
                {
                    BurstFire(settingId);

                    break;
                }

                case ActionModifier.UltaFire:
                {
                    UltaFire(settingId);

                    break;
                }
            }
        }

        private void SingleFire(string id)
        {
            Vector3 targetPoint;

             _transmitterCamera = _cameraPresenter.GetView().GetGameObject().GetComponent<TransmitterHolder>();// Ref

                if (_transmitterCamera)
                {
                    RaycastHit hitReceiver = _rayCastService.Emit(_transmitterCamera.transform);

                    targetPoint = hitReceiver.point;

                    _receiverAnchorArea = hitReceiver.collider?.gameObject.GetComponent<ReceiverHolder>();

                      _logService.ShowLog(GetType().Name,
                                    Services.Log.LogType.Message,"Rifle Shoot!",
                                    LogOutputLocationType.Console);

                    if (_receiverAnchorArea)
                    {
                        _logService.ShowLog(GetType().Name,
                                    Services.Log.LogType.Message, "Receiver Id: " + _receiverAnchorArea.GetId() + " | " +
                                    "Receiver Name: " + _receiverAnchorArea.GetObjectName(),
                                    LogOutputLocationType.Console);
                    }
                }
                else targetPoint = Vector3.zero;

            RifleBulletItemView rifleBulletItemView = null;

            if (!_poolService.GetPoolDatas().Any(data => data.Name == PoolServiceConstants.SingleBulletItemViewPool))
            {
                   _poolService.InitPool(PoolServiceConstants.SingleBulletItemViewPool);
               
                    rifleBulletItemView = (RifleBulletItemView)_poolService.Spawn<RifleBulletItemView>(_mainHUDPresenter._sniperRifleItemView.FirePivot, 
                                                                                                                    PoolServiceConstants.SingleBulletItemViewPool);
                    _poolService.Despawn(rifleBulletItemView);                                                                                    
             }

           _poolService.Spawn<RifleBulletItemView>(_mainHUDPresenter._sniperRifleItemView.FirePivot, PoolServiceConstants.SingleBulletItemViewPool);
                                                                                                                  
           var rifleSettings = _shootingServiceSettings.FirstOrDefault(item => item.Id == id);

           Vector3 dirWithoutSpread  = targetPoint - rifleBulletItemView.GetGameObject().transform.position;

           float x = Random.Range(-rifleSettings.ShootingElement.Spread, rifleSettings.ShootingElement.Spread);
           float y = Random.Range(-rifleSettings.ShootingElement.Spread, rifleSettings.ShootingElement.Spread);

           Vector3 dirWithSpread = dirWithoutSpread + new Vector3(x, y, 0);

           rifleBulletItemView.transform.forward = dirWithSpread.normalized;

           rifleBulletItemView.gameObject.transform.parent = null;

           rifleBulletItemView.GetComponent<Rigidbody>().AddForce(dirWithSpread.normalized * rifleSettings.ShootingElement.Speed);

        }

        private void BurstFire(string id)
        {

        }

        private void UltaFire(string id)
        {

        }
    }
}
