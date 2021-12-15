using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ARLocation.UI
{

    public class LocationProviderInfo : MonoBehaviour
    {
        private List<Text> texts = new List<Text>();
        private ARLocationProvider locationProvider;
        private LoadingBar accuracyBar;
        private Transform mainCameraTransform;

        // 初始化設定
        void Start()
        {
            texts.Add(GameObject.Find(gameObject.name + "/Panel/Canvas/Latitude").GetComponent<Text>());
            texts.Add(GameObject.Find(gameObject.name + "/Panel/Canvas/Longitude").GetComponent<Text>());

            locationProvider = ARLocationProvider.Instance;

            mainCameraTransform = ARLocationManager.Instance.MainCamera.transform;
        }

        // U更新值
        void Update()
        {
            texts[0].text = "當前經度: " + locationProvider.CurrentLocation.latitude;
            texts[1].text = "當前緯度: " + locationProvider.CurrentLocation.longitude;

            var accuracy = locationProvider.CurrentLocation.accuracy;

            accuracyBar.FillPercentage = Mathf.Min(1, (float)accuracy / 25.0f);
            accuracyBar.Text = "" + (float)locationProvider.CurrentLocation.accuracy;
        }
    }
}
