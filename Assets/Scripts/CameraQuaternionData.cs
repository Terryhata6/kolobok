using UnityEngine;

[CreateAssetMenu(fileName = "CameraRotationData", menuName ="Data/Camera Rotation Data", order = 1)]
public class CameraQuaternionData : ScriptableObject
{
    public Quaternion Rotation;

}
