using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TextShaker : MonoBehaviour
{
    private TMP_Text textComponent;
    private Mesh mesh;
    private Vector3[] vertices;

    public float shakeAmount = 1.0f;
    public float shakeSpeed = 10f;

    void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
        textComponent.ForceMeshUpdate(); 
    }

    void Update()
    {
        textComponent.ForceMeshUpdate();

        TMP_TextInfo textInfo = textComponent.textInfo;
        int characterCount = textInfo.characterCount;

        for (int i = 0; i < characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible)
                continue;

            int vertexIndex = textInfo.characterInfo[i].vertexIndex;
            int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

            vertices = textInfo.meshInfo[materialIndex].vertices;

            Vector3 offset = new Vector3(
                Mathf.PerlinNoise(Time.time * shakeSpeed + i, 0f) - 0.5f,
                Mathf.PerlinNoise(0f, Time.time * shakeSpeed + i) - 0.5f,
                0f
            ) * shakeAmount;

            vertices[vertexIndex + 0] += offset;
            vertices[vertexIndex + 1] += offset;
            vertices[vertexIndex + 2] += offset;
            vertices[vertexIndex + 3] += offset;
        }

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
            textComponent.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }
}
