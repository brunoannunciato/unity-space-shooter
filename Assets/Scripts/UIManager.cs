using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _score;
    [SerializeField]
    private Image _liveSprite;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _reloadText;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _score.text = $"Score: {0}";
        _gameOverText.gameObject.SetActive(false);
        _reloadText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.Log("GameManager does not exists");
        }
    }

    public void UpdateScore(int newScore)
    {
        _score.text = $"Score: {newScore}";
    }

    public void UpdateLives(int currentLive)
    {
        _liveSprite.sprite = _livesSprites[currentLive];
    }

    public void ActiveGameOverOverlay() {
        StartCoroutine(FlickGameroverTextRoutine());
        _gameManager.GameOver();
        _reloadText.gameObject.SetActive(true);

    }

    private IEnumerator FlickGameroverTextRoutine()
    {
        while (true)
        {
            _gameOverText.gameObject.SetActive(true);

            yield return new WaitForSeconds(.5f);

            _gameOverText.gameObject.SetActive(false);

            yield return new WaitForSeconds(.25f);
        }
    }
}
