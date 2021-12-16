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

    /// 初始化

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

    /// 刷新位置信息（按鈕的點擊事件） 

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
            GetGps = "當前位置：N:" + Input.location.lastData.latitude + " E:" + Input.location.lastData.longitude;

            GetGps = GetGps + " Time:" + Input.location.lastData.timestamp;

            ShowGPS.text = GetGps;
        }
    }
    /// <summary>  

    /// 停止刷新位置（節省手機電量） 

    /// </summary>  

    void StopGPS()

    {

        Input.location.Stop();

    }

    IEnumerator StartGPS()

    {

        // Input.location 用於訪問設備的位置屬性（手持設備）, 靜態的LocationService位置    

        // LocationService.isEnabledByUser 用戶設置裡的定位服務是否啟用

        if (!Input.location.isEnabledByUser)

        {

            GetGps = "isEnabledByUser value is:" + Input.location.isEnabledByUser.ToString() + " Please turn on the GPS";

            yield return false;

        }

        // LocationService.Start() 啟動位置服務的更新,最後一個位置坐標會被使用   

        Input.location.Start(10.0f, 10.0f);

        int maxWait = 20;

        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)

        {

            // 暫停協同程序的執行(1秒)

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