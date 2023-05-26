using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextSetter highscoreTextSetter;
    [SerializeField] private Pause pause;
    [SerializeField] private AudioClip gameOverSound;

    public void ActivateGameOver(int distance)
    {
        gameObject.SetActive(true);
        AudioManager.instance.PlayClipAt(gameOverSound, transform.position);
        pause.DesactivatePause();
        CalculateHighscore(distance);
    }

    private void CalculateHighscore(int currentScore)
    {
        int highscore = PlayerPrefs.GetInt("Highscore", 0);

        if (highscore < currentScore)
        {
            highscoreTextSetter.NewText("Nouveau score : ");
            highscore = currentScore;
            PlayerPrefs.SetInt("Highscore", highscore);
            PlayerPrefs.Save();
        }

        highscoreTextSetter.UpdateText(highscore);
    }
}
