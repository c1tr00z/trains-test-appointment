using System;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.TrainsAppointment.Map;
using c1tr00z.TrainsAppointment.Map.Nodes;
using UnityEngine;
using Random = UnityEngine.Random;
namespace c1tr00z.TrainsAppointment.Trains {
    public class Train : MonoBehaviour {

        #region Private Fields

        private Vector3 _startPosition;
        
        private Vector3 _endPosition;

        private Node _targetNode;

        private Path _currentPath;

        private float _passedDistance = 0;

        private Map.Map _map;

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
            _map = FindObjectOfType<Map.Map>();
            var allNodes = _map.GetAllNodes();
            var myNode = allNodes[Random.Range(0, allNodes.Count)];
            var allPossiblePaths = _map.AllPaths.Where(p => p.Nodes.Contains(myNode)).ToList();
            _currentPath = allPossiblePaths[Random.Range(0, allPossiblePaths.Count)];
            _passedDistance = 0;
            _targetNode = _currentPath.Nodes.FirstOrDefault(n => n != myNode);
            _startPosition = myNode.transform.position;
            _endPosition = _targetNode.transform.position;
            transform.rotation = Quaternion.LookRotation((_endPosition - _startPosition).normalized);
        }

        private void Update() {
            _passedDistance += Time.deltaTime * _speed;
            var progress = _passedDistance / _currentPath.Length;
            transform.position = Vector3.Lerp(_startPosition, _endPosition, progress);
            if (progress >= 1) {
                var allPossiblePaths = _map.AllPaths.Where(p => p.Nodes.Contains(_targetNode)).ToList();
                _currentPath = allPossiblePaths[Random.Range(0, allPossiblePaths.Count)];
                _passedDistance = 0;
                _targetNode = _currentPath.Nodes.FirstOrDefault(n => n != _targetNode);
                _startPosition = transform.position;
                _endPosition = _targetNode.transform.position;
                transform.rotation = Quaternion.LookRotation((_endPosition - _startPosition).normalized);
            }
        }

        #endregion
    }
}