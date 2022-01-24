using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;
using System.Threading.Tasks;
using System.Threading;
using TMPro;

public class Shop : MonoBehaviour
{
    public RawImage rawImage1, rawImage2, rawImage3, rawImage4, rawImage5;
    FirebaseStorage storage;
    StorageReference storageReference;
    public TMP_Text loading, buyItem;


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
        loading.text = "0%";
        buyItem.text = "";
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

        // const long maxAllowedSize = 4 * 1024 * 1024;


        const long maxAllowedSize = 4 * 1024 * 1024;
        Task task = image.GetBytesAsync(maxAllowedSize, new StorageProgress<DownloadState>(state => {
            // called periodically during the download
            Debug.Log(String.Format(
                "Progress: {0} of {1} bytes transferred.",
                state.BytesTransferred,
                state.TotalByteCount
            ));
            loading.text = ((float)state.BytesTransferred / (float)state.TotalByteCount) * 100 + "%";
        }), CancellationToken.None).ContinueWithOnMainThread(task =>
        {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                loading.text = "100%";
                buyItem.text = "Item Purchased";
                StartCoroutine(LoadImage(Convert.ToString(task.Result), type));
            }
            else
            {
                Debug.Log(task.Exception);
            }
        });
    }
}
