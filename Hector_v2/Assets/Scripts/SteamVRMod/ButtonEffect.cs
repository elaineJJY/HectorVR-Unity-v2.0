//======= Copyright (c) Valve Corporation, All rights reserved. ===============
// Modified: Jingyi

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

namespace Valve.VR.InteractionSystem.Sample
{
    public class ButtonEffect : MonoBehaviour
    {
        public Color buttonDownColor = Color.cyan;
        public Color buttonUpColor = Color.white;
        public void OnButtonDown(Hand fromHand)
        {
            ColorSelf(buttonDownColor);
            fromHand.TriggerHapticPulse(1000);
        }

        public void OnButtonUp(Hand fromHand)
        {
            ColorSelf(buttonUpColor);
        }

        private void ColorSelf(Color newColor)
        {
            Renderer[] renderers = this.GetComponentsInChildren<Renderer>();
            for (int rendererIndex = 0; rendererIndex < renderers.Length; rendererIndex++)
            {
                renderers[rendererIndex].material.color = newColor;
            }
        }
    }
}