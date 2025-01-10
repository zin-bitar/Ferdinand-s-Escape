using UnityEditor;
using UnityEngine;
public class Utilitaires
{
    public static bool ObjetVisible(GameObject depart, GameObject arrive, float angleMax, float distanceMax)
    {
        bool visible = false;

        Vector3 positionDepart = depart.transform.position + Vector3.up * 0.5f;
        Vector3 positionArrive = arrive.transform.position + Vector3.up * 0.5f;
        Vector3 direction = positionArrive - positionDepart;

        if (Vector3.Angle(depart.transform.forward, direction) <= angleMax && direction.magnitude <= distanceMax)
        {
            RaycastHit hit;
            if (Physics.Raycast(positionDepart, direction, out hit, distanceMax))
            {
                if (hit.collider.gameObject == arrive)
                {
                    visible = true;
                }
            }
        }
        return visible;
    }

    public static string FormatTime(int totalSeconds)
    {
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}