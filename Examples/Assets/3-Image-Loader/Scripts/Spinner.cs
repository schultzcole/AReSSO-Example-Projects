#nullable enable
using UnityEngine;

namespace ImageLoader.Scripts
{
    public class Spinner : MonoBehaviour
    {
        [SerializeField] private float rotationRate = 1;
        [SerializeField] private Vector3 axis = Vector3.left;

        private Transform? trans;

        private void Awake() { trans = transform; }

        private void Update() { trans!.Rotate(axis, rotationRate * Time.deltaTime); }
    }
}