using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;

public class Shop : MonoBehaviour
{
    RawImage rawImage1;
    FirebaseStorage storage;
    StorageReference storageReference;

    IEnumerator LoadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if(request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            rawImage1.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }

    void Start()
    {
        rawImage1 = gameObject.GetComponent<RawImage>();

        storage = FirebaseStorage.DefaultInstance;
        storageReference = storage.GetReferenceFromUrl("gs://rockpaperscissors-62563.appspot.com");
    }


    void Update()
    {
        
    }
}
