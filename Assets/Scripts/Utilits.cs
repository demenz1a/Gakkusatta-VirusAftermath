using UnityEngine;

namespace GAKKUSATTA.Utilits{

    public static class Utilits
    {
        public static Vector3 GetRandomDir()
        {

            return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }

        public static void DeleteObject()
        {
            //Destroy(gameObject);
        }


    }

}