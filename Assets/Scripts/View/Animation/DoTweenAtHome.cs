using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DoTweenAtHome
{
    [SerializeField, Range(.1f, 2f)] private float _duration;
    [SerializeField] private AnimationCurve _curve = AnimationCurve.Linear(0, 0, 1, 1);
    private float _timeLeft;
    private float _factor;
    private Action<float> _updateAction;
    public Action<float> UpdateAction { set => _updateAction = value; }

    [ContextMenu("StartAnim")]
    public void Start()
    {
        _timeLeft = _duration;
    }
    
    public void Update(float deltaTime)
    {
        if(_timeLeft > 0)
        {
            _timeLeft -= deltaTime;

            _factor = 1 - (_timeLeft / _duration);
            _factor = _curve.Evaluate(_factor);

            _updateAction.Invoke(_factor);

            if(_timeLeft < 0)
            {
                End();
            }
        }
    }

    void End()
    {
        Debug.Log("Anim End !");
    }
}
