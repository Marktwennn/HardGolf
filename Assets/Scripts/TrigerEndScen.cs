using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrigerEndScen : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("��� ��� ������!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
