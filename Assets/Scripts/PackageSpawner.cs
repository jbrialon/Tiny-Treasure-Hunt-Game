using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PackageSpawner : MonoBehaviour
{
    public DrivingSurfaceManager DrivingSurfaceManager;
    public GameObject PackagePrefab;
    public GameObject EnnemyPrefab;

    private PackageBehaviour Package;

    private bool isEnnemySpawned = false;
    private float enemySpawnChance = 0.1f;

    public static Vector3 RandomInTriangle(Vector3 v1, Vector3 v2)
    {
        float u = Random.Range(0.0f, 1.0f);
        float v = Random.Range(0.0f, 1.0f);
        if (v + u > 1)
        {
            v = 1 - v;
            u = 1 - u;
        }

        return (v1 * u) + (v2 * v);
    }

    public static Vector3 FindRandomLocation(ARPlane plane)
    {
        // Select random triangle in Mesh
        var mesh = plane.GetComponent<ARPlaneMeshVisualizer>().mesh;
        var triangles = mesh.triangles;
        var vertices = mesh.vertices;

        if (triangles.Length < 3 || vertices.Length < 3)
        {
            // Handle the case where there are not enough triangles or vertices
            // to avoid the IndexOutOfRangeException.
            return Vector3.zero;
        }

        // Generate a random triangle index within the valid range.
        int triangleIndex = (int)Random.Range(0, (triangles.Length / 3)) * 3;

        // Ensure the triangle index is within bounds.
        if (triangleIndex >= triangles.Length)
        {
            triangleIndex = triangles.Length - 3; // Adjust to the last valid triangle.
        }

        var randomInTriangle = RandomInTriangle(vertices[triangles[triangleIndex]], vertices[triangles[triangleIndex + 1]]);
        var randomPoint = plane.transform.TransformPoint(randomInTriangle);

        return randomPoint;
    }

    public void SpawnPackage(ARPlane plane)
    {
        // find Random Location
        var randomLocation = FindRandomLocation(plane);

        // instantiate the Package, put it in random location and rotate it so it always face the camera
        var packageClone = GameObject.Instantiate(PackagePrefab);

        packageClone.transform.position = randomLocation;

        Package = packageClone.GetComponent<PackageBehaviour>();

        // Rotate the Package so it's always facing the camera
        Vector3 position = Package.gameObject.transform.position;
        position.y = 0f;
        Vector3 cameraPosition = Camera.main.transform.position;
        cameraPosition.y = 0f;
        Vector3 direction = cameraPosition - position;
        Vector3 targetRotationEuler = Quaternion.LookRotation(forward: direction).eulerAngles;

        Vector3 scaledEuler = Vector3.Scale(a: targetRotationEuler, b: Package.gameObject.transform.up.normalized ); // (0, 1, 0)

        Quaternion targetRotation = Quaternion.Euler(euler: scaledEuler);
        Package.gameObject.transform.rotation = Package.gameObject.transform.rotation * targetRotation;

        // Every package Spawn there is a 20% chance to spawn Kraken Tentacles around, 
        // happen once per game Session put the Kraken Tentacles around the Package
        if (Random.value <= enemySpawnChance && isEnnemySpawned == false)
        {   
            Debug.Log("Release the Kraken!");
            isEnnemySpawned = true;
            var enemyClone = GameObject.Instantiate(EnnemyPrefab);
            enemyClone.transform.position = randomLocation;
        } else {
            enemySpawnChance += 0.1f; // Increase the chance to spawn an enemy by 10%
        }
    }

    private void Update()
    {
        var lockedPlane = DrivingSurfaceManager.LockedPlane;
        if (lockedPlane != null)
        {
            if (Package == null)
            {
                SpawnPackage(lockedPlane);
            }
        }
    }
}
