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
    private Action _startAction;
    private Action _endAction;
    public Action<float> UpdateAction { set => _updateAction = value; }
    public Action StartAction { set => _startAction = value; }
    public Action EndAction { set => _endAction = value; }

    [ContextMenu("StartAnim")]
    public void Start()
    {
        _timeLeft = _duration;
        _startAction.Invoke();
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
        _endAction.Invoke();
    }

    public void EndAnimation()
    {
        _timeLeft = 0;
    }
}
