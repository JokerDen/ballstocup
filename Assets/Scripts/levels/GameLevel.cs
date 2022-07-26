using gameplay;
using UnityEngine;
using UnityEngine.Events;

namespace levels
{
    public class GameLevel : MonoBehaviour
    {
        [SerializeField] GameRotator rotator;
        [SerializeField] Spawner ballsSpawner;

        [SerializeField] BallsCounter finished;
        [SerializeField] BallsCounter successful;

        private int totalBalls;
        private int requiredBalls;

        public int SucceedBalls => successful.Num;
        public int RequiredBalls => requiredBalls;
        public int LeftBalls => Mathf.Max(totalBalls - finished.Num, 0);

        public UnityEvent onBallsChanged = new UnityEvent();
        public UnityEvent onLevelWin = new UnityEvent();
        public UnityEvent onLevelFail = new UnityEvent();

        [SerializeField] float finishDelay;

        private void Start()
        {
            finished.onNumChanged.AddListener(OnFinishedNumChanged);
            successful.onNumChanged.AddListener(OnSuccessfulNumChanged);
        }

        private void OnSuccessfulNumChanged(int arg0)
        {
            onBallsChanged.Invoke();
        }

        private void OnFinishedNumChanged(int arg0)
        {
            if (arg0 >= totalBalls)
            {
                CancelInvoke("Finish");
                Invoke("Finish", finishDelay);
            }
            onBallsChanged.Invoke();
        }

        private void Finish()
        {
            if (successful.Num >= requiredBalls)
                onLevelWin.Invoke();
            else
                onLevelFail.Invoke();
        }

        public void SetUp(int balls, int required)
        {
            totalBalls = balls;
            requiredBalls = required;
            
            ballsSpawner.Spawn(balls);
        }

        public void MoveX(float deltaX)
        {
            rotator.Rotate(deltaX);
        }
    }
}