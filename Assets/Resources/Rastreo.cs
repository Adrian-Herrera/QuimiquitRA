using UnityEngine;
using Vuforia;
using System.Collections.Generic;

public class Rastreo : MonoBehaviour, ITrackableEventHandler
{
    #region PROTECTED_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;

    public bool e_active = false;

    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;

        /*Debug.Log("Trackable " + mTrackableBehaviour.TrackableName +
                  " " + mTrackableBehaviour.CurrentStatus +
                  " -- " + mTrackableBehaviour.CurrentStatusInfo);*/

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS

    public virtual void OnTrackingFound()
    {
        if (mTrackableBehaviour)
        {
            //Debug.Log(mTrackableBehaviour.TrackableName);
            e_active = true;
            //string nombre = mTrackableBehaviour.TrackableName;
            
            //añadir(nombre);
            if (!GameObject.Find("TargetScript").GetComponent<Combinar>().m_comb)
            {
                var rendererComponents = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
                var colliderComponents = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
                var canvasComponents = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);

                // Enable rendering:
                foreach (var component in rendererComponents)
                    component.enabled = true;

                // Enable colliders:
                foreach (var component in colliderComponents)
                    component.enabled = true;

                // Enable canvas':
                foreach (var component in canvasComponents)
                    component.enabled = true;
            }
            
        }
    }


    public virtual void OnTrackingLost()
    {
        if (mTrackableBehaviour)
        {
            e_active = false;
            //string nombre = mTrackableBehaviour.TrackableName;
            //quitar(nombre);
            var rendererComponents = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
            var colliderComponents = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
            var canvasComponents = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);

            // Disable rendering:
            foreach (var component in rendererComponents)
                component.enabled = false;

            // Disable colliders:
            foreach (var component in colliderComponents)
                component.enabled = false;

            // Disable canvas':
            foreach (var component in canvasComponents)
                component.enabled = false;
        }
    }

    #endregion // PROTECTED_METHODS

    /*private void añadir(string s)
    {
        if (!GameObject.Find("TargetScript").GetComponent<Elements>().ListaElementos.Contains(s))
        {
            GameObject.Find("TargetScript").GetComponent<Elements>().ListaElementos.Add(s);
        }
        
    }

    private void quitar(string s)
    {
        if (GameObject.Find("TargetScript").GetComponent<Elements>().ListaElementos.Contains(s))
        {
            GameObject.Find("TargetScript").GetComponent<Elements>().ListaElementos.Remove(s);
        }

    }*/

    
}
