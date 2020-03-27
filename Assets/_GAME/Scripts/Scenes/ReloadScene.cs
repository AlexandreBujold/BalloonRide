using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BalloonGame.Scenes
{
    public class ReloadScene : MonoBehaviour
    {
        public float delay = 1f;
        public bool slowTimeOnCompletion = true;
        public float slowTime = 1f;
        [Range(0, 1)]
        public float timeScaleGoal = 0.25f;

        private Coroutine slowLoadCoroutine = null;

        public void LoadLevel(int levelIndex)
        {
            if (null != SceneManager.instance)
            {
                if (System.Enum.IsDefined(typeof(SceneIndexes), levelIndex))
                {
                    SceneManager.instance.ChangeScene((SceneIndexes)levelIndex);
                }
                else
                {
                    Debug.LogWarning(levelIndex.ToString() + " is not a valid build index!", gameObject);
                }
            }
        }

        public void ReloadLevel(int levelToLoadIndex)
        {
            Debug.Log(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + " has been completed!");
            LoadLevel(levelToLoadIndex);
        }

        public void ReloadLevelWithSlow()
        {
            SceneIndexes currentScene = SceneManager.instance.currentScene;
            int levelToLoadIndex = (int)currentScene;
            if (slowTimeOnCompletion == true)
            {
                if (slowLoadCoroutine == null)
                {
                    slowLoadCoroutine = StartCoroutine(SlowDownAndLoad(levelToLoadIndex));
                }
            }
        }

        public IEnumerator SlowDownAndLoad(int levelIndex)
        {
            yield return new WaitForSecondsRealtime(delay);
            //Slow down time then load Level
            for (float i = 0; i < slowTime; i += Time.fixedDeltaTime)
            {
                float t = i / slowTime;
                Time.timeScale = Mathf.Lerp(1, timeScaleGoal, t);
                yield return new WaitForFixedUpdate();
            }

            //Slow down complete, load level
            Time.timeScale = 1;
            ReloadLevel(levelIndex);
            slowLoadCoroutine = null;
        }
    }
}
