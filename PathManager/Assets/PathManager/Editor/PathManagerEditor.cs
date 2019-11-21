using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using EditoolsUnity;
#endif

[CustomEditor(typeof(PathManager))]
public class PathManagerEditor : EditorCustom<PathManager>
{
    #region f/p
    #endregion

    #region editor methods

    protected override void OnEnable()
    {
        base.OnEnable();
        Tools.current = Tool.None;
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        UiManager();
        SceneView.RepaintAll();
    }

    private void OnSceneGUI()
    {
        ShowAllPathUI();
    }

    #endregion

    #region GUIMethods

    private void UiManager()
    {
        EditoolsBox.HelpBox("Path Manager", MessageType.Info);
        AllPathUI();
       EditoolsButton.Button("Add Path", Color.white, eTarget.AddPath);
    
        if (eTarget.Paths.Count > 0)
            EditoolsButton.ButtonWithConfirm("Remove All Path", Color.red, eTarget.ClearPaths, "Suppress All Paths ? ",
                "Are your sure ?");
    }


    private void AllPathUI()
    {
        if (!eTarget) return;

        for (int i = 0; i < eTarget.Paths.Count; i++)
        {
            PE_Path _p = eTarget.Paths[i];
            EditoolsLayout.Foldout(ref _p.ShowPath, $"Show/Hide {_p.Id}", true);

            if (!_p.ShowPath) continue;

            EditoolsBox.HelpBox($"[{i}] {_p.Id} -> {_p.PathPoints.Count} total points");

            EditoolsLayout.Horizontal(true);

            EditoolsButton.ButtonWithConfirm("Remove This Path", Color.red, eTarget.RemovePath, i, $"Suppress Path {i + 1} ? ",
                "Are your sure ?");

            EditoolsButton.Button("+", Color.green, _p.AddPoint);
            EditoolsButton.Button("Editable", _p.IsEditable ? Color.green : Color.grey, SetActiveEdition, _p);
            EditoolsLayout.Horizontal(false);

            // New Line
            EditoolsLayout.Horizontal(true);
            EditoolsField.TextField(_p.Id, ref _p.Id);
            EditoolsField.ColorField(_p.PathColor, ref _p.PathColor);


            EditoolsLayout.Horizontal(false);

            ShowPathPointUi(_p);
            EditoolsLayout.Space(5);
        }
    }

    private void ShowPathPointUi(PE_Path _path)
    {
        if (_path.PathPoints.Count == 0) return;
        EditoolsLayout.Foldout(ref _path.ShowPoint, $"Show/Hide points {_path.Id}", true);
        if (_path.ShowPoint)
        {
            for (int i = 0; i < _path.PathPoints.Count; i++)
            {
                EditoolsLayout.Horizontal(true);

                _path.PathPoints[i] = EditoolsField.Vector3Field($"Path Point [{i + 1}]", _path.PathPoints[i]);
                EditoolsButton.ButtonWithConfirm("-", Color.magenta, _path.RemovePoint, i,
                    $"Suppress Point {i + 1} at {_path.PathPoints[i]} ? ", "Are your sure ?");

                EditoolsLayout.Horizontal(false);
            }
        }

        EditoolsLayout.Space(1);

        if (_path.PathPoints.Count > 0)
            EditoolsButton.ButtonWithConfirm("Remove All Points", Color.red, _path.ClearPoints, $"Suppress All Points ? ",
                "Are your sure ?");
    }

    void ShowAllPathUI()
    {
        for (int i = 0; i < eTarget.Paths.Count; i++)
        {
            PE_Path path = eTarget.Paths[i];
            if (!path.IsEditable)
                continue;
            EditoolsHandle.SetColor(path.PathColor);

            for (int j = 0; j < path.PathPoints.Count; j++)
            {
                path.PathPoints[j] = EditoolsHandle.PositionHandle(path.PathPoints[j], Quaternion.identity);
                EditoolsHandle.Label(path.PathPoints[j] + Vector3.up * 5, $"Point {j + 1}");
                EditoolsHandle.SetColor(Color.white);
                EditoolsHandle.DrawDottedLine(path.PathPoints[j] + Vector3.up * 5, path.PathPoints[j], .5f);
                EditoolsHandle.SetColor(path.PathColor);
                EditoolsHandle.DrawSolidDisc(path.PathPoints[j], Vector3.up, 0.1f);
                if (j < path.PathPoints.Count - 1)
                    EditoolsHandle.DrawLine(path.PathPoints[j], path.PathPoints[j + 1]);
              
                
            }
        }
    }

    void SetActiveEdition(PE_Path _path)
    {
        for (int i = 0; i < eTarget.Paths.Count; i++)
        {
            eTarget.Paths[i].IsEditable = false;
        }

        _path.IsEditable = true;
    }

    #endregion
}