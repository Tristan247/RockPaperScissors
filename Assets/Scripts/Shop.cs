using System;
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
    public RawImage rawImage1, rawImage2, rawImage3, rawImage4, rawImage5;
    FirebaseStorage storage;
    StorageReference storageReference;

    IEnumerator LoadImage(string MediaUrl, int type)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if(request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            try 
            {
                if (type == 1)
                {
                    rawImage1.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                }
                else if (type == 2)
                {
                    rawImage2.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                }
                else if (type == 3)
                {
                    rawImage3.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                }
                else if (type == 4)
                {
                    rawImage4.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                }
                else if (type == 5)
                {
                    rawImage5.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                }
            }
            catch(NullReferenceException e)
            {
                Debug.Log("Downloading");
            }
        }
    }

    void Start()
    {
        
    }

    public void BuyItem(int type)
    {
        storage = FirebaseStorage.DefaultInstance;
        storageReference = storage.GetReferenceFromUrl("gs://rockpaperscissors-62563.appspot.com");

        StorageReference image = null;

        if(type == 1)
        {
            image = storageReference.Child("B1.png");
        }
        else if(type == 2)
        {
            image = storageReference.Child("B2.png");
        }
        else if (type == 3)
        {
            image = storageReference.Child("B3.png");
        }
        else if (type == 4)
        {
            image = storageReference.Child("B6.png");
        }
        else if (type == 5)
        {
            image = storageReference.Child("B7.png");
        }

        image.GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
        {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                StartCoroutine(LoadImage(Convert.ToString(task.Result), type));
            }
            else
            {
                Debug.Log(task.Exception);
            }
        });
    }
}
