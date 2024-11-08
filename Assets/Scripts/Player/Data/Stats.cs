using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    [SerializeField]
    private int baseValue;

    private int cachedValue;
    private bool isDirty = true; // A flag to indicate if the value needs recalculation

    public List<int> modifiers = new List<int>();

    public Stats(int initialValue)
    {
        baseValue = initialValue;
    }

    public int GetValue()
    {
        if (isDirty)
        {
            cachedValue = baseValue;
            foreach (int modifier in modifiers)
            {
                cachedValue += modifier;
            }
            cachedValue = Mathf.Max(cachedValue, 0);
            isDirty = false;
        }
        return cachedValue;
    }

    public void AddModifier(int modifier)
    {
        if (modifier != 0)
        {
            modifiers.Add(modifier);
            isDirty = true;
        }
    }

    public void RemoveModifier(int modifier)
    {
        if (modifiers.Remove(modifier))
        {
            isDirty = true;
        }
    }
}
