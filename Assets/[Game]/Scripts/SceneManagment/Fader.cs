using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagment
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;

        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        public IEnumerator FadeInOut(float fadeTime)
        {
            yield return StartCoroutine(FadeOut(fadeTime));
            Debug.Log("Faded out");
            yield return StartCoroutine(FadeIn(fadeTime));
            Debug.Log("Faded in");
        }
        public IEnumerator FadeOut(float time)
        {
            while(canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime / time; //increase canvasGroup.alpha every frame.
                yield return null ;//new WaitForSeconds(time);
            }
        }
        public IEnumerator FadeIn(float time)
        {
            while(canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / time; //increase canvasGroup.alpha every frame.
                yield return null ;//new WaitForSeconds(time);
            }
        }
    }

}