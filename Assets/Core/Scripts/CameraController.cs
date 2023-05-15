using System;
using UnityEngine;
using MyJenga.UI.Scripts;

namespace MyJenga.Core.Scripts
{
	public class CameraController : MonoBehaviour
	{
		[SerializeField] private Camera _camera;

		[Header("Orbit movement settings")]
		[SerializeField] private float _rotateSpeed = 5.0f;
		[SerializeField] private float _orbitDistance = 20.0f;
		[SerializeField] private float _minXAngle = -89.0f;
		[SerializeField] private float _maxXAngle = 89.0f;
		[SerializeField] private float _transitionTime = 3.0f;
		[SerializeField] [Range(0.01f, 1.0f)] private float _rotationSlerp = 0.02f;

		private float _rotX;
		private float _rotY;

		private Vector3 _orbitCenter;
		private Quaternion _cameraRotation;

		private bool _isTransitioning;
		private float _elapsedTransitionTime;
		private Vector3 _originalCameraPosition;
		private Vector3 _targetCameraPosition;
		private Quaternion _originalCameraRotation;
		private Quaternion _targetCameraRotation;

		/// <summary>
		/// Notifies listeners if camera is currently transitioning or not
		/// </summary>
		public event Action<bool> TransitionStateChanged;

		/// <summary>
		/// The current transition status of this camera controller
		/// </summary>
		public bool IsTransitioning => _isTransitioning;

		private void Update()
		{
			// Click and drag with left mouse button to rotate camera
			if (Input.GetMouseButton(0) && !_isTransitioning && !UIStatics.IsMouseOverUI())
			{
				_rotX += -Input.GetAxis("Mouse Y") * _rotateSpeed;
				_rotY += Input.GetAxis("Mouse X") * _rotateSpeed;

				if (_rotX < _minXAngle)
				{
					_rotX = _minXAngle;
				}
				else if (_rotX > _maxXAngle)
				{
					_rotX = _maxXAngle;
				}
			}
		}

		private void LateUpdate()
		{
			if (_isTransitioning)
			{
				UpdateTransition();
			}
			else
			{
				UpdateMouseRotation();
			}
		}

		/// <summary>
		/// Initialises a new orbit for the camera.
		/// </summary>
		/// <param name="orbitCenter"></param>
		/// <param name="cameraStartRotation"></param>
		public void SetupOrbit(Vector3 orbitCenter, Quaternion cameraStartRotation)
		{
			if (_isTransitioning)
			{
				return;
			}

			_orbitCenter = orbitCenter;
			_cameraRotation = cameraStartRotation;
			_rotX = _cameraRotation.eulerAngles.x;
			_rotY = _cameraRotation.eulerAngles.y;

			Vector3 direction = new(0, 0, -_orbitDistance);
			_targetCameraPosition = _orbitCenter + _cameraRotation * direction;
			_targetCameraRotation = cameraStartRotation;
			_originalCameraPosition = _camera.transform.position;
			_originalCameraRotation = _camera.transform.rotation;

			// Start a transition if the target position has changed
			if (_targetCameraPosition != _originalCameraPosition)
			{
				_isTransitioning = true;
				TransitionStateChanged?.Invoke(true);
			}
		}

		private void UpdateMouseRotation()
		{
			Quaternion newRotation = Quaternion.Euler(_rotX, _rotY, 0);
			_cameraRotation = Quaternion.Slerp(_cameraRotation, newRotation, _rotationSlerp);

			Vector3 direction = new(0, 0, -_orbitDistance);
			_camera.transform.position = _orbitCenter + _cameraRotation * direction;
			_camera.transform.LookAt(_orbitCenter);
		}

		private void UpdateTransition()
		{
			_elapsedTransitionTime += Time.deltaTime;

			if (_elapsedTransitionTime >= _transitionTime)
			{
				// Fix final position / rotation
				_camera.transform.position = _targetCameraPosition;
				_camera.transform.rotation = _targetCameraRotation;

				_isTransitioning = false;
				_elapsedTransitionTime = 0.0f;
				TransitionStateChanged?.Invoke(false);

				return;
			}

			float fTime = _elapsedTransitionTime / _transitionTime;

			// Interpolate position and rotation
			_camera.transform.position = Vector3.Lerp(_originalCameraPosition, _targetCameraPosition, fTime);
			_camera.transform.rotation = Quaternion.Slerp(_originalCameraRotation, _targetCameraRotation, fTime);
		}
	}
}