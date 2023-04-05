using Model;
using Presenters;
using Services.Input;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Services.Ability
{
    public class AbilityService
    {
        private readonly SignalBus _signalBus;

        private List<IAbility> _allAbilities;

        private readonly PlayerIdleAbility _playerIdleAbility;
        private readonly PlayerMoveAbility _playerMoveAbility;
        private readonly PlayerFocusMoveAbility _playerFocusMoveAbility;
        private readonly PlayerLookAtAbility _playerLookAtAbility;

        // Attack.
        private readonly PlayerBaseAttackAbility _playerBaseAttackAbility;

        //Specific.
        private readonly PlayerNoneAbility _playerNoneAbility;
        private readonly PlayerInteractAbility _playerInteractAbility;

        // Camera Abilities.
        private readonly CameraRotateAbility _cameraRotateAbility;

        public AbilityService(SignalBus signalBus,
                            PlayerIdleAbility playerIdleAbility,
                            PlayerMoveAbility playerMoveAbility,
                            PlayerFocusMoveAbility playerFocusMoveAbility,
                            PlayerLookAtAbility playerLookAtAbility,
                            PlayerBaseAttackAbility playerBaseAttackAbility,
                            PlayerNoneAbility playerNoneAbility,
                            PlayerInteractAbility playerInteractAbility,
                            CameraRotateAbility cameraRotateAbility) 
        {
            _signalBus = signalBus;


            _playerIdleAbility = playerIdleAbility;

            _playerMoveAbility = playerMoveAbility;

            _playerFocusMoveAbility = playerFocusMoveAbility;

            _playerLookAtAbility = playerLookAtAbility;

            _playerBaseAttackAbility = playerBaseAttackAbility;

            _playerNoneAbility = playerNoneAbility;

             _playerInteractAbility = playerInteractAbility;

             _cameraRotateAbility = cameraRotateAbility;

            _allAbilities  = new List<IAbility>();

             _allAbilities.Add(_playerIdleAbility);

             _allAbilities.Add(_playerMoveAbility);

             _allAbilities.Add(_playerFocusMoveAbility);

             _allAbilities.Add(_playerLookAtAbility);
             _allAbilities.Add(_playerBaseAttackAbility);
             _allAbilities.Add(_playerNoneAbility);

             _allAbilities.Add(_playerInteractAbility);
             _allAbilities.Add(_cameraRotateAbility);
            
        }
      
        public void UseAbility(IAbilityWithOutParam ability, IPresenter presenter, ActionModifier actionModifier)
        {
            if (ability.ActivateAbility)
            {
                ability.StartAbility(presenter, actionModifier);
            }
        }

        public void UseAbility(IAbilityWithVector2Param ability, IPresenter presenter, Vector2 param, ActionModifier actionModifier)
        {
            if (ability.ActivateAbility)
            {
                ability.StartAbility(presenter, param, actionModifier);
            }
        }

        public void UseAbility(IAbilityWithVector3Param ability, IPresenter presenter, Vector3 param, ActionModifier actionModifier)
        {
            if (ability.ActivateAbility)
            {
                ability.StartAbility(presenter, param, actionModifier);
            }
        }

        public void UseAbility(IAbilityWithTransformParam ability, IPresenter presenter, Transform param, ActionModifier actionModifier)
        {
            if (ability.ActivateAbility)
            {
                ability.StartAbility(presenter, param, actionModifier);
            }
        }

        public void UseAbility(IAbilityWithBoolParam ability, IPresenter presenter, bool param, ActionModifier actionModifier)
        {
            if (ability.ActivateAbility)
            {
                ability.StartAbility(presenter, param, actionModifier);
            }
        }

        public void UseAbility(IAbilityWithAffectedPresenterParam ability, IPresenter presenter, IPresenter affectedPresenter, ActionModifier actionModifier)
        {
            if (ability.ActivateAbility)
            {
                ability.StartAbility(presenter, affectedPresenter, actionModifier);
            }
        }
        public void UseAbility(IAbilityWithAffectedPresenterVector2Param ability, IPresenter presenter, IPresenter affectedPresenter,Vector2 param, ActionModifier actionModifier)
        {
            if (ability.ActivateAbility)
            {
                ability.StartAbility(presenter, affectedPresenter, param, actionModifier);
            }
        }

        public void UseAbility(IAbilityWithAffectedPresentersParam ability, IPresenter presenter, IPresenter[] affectedPresenters, ActionModifier actionModifier)
        {
            if (ability.ActivateAbility)
            {
                ability.StartAbility(presenter, affectedPresenters, actionModifier);
            }
        }

        public IEnumerable<IAbility> GetAllAbility()
        {
            return _allAbilities;
        }

        public IAbility GetAbilityById(string abilityId)
        {
            
            return _allAbilities.FirstOrDefault(ability => ability.Id == abilityId);
        }
    }
}
