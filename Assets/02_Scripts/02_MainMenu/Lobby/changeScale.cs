using UnityEngine;
using System.Collections;

public class changeScale : MonoBehaviour
{
    public GameObject left;
    public GameObject right;

    public void OnClickButton()
    {
        StartCoroutine(ScaleX(left, left.transform.localScale.x, 6.5f, 0.4f));
        StartCoroutine(ScaleX(right, right.transform.localScale.x, 6.5f, 0.4f));
    }
    
    private IEnumerator ScaleX(GameObject target, float fromX, float toX, float duration)
    {
        float elapsed = 0f;
        Vector3 originalScale = target.transform.localScale; // y, z 유지용

        while (elapsed < duration)
        {
            float newX = Mathf.Lerp(fromX, toX, elapsed / duration);
            target.transform.localScale = new Vector3(newX, originalScale.y, originalScale.z);

            elapsed += Time.deltaTime;
            yield return null;
        }
        target.transform.localScale = new Vector3(toX, originalScale.y, originalScale.z);
    }

}
