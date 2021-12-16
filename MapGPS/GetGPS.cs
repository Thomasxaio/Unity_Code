using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI;

public class GetGPS : MonoBehaviour
{

    string GetGps = "";

    public Text ShowGPS;

    float RefreshTime;

    public MapLocation maplocation;

    /// <summary>  

    /// ��ʼ��

    /// </summary>  

    void Start()

    {

        StartCoroutine(StartGPS());

        GetGps = "N:" + Input.location.lastData.latitude + " E:" + Input.location.lastData.longitude;

        GetGps = GetGps + " Time:" + Input.location.lastData.timestamp;

        ShowGPS.text = GetGps;

        Debug.Log(GetGps);

        RefreshTime = Time.time + 2;

    }

    /// <summary>  

    /// ˢ��λ����Ϣ�����o���c���¼��� 

    /// </summary>  

    public void updateGps()

    {

        StartCoroutine(StartGPS());

        //GetGps = "N:" + Input.location.lastData.latitude + " E:" + Input.location.lastData.longitude;

        //GetGps = GetGps + " Time:" + Input.location.lastData.timestamp;

        //ShowGPS.text = GetGps;

        //Debug.Log(GetGps);

    }

    private void Update()
    {
        if(Time.time>RefreshTime)
        {
            RefreshTime = Time.time + 2;
            StartCoroutine(StartGPS());
            GetGps = "��ǰλ�ã�N:" + Input.location.lastData.latitude + " E:" + Input.location.lastData.longitude;

            GetGps = GetGps + " Time:" + Input.location.lastData.timestamp;

            ShowGPS.text = GetGps;
        }
    }
    /// <summary>  

    /// ֹͣˢ��λ�ã���ʡ�֙C����� 

    /// </summary>  

    void StopGPS()

    {

        Input.location.Stop();

    }

    IEnumerator StartGPS()

    {

        // Input.location ����L���O���λ�Ì��ԣ��ֳ��O�䣩, �o�B��LocationServiceλ��    

        // LocationService.isEnabledByUser �Ñ��O���e�Ķ�λ�����Ƿ���

        if (!Input.location.isEnabledByUser)

        {

            GetGps = "isEnabledByUser value is:" + Input.location.isEnabledByUser.ToString() + " Please turn on the GPS";

            yield return false;

        }

        // LocationService.Start() ����λ�÷��յĸ���,����һ��λ�����˕���ʹ��   

        Input.location.Start(10.0f, 10.0f);

        int maxWait = 20;

        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)

        {

            // ��ͣ�fͬ����Ĉ���(1��)

            yield return new WaitForSeconds(1);

            maxWait--;

        }

        if (maxWait < 1)

        {

            GetGps = "Init GPS service time out";

            yield return false;

        }

        if (Input.location.status == LocationServiceStatus.Failed)

        {

            GetGps = "Unable to determine device location";

            yield return false;

        }

        else

        {

            GetGps = "N:" + Input.location.lastData.latitude + " E:" + Input.location.lastData.longitude;

            GetGps = GetGps + " Time:" + Input.location.lastData.timestamp;

            maplocation.GPSLocation.x = Input.location.lastData.longitude;
            maplocation.GPSLocation.y = Input.location.lastData.latitude; 
            Debug.Log("Keyidingwei");

            yield return new WaitForSeconds(1);

        }

    }

}