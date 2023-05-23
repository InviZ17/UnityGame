using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public List<Ability> abilities;
    private List<float> _cooldownTimes;
    private List<float> _activeTimes;
    private List<bool> _abilityUsing;
    private List<AbilityState> _abilityStates;

    void Awake()
    {
        int abilityCount = abilities.Count;
        _abilityUsing = new List<bool>(abilityCount);
        _cooldownTimes = new List<float>(abilityCount);
        _activeTimes = new List<float>(abilityCount);
        _abilityStates = new List<AbilityState>(abilityCount);

        for (int i = 0; i < abilityCount; i++)
        {
            _abilityUsing.Add(false);
            _cooldownTimes.Add(0);
            _activeTimes.Add(0);
            _abilityStates.Add(AbilityState.ready);
        }
    }

    public bool GetAbilityUsing(int abilityIndex)
    {
        return _abilityUsing[abilityIndex];
    }

    public void SetAbilityUsing(int abilityIndex, bool val)
    {
        _abilityUsing[abilityIndex] = val;
    }

    enum AbilityState
    {
        ready,
        active,
        cooldown
    }

    void Update()
    {
        for (int abilityIndex = 0; abilityIndex < abilities.Count; abilityIndex++)
        {
            var ability = abilities[abilityIndex];
            if (ability == null) continue;

            var state = _abilityStates[abilityIndex];

            switch (state)
            {
                case AbilityState.ready:
                    if (GetAbilityUsing(abilityIndex))
                    {
                        ability.Activate(gameObject);
                        _abilityStates[abilityIndex] = AbilityState.active;
                        //_abilityStates[abilityIndex] = AbilityState.cooldown;
                        _activeTimes[abilityIndex] = ability.activeTime;
                    }
                    break;
                case AbilityState.active:
                    if (_activeTimes[abilityIndex] > 0)
                    {
                        _activeTimes[abilityIndex] -= Time.deltaTime;
                    }
                    else
                    {
                        _abilityStates[abilityIndex] = AbilityState.cooldown;
                        _cooldownTimes[abilityIndex] = ability.cooldownTime;
                    }
                    break;
                case AbilityState.cooldown:
                    if (_cooldownTimes[abilityIndex] > 0)
                    {
                        _cooldownTimes[abilityIndex] -= Time.deltaTime;
                    }
                    else
                    {
                        _abilityStates[abilityIndex] = AbilityState.ready;
                    }
                    break;
            }
        }
    }
}
