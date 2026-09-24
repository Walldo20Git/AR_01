using UnityEngine;
using Vuforia;

public class AR_BusinessCardManager : MonoBehaviour, ITrackableEventHandler
{
    [Header("Referencias UI")]
    [SerializeField] private GameObject contactPanel;
    [SerializeField] private GameObject model3DPanel;
    [SerializeField] private GameObject videoPanel;
    
    [Header("Controladores")]
    [SerializeField] private AR_UIController uiController;
    [SerializeField] private AR_VideoController videoController;
    
    private TrackableBehaviour mTrackableBehaviour;
    
    void Start() {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        
        // Estado inicial: solo panel de contacto visible
        contactPanel.SetActive(true);
        model3DPanel.SetActive(false);
        videoPanel.SetActive(false);
    }
    
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus) 
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED) {
            uiController.ShowUI();
        } else {
            uiController.HideUI();
            ResetExperience(); // Limpiar estado al perder tracking
        }
    }
    
    // 👉 Métodos públicos para botones de UI
    public void ToggleModel3D() {
        model3DPanel.SetActive(!model3DPanel.activeSelf);
        // Efecto de sonido opcional
    }
    
    public void ShowVideo() {
        contactPanel.SetActive(false);
        videoPanel.SetActive(true);
        videoController.PlayVideo();
    }
    
    public void BackToContact() {
        videoController.PauseVideo();
        videoPanel.SetActive(false);
        contactPanel.SetActive(true);
    }
    
    private void ResetExperience() {
        contactPanel.SetActive(true);
        model3DPanel.SetActive(false);
        videoPanel.SetActive(false);
    }
}