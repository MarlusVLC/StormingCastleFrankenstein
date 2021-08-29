using AI;
using UnityEditor;
using UnityEngine;
using Utilities;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfView_Editor : Editor
{
    private Vector3 _objectPos;
    private FieldOfView fow;
    
    
    private void OnSceneGUI()
    {
        fow = (FieldOfView) target;
        Handles.color = Color.white;
        Vector3 viewAngleA = fow.transform.DirFromAngle(-fow.ViewAngle/2, false);
        Vector3 viewAngleB = fow.transform.DirFromAngle(fow.ViewAngle / 2, false);
        
        if (fow.ShowCircumference)
        {
            Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.ViewRadius);
        }
        else
        {
            Handles.DrawWireArc(fow.transform.position, Vector3.up, viewAngleA, fow.ViewAngle, fow.ViewRadius);
        }

        _objectPos = fow.transform.position;
        Handles.DrawLine(_objectPos, _objectPos + viewAngleA * fow.ViewRadius);
        Handles.DrawLine(_objectPos, _objectPos + viewAngleB * fow.ViewRadius);

        
        
        Handles.color = Color.red;
        foreach (var visibleTarget in fow.VisibleTargets)
        {
            Handles.DrawLine(fow.transform.position, visibleTarget.transform.position);
        }
    }
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        fow = (FieldOfView) target;
        if (GUILayout.Button("Get Visible Targets"))
        {
            fow.InitializeTargetCollections();
            fow.FindVisibleTargets();
        }
    }
}