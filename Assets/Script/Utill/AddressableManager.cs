using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class AddressableManager : Singleton<AddressableManager>
{
	/// <summary>
	/// Get Resource But only Resource have address 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="name"></param>
	/// <returns></returns>
	public T GetResource<T>(string name)
	{
		var handle = Addressables.LoadAssetAsync<T>(name);

		handle.WaitForCompletion();

		return handle.Result;
	}
}