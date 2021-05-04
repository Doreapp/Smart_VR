using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public GameObject baliseIcon;
    public RawImage compassImage;
    public Transform player;
    Dictionary<GameObject, GameObject> markerbalise = new Dictionary<GameObject, GameObject>();
    float CompassUnit;

    // Start is called before the first frame update
    void Start()
    {
        CompassUnit = compassImage.rectTransform.rect.width / 360f;
    }

    // Update is called once per frame
    void Update()
    {
        compassImage.uvRect = new Rect(player.localEulerAngles.y / 360f, 0f, 1f, 1f);
        foreach (KeyValuePair<GameObject, GameObject> it in markerbalise)
        {
            ((RectTransform)it.Value.transform).anchoredPosition = GetPosOnCompass(it.Key);
        }
    }

    public void AddMarker(GameObject balise)
    {
        GameObject marker = Instantiate(baliseIcon, compassImage.transform);
        markerbalise.Add(balise, marker);
    }

    public void DeleteMarker(GameObject balise)
    {
        Destroy(markerbalise[balise]);
        markerbalise.Remove(balise);
        
        
    }

    Vector2 GetPosOnCompass(GameObject balise)
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 playerFwd = new Vector2(player.transform.forward.x, player.transform.forward.z);
        Vector2 balisePos = new Vector2(balise.transform.position.x, balise.transform.position.z);

        float angle = Vector2.SignedAngle(balisePos-playerPos,playerFwd);
        return new Vector2(CompassUnit * angle, 0f);
    }
}
