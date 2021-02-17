using System;
using UnityEngine;

namespace kolobok
{
    public class CustomSnapPoint : MonoBehaviour
    {
        public enum ConnectionType
        {
            LvlPart
        }

        public ConnectionType Type;

        private void OnDrawGizmos()
        {
            switch (Type)
            {
                case ConnectionType.LvlPart:
                    Gizmos.color = Color.green;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            //Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 1.0f);
        }
    }
}