using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    [FormerlySerializedAs("exitbackgroundImageCanvasGroup")] public CanvasGroup victorybackgroundImageCanvasGroup;
    public CanvasGroup defeatbackgroundImageCanvasGroup;

    private PlayerCharacter _player;
    private bool _isPlayerAtExit;
    private float _timer;

    private void Start()
    {
        _player = FindObjectOfType<PlayerCharacter>();
        Observer[] observers = FindObjectsOfType<Observer>();
        foreach (Observer observer in observers)
        {
            observer.player = _player.head.transform;
            observer.playerLayer = 1 << _player.gameObject.layer;
            observer.OnPlayerCaught += DefeatEndLevel;
        }
    }

    private void Update()
    {
        if (_isPlayerAtExit)
        {
            VictoryEndLevel();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player.gameObject)
        {
            _isPlayerAtExit = true;
        }
    }

    private void DefeatEndLevel()
    {
        StartCoroutine(EndLevel(defeatbackgroundImageCanvasGroup, true));
    }

    private void VictoryEndLevel()
    {
        StartCoroutine(EndLevel(victorybackgroundImageCanvasGroup, false));
    }

    private IEnumerator EndLevel(CanvasGroup canvasGroup, bool shouldRestart)
    {
        while (_timer < fadeDuration)
        {
            _timer += Time.deltaTime;
            canvasGroup.alpha = _timer / fadeDuration;
            yield return null;
        }

        yield return new WaitForSeconds(displayImageDuration);

        if (shouldRestart)
        {
             SceneManager.LoadScene(0);
        }
        else
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
        }
    }
}
