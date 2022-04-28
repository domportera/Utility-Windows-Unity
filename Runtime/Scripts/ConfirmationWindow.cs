﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UtilityWindows
{
    public class ConfirmationWindow : MonoBehaviour
    {
        [SerializeField] GameObject root;
        [SerializeField] Button confirmButton;
        [SerializeField] Button cancelButton;
        [SerializeField] Text confirmButtonText;
        [SerializeField] Text cancelButtonText;
        [SerializeField] Text descriptionText;

        [SerializeField] InputField inputText;
        [SerializeField] Text inputTitle;
        UnityAction<string> inputAction;

        public UnityEvent OnClose = new UnityEvent();

        public void SetActions(string _text, UnityAction _confirm, UnityAction _cancel = null, string _confirmButtonLabel = null, string _cancelButtonLabel = null)
        {
            ActivateBasicVersion();
            descriptionText.text = _text;
            confirmButton.onClick.AddListener(_confirm);
            confirmButton.onClick.AddListener(Close);


            if (_cancel != null)
            {
                cancelButton.onClick.AddListener(_cancel);
            }

            cancelButton.onClick.AddListener(() => { Close(); });

            if (_confirmButtonLabel != null)
            {
                confirmButtonText.text = _confirmButtonLabel;
            }

            if (_cancelButtonLabel != null)
            {
                cancelButtonText.text = _cancelButtonLabel;
            }
        }
        public void SetActionsInputField(string _inputLabel, UnityAction<string> _confirm, UnityAction _cancel = null, string _confirmButtonLabel = null, string _cancelButtonLabel = null, InputField.ContentType _contentType = InputField.ContentType.Standard)
        {
            ActivateInputVersion();
            inputTitle.text = _inputLabel;
            inputAction = _confirm;
            inputText.contentType = _contentType;
            confirmButton.onClick.AddListener(ConfirmTextEntry);
            confirmButton.onClick.AddListener(Close);

            if (_cancel != null)
            {
                cancelButton.onClick.AddListener(_cancel);
            }

            cancelButton.onClick.AddListener(() => { Close(); });

            if (_confirmButtonLabel != null)
            {
                confirmButtonText.text = _confirmButtonLabel;
                Debug.Log($"Confirm button label {_confirmButtonLabel}");
            }

            if (_cancelButtonLabel != null)
            {
                cancelButtonText.text = _cancelButtonLabel;
            }
        }

        void ConfirmTextEntry()
        {
            inputAction(inputText.text);
        }

        void ActivateInputVersion()
        {
            inputText.gameObject.SetActive(true);
            inputTitle.gameObject.SetActive(true);
            descriptionText.gameObject.SetActive(false);
        }

        void ActivateBasicVersion()
        {
            inputText.gameObject.SetActive(false);
            inputTitle.gameObject.SetActive(false);
            descriptionText.gameObject.SetActive(true);
        }


        void Close()
        {
            OnClose.Invoke();
            Destroy(root);
        }
    }
}