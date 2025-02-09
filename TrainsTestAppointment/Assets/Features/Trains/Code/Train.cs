using System;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.TrainsAppointment.Map;
using c1tr00z.TrainsAppointment.Map.Nodes;
using c1tr00z.TrainsAppointment.Pathfinding;
using c1tr00z.TrainsAppointment.Utils;
using UnityEngine;
using Random = UnityEngine.Random;
namespace c1tr00z.TrainsAppointment.Trains {
    public class Train : MonoBehaviour {

        #region Private Fields

        private Vector3 _startPosition;
        
        private Vector3 _endPosition;

        private Node _currentNode;

        private Node _targetNode;

        private RoutePart _routePart;

        private float _passedDistance = 0;

        private Route _route = new();

        #endregion
        
        #region Serialized Fields

        [SerializeField] private float _speed = 10;
        [SerializeField] private float _miningSpeed = 1;

        #endregion

        #region Accessors

        // public List<Path> 

        #endregion

        #region Unity Events

        private void Awake() {
            _currentNode = MapUtils.GetRandomNode();
            transform.position = _currentNode.transform.position;
            FindRoute();
            // _targetNode = _route.Dequeue().targetNode;
            _targetNode = _routePart.targetNode;
            _passedDistance = 0;
            _startPosition = transform.position;
            _endPosition = _targetNode.transform.position;
            transform.rotation = Quaternion.LookRotation((_endPosition - _startPosition).normalized);
        }

        private void Update() {
            _passedDistance += Time.deltaTime * _speed;
            var progress = _passedDistance / _routePart.lenght;
            transform.position = Vector3.Lerp(_startPosition, _endPosition, progress);
            if (progress >= 1) {
                _currentNode = _targetNode;
                if (_route.Count == 0) {
                    FindRoute();
                } else {
                    _routePart = _route.Dequeue();
                }
                _targetNode = _routePart.targetNode;
                _passedDistance = 0;
                _startPosition = transform.position;
                _endPosition = _targetNode.transform.position;
                transform.rotation = Quaternion.LookRotation((_endPosition - _startPosition).normalized);
            }
        }

        private void FindRoute() {
            var allMines = MapUtils.GetAllMines();
            var allBases = MapUtils.GetAllBases();
            var allRoutes = new List<Route>();
            allMines.ForEach(m => {
                var routesToMines = PathfindingUtils.FindAllPossibleRoutes(_currentNode, m);
                routesToMines.ForEach(r2m => {
                    allBases.ForEach(b => {
                        var routesToBases = PathfindingUtils.FindAllPossibleRoutes(m, b);
                        routesToBases.ForEach(r2b => {
                            var route = new Route();
                            route.AddToRoute(r2m);
                            route.AddToRoute(r2b);
                            allRoutes.Add(route);
                        });
                    });
                });
            });
            _route = allRoutes.MaxElement(r => r.CalculatePrice(_speed, _miningSpeed));
            _routePart = _route.Dequeue();
        }

        private void OnDrawGizmos() {
            if (_targetNode is not null) {
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(transform.position, _targetNode.transform.position);
            }
            if (_route.Count > 0) {
                Gizmos.color = Color.yellow;
                var allRouteParts = _route.ToList();
                allRouteParts.ForEach(p => Gizmos.DrawLine(p.targetNode.transform.position, p.startNode.transform.position));
            }
        }

        #endregion
    }
}