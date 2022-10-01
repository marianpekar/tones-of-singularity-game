using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    private InputReader inputReader;

    [SerializeField]
    private Slider mouseSensitivitySlider;

    [SerializeField]
    private GameObject[] elementsToHide;

    [SerializeField]
    private CinemachineFreeLook cinemachine;

    private void Awake()
    {
        HideElements(true);
    }

    private bool toggle = true;
    public void ToggleElements()
    {
        toggle = !toggle;
        HideElements(toggle);
    }

    public void SetMouseLookSensitivity()
    {
        cinemachine.m_YAxis.m_MaxSpeed = mouseSensitivitySlider.value;
        cinemachine.m_XAxis.m_MaxSpeed = mouseSensitivitySlider.value * 100 + 100;
    }

    private void HideElements(bool hide)
    {
        foreach (var element in elementsToHide)
        {
            element.SetActive(!hide);
        }
    }
}
