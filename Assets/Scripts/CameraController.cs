// Below are the namespaces contains interfaces and classes that define generic collections
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  MonoBehaviour is a base class and CameraController component extends from this class
public class CameraController : MonoBehaviour
{
    // SerializeField attribute added to  serialize Transform variable and this variable declared as private
    [SerializeField] private Transform player;


    // Update() is a predefined menthod of MonoBehaviour objects and allows user to change positiom
    private void Update()
    {
        // transform.position stores the position of a game object relative to the world coordinate system
         transform.position = new Vector3(player.position.x, player.position.y+(0.2f), transform.position.z);
    }
}