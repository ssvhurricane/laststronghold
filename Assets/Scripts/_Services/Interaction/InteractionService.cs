using Data.Settings;
using Presenters;
using Presenters.Window;
using Services.Area;
using Services.Log;
using Services.RayCast;
using UnityEngine;
using View.Camera;
using Zenject;

namespace Services.Interaction
{  
    public class InteractionService 
    { 
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;
        private readonly RayCastService _rayCastService;
        private readonly AreaService _areaService;
        private readonly CameraPresenter _cameraPresenter;

        private readonly MainHUDPresenter _mainHUDPresenter;

        private readonly InteractionServiceSettings[] _interactionServiceSettings;
        
        private TransmitterHolder _transmitterCamera;
        private ReceiverHolder _receiverAnchorArea;

        public InteractionService( SignalBus signalBus, 
                                    LogService logService,
                                    RayCastService rayCastService,
                                    AreaService areaService,
                                    CameraPresenter cameraPresenter,
                                    MainHUDPresenter mainHUDPresenter,
                                    InteractionServiceSettings[] interactionServiceSettings)
        {
            _signalBus = signalBus;

            _logService = logService;

            _rayCastService = rayCastService;
            _areaService = areaService;
            _cameraPresenter = cameraPresenter;
            _mainHUDPresenter = mainHUDPresenter;

            _interactionServiceSettings = interactionServiceSettings;
        }

        public void Proccessing(string interactId)
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

                    // TODO: Get Current Area..
                    //_areaService.GetAreaByName();
                }
            }

        }
    }
}
