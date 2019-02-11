using System.Collections;
using UnityEngine;

[RequireComponent(typeof(GameStateManager))]
[RequireComponent(typeof(MementoManager))]
[RequireComponent(typeof(MoverManager))]
[RequireComponent(typeof(SpawnPointManager))]
public class WaveGenerator : MonoBehaviour
{
    public GameSettings gameSettings;

    [Range(0, 10)]
    public float secondsWaveIndicatorTakes = 2;

    [Range(0, 10)]
    public float secondsBetweenWaveIndicatorAndStart = .5f;

    public GameObject waveIndicator;
    public GameObject dogIndicator;
    public AudioClip dogBark;
    public GameObject windowObject;

    [Range(0, 10)]
    public float secondsToBreakWindow = 3;

    private GameStateManager _gameStateManager;
    private Animator _waveIndicatorAnimator;
    private MementoManager _mementoManager;
    private MoverManager _moverManager;
    private SpawnPointManager _spawnPointManager;
    private Coroutine _runningWave;
    private AudioSource _audioSource;
    private Animator _dogAnimation;
    private Animator _windowAnimation;


    private void Start()
    {
        _gameStateManager = GetComponent<GameStateManager>();
        _waveIndicatorAnimator = waveIndicator.GetComponent<Animator>();
        _mementoManager = GetComponent<MementoManager>();
        _moverManager = GetComponent<MoverManager>();
        _spawnPointManager = GetComponent<SpawnPointManager>();

        _dogAnimation = dogIndicator.GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        _windowAnimation = windowObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (_gameStateManager.gameState != GameStateManager.GameState.Running
            && _gameStateManager.gameState != GameStateManager.GameState.Paused)
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
        _gameStateManager.currentWaveNumber = Mathf.Clamp(_gameStateManager.currentWaveNumber + 1, 0, gameSettings.waves.Length);
        _gameStateManager.currentWave = gameSettings.waves[_gameStateManager.currentWaveNumber - 1].Clone();

        float extraPause = Mathf.Min(gameSettings.timeBetweenWaves - secondsWaveIndicatorTakes, secondsWaveIndicatorTakes);
        yield return new WaitForSeconds(extraPause);

        _waveIndicatorAnimator.SetInteger("WaveNumber", _gameStateManager.currentWaveNumber);

        yield return new WaitForSeconds(secondsWaveIndicatorTakes);

        _waveIndicatorAnimator.SetInteger("WaveNumber", 0);

        yield return new WaitForSeconds(secondsBetweenWaveIndicatorAndStart);

        _dogAnimation.SetTrigger("WaveSpawning");

        _audioSource.clip = dogBark;
        _audioSource.Play();

        foreach (var spawnPoint in _spawnPointManager.spawnPoints)
        {
            spawnPoint.enabled = spawnPoint.enableOnWave <= _gameStateManager.currentWaveNumber;
        }

        if (_gameStateManager.currentWaveNumber == _gameStateManager.windowSmashWave)
        {
            _windowAnimation.SetTrigger("windowBreak");
            yield return new WaitForSeconds(secondsToBreakWindow);
        }

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
            _runningWave = null;
        }
    }
}
