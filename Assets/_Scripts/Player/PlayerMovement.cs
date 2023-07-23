using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Purplik.PixelRoguelike.Input;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputReader input;
    private CharacterController _chc;

    [SerializeField] private float playerSpeed;

    private Vector2 _moveDir;

    private void Awake()
    {
        _chc = GetComponent<CharacterController>();
    }

    private void Start()
    {
        input.MoveEvent += HandleMove;
    }

    private void Update()
    {
        Vector3 move = new Vector3(_moveDir.x, 0f, _moveDir.y);
        _chc.Move(move * Time.deltaTime * playerSpeed);
    }

    private void HandleMove(Vector2 dir)
    {
        _moveDir = dir;
    }
}
