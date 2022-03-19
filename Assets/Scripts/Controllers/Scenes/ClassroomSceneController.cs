using System;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Controllers.Scenes
{
    public class ClassroomSceneController : MonoBehaviour
    {
        [Header("Required UI Elements")]
        [SerializeField] private Button exitButton;
        [SerializeField] private Button effectsButton;
        [SerializeField] private GameObject postProcessVolumeObj;


        [Header("Quality Canvas UI Elements")]
        [SerializeField] private Button lowQualityButton;
        [SerializeField] private Button mediumQualityButton;
        [SerializeField] private Button highQualityButton;

        // State

        private bool isPostProcessEnabled = false;

        // Cached

        private Image effectsButtonImage;

        private void Awake()
        {
            effectsButtonImage = effectsButton.GetComponent<Image>();
            BindEventListeners();
            SetQualityLevel(PlayerPrefs.GetInt(Properties.PlayerPrefsKeys.QualitySetting, 0));
        }

        /// <summary>
        /// Bind event listeners for elements
        /// </summary>
        private void BindEventListeners()
        {
            try
            {
                exitButton.onClick.AddListener(() => Application.Quit());
                effectsButton.onClick.AddListener(() => TogglePostProcess());
                lowQualityButton.onClick.AddListener(() => SetQualityLevel(0));
                mediumQualityButton.onClick.AddListener(() => SetQualityLevel(1));
                highQualityButton.onClick.AddListener(() => SetQualityLevel(2));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Toggle Post Process effect
        /// </summary>
        private void TogglePostProcess()
        {
            isPostProcessEnabled = !isPostProcessEnabled;
            Color current = effectsButtonImage.color;
            current.a = (isPostProcessEnabled ? 1f : 0.5f);
            effectsButtonImage.color = current;
            postProcessVolumeObj.SetActive(isPostProcessEnabled);
        }

        /// <summary>
        /// Set quality level
        /// </summary>
        /// <param name="index"> Quality level index </param>
        private void SetQualityLevel(int index)
        {
            QualitySettings.SetQualityLevel(index);
            PlayerPrefs.SetInt(Properties.PlayerPrefsKeys.QualitySetting, index);
        }
    }
}