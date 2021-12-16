using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLocation : MonoBehaviour
{
    public Vector2 LeftTop = new Vector2(121.516379f,25.035224f  );
    public Vector2 LeftBottom = new Vector2(121.522368f,25.032413f  );
    public Vector2 RightTop = new Vector2(121.517742f,25.038520f  );
    public Vector2 RightBottom = new Vector2(121.524241f,25.035426f  );


    public Vector2 LeftTop1 = new Vector2(-0.4453704f, 0.4635417f);
    public Vector2 LeftBottom1 = new Vector2(-0.3964167f, -0.465625f);
    public Vector2 RightTop1 = new Vector2(25.038520f, 121.517742f);
    public Vector2 RightBottom1 = new Vector2(0.4311111F, -0.48f);
    public Image LocationMark;

    public Vector2 GPSLocation;

    public Image map;
    float a1 = 0, a2 = 0, tx = 0, a3 = 0, a4 = 0, ty = 0;
    // Start is called before the first frame update
    void Start()
    {
        Vector2[] input = new Vector2[3];
        input[0] = LeftTop;
        input[1] = LeftBottom;
        input[2] = RightBottom;
        Vector2[] output = new Vector2[3];
        output[0] = LeftTop1;
        output[1] = LeftBottom1;
        output[2] = RightBottom1;
        //output[0] = new Vector2(-0.5f,  0.5f);
        //output[1] = new Vector2(-0.5f, -0.5f);
        //output[2] = new Vector2(0.5f, -0.5f);
        CalcAffine(input, output);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 t= GpsToScreenLoc(GPSLocation); 
        LocationMark.GetComponent<RectTransform>().anchoredPosition = new Vector2(map.GetComponent<RectTransform>().rect.width * t.x, map.GetComponent<RectTransform>().rect.height * t.y);
    }

    int TernaryEquation(Vector2[] input, float[] output ,ref float  a1, ref float  a2, ref float  t)
    {
        float x0 = input[0].x;
        float x1 = input[1].x;
        float x2 = input[2].x;
        float y0 = input[0].y;
        float y1 = input[1].y;
        float y2 = input[2].y;
        float z0 = output[0];
        float z1 = output[1];
        float z2 = output[2];

        float A1 = y0 * x1 - y1 * x0;
        float A2 = y0 * x2 - y2 * x0;
        float B1 = x1 - x0;
        float B2 = x2 - x0;
        float C1 = z0 * x1 - z1 * x0;
        float C2 = z0 * x2 - z2 * x0;

        float d = B1 * A2 - B2 * A1;
        if (0 == d)
        {
            return -1;
        }

        float c = (C1 * A2 - C2 * A1) / d;
        float b;
        if (0 == A1)
        {
            if (0 == A2)
            {
                return -2;
            }
            else
            {
                b = (C2 - c * B2) / A2;
            }
        }
        else
        {
            b = (C1 - c * B1) / A1;
        }

        float a;
        if (0 == x0)
        {
            if (0 == x1)
            {
                if (0 == x2)
                {
                    return -3;
                }
                else
                {
                    a = (z2 - b * y2 - c) / x2;
                }
            }
            else
            {
                a = (z1 - b * y1 - c) / x1;
            }
        }
        else
        {
            a = (z0 - b * y0 - c) / x0;
        }

        a1 = a;
        a2 = b;
        t = c;

        return 0;
    }

    void CalcAffine(Vector2[] input, Vector2[] output/*, Vector2 inputA,ref  Vector2  outputB*/)
    {
        float[] outputx = new float[3];
        float[] outputy = new float[3];
        outputx[0] = output[0].x;
        outputx[1] = output[1].x;
        outputx[2] = output[2].x;
        outputy[0] = output[0].y;
        outputy[1] = output[1].y;
        outputy[2] = output[2].y;

        //float a1=0, a2=0, tx=0, a3=0, a4=0, ty=0;
        int resx = TernaryEquation(input, outputx, ref a1,ref  a2, ref tx);
        int resy = TernaryEquation(input, outputy,ref a3,ref  a4, ref ty);
       
    }

    public Vector2 GpsToScreenLoc(Vector2 GPSLoc)
    {
        return new Vector2(a1 * GPSLoc.x + a2 * GPSLoc.y + tx, a3 * GPSLoc.x + a4 * GPSLoc.y + ty);
    }
}
