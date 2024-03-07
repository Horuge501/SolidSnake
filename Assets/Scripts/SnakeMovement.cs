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

    private List<Transform> _segments = new List<Transform>();

    public Transform segmentPrefab;

    [SerializeField] private int initialState = 4;

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

        ResetGame();
    }

    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        Vector3 newPosition = transform.position + (Vector3)(_direction * speed * Time.fixedDeltaTime);
        transform.position = new Vector3(Mathf.Round(newPosition.x) + _direction.x, Mathf.Round(newPosition.y) + _direction.y, 0.0f);
    }

    private void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }

    private void ResetGame()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(transform);

        for (int i = 1; i < initialState; i++)
        {
            _segments.Add(Instantiate(segmentPrefab));
        }

        transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Apple")
        {
            Grow();
        }
        else if (collision.tag == "Obstacle")
        {
            ResetGame();
        }
    }
}
