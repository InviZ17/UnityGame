using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractModificator : ScriptableObject
{
    public string _name;
    public string _description;

    public virtual void UpdateFromStats(Stat stat)
    {
        throw new NotImplementedException();
    }

}