using System;
using UnityEngine;

namespace Main 
{	public class MovementCase  {
	
	
	
	    public Transform ball;
	    public Transform target;
	    public Vector3 center = Vector3.zero;
	    public float dis = 3f;
	
	    private float _px = 0f;
	    private float _pz = 0f;
	    private float _py = 0f;
	    private float _angele = 0f;
	
	    private float _ratio = 0.05f;
	    private float _moveSpeed = 0.05f;
	    private float _a = 0.001f;
	    private float _ax = 0.001f;
	    private float _az = 0.001f;
	
	    private float _moveSpeedx = 0.05f;
	    private float _moveSpeedz = 0.01f;
	
	
	
	
	    private void _yuanMove()
	    {
	        _angele += 0.1f;
	        _px = center.x + dis * (float)Math.Cos(_angele);
	        _pz = center.z + dis * (float)Math.Sin(_angele);
	        ball.localPosition = new Vector3(_px, ball.localPosition.y, _pz);
	    }
	    private void _tuoYuanMove()
	    {
	
	        _angele += 0.1f;
	        _px = center.x + dis * (float)Math.Cos(_angele);
	        _pz = center.z + dis * 0.5f * (float)Math.Sin(_angele);
	        _py = 0f;
	        ball.localPosition = new Vector3(_px, _py, _pz);
	    }
	    private void _huanDongMove()
	    {
	        float dis = Vector3.Distance(ball.localPosition, target.localPosition);
	
	        float speed = dis * _ratio;
	
	        _px = ball.localPosition.x + speed;
	        _py = 0f;
	        _pz = 0f;
	        ball.localPosition = new Vector3(_px, _py, _pz);
	    }
	
	    private void _JianXieMove()
	    {
	        _angele += 0.1f;
	        // _px = center.x + dis * (float)Math.Cos(_angele);
	        _pz = center.z + dis * (float)Math.Sin(_angele);
	        _py = 0f;
	        _px = 0f;
	        ball.localPosition = new Vector3(_px, _py, _pz);
	    }
	    private void _ZhengCiBoMove()
	    {
	        _angele += 0.15f;
	        _px = ball.localPosition.x + _moveSpeed;
	        _pz = center.z + dis * (float)Math.Sin(_angele);
	        _py = 0f;
	        ball.localPosition = new Vector3(_px, _py, _pz);
	    }
	    private void _YuSuMove()
	    {
	        float dis = Vector3.Distance(ball.localPosition, target.localPosition);
	        if (dis > 0.5f)
	        {
	            _px = ball.localPosition.x + _moveSpeed;
	            _py = 0f;
	            _pz = 0f;
	            ball.localPosition = new Vector3(_px, _py, _pz);
	        }
	    }
	    private void _JiaSuMove()
	    {
	
	        float dis = Vector3.Distance(ball.localPosition, target.localPosition);
	        if (dis > 0.5f)
	        {
	            _moveSpeed += _a;
	            _px = ball.localPosition.x + _moveSpeed;
	            _py = 0f;
	            _pz = 0f;
	            ball.localPosition = new Vector3(_px, _py, _pz);
	        }
	        else
	        {
	
	        }
	
	    }
	
	    private void _MoCaLiMove()
	    {
	        if (_moveSpeed >= 0.001f)
	            _moveSpeed *= 0.92f;
	        else
	            _moveSpeed = 0f;
	        _px = ball.localPosition.x + _moveSpeed;
	        _py = 0f;
	        _pz = 0f;
	        ball.localPosition = new Vector3(_px, _py, _pz);
	    }
	
	    private void _tanXinMove()
	    {
	        float disx = target.localPosition.x - ball.localPosition.x;
	        float disz = target.localPosition.z - ball.localPosition.z;
	        _ax = disx * 0.1f;
	        _az = disz * 0.1f;
	        _moveSpeedx += _ax;
	        _moveSpeedz += _az;
	        _moveSpeedx *= 0.95f;
	        _moveSpeedz *= 0.95f;
	        _px = ball.localPosition.x + _moveSpeedx;
	        _py = 0f;
	        _pz = ball.localPosition.z + _moveSpeedz;
	        ball.localPosition = new Vector3(_px, _py, _pz);
	        if (Math.Abs(_moveSpeedx) <= 0.0001f && Math.Abs(_moveSpeedz) <= 0.0001f)
	        {
	            target.localPosition = new Vector3(UnityEngine.Random.Range(-18f, 18f), 0f, UnityEngine.Random.Range(-10f, 10f));
	        }
	    }
	
	}
}
