using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    public GameSettings gameSettings;
    public GameState gameState;
    public int currentWaveNumber;
    public GameSettings.Wave currentWave;
    public GameObject endMessageObject;

    public int currentFeels;
    public GameSettings SceneName;

    public Image stolenFlash;
    public bool itemStolen;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public float flashSpeed = 5f;

    public int windowSmashWave;

    public Canvas pauseCanvas;

    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unpaused;

    private bool _pausedToggled = false;
    private Coroutine _flashRoutine;

    public enum GameState
    {
        PreStart,
        Menu,
        Running,
        Paused,
        GameOver,
        EndScreen,
    }

    private void Start()
    {
        gameState = GameState.Running;
        currentFeels = gameSettings.startingFeels;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.R))
        {
            Destroy(gameObject);
            SceneManager.LoadScene("TitleScene");
            return;
        }

        if (gameState == GameState.EndScreen)
        {
            return;
        }

        if (Input.GetAxisRaw("Cancel") > 0)
        {
            if (!_pausedToggled)
            {
                if (gameState == GameState.Running)
                {
                    pauseCanvas.gameObject.SetActive(true);
                    gameState = GameState.Paused;
                    Time.timeScale = 0;
                    paused.TransitionTo(.01f);

                }
                else
                {
                    pauseCanvas.gameObject.SetActive(false);
                    gameState = GameState.Running;
                    Time.timeScale = 1;
                    unpaused.TransitionTo(0.1f);
                }

                _pausedToggled = true;
            }
        }
        else
        {
            _pausedToggled = false;
        }

        if (currentFeels <= 0)
        {
            gameState = GameState.GameOver;
        }

        if (gameState == GameState.GameOver)
        {
            gameState = GameState.EndScreen;

            foreach (var otherManager in gameObject.GetComponents<MonoBehaviour>().Where(x => x != this))
            {
                Destroy(otherManager);
            }

            StartCoroutine(PlayVictoryDefeatScreen());
        }

        //flash the screen when an item is stolen
        if (itemStolen)
        {
            if (_flashRoutine != null)
            {
                StopCoroutine(_flashRoutine);
            }

            _flashRoutine = StartCoroutine(FlashItemStolen());
            itemStolen = false;
        }
    }

    private IEnumerator PlayVictoryDefeatScreen()
    {
        Animator animator = endMessageObject.GetComponent<Animator>();

        if (currentFeels > 0)
        {
            animator.SetTrigger("Victory");
        }
        else
        {
            animator.SetTrigger("Defeat");
        }

        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(gameSettings.SceneName);
    }

    private IEnumerator FlashItemStolen()
    {
        stolenFlash.color = flashColour;

        float flashProgressTime = 0;

        do
        {
            flashProgressTime += Time.deltaTime;
            stolenFlash.color = Color.Lerp(stolenFlash.color, Color.clear, Mathf.InverseLerp(0, flashSpeed, flashProgressTime));
            yield return new WaitForEndOfFrame();
        } while (flashProgressTime <= flashSpeed);
    }
}
