using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private SnakeController controller;

    private Vector2 _direction = Vector2.right;

    private List<Transform> _segments;

    private void Awake()
    {
        controller = new SnakeController();
    }

    void Start()
    {
        controller.Enable();
        controller.Movement.Up.performed += ctx => _direction = Vector2.up;
        controller.Movement.Down.performed += ctx => _direction = Vector2.down;
        controller.Movement.Left.performed += ctx => _direction = Vector2.left;
        controller.Movement.Right.performed += ctx => _direction = Vector2.right;
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = transform.position + (Vector3)(_direction * speed * Time.fixedDeltaTime);
        transform.position = new Vector3(Mathf.Round(newPosition.x) + _direction.x, Mathf.Round(newPosition.y) + _direction.y, 0.0f);
    }
}
