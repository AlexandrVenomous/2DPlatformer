using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class PathFollower : MonoBehaviour
{
    private const float ZERO_AXIS = 0f;

    [SerializeField] private Transform _waypointsPath;
    [SerializeField] private float _durationOfWaiting;

    private Mover _mover;
    private Transform[] _waypoints;
    private int _currentWaypoint = 0;
    private bool _isReached = false;
    private float _difference = 0f;
    private float _horizontalAxis = 0f;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        InitiateWaypoints();
        DefineHorizontalAxis();
    }

    private void Update()
    {
        if (IsWaypointReached())
        {
            _isReached = true;
            _horizontalAxis = ZERO_AXIS;
            _currentWaypoint = ++_currentWaypoint % _waypoints.Length;

            StartCoroutine(WaitAndDefineAxis());
        }

        _mover.Move(_horizontalAxis);
    }

    private void InitiateWaypoints()
    {
        int waypointsCount = _waypointsPath.childCount;

        _waypoints = new Transform[waypointsCount];

        for (int i = 0; i < waypointsCount; i++)
            _waypoints[i] = _waypointsPath.GetChild(i);
    }

    private IEnumerator WaitAndDefineAxis()
    {
        WaitForSeconds wait = new(_durationOfWaiting);

        yield return wait;

        DefineHorizontalAxis();
        _isReached = false;
    }

    private bool IsWaypointReached()
    {
        if (_isReached == false)
        {
            if (_horizontalAxis > ZERO_AXIS)
                return transform.position.x > _waypoints[_currentWaypoint].position.x;
            else
                return transform.position.x < _waypoints[_currentWaypoint].position.x;
        }
        else
        {
            return !_isReached;
        }
    }

    private void DefineHorizontalAxis()
    {
        float right = 1f;
        float left = -right;

        _difference = _waypoints[_currentWaypoint].position.x - transform.position.x;

        if (_difference > _horizontalAxis)
            _horizontalAxis = right;
        else if (_difference < _horizontalAxis)
            _horizontalAxis = left;
    }
}
