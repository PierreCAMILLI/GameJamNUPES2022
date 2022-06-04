using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class LoadingBarUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image _imageFill;
    [SerializeField] private CanvasGroup _canvasGroup;

    [Space]
    [SerializeField] private float _fadeDuration = 1f;

    private AsyncOperation _operation;

    void LateUpdate()
    {
        _imageFill.fillAmount = _operation?.progress ?? 0f;
    }

    private void Reset()
    {
        _imageFill = GetComponentsInChildren<UnityEngine.UI.Image>()
            .Where(i => i.type == UnityEngine.UI.Image.Type.Filled)
            .FirstOrDefault();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetOperation(AsyncOperation operation)
    {
        _operation = operation;
    }

    public void ToggleDisplay(bool toggle)
    {
        if (!gameObject.activeInHierarchy)
        {
            _canvasGroup.alpha = toggle ? 0f : 1f;
        }
        _canvasGroup.DOFade(toggle ? 1f : 0f, _fadeDuration);
    }
}
