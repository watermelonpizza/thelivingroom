using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameStateManager))]
[RequireComponent(typeof(MementoManager))]
[RequireComponent(typeof(MoverManager))]
public class WaveGenerator : MonoBehaviour
{
    public GameSettings gameSettings;

    private GameStateManager _gameStateManager;
    private MementoManager _mementoManager;
    private MoverManager _moverManager;
    private Coroutine _runningWave;

    public AudioSource dogBark; //IN PROGRESS

    public GameObject dogIndicator;
    Animator dogAnim;

    private void Start()
    {
        _gameStateManager = GetComponent<GameStateManager>();
        _mementoManager = GetComponent<MementoManager>();
        _moverManager = GetComponent<MoverManager>();

        dogAnim = dogIndicator.GetComponent <Animator>();
    }

    private void Update()
    {
        if (_gameStateManager.gameState != GameStateManager.GameState.Running)
        {
            if (_runningWave != null)
            {
                StopCoroutine(_runningWave);
            }

            return;
        }

        if (_runningWave == null)
        {
            _runningWave = StartCoroutine(RunNextWave());
        }
    }


    private IEnumerator RunNextWave()
    {
        dogAnim.SetTrigger("WaveSpawning");


        _gameStateManager.currentWaveNumber = Mathf.Clamp(_gameStateManager.currentWaveNumber + 1, 0, gameSettings.waves.Length);

        _gameStateManager.currentWave = gameSettings.waves[_gameStateManager.currentWaveNumber - 1].Clone();

        while (_gameStateManager.currentWave.numberOfEnemies > 0)
        {
            Memento memento = null;
            yield return new WaitUntil(() => _mementoManager.TryClaimMemento(out memento));

            var mover = _moverManager.SpawnMover();
            _gameStateManager.currentWave.numberOfEnemies--;
            mover.TargetMemento(memento);

            yield return new WaitForSeconds(_gameStateManager.currentWave.secondsBetweenEachSpawn + Random.Range(-_gameStateManager.currentWave.spawnJitter, _gameStateManager.currentWave.spawnJitter));
        }

        yield return new WaitUntil(() => _moverManager.AllMoversDestroyed());

        // Wave ended. Move to next wave.
        if (_gameStateManager.currentWaveNumber == gameSettings.waves.Length)
        {
            _gameStateManager.gameState = GameStateManager.GameState.GameOver;
        }
        else
        {
            yield return new WaitForSeconds(gameSettings.timeBetweenWaves);
            _runningWave = null;

        }
    }
}
