using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class FlashText : MonoBehaviour
{
    public float fadeInDuration;
    public float fadeOutDuration;
    public float opaqueDuration;
    private TMP_Text _text;
    
    private void Awake()
    {
        gameObject.SetActive(false);
        _text = GetComponent<TMP_Text>();
    }

    public void Flash()
    {
        gameObject.SetActive(true);
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        var fadeTimer = 0.0f;
        var finalCol = _text.color;
        var startCol = new Color(255, 255, 255, 0);
        _text.color = startCol;
        while (fadeTimer < fadeInDuration)
        {
            fadeTimer += Time.deltaTime;
            _text.color = Color.Lerp(startCol, finalCol, fadeTimer / fadeInDuration);
            yield return null;
        }

        yield return new WaitForSeconds(opaqueDuration);
        fadeTimer = 0;
        while (fadeTimer < fadeOutDuration)
        {
            fadeTimer += Time.deltaTime;
            _text.color = Color.Lerp(finalCol, startCol, fadeTimer / fadeInDuration);
            yield return null;
        }
    }
}
