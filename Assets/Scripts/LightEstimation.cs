using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(Light))]
public class LightEstimation : MonoBehaviour
{
    public ARCameraManager ARCameraManager;
    public Light Light;

    private void Start()
    {
        ARCameraManager.frameReceived += FrameReceived;

        Light = GetComponent<Light>();
    }

    private void FrameReceived(ARCameraFrameEventArgs args)
    {
        ARLightEstimationData lightEstimation = args.lightEstimation;

        if (lightEstimation.averageBrightness.HasValue)
        {
            Light.intensity = lightEstimation.averageBrightness.Value;
        }

        if (lightEstimation.averageColorTemperature.HasValue)
        {
            Light.colorTemperature = lightEstimation.averageColorTemperature.Value;
        }

        if (lightEstimation.colorCorrection.HasValue)
        {
            Light.color = lightEstimation.colorCorrection.Value;
        }

        if (lightEstimation.mainLightDirection.HasValue)
        {
            Light.transform.rotation = Quaternion.LookRotation(lightEstimation.mainLightDirection.Value);
        }

        if (lightEstimation.mainLightColor.HasValue)
        {
            Light.color = lightEstimation.mainLightColor.Value;
        }

        if (lightEstimation.mainLightIntensityLumens.HasValue)
        {
            Light.intensity = lightEstimation.averageMainLightBrightness.Value;
        }

        if (lightEstimation.ambientSphericalHarmonics.HasValue)
        {
            RenderSettings.ambientMode = AmbientMode.Skybox;
            RenderSettings.ambientProbe = lightEstimation.ambientSphericalHarmonics.Value;
        }
    }
}
