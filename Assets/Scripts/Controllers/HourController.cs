using System;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class HourController : MonoBehaviour
    {
        [Header("Required Elements")]
        [SerializeField] private TextMeshProUGUI hourLabel;

        private void Update() => UpdateHourLabel();

        private void UpdateHourLabel() => hourLabel.text = DateTime.Now.ToString("HH:mm");
    }
}