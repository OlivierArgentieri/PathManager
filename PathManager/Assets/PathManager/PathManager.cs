using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class PathManager : MonoBehaviour
{
    #region f/p

    
    public List<PE_Path> Paths = new List<PE_Path>();

    public static Action<List<PE_Path>> OnLoadPath;
    #endregion
    
    
    #region unity methods

    private void Start()
    {
        OnLoadPath?.Invoke(Paths);
    }

    #endregion
    
    #region custom methods
    
    public void AddPath()=> Paths.Add(new PE_Path());

    public void RemovePath(int _index) => Paths.RemoveAt(_index);
    public void ClearPaths() => Paths.Clear();
    
    #endregion

}

[Serializable]
public class PE_Path
{
    #region f/p
    
    public string Id = "Path 1";
    public bool ShowPath = true;
    public bool ShowPoint = true;
    public Color PathColor = Color.white;
    public bool IsEditable = false;
    public List<Vector3> PathPoints = new List<Vector3>();
    
    #endregion
    
    

    #region unity methods

    #endregion
    
    
    #region custom methods
    
    public void AddPoint()
    {
        int _count = PathPoints.Count;
        if (_count < 1)
            PathPoints.Add(Vector3.zero);
        else
            PathPoints.Add(PathPoints.Last() + Vector3.forward);
    }

    public void RemovePoint(int _index) => PathPoints.RemoveAt(_index);
    public void ClearPoints() => PathPoints.Clear();
    
    #endregion
}