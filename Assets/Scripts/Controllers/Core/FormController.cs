using MVC.BL;
using MVC.Model;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Controllers.Core
{
    /// <summary>
    /// Controller for Form UI Canvas
    /// </summary>
    public class FormController : MonoBehaviour
    {
        [Header("Required Fields")]
        [SerializeField] private TMP_InputField nameInput;
        [SerializeField] private TMP_InputField emailInput;
        [SerializeField] private TMP_InputField phoneInput;
        [SerializeField] private TextMeshProUGUI validationLabel;
        [SerializeField] private Button submitButton;

        // Cached

        private UserBL userBL;

        // State

        private User user;
        [SerializeField] private bool isUpdate = false;

        private void Awake()
        {
            userBL = new UserBL();
            SetValidationMessage(string.Empty, Color.red);
            BindEventListeners();
            nameInput.caretWidth = emailInput.caretWidth = phoneInput.caretWidth = 0;
        }

        private IEnumerator Start()
        {
            yield return CheckIfUserExists();
        }

        /// <summary>
        /// Check if user exists on saved file or database
        /// </summary>
        private IEnumerator CheckIfUserExists()
        {
            try
            {
                if (ProgressManager.HasProgress())
                {
                    user = ProgressManager.LoadProgress().User;
                    if (user == null || user.Id <= 0)
                    {
                        user = userBL.FindUnique();

                        if (user == null || user.Id <= 0)
                        {
                            SetValidationMessage("Erro ao carregar dados do usuário salvo!", Color.red);
                            yield break;
                        }
                    }

                    isUpdate = true;
                    FillFields();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }

            yield return null;
        }

        /// <summary>
        /// Bind event listeners for elements
        /// </summary>
        private void BindEventListeners() => submitButton.onClick.AddListener(() => OnSubmit());

        /// <summary>
        /// Fill form fields with saved user data
        /// </summary>
        private void FillFields()
        {
            try
            {
                nameInput.text = user.Name;
                emailInput.text = user.Email;
                phoneInput.text = user.Phone;
            }
            catch (Exception ex)
            {
                SetValidationMessage("Erro ao carregar dados do usuário salvo!", Color.red);
                throw ex;
            }
        }

        /// <summary>
        /// On submit form 
        /// </summary>
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

                Save();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
                SetValidationMessage("Erro ao salvar os dados!", Color.red);
            }
        }

        /// <summary>
        /// Save or update user data
        /// </summary>
        private void Save()
        {
            if (isUpdate)
            {
                user.Name = nameInput.text;
                user.Email = emailInput.text;
                user.Phone = phoneInput.text;

                bool hasUpdated = userBL.Update(user);
                if (!hasUpdated)
                {
                    SetValidationMessage("Erro ao salvar os dados!", Color.red);
                    throw new Exception();
                }

                ProgressManager.SaveProgress(new PlayerProgress()
                {
                    User = user
                });
            }

            if (!isUpdate)
            {
                bool hasInserted = userBL.Insert(new User()
                {
                    Name = nameInput.text,
                    Email = emailInput.text,
                    Phone = phoneInput.text
                });

                if (!hasInserted)
                {
                    SetValidationMessage("Erro ao salvar os dados!", Color.red);
                    throw new Exception();
                }

                user = userBL.FindUnique();
                ProgressManager.SaveProgress(new PlayerProgress()
                {
                    User = user
                });

                isUpdate = true;
            }


            SetValidationMessage("Dados salvos com sucesso", Color.green);
        }

        /// <summary>
        /// Validate form value fields
        /// </summary>
        /// <returns> string.Empty if correct, field name if incorrect </returns>
        private string ValidateFields()
        {
            if (string.IsNullOrEmpty(nameInput.text) || string.IsNullOrWhiteSpace(nameInput.text)) return "Nome";
            if (string.IsNullOrEmpty(emailInput.text) || string.IsNullOrWhiteSpace(emailInput.text) || !Validators.IsValidEmail(emailInput.text)) return "Email";
            if (string.IsNullOrEmpty(phoneInput.text) || string.IsNullOrWhiteSpace(phoneInput.text) || !Validators.IsValidPhone(phoneInput.text)) return "Telefone";
            return string.Empty;
        }

        /// <summary>
        /// Update validation message
        /// </summary>
        /// <param name="message"> Message to be displayed </param>
        /// <param name="color"> Message's color </param>
        private void SetValidationMessage(string message, Color color)
        {
            validationLabel.text = message;
            validationLabel.color = color;
        }
    }
}