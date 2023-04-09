using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityHolder : MonoBehaviour
{
    public Ability ability;
    private float _cooldownTime;
    private float _activeTime;
    private bool _abilityUsing;

    public bool AbilityUsing
    {
        get
        {
            return _abilityUsing;
        }
        set
        {
            _abilityUsing = value;
        }
    }


    enum AbilityState
    {
        ready,
        active,
        cooldown
    }

    private AbilityState _state = AbilityState.ready;

    void Update()
    {
        if (ability == null) return; // Add this line to check if the ability is null

        switch (_state)
        {
            case AbilityState.ready:
                if (_abilityUsing)
                {
                    Debug.Log("using ability...");
                    ability.Activate(gameObject);
                    _state = AbilityState.active;
                    _activeTime = ability.activeTime;
                }
                break;
            case AbilityState.active:
                if (_activeTime > 0)
                {
                    _activeTime -= Time.deltaTime;
                }
                else
                {
                    _state = AbilityState.cooldown;
                    _cooldownTime = ability.cooldownTime;
                }
                break;
            case AbilityState.cooldown:
                if (_cooldownTime > 0)
                {
                    _cooldownTime -= Time.deltaTime;
                }
                else
                {
                    _state = AbilityState.ready;
                }
                break;
        }
    }



}
