using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ͼ�α���
/// </summary>
public class TextureFormation : MonoBehaviour
{
    public int samplingStep = 5;
    public float scale = 1f;

    public Texture2D texture;

    private List<Vector3> posList = new List<Vector3>();

    protected Transform trans;


    public void Awake()
    {
        trans = transform;
    }


    private void CalculatePoints()
    {
        if (Application.isPlaying)
        {
            if (0 != posList.Count)
                return;
        }
        else
        {
            posList.Clear();
        }

        //543  128
        var widthStep = texture.width / samplingStep;
        var heightStep = texture.height / samplingStep;
        for (int i = 0; i <= heightStep; i += samplingStep)
        {
            for (int j = 0; j <= widthStep; j += samplingStep)
            {
                int colorPixelCnt = 0;
                for (int ii = 0; ii <= samplingStep; ++ii) //hh
                {
                    for (int jj = 0; jj <= samplingStep; ++jj) //ww
                    {
                        var color = texture.GetPixel(j * samplingStep + jj, i * samplingStep + ii);
                        if (color.r + color.g + color.b > 1f)
                        {
                            ++colorPixelCnt;
                        }
                    }
                }
                if (colorPixelCnt > samplingStep)
                {
                    var pos = new Vector3(-texture.width / 2 + j * samplingStep + samplingStep / 2f, 0, -texture.height / 2 + i * samplingStep + samplingStep / 2f);
                    pos *= scale;
                    posList.Add(pos);
                }
            }
        }
    }

    public IEnumerable<Vector3> EvaluatePoints()
    {
        CalculatePoints();
        var rootPos = Vector3.zero;
        if (null != trans)
            rootPos = trans.position;
        for (int i = 0; i < posList.Count; ++i)
        {
            yield return rootPos + posList[i];
        }
    }

    public void ReCalculate()
    {
        posList.Clear();
    }
}
