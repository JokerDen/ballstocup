using UnityEngine;

public class RandomizeColor : MonoBehaviour
{
    public Color[] colors;
    
    private void Start()
    {
        var mr = GetComponent<MeshRenderer>();
        var mat = mr.material;
        var c = colors[Random.Range(0, colors.Length)];
        
        /*var c = mat.GetColor("_Color");
        
        float H;
        float S;
        float V;
        Color.RGBToHSV(c, out H, out S, out V);
        c = Color.HSVToRGB(Random.value, S, V);*/
        
        mat.SetColor("_Color", c);
        mat.SetColor("_EmissionColor", c);
    }
}
