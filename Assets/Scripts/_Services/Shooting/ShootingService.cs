using Data.Settings;
using Presenters;
using Services.Input;
using Services.Log;
using Services.RayCast;
using Zenject;

namespace Services.Shooting
{
    public class ShootingService 
    { 
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;

        private readonly RayCastService _rayCastService;

        private readonly CameraPresenter _cameraPresenter;

        private readonly ShootingServiceSettings[] _shootingServiceSettings;
        private TransmitterHolder _transmitterCamera;
        private ReceiverHolder _receiverAnchorArea;

        public ShootingService(SignalBus signalBus, 
                                LogService logService,
                                RayCastService rayCastService,
                                CameraPresenter cameraPresenter,
                                ShootingServiceSettings[] shootingServiceSettings)
        {
            _signalBus = signalBus;

            _logService = logService;

            _rayCastService = rayCastService;

             _cameraPresenter = cameraPresenter;

            _shootingServiceSettings = shootingServiceSettings;
        }

        public void RifleShoot(ActionModifier actionModifier)
        {
                // TODO:

                _transmitterCamera = _cameraPresenter.GetView().GetGameObject().GetComponent<TransmitterHolder>();

                if (_transmitterCamera)
                {
                    var hitReceiver = _rayCastService.Emit(_transmitterCamera.transform);

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
        }
    }
}
