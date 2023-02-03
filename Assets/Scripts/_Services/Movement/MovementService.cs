using Data.Settings;
using UnityEngine;
using Zenject;
using System.Linq;
using View;
using Services.Log;

namespace Services.Movement
{
    public class MovementService 
    {
        private readonly SignalBus _signalBus;
        private readonly LogService _logService;

        private readonly MovementServiceSettings[] _movementServiceSettings;
        private MovementServiceSettings _settings;

        private float _xRotation, _yRotation;

        public MovementService(SignalBus signalBus,
                               LogService logService,  
                               MovementServiceSettings[] movementServiceSettings) 
        {
            _signalBus = signalBus;
            _logService = logService;
            _movementServiceSettings = movementServiceSettings;
        }

        public MovementServiceSettings InitService(string settingsID)
        { 
           return _settings = _movementServiceSettings?.FirstOrDefault(_=>_.Id == settingsID);
        }

        public void MoveWithDirection(IView view, Vector2 direction, Rigidbody viewRigidbody)
        {
            if (viewRigidbody != null)
            {
                Vector3 targetVelocity = (view.GetGameObject().transform.right * direction.x)
                        + (view.GetGameObject().transform.forward * direction.y);

                if (IsGrounded(view, viewRigidbody))
                    viewRigidbody.AddForce(targetVelocity - viewRigidbody.velocity, ForceMode.VelocityChange);
                else
                    viewRigidbody.AddForce((targetVelocity - viewRigidbody.velocity) * _settings.Move.AirResistance, ForceMode.VelocityChange);
            }
        }

        public void OrbitalMove(IView baseView, Vector3 targetView, Quaternion angle)
        {
            baseView.GetGameObject().transform.position
                = angle * (baseView.GetGameObject().transform.position - targetView)
                + targetView;
        }

        public void MoveToWardsWithRadius(IView baseView, Vector3 targetView, float radius =.0f)
        {
            // TODO: radius
             baseView.GetGameObject().transform.position = Vector3.MoveTowards(baseView.GetGameObject().transform.position,
                                                                            targetView,10 * Time.deltaTime);
        }
       
        public void RotateWithDirection(IView view, Vector2 direction, Rigidbody viewRigidbody)
        {
            if (viewRigidbody != null)
                    viewRigidbody.MoveRotation(viewRigidbody.rotation * Quaternion.Euler(0f, direction.x * _settings.Rotate.Sensitivity * Time.smoothDeltaTime, 0f));
        }

        public void RotateWithClampDirection(IView view, Vector2 direction)
        {
             _xRotation -= direction.y * _settings.Rotate.Sensitivity * Time.smoothDeltaTime;

            _yRotation += direction.x * _settings.Rotate.Sensitivity * Time.smoothDeltaTime;

            _xRotation = Mathf.Clamp(_xRotation, _settings.Rotate.UpperLimit, _settings.Rotate.BottomLimit);

            _yRotation = Mathf.Clamp(_yRotation, _settings.Rotate.LeftLimit, _settings.Rotate.RightLimit);

            view.GetGameObject().transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        }

        public void RotateToWardsWithDirection(IView view, Transform direction)
        {
            // TODO:
            // Determine which direction to rotate towards
            Vector3 targetDirection = view.GetGameObject().transform.position - direction.position;

            // The step size is equal to speed times frame time.
            float singleStep = 1.0f * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(view.GetGameObject().transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(view.GetGameObject().transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            view.GetGameObject().transform.rotation = Quaternion.LookRotation(newDirection);
        }

        public void Jump(IView view, Rigidbody viewRigidbody)
        {
            if (viewRigidbody != null)
                if (IsGrounded(view, viewRigidbody))
                    viewRigidbody.AddForce(view.GetGameObject().transform.up * _settings.Jump.Speed, _settings.Jump.ForceMode);
        }
      
        public void Follow(IView baseView, IView targetView, Vector3 followOffset, Vector3 position, float followSpeed) 
        {
            var smoothedPosition = Vector3.Lerp(baseView.GetGameObject().transform.position,
                                                (targetView.GetGameObject().transform.position + followOffset),
                                                followSpeed);

           baseView.GetGameObject().transform.position = smoothedPosition + position;
        }

        public void Parent(IView baseView, IView targetView, bool resetParent = false) 
        {
            if(!resetParent)
                 baseView.GetGameObject().transform.parent = targetView.GetGameObject().transform;
            else
                 baseView.GetGameObject().transform.parent = null;
        }

        public void Parent(IView baseView, GameObject targetView, bool resetParent = false)
        {
            if (!resetParent)
                baseView.GetGameObject().transform.parent = targetView.transform;
            else
                baseView.GetGameObject().transform.parent = null;
        }

        public void LookAt(IView baseView, Vector3 targetView, Vector3 dir)
        {
            baseView.GetGameObject().transform.LookAt(targetView, dir);
        }

        public bool IsGrounded(IView view, Rigidbody viewRigidbody) 
        {
            if (viewRigidbody != null)
            {
                RaycastHit hitInfo;
                if (Physics.Raycast(viewRigidbody.worldCenterOfMass, Vector3.down, out hitInfo, _settings.Jump.Dis2Ground + 0.1f, _settings.Jump.GroundLayers))
                    return true;
            }
            
           return false;
        }
    }
}