using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EffectsManager : GameBehaviour<EffectsManager>
{
    public Volume volume;
    private ChromaticAberration aberration;
    private Bloom bloom;
    private Vignette vignette;

    public void SetBloom(float _intensity)
    {
        volume.profile.TryGet(out bloom);
        bloom.intensity.value = _intensity;
    }

    #region Chromatic
    public void SetChromatic(float _value)
    {
        volume.profile.TryGet(out aberration);
        aberration.intensity.value = _value;
    }

    public void TweenChromaticInOut(float _intensity, float _duration)
    {
        volume.profile.TryGet(out aberration);
        DOTween.To(() => aberration.intensity.value, (x) => aberration.intensity.value = x, _intensity, _duration).
            OnComplete(() => TweenChromatic(0, _duration));
    }

    public void TweenChromatic(float _intensity, float _duration)
    {
        volume.profile.TryGet(out aberration);
        DOTween.To(() => aberration.intensity.value, (x) => aberration.intensity.value = x, _intensity, _duration);
    }
    #endregion

    #region Vignette
    public void SetVignette(float _intensity)
    {
        volume.profile.TryGet(out vignette);
        vignette.intensity.value = _intensity;
    }

    public void TweenVignetteInOut(float _intensity, float _duration)
    {
        volume.profile.TryGet(out vignette);
        DOTween.To(() => vignette.intensity.value, (x) => vignette.intensity.value = x, _intensity, _duration).
            OnComplete(()=> TweenVignette(0, _duration));
    }

    public void TweenVignette(float _intensity, float _duration)
    {
        volume.profile.TryGet(out vignette);
        DOTween.To(() => vignette.intensity.value, (x) => vignette.intensity.value = x, _intensity, _duration);
    }
    #endregion
}
