using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 _lookOffset = new Vector3(0, 0, -10);
    private Vector3 _shakeOffset;
    private Vector3 _lastPos;
    private readonly List<Shake> _shakesToRemove = new List<Shake>();
    private readonly List<Shake> _shakes = new List<Shake>();

    [SerializeField] private float _smoothSpeed;
    [field: SerializeField] public Transform Target { get; set; }
    [field: SerializeField] public List<Transform> SecondaryTargets { get; set; }
    [field: SerializeField] public bool SecondaryTargetFollow { get; set; }
    [field: SerializeField] public bool FollowSnapshot { get; set; }
    [field: SerializeField] public float SnapshotLerpValue { get; set; } = 0.2f;
    public Vector3 Movement { get; set; }
    public static CameraFollow Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void LateUpdate()
    {
        CalculateCameraShake();
        Follow();
    }

    private void Follow()
    {
        //Don't do anything if no target
        if (Target == null)
            return;

        Vector3 targetPosition = Target.position;

        Vector3 secondaryTargetPosition = targetPosition;
        for (int i = 0; i < SecondaryTargets.Count; i++)
        {
            Transform secondaryTarget = SecondaryTargets[i];
            secondaryTargetPosition += secondaryTarget.position;
        }
        secondaryTargetPosition /= 1 + SecondaryTargets.Count;
        Vector3 finalTargetPosition = Vector2.Lerp(targetPosition, secondaryTargetPosition, 0.2f);
        if (!SecondaryTargetFollow)
        {
            finalTargetPosition = targetPosition;
        }
        Vector3 snapPosition = finalTargetPosition + _lookOffset + _shakeOffset;
        Vector3 pixelPosition = Helpers.PixelSnap(snapPosition);
        transform.position = pixelPosition;
        Movement = transform.position - _lastPos;
        _lastPos = transform.position;
        Movement = Helpers.PixelSnap(Movement);
    }

    private void CalculateCameraShake()
    {
        _shakeOffset = Vector3.zero;
        for (int i = 0; i < _shakes.Count; i++)
        {
            Shake shake = _shakes[i];
            float xOffset = UnityEngine.Random.Range(-shake.strength, shake.strength);
            float yOffset = UnityEngine.Random.Range(-shake.strength, shake.strength);
            _shakeOffset += new Vector3(xOffset, yOffset);
            shake.time -= Time.deltaTime;
            shake.strength *= 0.98f;
            if (shake.time <= 0f)
            {
                _shakesToRemove.Add(shake);
            }
        }

        for (int i = 0; i < _shakesToRemove.Count; i++)
        {
            Shake shake = _shakesToRemove[i];
            _shakes.Remove(shake);
        }

        _shakesToRemove.Clear();
    }

    public void Screenshake(int pixelStrength, float duration)
    {
        float strength = pixelStrength / 16f;
        Shake shake = new Shake(strength, duration);
        _shakes.Add(shake);
    }

    private class Shake
    {
        public float strength;
        public float time;
        public Shake(float strength, float time)
        {
            this.strength = strength;
            this.time = time;
        }
    }
}