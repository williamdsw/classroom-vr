using System;
using TMPro;
using UnityEngine;

namespace Controllers.Core
{
    /// <summary>
    /// Controller for elements of TV Canvas
    /// </summary>
    public class HourController : MonoBehaviour
    {
        [Header("Required Elements")]
        [SerializeField] private TextMeshProUGUI hourLabel;
        [SerializeField] private TextMeshProUGUI fpsLabel;

        // State

        private float count;

        private void Update()
        {
            UpdateHourLabel();
            UpdateFPSLabel();
        }

        /// <summary>
        /// Update current hour label
        /// </summary>
        private void UpdateHourLabel() => hourLabel.text = DateTime.Now.ToString("HH:mm");

        /// <summary>
        /// Update current FPS label (for debugging)
        /// </summary>
        private void UpdateFPSLabel()
        {
            count = (1f / Time.deltaTime);
            fpsLabel.text = Math.Round(count).ToString();
        }
    }
}