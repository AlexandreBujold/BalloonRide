﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

namespace BalloonGame.Scenes
{
    public class SceneManager : MonoBehaviour
    {
        //LoadScene should simply LOAD the scene
        //ChangeScenes should LOAD if not loaded, and then change to that scene (set as active scene)

        [Header("Scene Management Properties")]
        public static SceneManager instance;
        public bool unloadCurrentOnChange = true;
        public SceneIndexes currentScene;
        private SceneIndexes pastScene;

        [Header("Loading Screen Properties")]
        public CanvasGroup loadingScreen;
        public bool animate = true;
        public float fadeTime;
        public AnimationCurve fadeCurve = new AnimationCurve();
        private bool loadingScreenVisible = false;
        private Coroutine transitionAnimation;
        [Space]
        public UnityEngine.UI.Image loadingScreenImage;
        public List<Sprite> loadingScreenImages;

        [Space]
        public Slider progressBar;
        [Space]

        [Header("Tips Properties")]
        public bool showTips = true;
        public Text tipText;
        public CanvasGroup tipCanvasGroup;
        public List<string> tips;
        [Space]
        public float timeBetweenTips = 3;
        private Coroutine cycleTipsCoroutine;
        public UnityEngine.UI.Text loadingScreenTitleText;

        [HideInInspector]
        public List<AsyncOperation> loadingScenes;

        public bool paused = false;
        public bool pauseEnabled = false;

        public UnityEvent OnPause;

        // Start is called before the first frame update
        void Start()
        {
            //Singleton
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(instance.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            //Set loading screen to inspector value and do not animate
            SetLoadingScreen(loadingScreenVisible, false);
            //Reset loadingScenes list
            loadingScenes = new List<AsyncOperation>();
            UpdateCurrentScene();
            pastScene = currentScene;
        }

        // Update is called once per frame
        void Update()
        {
            //Testing

            //if (UnityEngine.Input.GetMouseButtonDown(0))
            //{
            //    SetLoadingScreen(!loadingScreenVisible, animate);
            //}

            //if (UnityEngine.Input.GetMouseButtonDown(1))
            //{
            //    ChangeScene(SceneIndexes.START_MENU);
            //}

            CheckPause();
        }

        #region Scene Loading and Changing

        public void LoadScene(SceneIndexes scene) //Load the selected scene async
        {
            loadingScenes.Add(UnityEngine.SceneManagement.SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Additive));
        }

        public void ChangeScene(SceneIndexes scene) //Change scene from current scene to the selected one
        {
            SetLoadingScreen(true, true);
            StartCoroutine(LoadSceneDelayed(scene, fadeTime));
            //LoadScene(scene);

            //StartCoroutine(TrackLoadProgress(scene, true));
        }

        private IEnumerator LoadSceneDelayed(SceneIndexes scene, float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            LoadScene(scene);
            StartCoroutine(TrackLoadProgress(scene, true));
        }

        public IEnumerator TrackLoadProgress(SceneIndexes scene, bool setActiveOnLoad) //Track all scenes that are loading, and calculate a load progress from that. Once scene is loaded, set it as active
        {
            float loadProgress;
            for (int i = 0; i < loadingScenes.Count; i++)
            {
                if (loadingScenes[i] == null)
                {
                    Debug.LogWarning("It's null " + loadingScenes.Count);
                }

                while (loadingScenes[i].isDone == false)
                {
                    loadProgress = 0;

                    foreach (AsyncOperation operation in loadingScenes)
                    {
                        loadProgress += operation.progress;
                    }

                    //Calculate load progress
                    loadProgress = loadProgress / loadingScenes.Count;
                    UpdateLoadingBar(loadProgress);
                    yield return null;
                }
            }

            yield return new WaitForSeconds(0.5f);

            if (unloadCurrentOnChange && (int)currentScene < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
            {
                UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync((int)currentScene);
            }

            if (setActiveOnLoad)
            {
                //if (ifScene_CurrentlyLoaded_inEditor(UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex((int)scene).name))
                //{
                //    UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex((int)scene));
                //}
                UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex((int)scene));
                UpdateCurrentScene();
            }

            yield return new WaitForSeconds(0.5f);

            SetLoadingScreen(false, true);
        }

        #endregion

        #region Loading Screen
        public void SetLoadingScreen(bool visible, bool animate) //Toggle visiblity, animation for visiblity changes and if tips are shown
        {
            if (visible)
            {
                if (loadingScreenImage != null)
                {
                    loadingScreenImage.sprite = loadingScreenImages[Random.Range(0, loadingScreenImages.Count)];
                }

                if (animate)
                {
                    if (transitionAnimation == null)
                    {
                        transitionAnimation = StartCoroutine(Fade(true, fadeTime));
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    loadingScreen.alpha = 1;
                    loadingScreenVisible = true;
                }

                if (showTips)
                {
                    if (cycleTipsCoroutine == null)
                    {
                        cycleTipsCoroutine = StartCoroutine(CycleTips());
                    }
                }
            }
            else
            {
                if (animate)
                {
                    if (transitionAnimation == null)
                    {
                        transitionAnimation = StartCoroutine(Fade(false, fadeTime));
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    loadingScreen.alpha = 0;
                    loadingScreenVisible = false;
                }
                StopCoroutine(CycleTips());
                cycleTipsCoroutine = null;
            }
        }

        public void UpdateLoadingBar(float progressValue) //Update the value of the loading bar
        {
            if (progressBar != null)
            {
                progressBar.value = progressValue;
            }
        }

        public void UpdateTipText(string newText, bool visible) //Update the tip text with a new string
        {
            if (tipText != null)
            {
                tipText.text = "Tip: " + newText;
            }

            if (tipCanvasGroup != null)
            {
                tipCanvasGroup.alpha = visible == true ? 1 : 0;
            }
        }

        public void UpdateTipText(int textIndex, bool visible) //Update the tip text from a tip index value
        {
            if (tipText != null)
            {
                tipText.text = string.Format("Tip #{0}: {1}", (textIndex + 1).ToString(), tips[textIndex]);
            }

            if (tipCanvasGroup != null)
            {
                tipCanvasGroup.alpha = visible == true ? 1 : 0;
            }
        }

        private void UpdateCurrentScene() //Update currentScene value for tracking purposes
        {
            SceneIndexes temp = currentScene;
            currentScene = (SceneIndexes)UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            if (temp != currentScene)
            {
                pastScene = temp;
            }
        }

        public IEnumerator CycleTips() //Responsible for selecting tips and transitioning them
        {
            bool transition = true;
            float timer = 0;
            int targetTextIndex = 0;
            bool firstLoad = true;
            UpdateTipText(Random.Range(0, tips.Count), true);

            while (loadingScreenVisible)
            {
                if (timer >= timeBetweenTips) //If timer has reached its time, get a new target text
                {
                    targetTextIndex = Random.Range(0, tips.Count);
                    transition = true;
                    timer = 0;
                }

                if (firstLoad)
                {
                    firstLoad = false;
                    transition = false;
                }

                if (transition == true && showTips)
                {
                    //First, fade out

                    if (tipCanvasGroup != null)
                    {
                        for (float t = 0; t <= 1; t += 1 / (fadeTime / Time.deltaTime))
                        {
                            tipCanvasGroup.alpha = 1 - fadeCurve.Evaluate(t);
                            yield return new WaitForEndOfFrame();
                        }
                    }

                    //Second, apply new text
                    UpdateTipText(targetTextIndex, false);

                    //Third, fade back in
                    if (tipCanvasGroup != null)
                    {
                        for (float t = 0; t <= 1; t += 1 / (fadeTime / Time.deltaTime))
                        {
                            tipCanvasGroup.alpha = fadeCurve.Evaluate(t);
                            yield return new WaitForEndOfFrame();
                        }
                    }

                    //Lastly, turn off transition
                    transition = false;
                }

                if (showTips == false)
                {
                    Coroutine temp = cycleTipsCoroutine;
                    cycleTipsCoroutine = null;
                    StopCoroutine(temp);
                }

                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator Fade(bool fadeIn, float transitionTime) //Fade the loading screen in or out based on the fadeCurve
        {
            //Assign starting alpha based off of desired fade
            float alpha = 0;

            for (float t = 0; t < 1; t += 1 / (transitionTime / Time.deltaTime))
            {
                //Calculate Alpha value
                alpha = fadeIn == true ? fadeCurve.Evaluate(t) : 1 - fadeCurve.Evaluate(t);
                //Apply alpha value to canvas group
                if (loadingScreen != null)
                {
                    loadingScreen.alpha = alpha;
                }

                yield return new WaitForEndOfFrame();
            }
            if (loadingScreen != null) { loadingScreen.alpha = Mathf.Round(alpha); }
            yield return new WaitForEndOfFrame();
            loadingScreenVisible = fadeIn == true ? true : false;
            transitionAnimation = null;
        }

        #endregion

        #region Is Scene Loaded

#if UNITY_EDITOR
        bool ifScene_CurrentlyLoaded_inEditor(string sceneName_no_extention)
        {
            for (int i = 0; i < UnityEditor.SceneManagement.EditorSceneManager.sceneCount; ++i)
            {
                var scene = UnityEditor.SceneManagement.EditorSceneManager.GetSceneAt(i);

                if (scene.name == sceneName_no_extention)
                {
                    return true;//the scene is already loaded
                }
            }
            //scene not currently loaded in the hierarchy:
            return false;
        }
#endif


        bool isScene_CurrentlyLoaded(string sceneName_no_extention)
        {
            for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; ++i)
            {
                Scene scene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);
                if (scene.name == sceneName_no_extention)
                {
                    //the scene is already loaded
                    return true;
                }
            }

            return false;//scene not currently loaded in the hierarchy
        }

        #endregion

        private void CheckPause()
        {
            if(UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                if (currentScene != SceneIndexes.START_MENU)
                {
                    OnPause.Invoke();
                    if(pauseEnabled)
                    {
                        Time.timeScale = 1f;
                    }
                    else
                    {
                        Time.timeScale = 0f;
                    }
                    pauseEnabled = !pauseEnabled;
                }
            }
        }

        public void ResetMenu()
        {
            Time.timeScale = 1f;
            pauseEnabled = false;
        }
    }

}
