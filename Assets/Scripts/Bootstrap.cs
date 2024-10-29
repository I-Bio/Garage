using Input;
using PickUps;
using Players;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private PlayerSetup _player;
    [SerializeField] private InputSetup _input;
    [SerializeField] private Storage _storage;

    private void Awake()
    {
        _storage.Init();
        _input.Init();
        _player.Init();
    }
}