using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public GameObject ballIcon;
    public RawImage compassImage;
    public Transform player;
    Dictionary<GameObject, GameObject> markerball = new Dictionary<GameObject, GameObject>();
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
        foreach (KeyValuePair<GameObject, GameObject> it in markerball)
        {
            ((RectTransform)it.Value.transform).anchoredPosition = GetPosOnCompass(it.Key);
        }
    }

    public void AddMarker(GameObject ball)
    {
        GameObject marker = Instantiate(ballIcon, compassImage.transform);
        markerball.Add(ball, marker);
    }

    public void DeleteMarker(GameObject ball)
    {
        Destroy(markerball[ball]);
        markerball.Remove(ball);     
    }

    Vector2 GetPosOnCompass(GameObject ball)
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 playerFwd = new Vector2(player.transform.forward.x, player.transform.forward.z);
        Vector2 ballPos = new Vector2(ball.transform.position.x, ball.transform.position.z);

        float angle = Vector2.SignedAngle(ballPos-playerPos,playerFwd);
        return new Vector2(CompassUnit * angle, 0f);
    }
}
