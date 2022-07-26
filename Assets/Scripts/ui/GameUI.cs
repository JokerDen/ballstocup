using levels;
using UnityEngine;

namespace ui
{
    public class GameUI : MonoBehaviour
    {
        /*
         * TODO: UI manager and base screens implementation required; no need for test task
         */
        
        [Header("Gameplay HUD")]
        [SerializeField] GameObject gameplayCondition;
        [SerializeField] TextFormat levelText;
        [SerializeField] TextFormat requiredBallsText;
        [SerializeField] TextFormat leftBallsText;
        private LevelsManager levels;   // <- injection possible
        
        [Header("Win Fail Conditions")]
        [SerializeField] GameObject winCondition;
        [SerializeField] GameObject failCondition;

        private GameLevel currentLevel;

        private void Start()
        {
            levels = GameManager.current.levels;
            
            ShowLevelGameplay(levels.Current);
            levels.onLevelLoaded.AddListener(ShowLevelGameplay);
        }

        private void ShowLevelGameplay(GameLevel level)
        {
            if (level == currentLevel) return;
            
            currentLevel = level;
            
            if (currentLevel != null)
            {
                currentLevel.onLevelWin.AddListener(ShowWin);   
                currentLevel.onLevelFail.AddListener(ShowFail); 
                currentLevel.onBallsChanged.AddListener(UpdateBalls);
                UpdateBalls();
                levelText.Show(levels.CurrentIndex + 1);
            
                ShowCondition(gameplayCondition);
            } else
                ShowCondition(null);
        }

        private void UpdateBalls()
        {
            requiredBallsText.Show(currentLevel.SucceedBalls, currentLevel.RequiredBalls);
            leftBallsText.Show(currentLevel.LeftBalls);
        }

        private void ShowWin()
        {
            ShowCondition(winCondition);
        }

        private void ShowFail()
        {
            ShowCondition(failCondition);
        }

        private void ShowCondition(GameObject condition)
        {
            failCondition.SetActive(condition == failCondition);
            gameplayCondition.SetActive(condition == gameplayCondition);
            winCondition.SetActive(condition == winCondition);
        }
    }
}