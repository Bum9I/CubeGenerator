using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RecoloringBehaviour : MonoBehaviour
{
    [SerializeField] private float _recoloringDuration;
    [SerializeField] private float _delayDuration;

    private Color _startColor;
    private Color _nextColor;
    private Renderer _renderer;
    private float _recoloringTime;
    private float _delayTime;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        GenerateNextColor();
        _delayTime = _delayDuration;
    }

    private void Update()
    {
        _delayTime += Time.deltaTime;
        if (_delayTime < _delayDuration)
            return;

        _recoloringTime += Time.deltaTime;
        var progress = _recoloringTime / _recoloringDuration;
        var currentColor = Color.Lerp(_startColor, _nextColor, progress);
        _renderer.material.color = currentColor;

        if (_recoloringTime >= _recoloringDuration)
        {
            _recoloringTime = 0f;
            _delayTime = 0f;
            GenerateNextColor();
        }
    }

    private void GenerateNextColor()
    {
        _startColor = _renderer.material.color;
        _nextColor = Random.ColorHSV(0f, 1f, 0.8f, 1f, 1f, 1f);
    }
}