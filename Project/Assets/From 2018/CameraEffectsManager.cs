using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffectsManager : MonoBehaviour
{
    [SerializeField]
    private ShaderEffect_CorruptedVram corruptEffect;
    [SerializeField]
    private ShaderEffect_BleedingColors bleedingEffect;
    [SerializeField]
    private ShaderEffect_Tint tintEffect;

    [ContextMenu("RunGlitchEffect")]
    public void RunGlitchEffect(float scale = 1)
    {
        StartCoroutine(CameraGlitchEffect(scale));
    }

    public void SetHackMode(bool state)
    {
        tintEffect.enabled = state;
    }

    public void SetLoadingEffect(float percent)
    {

    }

    IEnumerator CameraGlitchEffect(float scale, float delta = 1f)
    {
        scale = Mathf.Abs(scale);
        corruptEffect.enabled = true;
        bleedingEffect.enabled = true;
        for (float i = 0; i <= scale; i += delta)
        {
            corruptEffect.shift = i;
            bleedingEffect.shift = i;
            bleedingEffect.intensity = i;
            yield return new WaitForEndOfFrame();
        }

        for (float i = scale; i >= 0; i -= delta)
        {
            corruptEffect.shift = i;
            bleedingEffect.shift = i;
            bleedingEffect.intensity = i;
            yield return new WaitForEndOfFrame();
        }
        corruptEffect.enabled = false;
        bleedingEffect.enabled = false;
    }
}
