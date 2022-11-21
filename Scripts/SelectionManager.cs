﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour
{
    private IRayProvider _rayProvider;
    private ISelector _selector;
    private ISelectionResponse _selectionResponse;

    private Transform _curSelection;

    private void Awake()
    {
        SceneManager.LoadScene("Environment", LoadSceneMode.Additive);
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);

        _selectionResponse = GetComponent<ISelectionResponse>();
        _rayProvider = GetComponent<IRayProvider>();
        _selector = GetComponent<ISelector>();
    }

    private void Update()
    {
        //Deselection/Selection Response
        if (_curSelection != null)
        {
            _selectionResponse.OnDeselect(_curSelection);
        }

        _selector.Check(_rayProvider.CreateRay());
        _curSelection = _selector.GetSelection();

        //Deselection/Selection Response
        if (_curSelection != null)
        {
            _selectionResponse.OnSelect(_curSelection);
        }
    }
}