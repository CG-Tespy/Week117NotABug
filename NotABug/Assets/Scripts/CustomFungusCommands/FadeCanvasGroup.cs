using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

/// <summary>
/// Like FadeUI, but only applies to CanvasGroup components, and fades only opacity.
/// </summary>
[CommandInfo("UI",
                 "Fade Canvas Group",
                 "Fades an object based using its CanvasGroup components")]
public class FadeCanvasGroup : TweenUI
{
    [SerializeField] protected FloatData targetAlpha = new FloatData(1f);

    CanvasGroup toFade;
    LTDescr fadeAction, firstFadeEase, secondFadeEase;

    protected override void ApplyTween(GameObject gameObject)
    {
        GetAppropriateCanvasGroupFrom(gameObject);
        FadeAsAppropriate();
    }

    void GetAppropriateCanvasGroupFrom(GameObject gameObject)
    {
        toFade = gameObject.GetComponent<CanvasGroup>();
    }

    void FadeAsAppropriate()
    {
        if (ShouldFadeInstantly())
        {
            ApplyFadeInstantly();
        }
        else
        {
            ApplyFadeOverTime();
        }
    }

    bool ShouldFadeInstantly()
    {
        return Mathf.Approximately(duration, 0f);
    }

    void ApplyFadeInstantly()
    {
        toFade.alpha = targetAlpha;
    }

    void ApplyFadeOverTime()
    {
        StartTheFadeAction();
        EaseTheFading();
    }

    void StartTheFadeAction()
    {
        var canvasGroupRectTrans = toFade.GetComponent<RectTransform>();
        fadeAction = LeanTween.alpha(canvasGroupRectTrans, targetAlpha, duration);
    }

    void EaseTheFading()
    {
        // The ease was applied twice like this the FadeUI component's source code, so I assume 
        // it should be the same here.
        fadeAction.setEase(tweenType).setEase(tweenType);
    }

    public override bool IsPropertyVisible(string propertyName)
    {
        return true;
    }
}
