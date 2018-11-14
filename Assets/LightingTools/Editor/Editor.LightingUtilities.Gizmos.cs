using UnityEngine;
using UnityEditor;

namespace EditorLightUtilities
{
    public static class LightingGizmos
    {
        public static void DrawSpotlightGizmo(Light spotlight)
        {
            var flatRadiusAtRange = spotlight.range * Mathf.Tan(spotlight.spotAngle * Mathf.Deg2Rad * 0.5f);

            var vectorLineUp = Vector3.Normalize(spotlight.gameObject.transform.position + spotlight.gameObject.transform.forward * spotlight.range + spotlight.gameObject.transform.up * flatRadiusAtRange - spotlight.gameObject.transform.position);
            var vectorLineDown = Vector3.Normalize(spotlight.gameObject.transform.position + spotlight.gameObject.transform.forward * spotlight.range + spotlight.gameObject.transform.up * -flatRadiusAtRange - spotlight.gameObject.transform.position);
            var vectorLineRight = Vector3.Normalize(spotlight.gameObject.transform.position + spotlight.gameObject.transform.forward * spotlight.range + spotlight.gameObject.transform.right * flatRadiusAtRange - spotlight.gameObject.transform.position);
            var vectorLineLeft = Vector3.Normalize(spotlight.gameObject.transform.position + spotlight.gameObject.transform.forward * spotlight.range + spotlight.gameObject.transform.right * -flatRadiusAtRange - spotlight.gameObject.transform.position);

            var rangeDiscDistance = Mathf.Cos(Mathf.Deg2Rad * spotlight.spotAngle / 2) * spotlight.range;
            var rangeDiscRadius = spotlight.range * Mathf.Sin(spotlight.spotAngle * Mathf.Deg2Rad * 0.5f);
            var nearDiscDistance = Mathf.Cos(Mathf.Deg2Rad * spotlight.spotAngle / 2) * spotlight.shadowNearPlane;
            var nearDiscRadius = spotlight.shadowNearPlane * Mathf.Sin(spotlight.spotAngle * Mathf.Deg2Rad * 0.5f);

            //Draw Near Plane Disc
            if (spotlight.shadows != LightShadows.None) Handles.Disc(spotlight.gameObject.transform.rotation, spotlight.gameObject.transform.position + spotlight.gameObject.transform.forward * nearDiscDistance, spotlight.gameObject.transform.forward, nearDiscRadius, false, 1);
            //Draw Range disc
            Handles.Disc(spotlight.gameObject.transform.rotation, spotlight.gameObject.transform.position + spotlight.gameObject.transform.forward * rangeDiscDistance, spotlight.gameObject.transform.forward, rangeDiscRadius, false, 1);
            //Draw Lines
            Gizmos.DrawLine(spotlight.gameObject.transform.position, spotlight.gameObject.transform.position + vectorLineUp * spotlight.range);
            Gizmos.DrawLine(spotlight.gameObject.transform.position, spotlight.gameObject.transform.position + vectorLineDown * spotlight.range);
            Gizmos.DrawLine(spotlight.gameObject.transform.position, spotlight.gameObject.transform.position + vectorLineRight * spotlight.range);
            Gizmos.DrawLine(spotlight.gameObject.transform.position, spotlight.gameObject.transform.position + vectorLineLeft * spotlight.range);

            //Draw Range Arcs
            Handles.DrawWireArc(spotlight.gameObject.transform.position, spotlight.gameObject.transform.right, vectorLineUp, spotlight.spotAngle, spotlight.range);
            Handles.DrawWireArc(spotlight.gameObject.transform.position, spotlight.gameObject.transform.up, vectorLineLeft, spotlight.spotAngle, spotlight.range);
        }

        public static void DrawDirectionalLightGizmo(Transform directionalLightTransform)
        {
            var gizmoSize = 0.2f;
            Handles.Disc(directionalLightTransform.rotation, directionalLightTransform.position, directionalLightTransform.gameObject.transform.forward, gizmoSize, false, 1);
            Gizmos.DrawLine(directionalLightTransform.position, directionalLightTransform.position + directionalLightTransform.forward);
            Gizmos.DrawLine(directionalLightTransform.position + directionalLightTransform.up * gizmoSize, directionalLightTransform.position + directionalLightTransform.up * gizmoSize + directionalLightTransform.forward);
            Gizmos.DrawLine(directionalLightTransform.position + directionalLightTransform.up * -gizmoSize, directionalLightTransform.position + directionalLightTransform.up * -gizmoSize + directionalLightTransform.forward);
            Gizmos.DrawLine(directionalLightTransform.position + directionalLightTransform.right * gizmoSize, directionalLightTransform.position + directionalLightTransform.right * gizmoSize + directionalLightTransform.forward);
            Gizmos.DrawLine(directionalLightTransform.position + directionalLightTransform.right * -gizmoSize, directionalLightTransform.position + directionalLightTransform.right * -gizmoSize + directionalLightTransform.forward);
        }

        public static void DrawCross(Transform m_transform)
        {
            var gizmoSize = 0.25f;
            Gizmos.DrawLine(m_transform.position, m_transform.position + m_transform.TransformVector(m_transform.root.forward * gizmoSize / m_transform.localScale.z));
            Gizmos.DrawLine(m_transform.position, m_transform.position + m_transform.TransformVector(m_transform.root.forward * -gizmoSize / m_transform.localScale.z));
            Gizmos.DrawLine(m_transform.position, m_transform.position + m_transform.TransformVector(m_transform.root.up * gizmoSize / m_transform.localScale.y));
            Gizmos.DrawLine(m_transform.position, m_transform.position + m_transform.TransformVector(m_transform.root.up * -gizmoSize / m_transform.localScale.y));
            Gizmos.DrawLine(m_transform.position, m_transform.position + m_transform.TransformVector(m_transform.root.right * gizmoSize / m_transform.localScale.x));
            Gizmos.DrawLine(m_transform.position, m_transform.position + m_transform.TransformVector(m_transform.root.right * -gizmoSize / m_transform.localScale.x));
        }
    }
}
