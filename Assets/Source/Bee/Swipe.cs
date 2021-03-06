﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour {

    private bool _swipeLeft;
    private bool _swipeRight;
    private bool _swipeUp;
    private bool _swipeDown;
    private bool _isDragging = false;

    private Vector2 _startTouch;
    private float _startTime;
    private Vector2 _swipeDelta;

    private float _defaultInertia = 5f;
    private float _currentInertia = 0.0f;

    public Vector2 swipeDelta { get { return _swipeDelta; } }
    public bool swipeLeft { get { return _swipeLeft; } }
    public bool swipeRight { get { return _swipeRight; } }
    public bool swipeUp { get { return _swipeUp; } }
    public bool swipeDown { get { return _swipeDown; } }

    public bool canSwipe = false;

    private void Update()
    {
        _swipeLeft = false;
        _swipeRight = false;
        _swipeUp = false;
        _swipeDown = false;

        if(canSwipe == false)
        {
            Reset();
            return;
        }

        #region Standalone Inputs
        if(Input.GetMouseButtonDown(0))
        {
            _startTouch = Input.mousePosition;
            _isDragging = true;
        } else if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            Reset();
        }
        #endregion

        #region Mobile Inputs
        if(Input.touches.Length != 0)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                _startTouch = Input.touches[0].position;
                _isDragging = true;
            } else if(Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                _isDragging = false;
                Reset();
            }
        }
        #endregion

        // Calculate the distance
        //_swipeDelta = Vector2.zero;
        if(_swipeDelta != Vector2.zero && _currentInertia > 0.0f)
        {
            _swipeDelta -= new Vector2(1.0f, 1.0f);
            _currentInertia--;
        } else if (_isDragging)
        {
            if(Input.touches.Length > 0)
            {
                _currentInertia = _defaultInertia;
                _swipeDelta = Input.touches[0].position - _startTouch;
            } else if(Input.GetMouseButton(0))
            {
                _swipeDelta = (Vector2)Input.mousePosition - _startTouch;
            }
        }

        // Did we cross the deadzone ?
        if(_swipeDelta.magnitude > 0)
        {
            // Direction ?
            float x = _swipeDelta.x;
            float y = _swipeDelta.y;

            // Left or Right
            if(x < 0)
            {
                _swipeLeft = true;
            } else
            {
                _swipeRight = true;
            }
            
            // Up or Down
            if(y < 0)
            {
                _swipeDown = true;
            } else
            {
                _swipeUp = true;
            }
        }
    }

    private void Reset()
    {
        _startTouch = Vector2.zero;
        _swipeDelta = Vector2.zero;
        _isDragging = false;
    }
}
