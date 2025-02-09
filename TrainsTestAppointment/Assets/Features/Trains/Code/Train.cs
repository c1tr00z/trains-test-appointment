using System;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.TrainsAppointment.Map;
using c1tr00z.TrainsAppointment.Map.Nodes;
using c1tr00z.TrainsAppointment.MineResources;
using c1tr00z.TrainsAppointment.Pathfinding;
using c1tr00z.TrainsAppointment.Utils;
using UnityEngine;
using Random = UnityEngine.Random;
namespace c1tr00z.TrainsAppointment.Trains {
    public class Train : MonoBehaviour, INodePasser, IResourceHolder {

        #region Private Fields

        private Vector3 _startPosition;
        
        private Vector3 _endPosition;

        private Node _currentNode;

        private Node _targetNode;

        private RoutePart _routePart;

        private float _passedDistance = 0;

        private Route _route = new();

        private bool _occupied;

        #endregion
        
        #region Serialized Fields

        [SerializeField] private float _speed = 10;
        [SerializeField] private float _timeToMine = 1;

        #endregion

        #region Unity Events

        private void Awake() {
            _currentNode = MapUtils.GetRandomNode();
            transform.position = _currentNode.transform.position;
            FindRoute();
            _targetNode = _routePart.targetNode;
            _passedDistance = 0;
            _startPosition = transform.position;
            _endPosition = _targetNode.transform.position;
            transform.rotation = Quaternion.LookRotation((_endPosition - _startPosition).normalized);
        }

        private void Update() {
            if (_occupied) {
                return;
            }
            _passedDistance += Time.deltaTime * _speed;
            var progress = _passedDistance / _routePart.lenght;
            transform.position = Vector3.Lerp(_startPosition, _endPosition, progress);
            if (progress >= 1) {
                if (_targetNode is IPassableNode passable) {
                    passable.Pass(this);
                }
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

        private void OnDrawGizmosSelected() {
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

        #region Class Implementation

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
            _route = allRoutes.MaxElement(r => r.CalculatePrice(_speed, _timeToMine));
            _routePart = _route.Dequeue();
        }

        #endregion

        #region INodePasser Implementation

        public float TimeToMine => _timeToMine;

        public void Occupy() {
            _occupied = true;
        }

        public void Release() {
            _occupied = false;
        }

        #endregion
        
        #region IResourceHolder Implementation

        public bool HasResource { get; private set; }

        public void AddResource() {
            HasResource = true;
        }

        public void RemoveResource() {
            HasResource = false;
        }

        #endregion
    }
}