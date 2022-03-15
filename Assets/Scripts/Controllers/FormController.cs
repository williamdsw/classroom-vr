using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class FormController : MonoBehaviour
    {
        [Header("Required Fields")]
        [SerializeField] private TMP_InputField nameInput;
        [SerializeField] private TMP_InputField emailInput;
        [SerializeField] private TMP_InputField phoneInput;
        [SerializeField] private TextMeshProUGUI validationLabel;
        [SerializeField] private Button submitButton;

        private TouchScreenKeyboard touchScreenKeyboard;
        private static string inputText = string.Empty;

        private void Awake()
        {
            SetValidationMessage(string.Empty, Color.red);
            BindEventListeners();
            nameInput.caretWidth = emailInput.caretWidth = phoneInput.caretWidth = 0;
        }

        private void BindEventListeners()
        {
            submitButton.onClick.AddListener(() => OnSubmit());

            nameInput.onSelect.AddListener((value) =>
            {
                touchScreenKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.NamePhonePad);
                Debug.Log(touchScreenKeyboard);
                Debug.Log(TouchScreenKeyboard.isSupported);
                Debug.Log(TouchScreenKeyboard.area);
                Debug.Log(TouchScreenKeyboard.hideInput);
                Debug.Log(TouchScreenKeyboard.visible);
            });
        }

        private void OnSubmit()
        {
            try
            {
                string field = ValidateFields();
                if (!string.IsNullOrEmpty(field))
                {
                    SetValidationMessage(string.Format("{0} é obrigatório!", field), Color.red);
                    return;
                }

                // TODO
                SetValidationMessage("Dados salvos com sucesso", Color.green);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
                SetValidationMessage("Erro ao salvar os dados!", Color.red);
            }
        }

        private string ValidateFields()
        {
            if (string.IsNullOrEmpty(nameInput.text) || string.IsNullOrWhiteSpace(nameInput.text)) return "Nome";
            if (string.IsNullOrEmpty(emailInput.text) || string.IsNullOrWhiteSpace(emailInput.text)) return "Email";
            if (string.IsNullOrEmpty(phoneInput.text) || string.IsNullOrWhiteSpace(phoneInput.text)) return "Telefone";
            return string.Empty;
        }

        private void SetValidationMessage(string message, Color color)
        {
            validationLabel.text = message;
            validationLabel.color = color;
        }
    }
}