using System;
using UnityEngine;

[Serializable]
public class LevelSystem
{
    public Action OnLevelUp;

    private float _levelUpExp;
    private float _currentExp;
    private int _level;
    private float _levelUpMultiplier;

    public float ExpPercent => _currentExp / _levelUpExp;
    public int Level => _level;

    public LevelSystem(float levelUpMultiplier, float levelUpExp)
    {
        _level = 1;
        _currentExp = 0;
        _levelUpExp = levelUpExp;
        _levelUpMultiplier = levelUpMultiplier;
    }

    public void AddExp(float expAmount)
    {
        _currentExp += expAmount;

        while (_currentExp >= _levelUpExp)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        _currentExp -= _levelUpExp;
        _level++;
        OnLevelUp?.Invoke();

        _levelUpExp *= _levelUpMultiplier;
    }
}
